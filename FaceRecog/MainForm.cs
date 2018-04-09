using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
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

namespace FaceRecog
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private CascadeClassifier haar = new CascadeClassifier(Environment.CurrentDirectory + "\\..\\..\\haarcascade_frontalface_alt_tree.xml");
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
                    timer.Tick += refreshFrame;
                }

                timer.Start();
                btnStart.Text = "Pause";

                isRunning = true;
            } else
            {
                timer.Stop();
                capture.Dispose();

                btnStart.Text = "Start";

                isRunning = false;
            }
        }

        private void refreshFrame(object sender, EventArgs e)
        {
            // Image<Bgr, byte> temp = null;

            using (Image<Bgr, byte> nextFrame = capture.QueryFrame().ToImage<Bgr, byte>())
            {
                if (nextFrame != null)
                {
                    // there's only one channel (greyscale), hence the zero index
                    // var faces = nextFrame.DetectHaarCascade(haar)[0];
                    ibCamera.Image = nextFrame;

                    var grayframe = nextFrame.Convert<Gray, byte>();
                    try
                    {
                        var faces = haar.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                        foreach (var face in faces)
                        {
                            nextFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
                        }



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }

            }


        }


    }
}

