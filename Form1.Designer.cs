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
            txbWidth = new TextBox();
            txbHeight = new TextBox();
            pbArt = new PictureBox();
            btnRender = new Button();
            txbBraille = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbArt).BeginInit();
            SuspendLayout();
            // 
            // txbWidth
            // 
            txbWidth.Location = new Point(287, 12);
            txbWidth.Name = "txbWidth";
            txbWidth.Size = new Size(100, 23);
            txbWidth.TabIndex = 0;
            // 
            // txbHeight
            // 
            txbHeight.Location = new Point(287, 41);
            txbHeight.Name = "txbHeight";
            txbHeight.Size = new Size(100, 23);
            txbHeight.TabIndex = 1;
            // 
            // pbArt
            // 
            pbArt.BackgroundImageLayout = ImageLayout.Zoom;
            pbArt.Location = new Point(12, 12);
            pbArt.Name = "pbArt";
            pbArt.Size = new Size(128, 128);
            pbArt.TabIndex = 2;
            pbArt.TabStop = false;
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
            txbBraille.Location = new Point(12, 146);
            txbBraille.Multiline = true;
            txbBraille.Name = "txbBraille";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 364);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txbBraille);
            Controls.Add(btnRender);
            Controls.Add(pbArt);
            Controls.Add(txbHeight);
            Controls.Add(txbWidth);
            Name = "Form1";
            Text = "Form1";
            MouseUp += Form1_MouseUp;
            ((System.ComponentModel.ISupportInitialize)pbArt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txbWidth;
        private TextBox txbHeight;
        private PictureBox pbArt;
        private Button btnRender;
        private TextBox txbBraille;
        private Label label1;
        private Label label2;
    }
}