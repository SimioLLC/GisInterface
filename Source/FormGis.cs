using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SimioAPI.Extensions;

namespace GisAddIn
{
    public partial class FormGis : Form
    {
        public IDesignContext DesignContext { get; set; }

        /// <summary>
        /// A single map route
        /// </summary>
        public SimioMapRoute MapRoute { get; set; }

        public SimioMapRoutes MapRoutes { get; set; }

        /// <summary>
        /// info on how to transform map data to Simio facility
        /// </summary>
        public SimioMapTransform MapTransform { get; set; }

        public BingMapHelpers BingMapHelper { get; set; } = new BingMapHelpers();

        private GoogleMapHelpers GoogleMapHelper { get; set; } = new GoogleMapHelpers();

        /// <summary>
        /// The addresses from json file.
        /// </summary>
        public List<GisLocation> GisLocationList { get; set; }

        /// <summary>
        /// The randomly paired addresses
        /// </summary>
        public List<GisFromToPair> GisPairList { get; set; }

        public string ApplicationName { get; set; }

        public string BingMapKey { get; set; }
        public string GoogleMapKey { get; set; }
        public string ArcGISKey { get; set; }

        public FormGis()
        {
            InitializeComponent();
        }


        private void closeToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDisplayMapData_Click(object sender, EventArgs e)
        {
            if (MapRoute == null)
                return;

            if (!MapRoute.SegmentList.Any())
            {
                textMapDataSegments.Text = "(No Segments to Display)";
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (MapSegment segment in MapRoute.SegmentList)
            {
                sb.AppendLine(segment.ToString());
            }

            textMapDataSegments.Text = sb.ToString();
        }

        private void FormGis_Load(object sender, EventArgs e)
        {
            try
            {
                textBingMapsKey.Text = BingMapHelper.GetKeyString();
                textGoogleMapsKey.Text = GoogleMapHelper.GetKeyString(out string explanation);

                comboGisSource.Items.Clear();
                comboGisSource.Items.Add("Bing");
                comboGisSource.Items.Add("Google");

                BuildMapTransform();

            }
            catch (Exception ex)
            {
                alert($"Err={ex}");
            }

        }


        private void buttonQueryBingMaps_Click(object sender, EventArgs e)
        {
            try
            {
                textBingMapsResponse.Text = "";

                if (!BingMapHelper.GetRoute(textBingMapsKey.Text, textBingMapsFrom.Text, textBingMapsTo.Text,
                    out SimioMapRoute mapRoute,
                    out string requestUrl, out string explanation))
                {
                    alert($"Cannot Get Route. Err={explanation}");
                    return;
                }

                MapRoute = mapRoute;
                textBingMapsRequestUrl.Text = requestUrl;
                textBingMapsResponse.Text = explanation;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"QueryBingMaps Error={ex}");
            }
        }

        public void alert(string message)
        {
            MessageBox.Show(message);
        }



        private void buttonSaveBingKey_Click(object sender, EventArgs e)
        {
            try
            {
                BingMapHelper.PutKeyString(textBingMapsKey.Text.Trim());
            }
            catch (Exception ex)
            {
                alert($"Cannot Save Key. Err={ex}");
            }
        }

        private void buttonBingApplyRoute_Click(object sender, EventArgs e)
        {
            if (!SimioObjectHelpers.BuildSimioFacilityObjectsFromMapRoute(DesignContext, MapRoute, MapTransform, out string explanation))
            {
                alert($"Cannot Build Simio Objects. Err={explanation}");
            }

        }

        private void dgvAddressFileContents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonGetJsonFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "json Files (*.json)|*.json";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.CheckFileExists = true;

                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                string path = dialog.FileName;

                if (!SampleHelper.GetLocationData(path, out List<GisLocation> locationList, out string explanation))
                {
                    alert($"Cannot get LocationData. Err={explanation}");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                GisLocationList = new List<GisLocation>();
                foreach (GisLocation loc in locationList)
                {
                    GisLocationList.Add(loc);
                    sb.AppendLine(loc.ToString());
                }

                textJsonContents.Text = sb.ToString();


            }
            catch (Exception ex)
            {
                alert($"Cannot get JSON file. Err={ex.Message}");
            }
        }

        private void buttonCreatePairFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (GisLocationList == null || !GisLocationList.Any())
                {
                    alert($"A list of locations (addresses) is needed.");
                    return;
                }

                int pairCount = 100;
                if (!SampleHelper.CreatePairedData(GisLocationList, pairCount, out List<GisFromToPair> pairList, out string explanation))
                {
                    alert($"Cannot create {pairCount} pairs. Err={explanation}");
                    return;
                }

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = folder;
                dialog.OverwritePrompt = true;
                dialog.DefaultExt = "csv";
                dialog.FileName = "SamplePairData.csv";
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                string path = dialog.FileName;
                if (!SampleHelper.PutToTabDelimitedPairFile(path, pairList, out explanation))
                {
                    alert($"Could not write pairs to Path={path}");
                }

                alert($"Info: Wrote {pairList.Count} pairs to file={path}");

            }
            catch (Exception ex)
            {
                alert($"Cannot write GIS From-To Pairs. Err={ex.Message}");
            }
        }

        /// <summary>
        /// An address pairs file is simply a tab-delimited list of from/to addresses.
        /// See the tools tab, where this file can be created by random pairing of a sample list of addresses.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pairList"></param>
        /// <param name="explanation"></param>
        /// <returns></returns>
        private bool LoadAddressPairsFile(string path, out List<GisFromToPair> pairList, out string explanation)
        {
            pairList = new List<GisFromToPair>();
            explanation = "";

            try
            {
                textPairsFileContents.Text = path;

                if (!File.Exists(path))
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    dialog.InitialDirectory = folder;
                    dialog.Filter = "CSV Files (*.csv)|*.csv";
                    dialog.DefaultExt = "csv";
                    dialog.FileName = "SamplePairData.csv";
                    dialog.CheckFileExists = true;

                    DialogResult result = dialog.ShowDialog();
                    if (result != DialogResult.OK)
                        return false;

                    path = dialog.FileName;
                    textAddressPairsFilepath.Text = path;
                }

                if (!SampleHelper.GetFromTabDelimitedPairFile(path, out pairList, out explanation))
                {
                    explanation = $"Cannot get Pairs. Err={explanation}";
                    return false;
                }

                StringBuilder sb = new StringBuilder();
                foreach (GisFromToPair pair in pairList)
                {
                    sb.AppendLine(pair.ToString());
                }

                textPairsFileContents.Text = sb.ToString();

                return true;
            }
            catch (Exception ex)
            {
                explanation = $"File={path} Err={ex}";
                return false;
            }
        }

        private void buttonGetFile_Click(object sender, EventArgs e)
        {
            if (!LoadAddressPairsFile(textAddressPairsFilepath.Text, out List<GisFromToPair> pairList, out string explanation))
                alert(explanation);
        }


        /// <summary>
        /// Get the coordinate file using a map provider and save the results to a json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProcessCoordinateFile_Click(object sender, EventArgs e)
        {
            try
            {
                //Todo: Add processing
                if (string.IsNullOrEmpty(comboGisSource.Text))
                {
                    alert($"Please select a GIS Source");
                    return;
                }

                string path = textAddressPairsFilepath.Text;
                if (string.IsNullOrEmpty(path))
                {
                    alert($"Please select a Address Pairs file. See Tools tab to create one.");
                    return;
                }

                if (!LoadAddressPairsFile(path, out List<GisFromToPair> pairList, out string explanation))
                {
                    alert(explanation);
                    return;
                }

                SimioMapRoutes mapRoutes = null;

                switch (comboGisSource.Text)
                {
                    case "Bing":
                        BingMapKey = textBingMapsKey.Text;
                        if (!ProcessPairDataFile(path, BingMapHelper, BingMapKey, out mapRoutes, out explanation))
                        {
                            alert($"Cannot process Address file with Bing Maps. Err={explanation}");
                        }
                        break;

                    case "Google":
                        GoogleMapKey = textGoogleMapsKey.Text;
                        if (!ProcessPairDataFile(path, GoogleMapHelper, GoogleMapKey, out mapRoutes, out explanation))
                        {
                            alert($"Cannot process Address file with Google Maps. Err={explanation}");
                        }
                        break;

                    default:
                        {
                            alert($"Unsupported Map Source={comboGisSource.Text}");
                            return;
                        }

                }

                if (string.IsNullOrEmpty(explanation))
                {
                    alert($"Routes were processed with some Errors: {explanation}");
                }

                if (mapRoutes != null && mapRoutes.RouteList.Any())
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    dialog.OverwritePrompt = true;
                    dialog.FileName = "";
                    dialog.Filter = "json Files (*.json)|*.json";

                    DialogResult result = dialog.ShowDialog();

                    if (result != DialogResult.OK)
                        return;

                    path = dialog.FileName;

                    if (!JsonHelpers.SerializeToFile<SimioMapRoutes>(path, mapRoutes, false, out explanation))
                    {
                        alert($"Cannot save {mapRoutes.RouteList.Count} to File={path}. Err={explanation}");
                    }
                }

            }
            catch (Exception ex)
            {
                alert($"Cannot Process. Err={ex.Message}");
            }



        }


        /// <summary>
        /// Get the coordinate file and process the data to build Simio map route objects.
        /// If any are found, return is true, but unfound addresses are reported in explanation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool ProcessPairDataFile(string path, IMapHelper mapHelper, string mapKey,
            out SimioMapRoutes mapRoutes,
            out string explanation)
        {
            explanation = "";
            string marker = "Begin";
            int lineNbr = 0;
            mapRoutes = null;

            StringBuilder sbErrors = new StringBuilder();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string name = Path.GetFileNameWithoutExtension(path);
                mapRoutes = new SimioMapRoutes(name);

                mapRoutes.ProviderInformation = mapHelper.GetProviderInformation();

                //Todo: Add processing
                if (string.IsNullOrEmpty(comboGisSource.Text))
                {
                    alert($"Please select a GIS Source");
                    return false;
                }

                if (!LoadAddressPairsFile(textAddressPairsFilepath.Text, out List<GisFromToPair> pairList, out explanation))
                {
                    explanation = $"Cannot Load Pairs File={explanation}";
                    return false;
                }

                foreach (GisFromToPair pair in pairList)
                {
                    lineNbr++;
                    marker = $"Line={lineNbr} Loading Pair={pair}";
                    labelStatus.Text = marker;

                    if (!mapHelper.GetRoute(mapKey, pair.FromLocation.ToString(), pair.ToLocation.ToString(),
                        out SimioMapRoute mapRoute,
                        out string requestUrl, out explanation))
                    {
                        sbErrors.AppendLine($"Cannot Get Route. Err={explanation}");
                    }

                    mapRoutes.RouteList.Add(mapRoute);
                }

                explanation = sbErrors.ToString();
                return true;

            }
            catch (Exception ex)
            {
                explanation = $"Marker={marker} Err={ex.Message}";
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


        }



        private void buttonSaveGoogleKey_Click(object sender, EventArgs e)
        {
            try
            {
                GoogleMapHelper.PutKeyString(textGoogleMapsKey.Text.Trim());
            }
            catch (Exception ex)
            {
                alert($"Cannot Save Key. Err={ex}");
            }

        }

        private void buttonGoogleQueryRoute_Click(object sender, EventArgs e)
        {
            try
            {

                MapRoute.StartName = textGoogleMapsFrom.Text;
                MapRoute.EndName = textGoogleMapsTo.Text;

                if (!GoogleMapHelper.GetRoute(textGoogleMapsKey.Text, textGoogleMapsFrom.Text, textGoogleMapsTo.Text,
                    out SimioMapRoute mapRoute,
                    out string requestUrl, out string explanation))
                {
                    alert($"Cannot get Route. Err={explanation}");
                    return;
                }

                MapRoute = mapRoute;

                textGoogleMapsRequestUrl.Text = requestUrl;
                textGoogleMapsResponse.Text = explanation;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"QueryBingMaps Error={ex}");
            }

        }

        private void buttonGoogleApplyRoute_Click(object sender, EventArgs e)
        {
            if (!SimioObjectHelpers.BuildSimioFacilityObjectsFromMapRoute(DesignContext, MapRoute, MapTransform, out string explanation))
            {
                alert($"Cannot Build Simio Objects. Err={explanation}");
            }

        }


        private void buttonReadRoutesFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.CheckFileExists = true;
                dialog.Filter = "json Files (*.json)|*.json";

                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                    return;

                string path = dialog.FileName;

                if (!JsonHelpers.DeserializeFromFile(path, out SimioMapRoutes mapRoutes, out string explanation))
                {
                    alert($"Cannot get routes from file={path}");
                }

                MapRoutes = mapRoutes;

                textRoutesFileContents.Text = MapRoutes.BuildDisplayString();
            }
            catch (Exception ex)
            {
                alert($"Cannot read Routes from file. Err={ex.Message}");
            }
        }

        private void buttonComputeTransform_Click(object sender, EventArgs e)
        {
            BuildMapTransform();
        }

        private void BuildMapTransform()
        {
            try
            {
                MapTransform = new SimioMapTransform();

                MapTransform.BuildBoundingBox(textSimioBoxX.Text, textSimioBoxY.Text, textSimioBoxWidth.Text, textSimioBoxHeight.Text);

                MapTransform.BuildSimioScaling(textSimioMetersPerLon.Text, textSimioMetersPerLat.Text);
                MapTransform.BuildSimioOrigin(textFacilityX.Text, textFacilityY.Text, cbPutInBoxCenter.Checked);

                PointF center = new PointF(MapTransform.BoxCenter.X * MapTransform.SimioScaling.X,
                    MapTransform.BoxCenter.Y * MapTransform.SimioScaling.Y);
                labelBoundingBoxCenter.Text = $"BoundingBox Lon/Lat center is {MapTransform.BoxCenter.X:F3}, {MapTransform.BoxCenter.Y:F3}";
                textFacilityX.Text = $"{MapTransform.Origin.X:0.000}";
                textFacilityY.Text = $"{MapTransform.Origin.Y:0.000}";
            }
            catch (Exception ex)
            {

                alert($"Cannot build Lon,Lat to Simio transform. Err={ex.Message}");
            }

        }

        private void buttonApplyRouteFile_Click(object sender, EventArgs e)
        {
            if (MapRoutes == null)
            {
                alert($"No MapRoutes. Either load from file or get from Map Provider.");
                return;
            }

            if (MapTransform == null)
            {
                alert($"No MapTransform is loaded.");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!SimioObjectHelpers.BuildSimioFacilityObjectsFromMapRoutes(DesignContext, MapRoutes, MapTransform, out string explanation))
                {
                    alert($"Cannot Build Facility Objects from MapRoutes={MapRoutes}. Err={explanation}");
                    return;
                }
            }
            finally
            {
                Cursor.Current = Cursors.WaitCursor;
            }



        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogAbout dialog = new dialogAbout();
            DialogResult result = dialog.ShowDialog();
        }

    }
}
