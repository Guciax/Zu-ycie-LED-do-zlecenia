namespace Zużycie_LED_do_zlecenia
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxOrderNo = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEndOrder = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.bwLoadOrder = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lPcbUsage = new System.Windows.Forms.Label();
            this.lMbUsage = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lSmtNg = new System.Windows.Forms.Label();
            this.labelSmtLines = new System.Windows.Forms.Label();
            this.labelSmtDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonChangeSmtQty = new System.Windows.Forms.Button();
            this.labelProductionInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelOrderInfo = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.labelBoxesCount = new System.Windows.Forms.Label();
            this.labelPackedQty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelRealUsage = new System.Windows.Forms.Label();
            this.labelTeorUsage = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelModelInfo = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelScrap = new System.Windows.Forms.Label();
            this.labelNg = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxOrderNo
            // 
            this.textBoxOrderNo.AcceptsReturn = true;
            this.textBoxOrderNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxOrderNo.Location = new System.Drawing.Point(0, 0);
            this.textBoxOrderNo.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxOrderNo.Multiline = true;
            this.textBoxOrderNo.Name = "textBoxOrderNo";
            this.textBoxOrderNo.Size = new System.Drawing.Size(621, 41);
            this.textBoxOrderNo.TabIndex = 0;
            this.textBoxOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxOrderNo_KeyDown);
            this.textBoxOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOrderNo_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(1, 634);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(690, 219);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonEndOrder);
            this.panel1.Controls.Add(this.labelStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 855);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 98);
            this.panel1.TabIndex = 2;
            // 
            // buttonEndOrder
            // 
            this.buttonEndOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonEndOrder.Location = new System.Drawing.Point(569, 0);
            this.buttonEndOrder.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEndOrder.Name = "buttonEndOrder";
            this.buttonEndOrder.Size = new System.Drawing.Size(121, 98);
            this.buttonEndOrder.TabIndex = 1;
            this.buttonEndOrder.Text = "Zakończ";
            this.buttonEndOrder.UseVisualStyleBackColor = true;
            this.buttonEndOrder.Click += new System.EventHandler(this.buttonEndOrder_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStatus.Location = new System.Drawing.Point(7, 5);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(62, 20);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Status:";
            // 
            // bwLoadOrder
            // 
            this.bwLoadOrder.WorkerReportsProgress = true;
            this.bwLoadOrder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 954);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.DarkRed;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.lPcbUsage);
            this.panel9.Controls.Add(this.lMbUsage);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel9.Location = new System.Drawing.Point(1, 579);
            this.panel9.Margin = new System.Windows.Forms.Padding(1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(690, 53);
            this.panel9.TabIndex = 12;
            // 
            // lPcbUsage
            // 
            this.lPcbUsage.AutoSize = true;
            this.lPcbUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lPcbUsage.Location = new System.Drawing.Point(4, 27);
            this.lPcbUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPcbUsage.Name = "lPcbUsage";
            this.lPcbUsage.Size = new System.Drawing.Size(111, 20);
            this.lPcbUsage.TabIndex = 8;
            this.lPcbUsage.Text = "Zużycie PCB:";
            // 
            // lMbUsage
            // 
            this.lMbUsage.AutoSize = true;
            this.lMbUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lMbUsage.Location = new System.Drawing.Point(4, 4);
            this.lMbUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMbUsage.Name = "lMbUsage";
            this.lMbUsage.Size = new System.Drawing.Size(102, 20);
            this.lMbUsage.TabIndex = 7;
            this.lMbUsage.Text = "Zużycie MB:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lSmtNg);
            this.panel6.Controls.Add(this.labelSmtLines);
            this.panel6.Controls.Add(this.labelSmtDate);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.buttonChangeSmtQty);
            this.panel6.Controls.Add(this.labelProductionInfo);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 253);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(690, 121);
            this.panel6.TabIndex = 8;
            // 
            // lSmtNg
            // 
            this.lSmtNg.AutoSize = true;
            this.lSmtNg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lSmtNg.Location = new System.Drawing.Point(5, 42);
            this.lSmtNg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lSmtNg.Name = "lSmtNg";
            this.lSmtNg.Size = new System.Drawing.Size(63, 20);
            this.lSmtNg.TabIndex = 11;
            this.lSmtNg.Text = "Odpad:";
            // 
            // labelSmtLines
            // 
            this.labelSmtLines.AutoSize = true;
            this.labelSmtLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSmtLines.Location = new System.Drawing.Point(5, 86);
            this.labelSmtLines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSmtLines.Name = "labelSmtLines";
            this.labelSmtLines.Size = new System.Drawing.Size(90, 20);
            this.labelSmtLines.TabIndex = 10;
            this.labelSmtLines.Text = "Linia SMT:";
            // 
            // labelSmtDate
            // 
            this.labelSmtDate.AutoSize = true;
            this.labelSmtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSmtDate.Location = new System.Drawing.Point(5, 64);
            this.labelSmtDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSmtDate.Name = "labelSmtDate";
            this.labelSmtDate.Size = new System.Drawing.Size(50, 20);
            this.labelSmtDate.TabIndex = 9;
            this.labelSmtDate.Text = "Data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(7, -1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "SMT";
            // 
            // buttonChangeSmtQty
            // 
            this.buttonChangeSmtQty.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonChangeSmtQty.Enabled = false;
            this.buttonChangeSmtQty.Location = new System.Drawing.Point(571, 0);
            this.buttonChangeSmtQty.Margin = new System.Windows.Forms.Padding(4);
            this.buttonChangeSmtQty.Name = "buttonChangeSmtQty";
            this.buttonChangeSmtQty.Size = new System.Drawing.Size(117, 119);
            this.buttonChangeSmtQty.TabIndex = 7;
            this.buttonChangeSmtQty.Text = "Zmień ilość SMT";
            this.buttonChangeSmtQty.UseVisualStyleBackColor = true;
            this.buttonChangeSmtQty.Click += new System.EventHandler(this.buttonChangeSmtQty_Click);
            // 
            // labelProductionInfo
            // 
            this.labelProductionInfo.AutoSize = true;
            this.labelProductionInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProductionInfo.Location = new System.Drawing.Point(5, 20);
            this.labelProductionInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProductionInfo.Name = "labelProductionInfo";
            this.labelProductionInfo.Size = new System.Drawing.Size(88, 20);
            this.labelProductionInfo.TabIndex = 6;
            this.labelProductionInfo.Text = "Produkcja:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.labelOrderInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 179);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(690, 72);
            this.panel3.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(5, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kitting";
            // 
            // labelOrderInfo
            // 
            this.labelOrderInfo.AutoSize = true;
            this.labelOrderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOrderInfo.Location = new System.Drawing.Point(4, 21);
            this.labelOrderInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOrderInfo.Name = "labelOrderInfo";
            this.labelOrderInfo.Size = new System.Drawing.Size(76, 20);
            this.labelOrderInfo.TabIndex = 6;
            this.labelOrderInfo.Text = "Zlecenie:";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.labelBoxesCount);
            this.panel8.Controls.Add(this.labelPackedQty);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(1, 450);
            this.panel8.Margin = new System.Windows.Forms.Padding(1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(690, 72);
            this.panel8.TabIndex = 10;
            // 
            // labelBoxesCount
            // 
            this.labelBoxesCount.AutoSize = true;
            this.labelBoxesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBoxesCount.Location = new System.Drawing.Point(97, 46);
            this.labelBoxesCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBoxesCount.Name = "labelBoxesCount";
            this.labelBoxesCount.Size = new System.Drawing.Size(121, 20);
            this.labelBoxesCount.TabIndex = 10;
            this.labelBoxesCount.Text = "Ilość kartonów:";
            // 
            // labelPackedQty
            // 
            this.labelPackedQty.AutoSize = true;
            this.labelPackedQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPackedQty.Location = new System.Drawing.Point(17, 25);
            this.labelPackedQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPackedQty.Name = "labelPackedQty";
            this.labelPackedQty.Size = new System.Drawing.Size(191, 20);
            this.labelPackedQty.TabIndex = 9;
            this.labelPackedQty.Text = "Spakowanych wyrobów: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pakowanie";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkRed;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.labelRealUsage);
            this.panel5.Controls.Add(this.labelTeorUsage);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel5.Location = new System.Drawing.Point(1, 524);
            this.panel5.Margin = new System.Windows.Forms.Padding(1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(690, 53);
            this.panel5.TabIndex = 7;
            // 
            // labelRealUsage
            // 
            this.labelRealUsage.AutoSize = true;
            this.labelRealUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRealUsage.Location = new System.Drawing.Point(4, 27);
            this.labelRealUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRealUsage.Name = "labelRealUsage";
            this.labelRealUsage.Size = new System.Drawing.Size(210, 20);
            this.labelRealUsage.TabIndex = 8;
            this.labelRealUsage.Text = "Rzeczywiste zużycie LED:";
            // 
            // labelTeorUsage
            // 
            this.labelTeorUsage.AutoSize = true;
            this.labelTeorUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTeorUsage.Location = new System.Drawing.Point(4, 4);
            this.labelTeorUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTeorUsage.Name = "labelTeorUsage";
            this.labelTeorUsage.Size = new System.Drawing.Size(207, 20);
            this.labelTeorUsage.TabIndex = 7;
            this.labelTeorUsage.Text = "Teoretyczne zużycie LED:";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.labelModelInfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1, 44);
            this.panel4.Margin = new System.Windows.Forms.Padding(1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(690, 133);
            this.panel4.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Zużycie_LED_do_zlecenia.Properties.Resources.loading_bar;
            this.pictureBox1.Location = new System.Drawing.Point(60, 44);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(544, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // labelModelInfo
            // 
            this.labelModelInfo.AutoSize = true;
            this.labelModelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelModelInfo.Location = new System.Drawing.Point(3, 1);
            this.labelModelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelModelInfo.Name = "labelModelInfo";
            this.labelModelInfo.Size = new System.Drawing.Size(59, 20);
            this.labelModelInfo.TabIndex = 7;
            this.labelModelInfo.Text = "Model:";
            this.labelModelInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.labelScrap);
            this.panel7.Controls.Add(this.labelNg);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(1, 376);
            this.panel7.Margin = new System.Windows.Forms.Padding(1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(690, 72);
            this.panel7.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Kontrola wzrokowa";
            // 
            // labelScrap
            // 
            this.labelScrap.AutoSize = true;
            this.labelScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScrap.Location = new System.Drawing.Point(5, 43);
            this.labelScrap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelScrap.Name = "labelScrap";
            this.labelScrap.Size = new System.Drawing.Size(71, 20);
            this.labelScrap.TabIndex = 9;
            this.labelScrap.Text = "SCRAP:";
            // 
            // labelNg
            // 
            this.labelNg.AutoSize = true;
            this.labelNg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNg.Location = new System.Drawing.Point(39, 22);
            this.labelNg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNg.Name = "labelNg";
            this.labelNg.Size = new System.Drawing.Size(39, 20);
            this.labelNg.TabIndex = 8;
            this.labelNg.Text = "NG:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxOrderNo);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 41);
            this.panel2.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(621, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Historia";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 954);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Zużycie LED do zlecenia";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOrderNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonEndOrder;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker bwLoadOrder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelOrderInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelModelInfo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelTeorUsage;
        private System.Windows.Forms.Label labelRealUsage;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label labelProductionInfo;
        private System.Windows.Forms.Button buttonChangeSmtQty;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelScrap;
        private System.Windows.Forms.Label labelNg;
        private System.Windows.Forms.Label labelSmtDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSmtLines;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label labelBoxesCount;
        private System.Windows.Forms.Label labelPackedQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lSmtNg;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lPcbUsage;
        private System.Windows.Forms.Label lMbUsage;
    }
}

