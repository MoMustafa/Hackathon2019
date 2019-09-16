using Alturos.Yolo;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
            ((System.ComponentModel.ISupportInitialize)(this.ImgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgBox
            // 
            this.ImgBox.BackColor = System.Drawing.Color.White;
            this.ImgBox.Location = new System.Drawing.Point(12, 16);
            this.ImgBox.Name = "ImgBox";
            this.ImgBox.Size = new System.Drawing.Size(551, 370);
            this.ImgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
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
            this.btnDetect.Location = new System.Drawing.Point(574, 68);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(214, 31);
            this.btnDetect.TabIndex = 1;
            this.btnDetect.Text = "Detect";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.BtnDetect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(574, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(214, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.PictureBox ImgBox;
        private Button btnSave;

        private string CONFIG_FILE = "yolov2-tiny-voc.cfg";
        private string WEIGHTS_FILE = "yolov2-tiny-voc.weights";
        private string NAMES_FILE = "voc.names";
        private string COMPATIBLE_FILETYPES = "JPEG|*.jpg|PNG|*.png";

        private List<Rectangle> DetectedObjects = new List<Rectangle>();

        private void BtnOpen_Click(object sender, EventArgs args)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = COMPATIBLE_FILETYPES})
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImgBox.Image = Image.FromFile(fileDialog.FileName);
                }
            }
        }

        private void BtnDetect_Click(object sender, EventArgs args)
        {
            using (var yoloWrapper = new YoloWrapper(CONFIG_FILE, WEIGHTS_FILE, NAMES_FILE))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ImgBox.Image.Save(ms, ImageFormat.Png);
                    var items = yoloWrapper.Detect(ms.ToArray()).ToList();
                    DetectedObjects = DetectObjects(ImgBox, items);
                }
                
            }
        }

        private void BtnSave_Click(object sender, EventArgs args)
        {
            foreach(var detected_object in DetectedObjects)
            {
                Image obj = Crop(ImgBox.Image, detected_object);

                using(SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = COMPATIBLE_FILETYPES})
                {
                    saveFileDialog.ShowDialog();

                    if(saveFileDialog.FileName != "")
                    {
                        FileStream fs = (FileStream)saveFileDialog.OpenFile();

                        switch(saveFileDialog.FilterIndex)
                        {
                            case 1:
                                obj.Save(fs, ImageFormat.Jpeg);
                                break;
                            case 2:
                                obj.Save(fs, ImageFormat.Png);
                                break;
                        }

                        fs.Close();
                    }
                }
            }
        }

        private List<Rectangle> DetectObjects(PictureBox ImgToExamine, List<YoloItem> items)
        {
            var img = ImgToExamine.Image;
            var graphics = Graphics.FromImage(img);

            List<Rectangle> BoundingBoxes = new List<Rectangle>();

            foreach (var item in items)
            {
                var rect = new Rectangle(item.X, item.Y, item.Width, item.Height);
                var pen = new Pen(Color.Red, 3);

                graphics.DrawRectangle(pen, rect);

                BoundingBoxes.Add(rect);


            }

            ImgToExamine.Image = img;
            return BoundingBoxes;
        }

        private Image Crop(Image img, Rectangle selection)
        {
            Bitmap obj = img as Bitmap;

            return obj.Clone(selection, obj.PixelFormat);
        }
    }
}

