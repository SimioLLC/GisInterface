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
        private Label label10;
        private Button buttonBingApplyRoute;
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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.label5 = new System.Windows.Forms.Label();
            this.tabArcGIS = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tabGisResults = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textMapDataSegments = new System.Windows.Forms.TextBox();
            this.buttonDisplayMapData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabSimioDisplayInfo = new System.Windows.Forms.TabPage();
            this.panelDisplayBoundingBox = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelSimioWidth = new System.Windows.Forms.Label();
            this.textSimioBoxHeight = new System.Windows.Forms.TextBox();
            this.textSimioBoxWidth = new System.Windows.Forms.TextBox();
            this.textSimioBoxY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelBoxX = new System.Windows.Forms.Label();
            this.textSimioBoxX = new System.Windows.Forms.TextBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.panelSimioScaling = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textSimioMetersPerLat = new System.Windows.Forms.TextBox();
            this.textSimioMetersPerLon = new System.Windows.Forms.TextBox();
            this.buttonBingApplyRoute = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBingMaps.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabGoogleMaps.SuspendLayout();
            this.tabArcGIS.SuspendLayout();
            this.tabGisResults.SuspendLayout();
            this.tabSimioDisplayInfo.SuspendLayout();
            this.panelDisplayBoundingBox.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelSimioScaling.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1131, 36);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 684);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1131, 25);
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
            this.tabControl1.Controls.Add(this.tabSimioDisplayInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1131, 587);
            this.tabControl1.TabIndex = 2;
            // 
            // tabBingMaps
            // 
            this.tabBingMaps.Controls.Add(this.panel1);
            this.tabBingMaps.Location = new System.Drawing.Point(4, 29);
            this.tabBingMaps.Name = "tabBingMaps";
            this.tabBingMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabBingMaps.Size = new System.Drawing.Size(1123, 554);
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
            this.panel1.Size = new System.Drawing.Size(1117, 548);
            this.panel1.TabIndex = 11;
            // 
            // buttonSaveBingKey
            // 
            this.buttonSaveBingKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveBingKey.Location = new System.Drawing.Point(949, 12);
            this.buttonSaveBingKey.Name = "buttonSaveBingKey";
            this.buttonSaveBingKey.Size = new System.Drawing.Size(151, 34);
            this.buttonSaveBingKey.TabIndex = 12;
            this.buttonSaveBingKey.Text = "Save Bing Key";
            this.toolTip1.SetToolTip(this.buttonSaveBingKey, "Store the key under your Documents \\ SimioUserExtensions in ");
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
            this.tabGoogleMaps.Controls.Add(this.label5);
            this.tabGoogleMaps.Location = new System.Drawing.Point(4, 29);
            this.tabGoogleMaps.Name = "tabGoogleMaps";
            this.tabGoogleMaps.Size = new System.Drawing.Size(1123, 554);
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
            this.tabArcGIS.Location = new System.Drawing.Point(4, 29);
            this.tabArcGIS.Name = "tabArcGIS";
            this.tabArcGIS.Size = new System.Drawing.Size(1123, 554);
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
            this.tabGisResults.Location = new System.Drawing.Point(4, 29);
            this.tabGisResults.Name = "tabGisResults";
            this.tabGisResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabGisResults.Size = new System.Drawing.Size(1123, 554);
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
            this.textMapDataSegments.Size = new System.Drawing.Size(1106, 285);
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
            this.tabSimioDisplayInfo.Controls.Add(this.panelSimioScaling);
            this.tabSimioDisplayInfo.Controls.Add(this.panelDisplayBoundingBox);
            this.tabSimioDisplayInfo.Location = new System.Drawing.Point(4, 29);
            this.tabSimioDisplayInfo.Name = "tabSimioDisplayInfo";
            this.tabSimioDisplayInfo.Size = new System.Drawing.Size(1123, 554);
            this.tabSimioDisplayInfo.TabIndex = 4;
            this.tabSimioDisplayInfo.Text = "Simio Display Info";
            this.tabSimioDisplayInfo.UseVisualStyleBackColor = true;
            // 
            // panelDisplayBoundingBox
            // 
            this.panelDisplayBoundingBox.Controls.Add(this.label10);
            this.panelDisplayBoundingBox.Controls.Add(this.label7);
            this.panelDisplayBoundingBox.Controls.Add(this.label9);
            this.panelDisplayBoundingBox.Controls.Add(this.labelSimioWidth);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxHeight);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxWidth);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxY);
            this.panelDisplayBoundingBox.Controls.Add(this.label8);
            this.panelDisplayBoundingBox.Controls.Add(this.labelBoxX);
            this.panelDisplayBoundingBox.Controls.Add(this.textSimioBoxX);
            this.panelDisplayBoundingBox.Location = new System.Drawing.Point(26, 27);
            this.panelDisplayBoundingBox.Name = "panelDisplayBoundingBox";
            this.panelDisplayBoundingBox.Size = new System.Drawing.Size(553, 229);
            this.panelDisplayBoundingBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(352, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Bounding Box for Coordinates (Lon=X, Lat=Y)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Box Height";
            // 
            // labelSimioWidth
            // 
            this.labelSimioWidth.AutoSize = true;
            this.labelSimioWidth.Location = new System.Drawing.Point(13, 151);
            this.labelSimioWidth.Name = "labelSimioWidth";
            this.labelSimioWidth.Size = new System.Drawing.Size(91, 20);
            this.labelSimioWidth.TabIndex = 6;
            this.labelSimioWidth.Text = "Box Width:";
            // 
            // textSimioBoxHeight
            // 
            this.textSimioBoxHeight.Location = new System.Drawing.Point(138, 177);
            this.textSimioBoxHeight.Name = "textSimioBoxHeight";
            this.textSimioBoxHeight.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxHeight.TabIndex = 5;
            this.textSimioBoxHeight.Text = "30";
            this.textSimioBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textSimioBoxWidth
            // 
            this.textSimioBoxWidth.Location = new System.Drawing.Point(138, 144);
            this.textSimioBoxWidth.Name = "textSimioBoxWidth";
            this.textSimioBoxWidth.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxWidth.TabIndex = 4;
            this.textSimioBoxWidth.Text = "70";
            this.textSimioBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textSimioBoxY
            // 
            this.textSimioBoxY.Location = new System.Drawing.Point(316, 86);
            this.textSimioBoxY.Name = "textSimioBoxY";
            this.textSimioBoxY.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxY.TabIndex = 3;
            this.textSimioBoxY.Text = "20";
            this.textSimioBoxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(286, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Y:";
            // 
            // labelBoxX
            // 
            this.labelBoxX.AutoSize = true;
            this.labelBoxX.Location = new System.Drawing.Point(13, 87);
            this.labelBoxX.Name = "labelBoxX";
            this.labelBoxX.Size = new System.Drawing.Size(104, 20);
            this.labelBoxX.TabIndex = 1;
            this.labelBoxX.Text = "UpperLeft X:";
            // 
            // textSimioBoxX
            // 
            this.textSimioBoxX.Location = new System.Drawing.Point(138, 86);
            this.textSimioBoxX.Name = "textSimioBoxX";
            this.textSimioBoxX.Size = new System.Drawing.Size(120, 27);
            this.textSimioBoxX.TabIndex = 0;
            this.textSimioBoxX.Text = "-130";
            this.textSimioBoxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.panelContent.Size = new System.Drawing.Size(1131, 648);
            this.panelContent.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(221, 20);
            this.label10.TabIndex = 9;
            this.label10.Text = "This will be centered at (0,0)";
            // 
            // panelSimioScaling
            // 
            this.panelSimioScaling.Controls.Add(this.label12);
            this.panelSimioScaling.Controls.Add(this.label13);
            this.panelSimioScaling.Controls.Add(this.label14);
            this.panelSimioScaling.Controls.Add(this.textSimioMetersPerLat);
            this.panelSimioScaling.Controls.Add(this.textSimioMetersPerLon);
            this.panelSimioScaling.Location = new System.Drawing.Point(26, 262);
            this.panelSimioScaling.Name = "panelSimioScaling";
            this.panelSimioScaling.Size = new System.Drawing.Size(553, 229);
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
            this.buttonBingApplyRoute.Visible = false;
            this.buttonBingApplyRoute.Click += new System.EventHandler(this.buttonBingApplyRoute_Click);
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
            this.tabGisResults.ResumeLayout(false);
            this.tabGisResults.PerformLayout();
            this.tabSimioDisplayInfo.ResumeLayout(false);
            this.panelDisplayBoundingBox.ResumeLayout(false);
            this.panelDisplayBoundingBox.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelSimioScaling.ResumeLayout(false);
            this.panelSimioScaling.PerformLayout();
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
                float xx = float.Parse(textSimioBoxX.Text);
                float yy = float.Parse(textSimioBoxY.Text);
                float width = float.Parse(textSimioBoxWidth.Text);
                float height = float.Parse(textSimioBoxHeight.Text);
                RectangleF latlonBoundingBox = new RectangleF(xx, yy, width, height);

                float scaleX = float.Parse(textSimioMetersPerLon.Text);
                float scaleY = float.Parse(textSimioMetersPerLat.Text);
                PointF simioScaling = new PointF(scaleX, scaleY);

                ProcessBingMapsRouteRequest(textBingMapsKey.Text, MapData, latlonBoundingBox, simioScaling, textBingMapsFrom.Text, textBingMapsTo.Text);

                MapData.StartName = textBingMapsFrom.Text;
                MapData.EndName = textBingMapsTo.Text;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"QueryBingMaps Error={ex}");
            }
        }

        /// <summary>
        /// This method has a lot of logic that is specific to the sample. 
        /// To process a request you can easily just call the Execute method on the request.
        /// This will build much of the SimioMapData object.
        /// </summary>
        /// <param name="request"></param>
        private async void ProcessBingMapsRouteRequest( string bingKey, SimioMapData mapData, RectangleF lonlatBox, PointF simioScaling, string fromName, string toName)
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
                    PointF ptLoc = lonlatBox.Location; // new PointF( -130f, 20f);
                    float width = lonlatBox.Width; // 70f;
                    float height = lonlatBox.Height; // 30f; 

                    // Turning the thing on its side, since we want latitude to be 'Y'
                    mapData.LonLatBoundingBox = new RectangleF(ptLoc.X, ptLoc.Y, width, height);
                    mapData.Origin = new PointF(ptLoc.X + width / 2f, ptLoc.Y + height / 2f);

                    mapData.SimioScaling = simioScaling;

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

        private void buttonBingApplyRoute_Click(object sender, EventArgs e)
        {

        }
    }
}
