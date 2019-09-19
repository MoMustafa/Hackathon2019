using Alturos.Yolo;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tessnet2;

namespace DetectionProgram
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(AppContext.BaseDirectory);
        }

        private string CONFIG_FILE = "yolov2-tiny-voc.cfg";
        private string WEIGHTS_FILE = "yolov2-tiny-voc.weights";
        private string NAMES_FILE = "voc.names";
        private string COMPATIBLE_FILETYPES = "JPEG|*.jpg|PNG|*.png";

        private Button btnOCR;
        private Button btnOpen;
        private Button btnDetect;
        private Button btnSave;
        private PictureBox ImgBox;

        private List<Rectangle> DetectedObjects = new List<Rectangle>();

        private void BtnOpen_Click(object sender, EventArgs args)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = COMPATIBLE_FILETYPES })
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
            foreach (var detected_object in DetectedObjects)
            {
                Image obj = Crop(ImgBox.Image, detected_object);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = COMPATIBLE_FILETYPES })
                {
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        FileStream fs = (FileStream)saveFileDialog.OpenFile();

                        switch (saveFileDialog.FilterIndex)
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

        private void BtnOCR_Click(object sender, EventArgs args)
        {
            var ocr = new Tesseract();
            ocr.Init(@".\tessdata", "eng", false);

            using (OpenFileDialog fileDialog = new OpenFileDialog() { Filter = COMPATIBLE_FILETYPES })
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap img = (Bitmap) Image.FromFile(fileDialog.FileName);
                    ImgBox.Image = img;
                    var result = ocr.DoOCR(img, Rectangle.Empty);

                    foreach (var word in result)
                        MessageBox.Show(word.Text);
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
