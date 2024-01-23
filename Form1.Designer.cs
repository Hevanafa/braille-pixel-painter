namespace BraillePixelEditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbArt = new PictureBox();
            btnRender = new Button();
            txbBraille = new TextBox();
            label1 = new Label();
            label2 = new Label();
            cbBucketTool = new CheckBox();
            cbBlackFill = new CheckBox();
            btnSave = new Button();
            btnLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)pbArt).BeginInit();
            SuspendLayout();
            // 
            // pbArt
            // 
            pbArt.BackgroundImageLayout = ImageLayout.Zoom;
            pbArt.Location = new Point(12, 12);
            pbArt.Name = "pbArt";
            pbArt.Size = new Size(128, 128);
            pbArt.TabIndex = 2;
            pbArt.TabStop = false;
            pbArt.Paint += pbArt_Paint;
            pbArt.MouseDown += pbArt_MouseDown;
            pbArt.MouseMove += pbArt_MouseMove;
            pbArt.MouseUp += pbArt_MouseUp;
            // 
            // btnRender
            // 
            btnRender.Location = new Point(146, 12);
            btnRender.Name = "btnRender";
            btnRender.Size = new Size(75, 44);
            btnRender.TabIndex = 3;
            btnRender.Text = "Render Braille";
            btnRender.UseVisualStyleBackColor = true;
            btnRender.Click += btnRender_Click;
            // 
            // txbBraille
            // 
            txbBraille.BackColor = Color.White;
            txbBraille.BorderStyle = BorderStyle.FixedSingle;
            txbBraille.Location = new Point(12, 146);
            txbBraille.Multiline = true;
            txbBraille.Name = "txbBraille";
            txbBraille.ReadOnly = true;
            txbBraille.Size = new Size(250, 197);
            txbBraille.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(171, 76);
            label1.Name = "label1";
            label1.Size = new Size(216, 15);
            label1.TabIndex = 5;
            label1.Text = "it is recommended to use multiples of 2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(267, 110);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 6;
            label2.Text = "Braille size: 2x3 or 2x4";
            // 
            // cbBucketTool
            // 
            cbBucketTool.AutoSize = true;
            cbBucketTool.Location = new Point(146, 94);
            cbBucketTool.Name = "cbBucketTool";
            cbBucketTool.Size = new Size(62, 19);
            cbBucketTool.TabIndex = 7;
            cbBucketTool.Text = "Bucket";
            cbBucketTool.UseVisualStyleBackColor = true;
            // 
            // cbBlackFill
            // 
            cbBlackFill.AutoSize = true;
            cbBlackFill.Checked = true;
            cbBlackFill.CheckState = CheckState.Checked;
            cbBlackFill.Location = new Point(146, 121);
            cbBlackFill.Name = "cbBlackFill";
            cbBlackFill.Size = new Size(54, 19);
            cbBlackFill.TabIndex = 8;
            cbBlackFill.Text = "Black";
            cbBlackFill.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(320, 146);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 42);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save as PNG";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(320, 194);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 42);
            btnLoad.TabIndex = 10;
            btnLoad.Text = "Load from PNG";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 364);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(cbBlackFill);
            Controls.Add(cbBucketTool);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txbBraille);
            Controls.Add(btnRender);
            Controls.Add(pbArt);
            Name = "Form1";
            Text = "Form1";
            MouseUp += Form1_MouseUp;
            ((System.ComponentModel.ISupportInitialize)pbArt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pbArt;
        private Button btnRender;
        private TextBox txbBraille;
        private Label label1;
        private Label label2;
        private CheckBox cbBucketTool;
        private CheckBox cbBlackFill;
        private Button btnSave;
        private Button btnLoad;
    }
}