using System.Windows.Forms;

namespace DetectionProgram
{
    public partial class MainWindow : Form
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
            this.ImgBox = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDetect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOCR = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgBox
            // 
            this.ImgBox.BackColor = System.Drawing.Color.White;
            this.ImgBox.Location = new System.Drawing.Point(12, 16);
            this.ImgBox.Name = "ImgBox";
            this.ImgBox.Size = new System.Drawing.Size(551, 370);
            this.ImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgBox.TabIndex = 2;
            this.ImgBox.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(574, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(214, 31);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Location = new System.Drawing.Point(574, 53);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(214, 31);
            this.btnDetect.TabIndex = 1;
            this.btnDetect.Text = "Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.BtnDetect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(574, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(214, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnOCR
            // 
            this.btnOCR.Location = new System.Drawing.Point(574, 127);
            this.btnOCR.Name = "btnOCR";
            this.btnOCR.Size = new System.Drawing.Size(213, 31);
            this.btnOCR.TabIndex = 4;
            this.btnOCR.Text = "OCR";
            this.btnOCR.UseVisualStyleBackColor = true;
            this.btnOCR.Click += new System.EventHandler(this.BtnOCR_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOCR);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ImgBox);
            this.Controls.Add(this.btnDetect);
            this.Controls.Add(this.btnOpen);
            this.Name = "MainWindow";
            this.Text = "Detection Program";
            ((System.ComponentModel.ISupportInitialize)(this.ImgBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

