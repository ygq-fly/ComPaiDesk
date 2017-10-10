namespace jcPimSoftware
{
    partial class PimSettingForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cKxEs = new System.Windows.Forms.CheckBox();
            this.lblPimMode = new System.Windows.Forms.Label();
            this.cbxPimMode = new System.Windows.Forms.ComboBox();
            this.cbxPimOrder = new System.Windows.Forms.ComboBox();
            this.lblPimOrder = new System.Windows.Forms.Label();
            this.lblDbm = new System.Windows.Forms.Label();
            this.lblPimLimit = new System.Windows.Forms.Label();
            this.cbxPimSchema = new System.Windows.Forms.ComboBox();
            this.lblPimSchema = new System.Windows.Forms.Label();
            this.cbxPimUnit = new System.Windows.Forms.ComboBox();
            this.lblPimUnit = new System.Windows.Forms.Label();
            this.lblAtt = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.gbxTimeSweep = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPintsT = new System.Windows.Forms.NumericUpDown();
            this.nudTimesT = new System.Windows.Forms.NumericUpDown();
            this.lblPoints1 = new System.Windows.Forms.Label();
            this.lblTimes1 = new System.Windows.Forms.Label();
            this.gbxFrequency = new System.Windows.Forms.GroupBox();
            this.nudPointsF = new System.Windows.Forms.NumericUpDown();
            this.nudTimesF = new System.Windows.Forms.NumericUpDown();
            this.lblPoints2 = new System.Windows.Forms.Label();
            this.lblTimes2 = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.nudPimLimit = new System.Windows.Forms.NumericUpDown();
            this.nudTx = new System.Windows.Forms.NumericUpDown();
            this.nudAtt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.gbxTimeSweep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPintsT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimesT)).BeginInit();
            this.gbxFrequency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimesF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPimLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(46, 348);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save As";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(329, 394);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cKxEs
            // 
            this.cKxEs.AutoSize = true;
            this.cKxEs.Enabled = false;
            this.cKxEs.Font = new System.Drawing.Font("宋体", 12F);
            this.cKxEs.Location = new System.Drawing.Point(252, 18);
            this.cKxEs.Name = "cKxEs";
            this.cKxEs.Size = new System.Drawing.Size(131, 20);
            this.cKxEs.TabIndex = 8;
            this.cKxEs.Text = "EnableSquence";
            this.cKxEs.UseVisualStyleBackColor = true;
            this.cKxEs.Visible = false;
            // 
            // lblPimMode
            // 
            this.lblPimMode.AutoSize = true;
            this.lblPimMode.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPimMode.Location = new System.Drawing.Point(34, 18);
            this.lblPimMode.Name = "lblPimMode";
            this.lblPimMode.Size = new System.Drawing.Size(80, 16);
            this.lblPimMode.TabIndex = 28;
            this.lblPimMode.Text = "SweepType";
            // 
            // cbxPimMode
            // 
            this.cbxPimMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimMode.Font = new System.Drawing.Font("宋体", 12F);
            this.cbxPimMode.FormattingEnabled = true;
            this.cbxPimMode.Location = new System.Drawing.Point(136, 16);
            this.cbxPimMode.Name = "cbxPimMode";
            this.cbxPimMode.Size = new System.Drawing.Size(107, 24);
            this.cbxPimMode.TabIndex = 27;
            // 
            // cbxPimOrder
            // 
            this.cbxPimOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimOrder.Font = new System.Drawing.Font("宋体", 12F);
            this.cbxPimOrder.FormattingEnabled = true;
            this.cbxPimOrder.Location = new System.Drawing.Point(136, 173);
            this.cbxPimOrder.Name = "cbxPimOrder";
            this.cbxPimOrder.Size = new System.Drawing.Size(79, 24);
            this.cbxPimOrder.TabIndex = 7;
            // 
            // lblPimOrder
            // 
            this.lblPimOrder.AutoSize = true;
            this.lblPimOrder.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPimOrder.Location = new System.Drawing.Point(36, 173);
            this.lblPimOrder.Name = "lblPimOrder";
            this.lblPimOrder.Size = new System.Drawing.Size(80, 16);
            this.lblPimOrder.TabIndex = 6;
            this.lblPimOrder.Text = "PIM Order";
            // 
            // lblDbm
            // 
            this.lblDbm.AutoSize = true;
            this.lblDbm.Font = new System.Drawing.Font("宋体", 12F);
            this.lblDbm.Location = new System.Drawing.Point(449, 176);
            this.lblDbm.Name = "lblDbm";
            this.lblDbm.Size = new System.Drawing.Size(32, 16);
            this.lblDbm.TabIndex = 5;
            this.lblDbm.Text = "dBm";
            // 
            // lblPimLimit
            // 
            this.lblPimLimit.AutoSize = true;
            this.lblPimLimit.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPimLimit.Location = new System.Drawing.Point(278, 173);
            this.lblPimLimit.Name = "lblPimLimit";
            this.lblPimLimit.Size = new System.Drawing.Size(80, 16);
            this.lblPimLimit.TabIndex = 4;
            this.lblPimLimit.Text = "PIM Limit";
            // 
            // cbxPimSchema
            // 
            this.cbxPimSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimSchema.Font = new System.Drawing.Font("宋体", 12F);
            this.cbxPimSchema.FormattingEnabled = true;
            this.cbxPimSchema.Location = new System.Drawing.Point(136, 212);
            this.cbxPimSchema.Name = "cbxPimSchema";
            this.cbxPimSchema.Size = new System.Drawing.Size(79, 24);
            this.cbxPimSchema.TabIndex = 25;
            // 
            // lblPimSchema
            // 
            this.lblPimSchema.AutoSize = true;
            this.lblPimSchema.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPimSchema.Location = new System.Drawing.Point(36, 215);
            this.lblPimSchema.Name = "lblPimSchema";
            this.lblPimSchema.Size = new System.Drawing.Size(80, 16);
            this.lblPimSchema.TabIndex = 26;
            this.lblPimSchema.Text = "PimSchema";
            // 
            // cbxPimUnit
            // 
            this.cbxPimUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimUnit.Font = new System.Drawing.Font("宋体", 12F);
            this.cbxPimUnit.FormattingEnabled = true;
            this.cbxPimUnit.Location = new System.Drawing.Point(362, 212);
            this.cbxPimUnit.Name = "cbxPimUnit";
            this.cbxPimUnit.Size = new System.Drawing.Size(79, 24);
            this.cbxPimUnit.TabIndex = 29;
            // 
            // lblPimUnit
            // 
            this.lblPimUnit.AutoSize = true;
            this.lblPimUnit.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPimUnit.Location = new System.Drawing.Point(294, 215);
            this.lblPimUnit.Name = "lblPimUnit";
            this.lblPimUnit.Size = new System.Drawing.Size(64, 16);
            this.lblPimUnit.TabIndex = 30;
            this.lblPimUnit.Text = "PimUnit";
            // 
            // lblAtt
            // 
            this.lblAtt.AutoSize = true;
            this.lblAtt.Font = new System.Drawing.Font("宋体", 12F);
            this.lblAtt.Location = new System.Drawing.Point(320, 256);
            this.lblAtt.Name = "lblAtt";
            this.lblAtt.Size = new System.Drawing.Size(48, 16);
            this.lblAtt.TabIndex = 32;
            this.lblAtt.Text = "ATT：";
            // 
            // lblTx
            // 
            this.lblTx.AutoSize = true;
            this.lblTx.Font = new System.Drawing.Font("宋体", 12F);
            this.lblTx.Location = new System.Drawing.Point(76, 256);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(40, 16);
            this.lblTx.TabIndex = 34;
            this.lblTx.Text = "Tx：";
            // 
            // gbxTimeSweep
            // 
            this.gbxTimeSweep.Controls.Add(this.label1);
            this.gbxTimeSweep.Controls.Add(this.nudPintsT);
            this.gbxTimeSweep.Controls.Add(this.nudTimesT);
            this.gbxTimeSweep.Controls.Add(this.lblPoints1);
            this.gbxTimeSweep.Controls.Add(this.lblTimes1);
            this.gbxTimeSweep.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxTimeSweep.Location = new System.Drawing.Point(290, 53);
            this.gbxTimeSweep.Name = "gbxTimeSweep";
            this.gbxTimeSweep.Size = new System.Drawing.Size(186, 105);
            this.gbxTimeSweep.TabIndex = 40;
            this.gbxTimeSweep.TabStop = false;
            this.gbxTimeSweep.Text = "Time Sweep";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(151, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "S";
            // 
            // nudPintsT
            // 
            this.nudPintsT.DecimalPlaces = 1;
            this.nudPintsT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudPintsT.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPintsT.Location = new System.Drawing.Point(72, 66);
            this.nudPintsT.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPintsT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPintsT.Name = "nudPintsT";
            this.nudPintsT.Size = new System.Drawing.Size(79, 26);
            this.nudPintsT.TabIndex = 50;
            this.nudPintsT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPintsT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudTimesT
            // 
            this.nudTimesT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudTimesT.Location = new System.Drawing.Point(72, 24);
            this.nudTimesT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimesT.Name = "nudTimesT";
            this.nudTimesT.Size = new System.Drawing.Size(79, 26);
            this.nudTimesT.TabIndex = 49;
            this.nudTimesT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimesT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // lblPoints1
            // 
            this.lblPoints1.AutoSize = true;
            this.lblPoints1.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPoints1.Location = new System.Drawing.Point(15, 68);
            this.lblPoints1.Name = "lblPoints1";
            this.lblPoints1.Size = new System.Drawing.Size(56, 16);
            this.lblPoints1.TabIndex = 1;
            this.lblPoints1.Text = "Time：";
            // 
            // lblTimes1
            // 
            this.lblTimes1.AutoSize = true;
            this.lblTimes1.Font = new System.Drawing.Font("宋体", 12F);
            this.lblTimes1.Location = new System.Drawing.Point(14, 25);
            this.lblTimes1.Name = "lblTimes1";
            this.lblTimes1.Size = new System.Drawing.Size(64, 16);
            this.lblTimes1.TabIndex = 0;
            this.lblTimes1.Text = "Times：";
            // 
            // gbxFrequency
            // 
            this.gbxFrequency.Controls.Add(this.nudPointsF);
            this.gbxFrequency.Controls.Add(this.nudTimesF);
            this.gbxFrequency.Controls.Add(this.lblPoints2);
            this.gbxFrequency.Controls.Add(this.lblTimes2);
            this.gbxFrequency.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxFrequency.Location = new System.Drawing.Point(29, 53);
            this.gbxFrequency.Name = "gbxFrequency";
            this.gbxFrequency.Size = new System.Drawing.Size(186, 105);
            this.gbxFrequency.TabIndex = 41;
            this.gbxFrequency.TabStop = false;
            this.gbxFrequency.Text = "Frequency Sweep";
            // 
            // nudPointsF
            // 
            this.nudPointsF.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudPointsF.Location = new System.Drawing.Point(92, 66);
            this.nudPointsF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPointsF.Name = "nudPointsF";
            this.nudPointsF.Size = new System.Drawing.Size(79, 26);
            this.nudPointsF.TabIndex = 39;
            this.nudPointsF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPointsF.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudTimesF
            // 
            this.nudTimesF.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudTimesF.Location = new System.Drawing.Point(92, 25);
            this.nudTimesF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimesF.Name = "nudTimesF";
            this.nudTimesF.Size = new System.Drawing.Size(79, 26);
            this.nudTimesF.TabIndex = 38;
            this.nudTimesF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimesF.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // lblPoints2
            // 
            this.lblPoints2.AutoSize = true;
            this.lblPoints2.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPoints2.Location = new System.Drawing.Point(6, 68);
            this.lblPoints2.Name = "lblPoints2";
            this.lblPoints2.Size = new System.Drawing.Size(72, 16);
            this.lblPoints2.TabIndex = 3;
            this.lblPoints2.Text = "Points：";
            // 
            // lblTimes2
            // 
            this.lblTimes2.AutoSize = true;
            this.lblTimes2.Font = new System.Drawing.Font("宋体", 12F);
            this.lblTimes2.Location = new System.Drawing.Point(14, 25);
            this.lblTimes2.Name = "lblTimes2";
            this.lblTimes2.Size = new System.Drawing.Size(64, 16);
            this.lblTimes2.TabIndex = 2;
            this.lblTimes2.Text = "Times：";
            // 
            // btnDefault
            // 
            this.btnDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefault.Location = new System.Drawing.Point(329, 348);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(88, 30);
            this.btnDefault.TabIndex = 42;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Location = new System.Drawing.Point(189, 394);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 30);
            this.btnOk.TabIndex = 43;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.Location = new System.Drawing.Point(189, 348);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(88, 30);
            this.btnLoad.TabIndex = 44;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(229, 259);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(32, 16);
            this.lbl1.TabIndex = 45;
            this.lbl1.Text = "dBm";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(452, 256);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(24, 16);
            this.lbl2.TabIndex = 46;
            this.lbl2.Text = "dB";
            // 
            // nudPimLimit
            // 
            this.nudPimLimit.DecimalPlaces = 1;
            this.nudPimLimit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudPimLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPimLimit.Location = new System.Drawing.Point(362, 171);
            this.nudPimLimit.Maximum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            0});
            this.nudPimLimit.Minimum = new decimal(new int[] {
            1569325056,
            23283064,
            0,
            -2147483648});
            this.nudPimLimit.Name = "nudPimLimit";
            this.nudPimLimit.Size = new System.Drawing.Size(79, 26);
            this.nudPimLimit.TabIndex = 51;
            this.nudPimLimit.Value = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.nudPimLimit.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudTx
            // 
            this.nudTx.DecimalPlaces = 1;
            this.nudTx.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudTx.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTx.Location = new System.Drawing.Point(136, 251);
            this.nudTx.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTx.Name = "nudTx";
            this.nudTx.Size = new System.Drawing.Size(79, 26);
            this.nudTx.TabIndex = 52;
            this.nudTx.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudAtt
            // 
            this.nudAtt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudAtt.Location = new System.Drawing.Point(362, 254);
            this.nudAtt.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudAtt.Name = "nudAtt";
            this.nudAtt.Size = new System.Drawing.Size(79, 26);
            this.nudAtt.TabIndex = 53;
            this.nudAtt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(362, 294);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(107, 26);
            this.numericUpDown1.TabIndex = 54;
            this.numericUpDown1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 55;
            this.label2.Text = "Rx Ref";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Tx Ref";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 1;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(108, 294);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            -2147418112});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(107, 26);
            this.numericUpDown2.TabIndex = 56;
            this.numericUpDown2.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.numericUpDown2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown2_MouseDoubleClick);
            // 
            // PimSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 440);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.nudAtt);
            this.Controls.Add(this.nudTx);
            this.Controls.Add(this.nudPimLimit);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.cbxPimMode);
            this.Controls.Add(this.lblPimMode);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.gbxTimeSweep);
            this.Controls.Add(this.gbxFrequency);
            this.Controls.Add(this.lblTx);
            this.Controls.Add(this.lblAtt);
            this.Controls.Add(this.lblPimUnit);
            this.Controls.Add(this.cbxPimUnit);
            this.Controls.Add(this.lblPimSchema);
            this.Controls.Add(this.cbxPimSchema);
            this.Controls.Add(this.lblPimLimit);
            this.Controls.Add(this.lblDbm);
            this.Controls.Add(this.lblPimOrder);
            this.Controls.Add(this.cbxPimOrder);
            this.Controls.Add(this.cKxEs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PimSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "..";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PimSettingForm_Load);
            this.gbxTimeSweep.ResumeLayout(false);
            this.gbxTimeSweep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPintsT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimesT)).EndInit();
            this.gbxFrequency.ResumeLayout(false);
            this.gbxFrequency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimesF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPimLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cKxEs;
        private System.Windows.Forms.Label lblPimMode;
        private System.Windows.Forms.ComboBox cbxPimMode;
        private System.Windows.Forms.ComboBox cbxPimOrder;
        private System.Windows.Forms.Label lblPimOrder;
        private System.Windows.Forms.Label lblDbm;
        private System.Windows.Forms.Label lblPimLimit;
        private System.Windows.Forms.ComboBox cbxPimSchema;
        private System.Windows.Forms.Label lblPimSchema;
        private System.Windows.Forms.ComboBox cbxPimUnit;
        private System.Windows.Forms.Label lblPimUnit;
        private System.Windows.Forms.Label lblAtt;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.GroupBox gbxTimeSweep;
        private System.Windows.Forms.Label lblPoints1;
        private System.Windows.Forms.Label lblTimes1;
        private System.Windows.Forms.GroupBox gbxFrequency;
        private System.Windows.Forms.Label lblPoints2;
        private System.Windows.Forms.Label lblTimes2;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.NumericUpDown nudTimesF;
        private System.Windows.Forms.NumericUpDown nudPointsF;
        private System.Windows.Forms.NumericUpDown nudTimesT;
        private System.Windows.Forms.NumericUpDown nudPintsT;
        private System.Windows.Forms.NumericUpDown nudPimLimit;
        private System.Windows.Forms.NumericUpDown nudTx;
        private System.Windows.Forms.NumericUpDown nudAtt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
    }
}