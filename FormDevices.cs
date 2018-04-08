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
    public partial class FormDevices : Form
    {
        private TCamDevice[] deviceList = null;
        public TCamDevice selDevice { get; set; }

        public FormDevices()
        {
            InitializeComponent();

            listView1.View = View.List;

            deviceList = DeviceManager.GetAllDevices();
            if (deviceList.Length == 0)
            {
                MessageBox.Show("No compatible device found on your platform!");

            }
            foreach (TCamDevice device in deviceList)
            {
                this.listView1.Items.Add(device.Name);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listView1.SelectedIndices;
            if (indices.Count > 0)
            {
                selDevice = deviceList[indices[0]];
                Close();
            } else
            {
                MessageBox.Show("Please select a device");
            }
        }
    }
}
