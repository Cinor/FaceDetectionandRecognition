using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace FaceDetectionandRecognition
{
    public partial class Form1 : Form
    {
        private Capture _capture;
        private CascadeClassifier _cascadeClassifier;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_default.xml");
        }

        void Application_Idle(object sender, EventArgs e)
        {
            Image<Bgr, Byte> frame = _capture.QueryFrame().ToImage<Bgr, Byte>();
            pictureBox1.Image = frame.ToBitmap();
        }


        private void Facedetection(object sender, EventArgs e)
        {
            using (var imageFrame = _capture.QueryFrame().ToImage<Bgr, Byte>())
            {
                if (imageFrame != null)
                {
                    var grayframe = imageFrame.Convert<Gray, byte>();
                    var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    foreach (var face in faces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }
                pictureBox1.Image = imageFrame.ToBitmap();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _capture = new Capture();
            Application.Idle += new EventHandler(Application_Idle);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            _capture = new Capture();
            Application.Idle += new EventHandler(Facedetection);
        }
    }
}
