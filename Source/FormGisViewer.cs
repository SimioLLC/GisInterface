using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GisAddIn
{
    public partial class FormGisViewer : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private StatusStrip statusStrip1;
        private TabControl tabControl1;
        private TabPage tabBingMaps;
        private ToolStripStatusLabel labelStatus;
        private Label labelResponse;
        private TextBox textBingMapsResponse;
        private TextBox textBingMapsTo;
        private Label label1;
        private Button buttonQueryBingMaps;
        private TextBox textBingMapsFrom;
        private Label labelFrom;
        private Label label2;
        private Label label3;
        private TextBox textBingMapsKey;
        private Label labelRequestUrl;
        private TextBox textBingMapsRequestUrl;
        private Label label4;
        private TextBox textMapDataSegments;
        private Button buttonDisplayMapData;
        private TabPage tabGoogleMaps;
        private Label label5;
        private TabPage tabArcGIS;
        private Label label6;
        private Panel panel1;
        private Panel panelTop;
        private Label labelInstructions;
        private Panel panelContent;
        private Label labelBingNote;
        private Button buttonSaveBingKey;
        private ToolTip toolTip1;
        private IContainer components;
        private TabPage tabGisResults;

        /// <summary>
        /// Set by caller
        /// </summary>
        public SimioMapData MapData { get; set; }

        /// <summary>
        /// Set by caller
        /// </summary>
        public SimioLocationData LocationData { get; set; }

        public string ApplicationName { get; set; }

        public string BingMapKey { get; set; }
        public string GoogleMapKey { get; set; }
        public string ArcGISKey { get; set; }


        public FormGisViewer()
        {

            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGisViewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBingMaps = new System.Windows.Forms.TabPage();
            this.labelRequestUrl = new System.Windows.Forms.Label();
            this.textBingMapsRequestUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBingMapsKey = new System.Windows.Forms.TextBox();
            this.labelResponse = new System.Windows.Forms.Label();
            this.textBingMapsResponse = new System.Windows.Forms.TextBox();
            this.textBingMapsTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonQueryBingMaps = new System.Windows.Forms.Button();
            this.textBingMapsFrom = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.tabGoogleMaps = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tabArcGIS = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tabGisResults = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textMapDataSegments = new System.Windows.Forms.TextBox();
            this.buttonDisplayMapData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelBingNote = new System.Windows.Forms.Label();
            this.buttonSaveBingKey = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBingMaps.SuspendLayout();
            this.tabGoogleMaps.SuspendLayout();
            this.tabArcGIS.SuspendLayout();
            this.tabGisResults.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(71, 32);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_2);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(990, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(18, 20);
            this.labelStatus.Text = "...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBingMaps);
            this.tabControl1.Controls.Add(this.tabGoogleMaps);
            this.tabControl1.Controls.Add(this.tabArcGIS);
            this.tabControl1.Controls.Add(this.tabGisResults);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(990, 512);
            this.tabControl1.TabIndex = 2;
            // 
            // tabBingMaps
            // 
            this.tabBingMaps.Controls.Add(this.panel1);
            this.tabBingMaps.Location = new System.Drawing.Point(4, 29);
            this.tabBingMaps.Name = "tabBingMaps";
            this.tabBingMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabBingMaps.Size = new System.Drawing.Size(982, 479);
            this.tabBingMaps.TabIndex = 0;
            this.tabBingMaps.Text = "Bing Maps";
            this.tabBingMaps.UseVisualStyleBackColor = true;
            // 
            // labelRequestUrl
            // 
            this.labelRequestUrl.AutoSize = true;
            this.labelRequestUrl.Location = new System.Drawing.Point(19, 185);
            this.labelRequestUrl.Name = "labelRequestUrl";
            this.labelRequestUrl.Size = new System.Drawing.Size(110, 20);
            this.labelRequestUrl.TabIndex = 10;
            this.labelRequestUrl.Text = "Request URL";
            // 
            // textBingMapsRequestUrl
            // 
            this.textBingMapsRequestUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBingMapsRequestUrl.Location = new System.Drawing.Point(20, 219);
            this.textBingMapsRequestUrl.Multiline = true;
            this.textBingMapsRequestUrl.Name = "textBingMapsRequestUrl";
            this.textBingMapsRequestUrl.ReadOnly = true;
            this.textBingMapsRequestUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBingMapsRequestUrl.Size = new System.Drawing.Size(939, 87);
            this.textBingMapsRequestUrl.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "BingMapsKey";
            // 
            // textBingMapsKey
            // 
            this.textBingMapsKey.Location = new System.Drawing.Point(143, 12);
            this.textBingMapsKey.Name = "textBingMapsKey";
            this.textBingMapsKey.Size = new System.Drawing.Size(640, 27);
            this.textBingMapsKey.TabIndex = 7;
            // 
            // labelResponse
            // 
            this.labelResponse.AutoSize = true;
            this.labelResponse.Location = new System.Drawing.Point(19, 322);
            this.labelResponse.Name = "labelResponse";
            this.labelResponse.Size = new System.Drawing.Size(84, 20);
            this.labelResponse.TabIndex = 6;
            this.labelResponse.Text = "Response";
            // 
            // textBingMapsResponse
            // 
            this.textBingMapsResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBingMapsResponse.Location = new System.Drawing.Point(19, 357);
            this.textBingMapsResponse.Multiline = true;
            this.textBingMapsResponse.Name = "textBingMapsResponse";
            this.textBingMapsResponse.ReadOnly = true;
            this.textBingMapsResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBingMapsResponse.Size = new System.Drawing.Size(940, 98);
            this.textBingMapsResponse.TabIndex = 5;
            // 
            // textBingMapsTo
            // 
            this.textBingMapsTo.Location = new System.Drawing.Point(143, 138);
            this.textBingMapsTo.Name = "textBingMapsTo";
            this.textBingMapsTo.Size = new System.Drawing.Size(201, 27);
            this.textBingMapsTo.TabIndex = 2;
            this.textBingMapsTo.Text = "Key West, FL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "To";
            // 
            // buttonQueryBingMaps
            // 
            this.buttonQueryBingMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQueryBingMaps.Location = new System.Drawing.Point(808, 101);
            this.buttonQueryBingMaps.Name = "buttonQueryBingMaps";
            this.buttonQueryBingMaps.Size = new System.Drawing.Size(151, 42);
            this.buttonQueryBingMaps.TabIndex = 3;
            this.buttonQueryBingMaps.Text = "Query Route";
            this.buttonQueryBingMaps.UseVisualStyleBackColor = true;
            this.buttonQueryBingMaps.Click += new System.EventHandler(this.buttonQueryBingMaps_Click);
            // 
            // textBingMapsFrom
            // 
            this.textBingMapsFrom.Location = new System.Drawing.Point(143, 101);
            this.textBingMapsFrom.Name = "textBingMapsFrom";
            this.textBingMapsFrom.Size = new System.Drawing.Size(201, 27);
            this.textBingMapsFrom.TabIndex = 1;
            this.textBingMapsFrom.Text = "Seattle, WA";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(19, 101);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(48, 20);
            this.labelFrom.TabIndex = 0;
            this.labelFrom.Text = "From";
            // 
            // tabGoogleMaps
            // 
            this.tabGoogleMaps.Controls.Add(this.label5);
            this.tabGoogleMaps.Location = new System.Drawing.Point(4, 25);
            this.tabGoogleMaps.Name = "tabGoogleMaps";
            this.tabGoogleMaps.Size = new System.Drawing.Size(985, 442);
            this.tabGoogleMaps.TabIndex = 2;
            this.tabGoogleMaps.Text = "Google Maps";
            this.tabGoogleMaps.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(243, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "(Google Maps test will go here)";
            // 
            // tabArcGIS
            // 
            this.tabArcGIS.Controls.Add(this.label6);
            this.tabArcGIS.Location = new System.Drawing.Point(4, 25);
            this.tabArcGIS.Name = "tabArcGIS";
            this.tabArcGIS.Size = new System.Drawing.Size(985, 442);
            this.tabArcGIS.TabIndex = 3;
            this.tabArcGIS.Text = "ArcGIS";
            this.tabArcGIS.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(244, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "(ArcGIS Maps test will go here)";
            // 
            // tabGisResults
            // 
            this.tabGisResults.Controls.Add(this.label4);
            this.tabGisResults.Controls.Add(this.textMapDataSegments);
            this.tabGisResults.Controls.Add(this.buttonDisplayMapData);
            this.tabGisResults.Controls.Add(this.label2);
            this.tabGisResults.Location = new System.Drawing.Point(4, 25);
            this.tabGisResults.Name = "tabGisResults";
            this.tabGisResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabGisResults.Size = new System.Drawing.Size(985, 442);
            this.tabGisResults.TabIndex = 1;
            this.tabGisResults.Text = "GIS Results";
            this.tabGisResults.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "MapData List";
            // 
            // textMapDataSegments
            // 
            this.textMapDataSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textMapDataSegments.Location = new System.Drawing.Point(8, 150);
            this.textMapDataSegments.Multiline = true;
            this.textMapDataSegments.Name = "textMapDataSegments";
            this.textMapDataSegments.ReadOnly = true;
            this.textMapDataSegments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textMapDataSegments.Size = new System.Drawing.Size(968, 285);
            this.textMapDataSegments.TabIndex = 11;
            // 
            // buttonDisplayMapData
            // 
            this.buttonDisplayMapData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayMapData.Location = new System.Drawing.Point(820, 57);
            this.buttonDisplayMapData.Name = "buttonDisplayMapData";
            this.buttonDisplayMapData.Size = new System.Drawing.Size(156, 42);
            this.buttonDisplayMapData.TabIndex = 4;
            this.buttonDisplayMapData.Text = "Display MapData";
            this.buttonDisplayMapData.UseVisualStyleBackColor = true;
            this.buttonDisplayMapData.Click += new System.EventHandler(this.buttonDisplayMapData_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Results Normalized for Simio";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelInstructions);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(990, 61);
            this.panelTop.TabIndex = 3;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.tabControl1);
            this.panelContent.Controls.Add(this.panelTop);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 36);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(990, 573);
            this.panelContent.TabIndex = 4;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstructions.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelInstructions.Location = new System.Drawing.Point(11, 18);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(757, 25);
            this.labelInstructions.TabIndex = 9;
            this.labelInstructions.Text = "Query the route, and then Close the form to place the results on the Simio Facili" +
    "ty View";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSaveBingKey);
            this.panel1.Controls.Add(this.labelBingNote);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelRequestUrl);
            this.panel1.Controls.Add(this.textBingMapsRequestUrl);
            this.panel1.Controls.Add(this.labelFrom);
            this.panel1.Controls.Add(this.labelResponse);
            this.panel1.Controls.Add(this.textBingMapsFrom);
            this.panel1.Controls.Add(this.textBingMapsResponse);
            this.panel1.Controls.Add(this.buttonQueryBingMaps);
            this.panel1.Controls.Add(this.textBingMapsKey);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBingMapsTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 473);
            this.panel1.TabIndex = 11;
            // 
            // labelBingNote
            // 
            this.labelBingNote.AutoSize = true;
            this.labelBingNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBingNote.Location = new System.Drawing.Point(19, 49);
            this.labelBingNote.Name = "labelBingNote";
            this.labelBingNote.Size = new System.Drawing.Size(911, 20);
            this.labelBingNote.TabIndex = 11;
            this.labelBingNote.Text = "Note: Get your free key from Microsoft Bing Maps ( search \"Getting a Bing Maps Ke" +
    "y\")  and paste it in the textbox above.";
            // 
            // buttonSaveBingKey
            // 
            this.buttonSaveBingKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveBingKey.Location = new System.Drawing.Point(808, 12);
            this.buttonSaveBingKey.Name = "buttonSaveBingKey";
            this.buttonSaveBingKey.Size = new System.Drawing.Size(151, 34);
            this.buttonSaveBingKey.TabIndex = 12;
            this.buttonSaveBingKey.Text = "Save Bing Key";
            this.toolTip1.SetToolTip(this.buttonSaveBingKey, "Store the key under your Documents \\ SimioUserExtensions in ");
            this.buttonSaveBingKey.UseVisualStyleBackColor = true;
            this.buttonSaveBingKey.Click += new System.EventHandler(this.buttonSaveBingKey_Click);
            // 
            // FormGisViewer
            // 
            this.ClientSize = new System.Drawing.Size(990, 634);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormGisViewer";
            this.Load += new System.EventHandler(this.FormGisViewer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabBingMaps.ResumeLayout(false);
            this.tabGoogleMaps.ResumeLayout(false);
            this.tabGoogleMaps.PerformLayout();
            this.tabArcGIS.ResumeLayout(false);
            this.tabArcGIS.PerformLayout();
            this.tabGisResults.ResumeLayout(false);
            this.tabGisResults.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void closeToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQueryBingMaps_Click(object sender, EventArgs e)
        {
            try
            {

                ProcessBingMapsRouteRequest(textBingMapsKey.Text, MapData, textBingMapsFrom.Text, textBingMapsTo.Text);

                MapData.StartName = textBingMapsFrom.Text;
                MapData.EndName = textBingMapsTo.Text;

            }
            catch (Exception ex)
            {
                string xx = "";
            }
        }

        /// <summary>
        /// This method has a lot of logic that is specific to the sample. 
        /// To process a request you can easily just call the Execute method on the request.
        /// This will build much of the SimioMapData object.
        /// </summary>
        /// <param name="request"></param>
        private async void ProcessBingMapsRouteRequest( string bingKey, SimioMapData mapData, string fromName, string toName)
        {
            try
            {
                // Build a list of our two waypoints (from and to)
                List<SimpleWaypoint> wpList = new List<SimpleWaypoint>();
                wpList.Add(new SimpleWaypoint(fromName));  //e.g. "Pittsburgh, PA"));
                wpList.Add(new SimpleWaypoint(toName));    // e.g. "Sewickley, PA"));

                List<RouteAttributeType> routeAttributes = new List<RouteAttributeType>();
                routeAttributes.Add(RouteAttributeType.RoutePath);

                // Construct the request and attributes
                var request = new RouteRequest()
                {
                    BingMapsKey = bingKey,
                    Waypoints = wpList
                };

                request.RouteOptions = new RouteOptions();
                request.RouteOptions.RouteAttributes = routeAttributes;

                var start = DateTime.Now;
                // Async. Execute the request.
                var response = await request.Execute((remainingTime) =>
                {
                    if (remainingTime > -1)
                    {
                        // Here, you could handle work, such as updating a progress bar.
                    }
                });

                // At this point we have the response
                textBingMapsRequestUrl.Text = request.GetRequestUrl();

                var r2 = response;

                if (r2 != null && r2.ResourceSets != null
                    && r2.ResourceSets.Length > 0
                    && r2.ResourceSets[0].Resources != null
                    && r2.ResourceSets[0].Resources.Length > 0)
                {
                    ResourceSet rs = (ResourceSet)r2.ResourceSets[0];
                    Route route = (Route) rs.Resources[0];
                    RouteLeg[] legs = route.RouteLegs;
                    ItineraryItem[] itineraries = legs[0].ItineraryItems;
                    ItineraryItem itinItem = itineraries[2];
                    string bb = route.BoundingBox.ToString();

                    mapData.SegmentList.Clear();

                    // We could make the bounding box from the one that Bing Maps sends us, 
                    // but we're going to do our own to match the usa 'map'.
                    ////PointF ptLoc = new PointF((float)route.BoundingBox[1], (float)route.BoundingBox[0]);
                    ////float width = (float)(route.BoundingBox[3] - route.BoundingBox[1]);
                    ////float height = (float)(route.BoundingBox[2] - route.BoundingBox[0]);

                    // We're going to bound according to the contiguous USA, which is appox.
                    // lat 20 to 50, and lon -60 to -130
                    PointF ptLoc = new PointF( -130f, 20f);
                    float width = 70f;
                    float height = 30f; 

                    // Turning the thing on its side, since we want latitude to be 'Y'
                    mapData.BoundingBox = new RectangleF(ptLoc.X, ptLoc.Y, width, height);
                    mapData.Origin = new PointF(ptLoc.X + width / 2f, ptLoc.Y + height / 2f);

                    // Build something for the form's 'result textbox
                    StringBuilder sb = new StringBuilder();

                    // Create segments from the itineraries, and pick up the indices
                    // that reference the RoutePath array of lat,lon coordinates.
                    // We are assuming a single itinerary. See Bing Maps for for info.
                    for (var ii = 0; ii < itineraries.Length; ii++)
                    {
                        ItineraryItem item = itineraries[ii];

                        if ( route.RoutePath != null )
                        {
                            int idxStart = item.Details[0].StartPathIndices[0];
                            int idxEnd = item.Details[0].EndPathIndices[0];

                            double lat = route.RoutePath.Line.Coordinates[idxStart][0];
                            double lon = route.RoutePath.Line.Coordinates[idxStart][1];
                            MapCoordinate mcStart = new MapCoordinate(lat, lon);

                            lat = route.RoutePath.Line.Coordinates[idxEnd][0];
                            lon = route.RoutePath.Line.Coordinates[idxEnd][1];

                            MapCoordinate mcEnd = new MapCoordinate(lat, lon);

                            if ( ii == 0 )
                            {
                                mapData.AddFirstSegment(mcStart, mcEnd);
                            }
                            else
                            {
                                mapData.AppendSegment(mcEnd);
                            }
                        }
                        sb.AppendLine($"Compass={item.CompassDirection} Distance={item.TravelDistance} >> {item.Instruction.Text}");
                    } // for each itinerary

                    textBingMapsResponse.Text = sb.ToString();
                }
                else
                {
                    textBingMapsResponse.Text = "No results found.";
                }

            }
            catch (Exception ex)
            {
                alert(ex.Message);
            }

        }


        public void alert(string message)
        {
            MessageBox.Show(message);
        }


        /// <summary>
        /// This method has a lot of logic that is specific to the sample. 
        /// To process a request you can easily just call the Execute method on the request.
        /// </summary>
        /// <param name="request"></param>
        private async void ProcessGeoCodeRequest(string bingKey, string queryAddress, SimioLocationData locData )
        {

            try
            {
                var request = new GeocodeRequest()
                {
                    Query = queryAddress,
                    IncludeIso2 = true,
                    IncludeNeighborhood = true,
                    MaxResults = 1,
                    BingMapsKey = bingKey
                };

                var start = DateTime.Now;
                //Execute the request.

                var response = await request.Execute(); 

                var end = DateTime.Now;
                var processingTime = end - start;

                // Process the result
                var r2 = response;
                if (r2 != null && r2.ResourceSets != null
                    && r2.ResourceSets.Length > 0
                    && r2.ResourceSets[0].Resources != null
                    && r2.ResourceSets[0].Resources.Length > 0)
                {
                    ResourceSet rs = (ResourceSet)r2.ResourceSets[0];
                    var resource = (Location) rs.Resources[0];
                    var pt = (BingMapsRESTToolkit.Point) resource.Point;
                    var coordinates = pt.Coordinates;

                    MapCoordinate coord = new MapCoordinate(pt.Coordinates[0], pt.Coordinates[1]);
                    locData.CoordinateList.Add(coord);

                }

            }
            catch (Exception ex)
            {
                alert(ex.Message);
            }

        }


        private void buttonDisplayMapData_Click(object sender, EventArgs e)
        {
            if (MapData == null)
                return;

            if (!MapData.SegmentList.Any())
            {
                textMapDataSegments.Text = "(No Segments to Display)";
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach ( MapSegment segment in MapData.SegmentList )
            {
                sb.AppendLine(segment.ToString());
            }

            textMapDataSegments.Text = sb.ToString();
        }

        private void FormGisViewer_Load(object sender, EventArgs e)
        {
            try
            {
                textBingMapsKey.Text = BingMapHelpers.GetKeyString();

            }
            catch (Exception ex)
            {
                alert($"Err={ex}");
            }

        }

        private void buttonSaveBingKey_Click(object sender, EventArgs e)
        {
            try
            {
                BingMapHelpers.PutKeyString(textBingMapsKey.Text.Trim());
            }
            catch (Exception ex)
            {
                alert($"Cannot Save Key. Err={ex}");
            }
        }
    }
}
