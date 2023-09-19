
namespace NumberIdentifier
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.pictureBoxPaint = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLayers = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEpocs = new System.Windows.Forms.NumericUpDown();
            this.txtBatchSize = new System.Windows.Forms.NumericUpDown();
            this.txtEta = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEpocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEta)).BeginInit();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(358, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 234);
            this.label1.TabIndex = 1;
            // 
            // buttonClear
            // 
            this.buttonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.ForeColor = System.Drawing.Color.White;
            this.buttonClear.Location = new System.Drawing.Point(371, 338);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(141, 39);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // pictureBoxPaint
            // 
            this.pictureBoxPaint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPaint.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxPaint.Enabled = false;
            this.pictureBoxPaint.Location = new System.Drawing.Point(14, 39);
            this.pictureBoxPaint.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPaint.Name = "pictureBoxPaint";
            this.pictureBoxPaint.Size = new System.Drawing.Size(326, 344);
            this.pictureBoxPaint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPaint.TabIndex = 3;
            this.pictureBoxPaint.TabStop = false;
            this.pictureBoxPaint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPaint_MouseDown);
            this.pictureBoxPaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPaint_MouseMove);
            this.pictureBoxPaint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPaint_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Blue;
            this.pictureBox1.Location = new System.Drawing.Point(371, 255);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 34);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(346, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 9);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(326, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(371, 391);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 39);
            this.button1.TabIndex = 7;
            this.button1.Text = "Try";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(14, 407);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(163, 24);
            this.numericUpDown1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(203, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Expected: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Coral;
            this.label4.Location = new System.Drawing.Point(12, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txtEta);
            this.panel1.Controls.Add(this.txtBatchSize);
            this.panel1.Controls.Add(this.txtEpocs);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblBatchSize);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtLayers);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(533, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 422);
            this.panel1.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(25, 330);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(321, 39);
            this.button2.TabIndex = 12;
            this.button2.Text = "Start Learning";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(22, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Layers";
            // 
            // txtLayers
            // 
            this.txtLayers.Location = new System.Drawing.Point(25, 51);
            this.txtLayers.Name = "txtLayers";
            this.txtLayers.Size = new System.Drawing.Size(321, 24);
            this.txtLayers.TabIndex = 14;
            this.txtLayers.Text = "784, 30, 10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(22, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Epocs";
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.AutoSize = true;
            this.lblBatchSize.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblBatchSize.Location = new System.Drawing.Point(22, 172);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(70, 17);
            this.lblBatchSize.TabIndex = 17;
            this.lblBatchSize.Text = "Batch Size";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(22, 241);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Eta";
            // 
            // txtEpocs
            // 
            this.txtEpocs.Location = new System.Drawing.Point(25, 121);
            this.txtEpocs.Name = "txtEpocs";
            this.txtEpocs.Size = new System.Drawing.Size(321, 24);
            this.txtEpocs.TabIndex = 21;
            this.txtEpocs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // txtBatchSize
            // 
            this.txtBatchSize.Location = new System.Drawing.Point(25, 199);
            this.txtBatchSize.Name = "txtBatchSize";
            this.txtBatchSize.Size = new System.Drawing.Size(321, 24);
            this.txtBatchSize.TabIndex = 22;
            this.txtBatchSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // txtEta
            // 
            this.txtEta.DecimalPlaces = 2;
            this.txtEta.Location = new System.Drawing.Point(25, 272);
            this.txtEta.Name = "txtEta";
            this.txtEta.Size = new System.Drawing.Size(321, 24);
            this.txtEta.TabIndex = 23;
            this.txtEta.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.checkBox1.Location = new System.Drawing.Point(371, 310);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(116, 21);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Hand Drawing";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(25, 377);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(321, 39);
            this.button3.TabIndex = 24;
            this.button3.Text = "Load Last Weights";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(916, 445);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxPaint);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Number Identifier";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPaint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEpocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.PictureBox pictureBoxPaint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLayers;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtEta;
        private System.Windows.Forms.NumericUpDown txtBatchSize;
        private System.Windows.Forms.NumericUpDown txtEpocs;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
    }
}