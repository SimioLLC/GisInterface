using BingMapsRESTToolkit;
using PortalManager;
using SimioAPI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private Button buttonBingQueryRoute;
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
        private TabPage tabSimioDisplayInfo;
        private Panel panelDisplayBoundingBox;
        private Label label7;
        private Label label9;
        private Label labelSimioWidth;
        private TextBox textSimioBoxHeight;
        private TextBox textSimioBoxWidth;
        private TextBox textSimioBoxY;
        private Label label8;
        private Label labelBoxX;
        private TextBox textSimioBoxX;
        private Panel panelSimioScaling;
        private Label label12;
        private Label label13;
        private Label label14;
        private TextBox textSimioMetersPerLat;
        private TextBox textSimioMetersPerLon;
        private Button buttonBingApplyRoute;
        private TabPage tabUseAddressFile;
        private Button buttonQueryProviderAndStoreRouteFile;
        private Label label11;
        private TextBox textAddressPairsFilepath;
        private Panel panel2;
        private Button buttonGetFile;
        private Label labelFileContents;
        private ComboBox comboGisSource;
        private Label labelGisSource;
        private TabPage tabTools;
        private GroupBox groupBox1;
        private TextBox textJsonContents;
        private Label label18;
        private Label label16;
        private Button buttonGetJsonFile;
        private TextBox textBox1;
        private Button buttonCreatePairFile;
        private Label label17;
        private TextBox textPairsFileContents;
        private Button buttonGoogleApplyRoute;
        private Button buttonSaveGoogleKey;
        private Label label5;
        private Label label15;
        private Label label19;
        private TextBox textGoogleMapsRequestUrl;
        private Label label20;
        private Label label21;
        private TextBox textGoogleMapsFrom;
        private TextBox textGoogleMapsResponse;
        private Button buttonGoogleQueryRoute;
        private TextBox textGoogleMapsKey;
        private Label label22;
        private TextBox textGoogleMapsTo;
        private GroupBox groupAddressPairs;
        private GroupBox groupApplyRoutes;
        private Button buttonApplyRouteFile;
        private GroupBox groupFetchAndStoreRoutes;
        private Label labelRoutesJsonFile;
        private Label label24;
        private TextBox textRoutesFileContents;
        private Button buttonReadRoutesFromFile;
        private Panel panel4;
        private Label labelBoundingBoxCenter;
        private Button buttonComputeTransform;
        private Panel panel3;
        private Label label23;
        private Label label25;
        private Label label26;
        private TextBox textFacilityY;
        private TextBox textFacilityX;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TabPage tabGisResults;


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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBingApplyRoute = new System.Windows.Forms.Button();
            this.buttonSaveBingKey = new System.Windows.Forms.Button();
            this.labelBingNote = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelRequestUrl = new System.Windows.Forms.Label();
            this.textBingMapsRequestUrl = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelResponse = new System.Windows.Forms.Label();
            this.textBingMapsFrom = new System.Windows.Forms.TextBox();
            this.textBingMapsResponse = new System.Windows.Forms.TextBox();
            this.buttonBingQueryRoute = new System.Windows.Forms.Button();
            this.textBingMapsKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBingMapsTo = new System.Windows.Forms.TextBox();
            this.tabGoogleMaps = new System.Windows.Forms.TabPage();
            this.buttonGoogleApplyRoute = new System.Windows.Forms.Button();
            this.buttonSaveGoogleKey = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textGoogleMapsRequestUrl = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textGoogleMapsFrom = new System.Windows.Forms.TextBox();
            this.textGoogleMapsResponse = new System.Windows.Forms.TextBox();
            this.buttonGoogleQueryRoute = new System.Windows.Forms.Button();
            this.textGoogleMapsKey = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textGoogleMapsTo = new System.Windows.Forms.TextBox();
            this.tabArcGIS = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tabUseAddressFile = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupAddressPairs = new System.Windows.Forms.GroupBox();
            this.textAddressPairsFilepath = new System.Windows.Forms.TextBox();
            this.buttonGetFile = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.labelFileContents = new System.Windows.Forms.Label();
            this.textPairsFileContents = new System.Windows.Forms.TextBox();
            this.groupApplyRoutes = new System.Windows.Forms.GroupBox();
            this.buttonApplyRouteFile = new System.Windows.Forms.Button();
            this.groupFetchAndStoreRoutes = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textRoutesFileContents = new System.Windows.Forms.TextBox();
            this.buttonReadRoutesFromFile = new System.Windows.Forms.Button();
            this.labelRoutesJsonFile = new System.Windows.Forms.Label();
            this.comboGisSource = new System.Windows.Forms.ComboBox();
            this.labelGisSource = new System.Windows.Forms.Label();
            this.buttonQueryProviderAndStoreRouteFile = new System.Windows.Forms.Button();
            this.tabGisResults = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textMapDataSegments = new System.Windows.Forms.TextBox();
            this.buttonDisplayMapData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabSimioDisplayInfo = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelDisplayBoundingBox = new System.Windows.Forms.Panel();
            this.labelBoundingBoxCenter = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelSimioWidth = new System.Windows.Forms.Label();
            this.textSimioBoxHeight = new System.Windows.Forms.TextBox();
            this.textSimioBoxWidth = new System.Windows.Forms.TextBox();
            this.textSimioBoxY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelBoxX = new System.Windows.Forms.Label();
            this.textSimioBoxX = new System.Windows.Forms.TextBox();
            this.buttonComputeTransform = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textFacilityY = new System.Windows.Forms.TextBox();
            this.textFacilityX = new System.Windows.Forms.TextBox();
            this.panelSimioScaling = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textSimioMetersPerLat = new System.Windows.Forms.TextBox();
            this.textSimioMetersPerLon = new System.Windows.Forms.TextBox();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textJsonContents = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonGetJsonFile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonCreatePairFile = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBingMaps.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabGoogleMaps.SuspendLayout();
            this.tabArcGIS.SuspendLayout();
            this.tabUseAddressFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupAddressPairs.SuspendLayout();
            this.groupApplyRoutes.SuspendLayout();
            this.groupFetchAndStoreRoutes.SuspendLayout();
            this.tabGisResults.SuspendLayout();
            this.tabSimioDisplayInfo.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelDisplayBoundingBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelSimioScaling.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1131, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(73, 32);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click_2);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 683);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1131, 26);
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
            this.tabControl1.Controls.Add(this.tabUseAddressFile);
            this.tabControl1.Controls.Add(this.tabGisResults);
            this.tabControl1.Controls.Add(this.tabSimioDisplayInfo);
            this.tabControl1.Controls.Add(this.tabTools);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1131, 586);
            this.tabControl1.TabIndex = 2;
            // 
            // tabBingMaps
            // 
            this.tabBingMaps.Controls.Add(this.panel1);
            this.tabBingMaps.Location = new System.Drawing.Point(4, 29);
            this.tabBingMaps.Name = "tabBingMaps";
            this.tabBingMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabBingMaps.Size = new System.Drawing.Size(1123, 553);
            this.tabBingMaps.TabIndex = 0;
            this.tabBingMaps.Text = "Bing Maps";
            this.tabBingMaps.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBingApplyRoute);
            this.panel1.Controls.Add(this.buttonSaveBingKey);
            this.panel1.Controls.Add(this.labelBingNote);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelRequestUrl);
            this.panel1.Controls.Add(this.textBingMapsRequestUrl);
            this.panel1.Controls.Add(this.labelFrom);
            this.panel1.Controls.Add(this.labelResponse);
            this.panel1.Controls.Add(this.textBingMapsFrom);
            this.panel1.Controls.Add(this.textBingMapsResponse);
            this.panel1.Controls.Add(this.buttonBingQueryRoute);
            this.panel1.Controls.Add(this.textBingMapsKey);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBingMapsTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 547);
            this.panel1.TabIndex = 11;
            // 
            // buttonBingApplyRoute
            // 
            this.buttonBingApplyRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBingApplyRoute.Location = new System.Drawing.Point(949, 149);
            this.buttonBingApplyRoute.Name = "buttonBingApplyRoute";
            this.buttonBingApplyRoute.Size = new System.Drawing.Size(151, 42);
            this.buttonBingApplyRoute.TabIndex = 13;
            this.buttonBingApplyRoute.Text = "Apply Route";
            this.toolTip1.SetToolTip(this.buttonBingApplyRoute, "Apply Route to Simio Facility View");
            this.buttonBingApplyRoute.UseVisualStyleBackColor = true;
            this.buttonBingApplyRoute.Click += new System.EventHandler(this.buttonBingApplyRoute_Click);
            // 
            // buttonSaveBingKey
            // 
            this.buttonSaveBingKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveBingKey.Location = new System.Drawing.Point(949, 12);
            this.buttonSaveBingKey.Name = "buttonSaveBingKey";
            this.buttonSaveBingKey.Size = new System.Drawing.Size(151, 34);
            this.buttonSaveBingKey.TabIndex = 12;
            this.buttonSaveBingKey.Text = "Save Bing Key";
            this.toolTip1.SetToolTip(this.buttonSaveBingKey, "Store the key under your Documents > SimioUserExtensions Folder");
            this.buttonSaveBingKey.UseVisualStyleBackColor = true;
            this.buttonSaveBingKey.Click += new System.EventHandler(this.buttonSaveBingKey_Click);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "BingMapsKey";
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
            this.textBingMapsRequestUrl.Size = new System.Drawing.Size(1080, 87);
            this.textBingMapsRequestUrl.TabIndex = 9;
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
            // labelResponse
            // 
            this.labelResponse.AutoSize = true;
            this.labelResponse.Location = new System.Drawing.Point(19, 322);
            this.labelResponse.Name = "labelResponse";
            this.labelResponse.Size = new System.Drawing.Size(84, 20);
            this.labelResponse.TabIndex = 6;
            this.labelResponse.Text = "Response";
            // 
            // textBingMapsFrom
            // 
            this.textBingMapsFrom.Location = new System.Drawing.Point(143, 101);
            this.textBingMapsFrom.Name = "textBingMapsFrom";
            this.textBingMapsFrom.Size = new System.Drawing.Size(201, 27);
            this.textBingMapsFrom.TabIndex = 1;
            this.textBingMapsFrom.Text = "Seattle, WA";
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
            this.textBingMapsResponse.Size = new System.Drawing.Size(1081, 98);
            this.textBingMapsResponse.TabIndex = 5;
            // 
            // buttonBingQueryRoute
            // 
            this.buttonBingQueryRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBingQueryRoute.Location = new System.Drawing.Point(949, 101);
            this.buttonBingQueryRoute.Name = "buttonBingQueryRoute";
            this.buttonBingQueryRoute.Size = new System.Drawing.Size(151, 42);
            this.buttonBingQueryRoute.TabIndex = 3;
            this.buttonBingQueryRoute.Text = "Query Route";
            this.buttonBingQueryRoute.UseVisualStyleBackColor = true;
            this.buttonBingQueryRoute.Click += new System.EventHandler(this.buttonQueryBingMaps_Click);
            // 
            // textBingMapsKey
            // 
            this.textBingMapsKey.Location = new System.Drawing.Point(143, 12);
            this.textBingMapsKey.Name = "textBingMapsKey";
            this.textBingMapsKey.Size = new System.Drawing.Size(640, 27);
            this.textBingMapsKey.TabIndex = 7;
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
            // textBingMapsTo
            // 
            this.textBingMapsTo.Location = new System.Drawing.Point(143, 138);
            this.textBingMapsTo.Name = "textBingMapsTo";
            this.textBingMapsTo.Size = new System.Drawing.Size(201, 27);
            this.textBingMapsTo.TabIndex = 2;
            this.textBingMapsTo.Text = "Key West, FL";
            // 
            // tabGoogleMaps
            // 
            this.tabGoogleMaps.Controls.Add(this.buttonGoogleApplyRoute);
            this.tabGoogleMaps.Controls.Add(this.buttonSaveGoogleKey);
            this.tabGoogleMaps.Controls.Add(this.label5);
            this.tabGoogleMaps.Controls.Add(this.label15);
            this.tabGoogleMaps.Controls.Add(this.label19);
            this.tabGoogleMaps.Controls.Add(this.textGoogleMapsRequestUrl);
            this.tabGoogleMaps.Controls.Add(this.label20);
            this.tabGoogleMaps.Controls.Add(this.label21);
            this.tabGoogleMaps.Controls.Add(this.textGoogleMapsFrom);
            this.tabGoogleMaps.Controls.Add(this.textGoogleMapsResponse);
            this.tabGoogleMaps.Controls.Add(this.buttonGoogleQueryRoute);
            this.tabGoogleMaps.Controls.Add(this.textGoogleMapsKey);
            this.tabGoogleMaps.Controls.Add(this.label22);
            this.tabGoogleMaps.Controls.Add(this.textGoogleMapsTo);
            this.tabGoogleMaps.Location = new System.Drawing.Point(4, 29);
            this.tabGoogleMaps.Name = "tabGoogleMaps";
            this.tabGoogleMaps.Size = new System.Drawing.Size(1123, 564);
            this.tabGoogleMaps.TabIndex = 2;
            this.tabGoogleMaps.Text = "Google Maps";
            this.tabGoogleMaps.UseVisualStyleBackColor = true;
            // 
            // buttonGoogleApplyRoute
            // 
            this.buttonGoogleApplyRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGoogleApplyRoute.Location = new System.Drawing.Point(951, 199);
            this.buttonGoogleApplyRoute.Name = "buttonGoogleApplyRoute";
            this.buttonGoogleApplyRoute.Size = new System.Drawing.Size(151, 42);
            this.buttonGoogleApplyRoute.TabIndex = 27;
            this.buttonGoogleApplyRoute.Text = "Apply Route";
            this.toolTip1.SetToolTip(this.buttonGoogleApplyRoute, "Apply Route to Simio Facility View");
            this.buttonGoogleApplyRoute.UseVisualStyleBackColor = true;
            this.buttonGoogleApplyRoute.Click += new System.EventHandler(this.buttonGoogleApplyRoute_Click);
            // 
            // buttonSaveGoogleKey
            // 
            this.buttonSaveGoogleKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveGoogleKey.Location = new System.Drawing.Point(951, 62);
            this.buttonSaveGoogleKey.Name = "buttonSaveGoogleKey";
            this.buttonSaveGoogleKey.Size = new System.Drawing.Size(151, 34);
            this.buttonSaveGoogleKey.TabIndex = 26;
            this.buttonSaveGoogleKey.Text = "Save Google Key";
            this.toolTip1.SetToolTip(this.buttonSaveGoogleKey, "Store the key under your Documents > SimioUserExtensions Folder");
            this.buttonSaveGoogleKey.UseVisualStyleBackColor = true;
            this.buttonSaveGoogleKey.Click += new System.EventHandler(this.buttonSaveGoogleKey_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(860, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Note: Get your free key from Google Maps ( search \"Getting Google Maps Key\")  and" +
    " paste it in the textbox above.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(21, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 20);
            this.label15.TabIndex = 22;
            this.label15.Text = "GoogleMapsKey";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 235);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 20);
            this.label19.TabIndex = 24;
            this.label19.Text = "Request URL";
            // 
            // textGoogleMapsRequestUrl
            // 
            this.textGoogleMapsRequestUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textGoogleMapsRequestUrl.Location = new System.Drawing.Point(22, 269);
            this.textGoogleMapsRequestUrl.Multiline = true;
            this.textGoogleMapsRequestUrl.Name = "textGoogleMapsRequestUrl";
            this.textGoogleMapsRequestUrl.ReadOnly = true;
            this.textGoogleMapsRequestUrl.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textGoogleMapsRequestUrl.Size = new System.Drawing.Size(1080, 87);
            this.textGoogleMapsRequestUrl.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(21, 151);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 20);
            this.label20.TabIndex = 14;
            this.label20.Text = "From";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(21, 372);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Response";
            // 
            // textGoogleMapsFrom
            // 
            this.textGoogleMapsFrom.Location = new System.Drawing.Point(145, 151);
            this.textGoogleMapsFrom.Name = "textGoogleMapsFrom";
            this.textGoogleMapsFrom.Size = new System.Drawing.Size(201, 27);
            this.textGoogleMapsFrom.TabIndex = 15;
            this.textGoogleMapsFrom.Text = "Seattle, WA";
            // 
            // textGoogleMapsResponse
            // 
            this.textGoogleMapsResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textGoogleMapsResponse.Location = new System.Drawing.Point(21, 407);
            this.textGoogleMapsResponse.Multiline = true;
            this.textGoogleMapsResponse.Name = "textGoogleMapsResponse";
            this.textGoogleMapsResponse.ReadOnly = true;
            this.textGoogleMapsResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textGoogleMapsResponse.Size = new System.Drawing.Size(1081, 98);
            this.textGoogleMapsResponse.TabIndex = 19;
            // 
            // buttonGoogleQueryRoute
            // 
            this.buttonGoogleQueryRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGoogleQueryRoute.Location = new System.Drawing.Point(951, 151);
            this.buttonGoogleQueryRoute.Name = "buttonGoogleQueryRoute";
            this.buttonGoogleQueryRoute.Size = new System.Drawing.Size(151, 42);
            this.buttonGoogleQueryRoute.TabIndex = 17;
            this.buttonGoogleQueryRoute.Text = "Query Route";
            this.buttonGoogleQueryRoute.UseVisualStyleBackColor = true;
            this.buttonGoogleQueryRoute.Click += new System.EventHandler(this.buttonGoogleQueryRoute_Click);
            // 
            // textGoogleMapsKey
            // 
            this.textGoogleMapsKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textGoogleMapsKey.Location = new System.Drawing.Point(145, 62);
            this.textGoogleMapsKey.Name = "textGoogleMapsKey";
            this.textGoogleMapsKey.Size = new System.Drawing.Size(640, 27);
            this.textGoogleMapsKey.TabIndex = 21;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(21, 191);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(28, 20);
            this.label22.TabIndex = 18;
            this.label22.Text = "To";
            // 
            // textGoogleMapsTo
            // 
            this.textGoogleMapsTo.Location = new System.Drawing.Point(145, 188);
            this.textGoogleMapsTo.Name = "textGoogleMapsTo";
            this.textGoogleMapsTo.Size = new System.Drawing.Size(201, 27);
            this.textGoogleMapsTo.TabIndex = 16;
            this.textGoogleMapsTo.Text = "Key West, FL";
            // 
            // tabArcGIS
            // 
            this.tabArcGIS.Controls.Add(this.label6);
            this.tabArcGIS.Location = new System.Drawing.Point(4, 29);
            this.tabArcGIS.Name = "tabArcGIS";
            this.tabArcGIS.Size = new System.Drawing.Size(1123, 564);
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
            // tabUseAddressFile
            // 
            this.tabUseAddressFile.Controls.Add(this.panel2);
            this.tabUseAddressFile.Location = new System.Drawing.Point(4, 29);
            this.tabUseAddressFile.Name = "tabUseAddressFile";
            this.tabUseAddressFile.Size = new System.Drawing.Size(1123, 564);
            this.tabUseAddressFile.TabIndex = 5;
            this.tabUseAddressFile.Text = "Use Address File";
            this.tabUseAddressFile.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupAddressPairs);
            this.panel2.Controls.Add(this.groupApplyRoutes);
            this.panel2.Controls.Add(this.groupFetchAndStoreRoutes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1123, 564);
            this.panel2.TabIndex = 16;
            this.toolTip1.SetToolTip(this.panel2, "Query Map Provide and Store Routine Information");
            // 
            // groupAddressPairs
            // 
            this.groupAddressPairs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAddressPairs.Controls.Add(this.textAddressPairsFilepath);
            this.groupAddressPairs.Controls.Add(this.buttonGetFile);
            this.groupAddressPairs.Controls.Add(this.label11);
            this.groupAddressPairs.Controls.Add(this.labelFileContents);
            this.groupAddressPairs.Controls.Add(this.textPairsFileContents);
            this.groupAddressPairs.Location = new System.Drawing.Point(11, 20);
            this.groupAddressPairs.Name = "groupAddressPairs";
            this.groupAddressPairs.Size = new System.Drawing.Size(1092, 165);
            this.groupAddressPairs.TabIndex = 35;
            this.groupAddressPairs.TabStop = false;
            this.groupAddressPairs.Text = "Address Pairs";
            // 
            // textAddressPairsFilepath
            // 
            this.textAddressPairsFilepath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textAddressPairsFilepath.Location = new System.Drawing.Point(9, 37);
            this.textAddressPairsFilepath.Name = "textAddressPairsFilepath";
            this.textAddressPairsFilepath.Size = new System.Drawing.Size(1030, 27);
            this.textAddressPairsFilepath.TabIndex = 13;
            this.toolTip1.SetToolTip(this.textAddressPairsFilepath, "Location of file of address pairs");
            // 
            // buttonGetFile
            // 
            this.buttonGetFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetFile.Location = new System.Drawing.Point(1045, 37);
            this.buttonGetFile.Name = "buttonGetFile";
            this.buttonGetFile.Size = new System.Drawing.Size(41, 23);
            this.buttonGetFile.TabIndex = 16;
            this.buttonGetFile.Text = "...";
            this.toolTip1.SetToolTip(this.buttonGetFile, "Locate and Read the Address File");
            this.buttonGetFile.UseVisualStyleBackColor = true;
            this.buttonGetFile.Click += new System.EventHandler(this.buttonGetFile_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 20);
            this.label11.TabIndex = 14;
            this.label11.Text = "GIS Pairs CSV File";
            // 
            // labelFileContents
            // 
            this.labelFileContents.AutoSize = true;
            this.labelFileContents.Location = new System.Drawing.Point(6, 76);
            this.labelFileContents.Name = "labelFileContents";
            this.labelFileContents.Size = new System.Drawing.Size(236, 20);
            this.labelFileContents.TabIndex = 18;
            this.labelFileContents.Text = "GIS Pairs File (CSV) Contents";
            // 
            // textPairsFileContents
            // 
            this.textPairsFileContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPairsFileContents.Location = new System.Drawing.Point(9, 96);
            this.textPairsFileContents.Multiline = true;
            this.textPairsFileContents.Name = "textPairsFileContents";
            this.textPairsFileContents.ReadOnly = true;
            this.textPairsFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textPairsFileContents.Size = new System.Drawing.Size(1077, 63);
            this.textPairsFileContents.TabIndex = 31;
            // 
            // groupApplyRoutes
            // 
            this.groupApplyRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupApplyRoutes.Controls.Add(this.buttonApplyRouteFile);
            this.groupApplyRoutes.Location = new System.Drawing.Point(12, 459);
            this.groupApplyRoutes.Name = "groupApplyRoutes";
            this.groupApplyRoutes.Size = new System.Drawing.Size(1091, 75);
            this.groupApplyRoutes.TabIndex = 34;
            this.groupApplyRoutes.TabStop = false;
            this.groupApplyRoutes.Text = "Apply Routes";
            // 
            // buttonApplyRouteFile
            // 
            this.buttonApplyRouteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyRouteFile.Location = new System.Drawing.Point(888, 27);
            this.buttonApplyRouteFile.Name = "buttonApplyRouteFile";
            this.buttonApplyRouteFile.Size = new System.Drawing.Size(197, 42);
            this.buttonApplyRouteFile.TabIndex = 32;
            this.buttonApplyRouteFile.Text = "Apply Routes";
            this.toolTip1.SetToolTip(this.buttonApplyRouteFile, "Apply Routes to Simio Facility View");
            this.buttonApplyRouteFile.UseVisualStyleBackColor = true;
            this.buttonApplyRouteFile.Click += new System.EventHandler(this.buttonApplyRouteFile_Click);
            // 
            // groupFetchAndStoreRoutes
            // 
            this.groupFetchAndStoreRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFetchAndStoreRoutes.Controls.Add(this.label24);
            this.groupFetchAndStoreRoutes.Controls.Add(this.textRoutesFileContents);
            this.groupFetchAndStoreRoutes.Controls.Add(this.buttonReadRoutesFromFile);
            this.groupFetchAndStoreRoutes.Controls.Add(this.labelRoutesJsonFile);
            this.groupFetchAndStoreRoutes.Controls.Add(this.comboGisSource);
            this.groupFetchAndStoreRoutes.Controls.Add(this.labelGisSource);
            this.groupFetchAndStoreRoutes.Controls.Add(this.buttonQueryProviderAndStoreRouteFile);
            this.groupFetchAndStoreRoutes.Location = new System.Drawing.Point(11, 191);
            this.groupFetchAndStoreRoutes.Name = "groupFetchAndStoreRoutes";
            this.groupFetchAndStoreRoutes.Size = new System.Drawing.Size(1092, 265);
            this.groupFetchAndStoreRoutes.TabIndex = 33;
            this.groupFetchAndStoreRoutes.TabStop = false;
            this.groupFetchAndStoreRoutes.Text = "Get Routes. Either Query Map Provider or Read from File";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 119);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(228, 20);
            this.label24.TabIndex = 33;
            this.label24.Text = "Routes File (JSON) Contents";
            // 
            // textRoutesFileContents
            // 
            this.textRoutesFileContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textRoutesFileContents.Location = new System.Drawing.Point(8, 139);
            this.textRoutesFileContents.Multiline = true;
            this.textRoutesFileContents.Name = "textRoutesFileContents";
            this.textRoutesFileContents.ReadOnly = true;
            this.textRoutesFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textRoutesFileContents.Size = new System.Drawing.Size(1077, 120);
            this.textRoutesFileContents.TabIndex = 32;
            // 
            // buttonReadRoutesFromFile
            // 
            this.buttonReadRoutesFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReadRoutesFromFile.Location = new System.Drawing.Point(888, 34);
            this.buttonReadRoutesFromFile.Name = "buttonReadRoutesFromFile";
            this.buttonReadRoutesFromFile.Size = new System.Drawing.Size(197, 39);
            this.buttonReadRoutesFromFile.TabIndex = 24;
            this.buttonReadRoutesFromFile.Text = "Get Routes From File...";
            this.toolTip1.SetToolTip(this.buttonReadRoutesFromFile, "Select a JSON file holding Routes information");
            this.buttonReadRoutesFromFile.UseVisualStyleBackColor = true;
            this.buttonReadRoutesFromFile.Click += new System.EventHandler(this.buttonReadRoutesFromFile_Click);
            // 
            // labelRoutesJsonFile
            // 
            this.labelRoutesJsonFile.AutoSize = true;
            this.labelRoutesJsonFile.Location = new System.Drawing.Point(7, 86);
            this.labelRoutesJsonFile.Name = "labelRoutesJsonFile";
            this.labelRoutesJsonFile.Size = new System.Drawing.Size(166, 20);
            this.labelRoutesJsonFile.TabIndex = 23;
            this.labelRoutesJsonFile.Text = "Routes JSON File: ...";
            // 
            // comboGisSource
            // 
            this.comboGisSource.FormattingEnabled = true;
            this.comboGisSource.Location = new System.Drawing.Point(124, 34);
            this.comboGisSource.Name = "comboGisSource";
            this.comboGisSource.Size = new System.Drawing.Size(279, 28);
            this.comboGisSource.TabIndex = 20;
            this.toolTip1.SetToolTip(this.comboGisSource, "Available Map Providers");
            // 
            // labelGisSource
            // 
            this.labelGisSource.AutoSize = true;
            this.labelGisSource.Location = new System.Drawing.Point(7, 37);
            this.labelGisSource.Name = "labelGisSource";
            this.labelGisSource.Size = new System.Drawing.Size(108, 20);
            this.labelGisSource.TabIndex = 19;
            this.labelGisSource.Text = "Map Provider";
            this.toolTip1.SetToolTip(this.labelGisSource, "Which GIS Map Provider to use");
            // 
            // buttonQueryProviderAndStoreRouteFile
            // 
            this.buttonQueryProviderAndStoreRouteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQueryProviderAndStoreRouteFile.Location = new System.Drawing.Point(424, 34);
            this.buttonQueryProviderAndStoreRouteFile.Name = "buttonQueryProviderAndStoreRouteFile";
            this.buttonQueryProviderAndStoreRouteFile.Size = new System.Drawing.Size(197, 39);
            this.buttonQueryProviderAndStoreRouteFile.TabIndex = 15;
            this.buttonQueryProviderAndStoreRouteFile.Text = "Query and Save Routes...";
            this.toolTip1.SetToolTip(this.buttonQueryProviderAndStoreRouteFile, "Query MapProvider for Routes and save them to a provider-agnostic JSON File.");
            this.buttonQueryProviderAndStoreRouteFile.UseVisualStyleBackColor = true;
            this.buttonQueryProviderAndStoreRouteFile.Click += new System.EventHandler(this.buttonProcessCoordinateFile_Click);
            // 
            // tabGisResults
            // 
            this.tabGisResults.Controls.Add(this.label4);
            this.tabGisResults.Controls.Add(this.textMapDataSegments);
            this.tabGisResults.Controls.Add(this.buttonDisplayMapData);
            this.tabGisResults.Controls.Add(this.label2);
            this.tabGisResults.Location = new System.Drawing.Point(4, 29);
            this.tabGisResults.Name = "tabGisResults";
            this.tabGisResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabGisResults.Size = new System.Drawing.Size(1123, 564);
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
            this.textMapDataSegments.Location = new System.Drawing.Point(21, 150);
            this.textMapDataSegments.Multiline = true;
            this.textMapDataSegments.Name = "textMapDataSegments";
            this.textMapDataSegments.ReadOnly = true;
            this.textMapDataSegments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textMapDataSegments.Size = new System.Drawing.Size(1093, 285);
            this.textMapDataSegments.TabIndex = 11;
            // 
            // buttonDisplayMapData
            // 
            this.buttonDisplayMapData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayMapData.Location = new System.Drawing.Point(958, 57);
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
            // tabSimioDisplayInfo
            // 
            this.tabSimioDisplayInfo.Controls.Add(this.panel4);
            this.tabSimioDisplayInfo.Location = new System.Drawing.Point(4, 29);
            this.tabSimioDisplayInfo.Name = "tabSimioDisplayInfo";
            this.tabSimioDisplayInfo.Size = new System.Drawing.Size(1123, 564);
            this.tabSimioDisplayInfo.TabIndex = 4;
            this.tabSimioDisplayInfo.Text = "Simio Display Info";
            this.tabSimioDisplayInfo.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelDisplayBoundingBox);
            this.panel4.Controls.Add(this.buttonComputeTransform);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panelSimioScaling);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1123, 564);
            this.panel4.TabIndex = 5;
            // 
            // panelDisplayBoundingBox
            // 
            this.panelDisplayBoundingBox.Controls.Add(this.labelBoundingBoxCenter);
            this.panelDisplayBoundingBox.Controls.Add(this.label7);
            this.panelDisplayBoundingBox.Controls.Add(this.label9);
            this.panelDisplayBoundingBox.Controls.Add(this.labelSimioWidth);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxHeight);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxWidth);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxY);
            this.panelDisplayBoundingBox.Controls.Add(this.label8);
            this.panelDisplayBoundingBox.Controls.Add(this.labelBoxX);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxX);
            this.panelDisplayBoundingBox.Location = new System.Drawing.Point(14, 13);
            this.panelDisplayBoundingBox.Name = "panelDisplayBoundingBox";
            this.panelDisplayBoundingBox.Size = new System.Drawing.Size(553, 186);
            this.panelDisplayBoundingBox.TabIndex = 1;
            // 
            // labelBoundingBoxCenter
            // 
            this.labelBoundingBoxCenter.AutoSize = true;
            this.labelBoundingBoxCenter.Location = new System.Drawing.Point(13, 156);
            this.labelBoundingBoxCenter.Name = "labelBoundingBoxCenter";
            this.labelBoundingBoxCenter.Size = new System.Drawing.Size(234, 20);
            this.labelBoundingBoxCenter.TabIndex = 9;
            this.labelBoundingBoxCenter.Text = "Bounding box is centered at ...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(394, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Bounding Box in lon,lat Coordinates (Lon=X, Lat=Y)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Box Height";
            // 
            // labelSimioWidth
            // 
            this.labelSimioWidth.AutoSize = true;
            this.labelSimioWidth.Location = new System.Drawing.Point(13, 88);
            this.labelSimioWidth.Name = "labelSimioWidth";
            this.labelSimioWidth.Size = new System.Drawing.Size(91, 20);
            this.labelSimioWidth.TabIndex = 6;
            this.labelSimioWidth.Text = "Box Width:";
            // 
            // textSimioBoxHeight
            // 
            this.textSimioBoxHeight.Location = new System.Drawing.Point(138, 114);
            this.textSimioBoxHeight.Name = "textSimioBoxHeight";
            this.textSimioBoxHeight.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxHeight.TabIndex = 5;
            this.textSimioBoxHeight.Text = "30";
            this.textSimioBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textSimioBoxWidth
            // 
            this.textSimioBoxWidth.Location = new System.Drawing.Point(138, 81);
            this.textSimioBoxWidth.Name = "textSimioBoxWidth";
            this.textSimioBoxWidth.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxWidth.TabIndex = 4;
            this.textSimioBoxWidth.Text = "70";
            this.textSimioBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textSimioBoxY
            // 
            this.textSimioBoxY.Location = new System.Drawing.Point(316, 46);
            this.textSimioBoxY.Name = "textSimioBoxY";
            this.textSimioBoxY.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxY.TabIndex = 3;
            this.textSimioBoxY.Text = "20";
            this.textSimioBoxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(286, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Y:";
            // 
            // labelBoxX
            // 
            this.labelBoxX.AutoSize = true;
            this.labelBoxX.Location = new System.Drawing.Point(13, 47);
            this.labelBoxX.Name = "labelBoxX";
            this.labelBoxX.Size = new System.Drawing.Size(104, 20);
            this.labelBoxX.TabIndex = 1;
            this.labelBoxX.Text = "UpperLeft X:";
            // 
            // textSimioBoxX
            // 
            this.textSimioBoxX.Location = new System.Drawing.Point(138, 46);
            this.textSimioBoxX.Name = "textSimioBoxX";
            this.textSimioBoxX.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxX.TabIndex = 0;
            this.textSimioBoxX.Text = "-130";
            this.textSimioBoxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonComputeTransform
            // 
            this.buttonComputeTransform.Location = new System.Drawing.Point(582, 407);
            this.buttonComputeTransform.Name = "buttonComputeTransform";
            this.buttonComputeTransform.Size = new System.Drawing.Size(192, 48);
            this.buttonComputeTransform.TabIndex = 3;
            this.buttonComputeTransform.Text = "Compute Transform";
            this.toolTip1.SetToolTip(this.buttonComputeTransform, "Compute the Lon,Lat to Simio Facility View  transfrom.");
            this.buttonComputeTransform.UseVisualStyleBackColor = true;
            this.buttonComputeTransform.Click += new System.EventHandler(this.buttonComputeTransform_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.textFacilityY);
            this.panel3.Controls.Add(this.textFacilityX);
            this.panel3.Location = new System.Drawing.Point(14, 205);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(553, 103);
            this.panel3.TabIndex = 4;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(219, 20);
            this.label23.TabIndex = 8;
            this.label23.Text = "Origin (Facility Coordinates)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(13, 72);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 20);
            this.label25.TabIndex = 7;
            this.label25.Text = "Facility Y";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(13, 40);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(78, 20);
            this.label26.TabIndex = 6;
            this.label26.Text = "Facility X";
            // 
            // textFacilityY
            // 
            this.textFacilityY.Location = new System.Drawing.Point(208, 66);
            this.textFacilityY.Name = "textFacilityY";
            this.textFacilityY.Size = new System.Drawing.Size(120, 27);
            this.textFacilityY.TabIndex = 5;
            this.textFacilityY.Text = "0.0";
            this.textFacilityY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textFacilityX
            // 
            this.textFacilityX.Location = new System.Drawing.Point(208, 37);
            this.textFacilityX.Name = "textFacilityX";
            this.textFacilityX.Size = new System.Drawing.Size(120, 27);
            this.textFacilityX.TabIndex = 4;
            this.textFacilityX.Text = "0.0";
            this.textFacilityX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelSimioScaling
            // 
            this.panelSimioScaling.Controls.Add(this.label12);
            this.panelSimioScaling.Controls.Add(this.label13);
            this.panelSimioScaling.Controls.Add(this.label14);
            this.panelSimioScaling.Controls.Add(this.textSimioMetersPerLat);
            this.panelSimioScaling.Controls.Add(this.textSimioMetersPerLon);
            this.panelSimioScaling.Location = new System.Drawing.Point(14, 314);
            this.panelSimioScaling.Name = "panelSimioScaling";
            this.panelSimioScaling.Size = new System.Drawing.Size(553, 139);
            this.panelSimioScaling.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(327, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "How Many Simio (Meters) per (Lon or Lat)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 20);
            this.label13.TabIndex = 7;
            this.label13.Text = "Simio Meters per Lat";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(170, 20);
            this.label14.TabIndex = 6;
            this.label14.Text = "Simio Meters per Lon";
            // 
            // textSimioMetersPerLat
            // 
            this.textSimioMetersPerLat.Location = new System.Drawing.Point(208, 93);
            this.textSimioMetersPerLat.Name = "textSimioMetersPerLat";
            this.textSimioMetersPerLat.Size = new System.Drawing.Size(120, 27);
            this.textSimioMetersPerLat.TabIndex = 5;
            this.textSimioMetersPerLat.Text = "1.0";
            this.textSimioMetersPerLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textSimioMetersPerLon
            // 
            this.textSimioMetersPerLon.Location = new System.Drawing.Point(208, 60);
            this.textSimioMetersPerLon.Name = "textSimioMetersPerLon";
            this.textSimioMetersPerLon.Size = new System.Drawing.Size(120, 27);
            this.textSimioMetersPerLon.TabIndex = 4;
            this.textSimioMetersPerLon.Text = "1.0";
            this.textSimioMetersPerLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.groupBox1);
            this.tabTools.Location = new System.Drawing.Point(4, 29);
            this.tabTools.Name = "tabTools";
            this.tabTools.Size = new System.Drawing.Size(1123, 564);
            this.tabTools.TabIndex = 6;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textJsonContents);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.buttonGetJsonFile);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.buttonCreatePairFile);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Location = new System.Drawing.Point(33, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(906, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Pair File from JSON address file";
            // 
            // textJsonContents
            // 
            this.textJsonContents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textJsonContents.Location = new System.Drawing.Point(163, 117);
            this.textJsonContents.Multiline = true;
            this.textJsonContents.Name = "textJsonContents";
            this.textJsonContents.ReadOnly = true;
            this.textJsonContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textJsonContents.Size = new System.Drawing.Size(674, 193);
            this.textJsonContents.TabIndex = 30;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(25, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(515, 20);
            this.label18.TabIndex = 29;
            this.label18.Text = "Creates a tab-delimited pair file from a json file of unique addresses";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(25, 120);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(108, 20);
            this.label16.TabIndex = 26;
            this.label16.Text = "File Contents";
            // 
            // buttonGetJsonFile
            // 
            this.buttonGetJsonFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetJsonFile.Location = new System.Drawing.Point(796, 91);
            this.buttonGetJsonFile.Name = "buttonGetJsonFile";
            this.buttonGetJsonFile.Size = new System.Drawing.Size(41, 23);
            this.buttonGetJsonFile.TabIndex = 24;
            this.buttonGetJsonFile.Text = "...";
            this.toolTip1.SetToolTip(this.buttonGetJsonFile, "Locate and Read the Address File");
            this.buttonGetJsonFile.UseVisualStyleBackColor = true;
            this.buttonGetJsonFile.Click += new System.EventHandler(this.buttonGetJsonFile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(163, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(627, 27);
            this.textBox1.TabIndex = 21;
            this.toolTip1.SetToolTip(this.textBox1, "Location of file of address pairs");
            // 
            // buttonCreatePairFile
            // 
            this.buttonCreatePairFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreatePairFile.Location = new System.Drawing.Point(666, 316);
            this.buttonCreatePairFile.Name = "buttonCreatePairFile";
            this.buttonCreatePairFile.Size = new System.Drawing.Size(171, 34);
            this.buttonCreatePairFile.TabIndex = 23;
            this.buttonCreatePairFile.Text = "Create Pair File...";
            this.toolTip1.SetToolTip(this.buttonCreatePairFile, "Get GIS Data for each record in the file");
            this.buttonCreatePairFile.UseVisualStyleBackColor = true;
            this.buttonCreatePairFile.Click += new System.EventHandler(this.buttonCreatePairFile_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(144, 20);
            this.label17.TabIndex = 22;
            this.label17.Text = "Address Json File";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelInstructions);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1131, 61);
            this.panelTop.TabIndex = 3;
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
            // panelContent
            // 
            this.panelContent.Controls.Add(this.tabControl1);
            this.panelContent.Controls.Add(this.panelTop);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 36);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1131, 647);
            this.panelContent.TabIndex = 4;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(93, 32);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // FormGisViewer
            // 
            this.ClientSize = new System.Drawing.Size(1131, 709);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabGoogleMaps.ResumeLayout(false);
            this.tabGoogleMaps.PerformLayout();
            this.tabArcGIS.ResumeLayout(false);
            this.tabArcGIS.PerformLayout();
            this.tabUseAddressFile.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupAddressPairs.ResumeLayout(false);
            this.groupAddressPairs.PerformLayout();
            this.groupApplyRoutes.ResumeLayout(false);
            this.groupFetchAndStoreRoutes.ResumeLayout(false);
            this.groupFetchAndStoreRoutes.PerformLayout();
            this.tabGisResults.ResumeLayout(false);
            this.tabGisResults.PerformLayout();
            this.tabSimioDisplayInfo.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panelDisplayBoundingBox.ResumeLayout(false);
            this.panelDisplayBoundingBox.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelSimioScaling.ResumeLayout(false);
            this.panelSimioScaling.PerformLayout();
            this.tabTools.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContent.ResumeLayout(false);
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
                textBingMapsResponse.Text = "";

                if ( !BingMapHelper.GetRoute( textBingMapsKey.Text, textBingMapsFrom.Text, textBingMapsTo.Text, 
                    out SimioMapRoute mapRoute,
                    out string requestUrl, out string explanation) )
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
            foreach ( MapSegment segment in MapRoute.SegmentList )
            {
                sb.AppendLine(segment.ToString());
            }

            textMapDataSegments.Text = sb.ToString();
        }

        private void FormGisViewer_Load(object sender, EventArgs e)
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
            if ( ! SimioObjectHelpers.BuildSimioFacilityObjectsFromMapRoute(DesignContext, MapRoute, MapTransform, out string explanation))
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

                if ( !SampleHelper.GetLocationData(path, out List<GisLocation> locationList, out string explanation))
                {
                    alert($"Cannot get LocationData. Err={explanation}");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                GisLocationList = new List<GisLocation>();
                foreach ( GisLocation loc in locationList)
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

                if ( !File.Exists(path))
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

                switch ( comboGisSource.Text)
                {
                    case "Bing":
                        BingMapKey = textBingMapsKey.Text;
                        if ( !ProcessPairDataFile(path, BingMapHelper, BingMapKey, out mapRoutes, out explanation ))
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

                if ( string.IsNullOrEmpty(explanation))
                {
                    alert($"Routes were processed with some Errors: {explanation}");
                }

                if ( mapRoutes != null && mapRoutes.RouteList.Any())
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

                    if ( !JsonHelpers.SerializeToFile<SimioMapRoutes>(path, mapRoutes,false, out explanation))
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

                if (!GoogleMapHelper.GetRoute( textGoogleMapsKey.Text, textGoogleMapsFrom.Text, textGoogleMapsTo.Text, 
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

                if ( !JsonHelpers.DeserializeFromFile(path, out SimioMapRoutes mapRoutes, out string explanation))
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

                labelBoundingBoxCenter.Text = $"BoundingBox Lon/Lat center is {MapTransform.BoxCenter.X:F3}, {MapTransform.BoxCenter.Y:F3}";
                MapTransform.BuildSimioOrigin(textFacilityX.Text, textFacilityY.Text);
                MapTransform.BuildSimioScaling(textSimioMetersPerLon.Text, textSimioMetersPerLat.Text);

            }
            catch (Exception ex)
            {

                alert($"Cannot build Lon,Lat to Simio transform. Err={ex.Message}");
            }

        }

        private void buttonApplyRouteFile_Click(object sender, EventArgs e)
        {
            if ( MapRoutes == null )
            {
                alert($"No MapRoutes. Either load from file or get from Map Provider.");
                return;
            }

            if (MapTransform == null)
            {
                alert($"No MapTransform is loaded.");
                return;
            }

            if ( !SimioObjectHelpers.BuildSimioFacilityObjectsFromMapRoutes(DesignContext, MapRoutes, MapTransform, out string explanation) )
            {
                alert($"Cannot Build Facility Objects from MapRoutes={MapRoutes}. Err={explanation}");
                return;
            }



        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogAbout dialog = new dialogAbout();
            DialogResult result = dialog.ShowDialog();
        }
    }
}
