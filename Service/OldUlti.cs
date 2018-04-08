using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections;

namespace FaceRecog
{
    public class DeviceManager
    {
        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescription(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
            int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        static ArrayList devices = new ArrayList();

        public static TCamDevice[] GetAllDevices()
        {
            String dName = "".PadRight(100);
            String dVer = "".PadRight(100);

            for (short i = 0; i < 10; i++)
            {
                if (capGetDriverDescription(i, ref dName, 100, ref dVer, 100))
                {
                    TCamDevice d = new TCamDevice(i);
                    d.Name = dName.Trim();
                    d.Version = dVer.Trim();

                    devices.Add(d);
                }
            }
            return (TCamDevice[])devices.ToArray(typeof(TCamDevice));
        }

        public static TCamDevice GetDevice(int deviceIndex)
        {
            return (TCamDevice)devices[deviceIndex];
        }
    }



    public class TCamDevice
    {
        private const short WM_CAP = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;

        
        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindow([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        int index;
        int deviceHandle;

        public TCamDevice(int index)
        {
            this.index = index;
        }

        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        private string _version;

        public string Version {
            get { return _version; }
            set { _version = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Init(int windowHeight, int windowWidth, int handle)
        {
            string deviceIndex = Convert.ToString(this.index);
            deviceHandle = capCreateCaptureWindow(ref deviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, windowWidth, windowHeight, handle, 0);

            if (SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, this.index, 0) > 0)
            {
                SendMessage(deviceHandle, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, 0x42, 0);
                SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, -1, 0);

                SetWindowPos(deviceHandle, 1, 0, 0, windowWidth, windowHeight, 6);
            }
        }

        public void ShowWindow(global::System.Windows.Forms.Control windowsControl)
        {
            Init(windowsControl.Height, windowsControl.Width, windowsControl.Handle.ToInt32());
        }

        /// <summary>
        /// Stop the webcam and destroy the handle
        /// </summary>
        public void Stop()
        {
            SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, this.index, 0);

            DestroyWindow(deviceHandle);
        }

    }

    class Ulti
    {

    }
}
