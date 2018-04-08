using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public TCamDevice camDevice { set; get; }

        private void btnStart_Click(object sender, EventArgs e)
        {
            using (FormDevices deviceWd = new FormDevices())
            {
                deviceWd.ShowInTaskbar = false;
                deviceWd.ShowDialog();

                camDevice = deviceWd.selDevice;

                camDevice.ShowWindow(this.pbCamera);
            }
            
        }
    }
}
