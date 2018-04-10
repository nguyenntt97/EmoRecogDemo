using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DlibDotNet;

namespace FaceRecog
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private CascadeClassifier faceCsc = new CascadeClassifier(Environment.CurrentDirectory + "\\..\\..\\haarcascade_frontalface_alt_tree.xml");

        FrontalFaceDetector frontalFaceDetector = FrontalFaceDetector.GetFrontalFaceDetector();
        ShapePredictor predictor = new ShapePredictor(Services.Utils.getResourceURI("Resources/shape_predictor_68_face_landmarks.dat"));

        private const int FPS = 50;
        private bool isRunning = false;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();

            // Console.WriteLine(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\haarcascade_frontalface_alt_tree.xml"));
        }

        // public TCamDevice camDevice { set; get; }


        private void btnStart_Click(object sender, EventArgs e)
        {
            /*
            using (FormDevices deviceWd = new FormDevices())
            {
                deviceWd.ShowInTaskbar = false;
                deviceWd.ShowDialog();

                camDevice = deviceWd.selDevice;

                camDevice.ShowWindow(this.pbCamera);
            }
            */

            /*
            Thread refreshThread = new Thread(refreshFrame);
            refreshThread.Start();
            */
            if (!isRunning)
            {
                capture = new VideoCapture();

                if (timer == null)
                {
                    timer = new Timer();
                    timer.Interval = 1000 / FPS;
                    timer.Tick += RefreshFrame;
                }

                timer.Start();
                btnStart.Text = "Pause";

                isRunning = true;
            }
            else
            {
                timer.Stop();
                capture.Dispose();

                btnStart.Text = "Start";

                isRunning = false;
            }
        }

        private void RefreshFrame(object sender, EventArgs e)
        {
            // Image<Bgr, byte> temp = null;

            //using (Image<Bgr, byte> nextFrame = capture.QueryFrame().ToImage<Bgr, byte>())
            //{
            //    if (nextFrame != null)
            //    {
            //        // there's only one channel (greyscale), hence the zero index
            //        // var faces = nextFrame.DetectHaarCascade(haar)[0];
            //        ibCamera.Image = nextFrame;

            //        var grayframe = nextFrame.Convert<Gray, byte>();
            //        try
            //        {
            //            var faces = faceCsc.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
            //            foreach (var face in faces)
            //            {
            //                nextFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
            //            }



            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }

            //    }

            //}

            using (Image<Bgr, byte> curFrame = capture.QueryFrame().ToImage<Bgr, byte>())
            {
                Mat gray = null;
                // Convert multi-channel BGR image to gray
                CvInvoke.CvtColor(curFrame, gray, ColorConversion.Bgr2Gray);

                CvInvoke.EqualizeHist(gray, gray);

                var detects = frontalFaceDetector.Detect(gray);
            }
            
        }

        // ROI: Region of Interest
        private bool myDetector(InputArray image, out System.Drawing.Rectangle[] ROIs)
        {
            Mat gray = null;

            // Convert multi-channel BGR image to gray
            if (image.GetChannels() > 1)
            {
                CvInvoke.CvtColor(image.GetMat(), gray, ColorConversion.Bgr2Gray);
            }
            else
            {
                gray = image.GetMat().Clone();
            }

            CvInvoke.EqualizeHist(gray, gray);
            ROIs = faceCsc.DetectMultiScale(gray, 1.1, 3, Size.Empty);
            return true;
        }
        // public abstract void GetLandmarks();

    }
}

