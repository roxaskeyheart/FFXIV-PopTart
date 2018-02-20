using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIV_PopTart
{
    public partial class Notify : Form
    {
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        public Notify()
        {
            InitializeComponent();
            SetForegroundWindow(Handle.ToInt32());
            Left = -16384;
            Activate();
        }
        
        private void btn_close_Click(object sender, EventArgs e)
        {
            Left = -16384;
            PopTart._notifyOpen = false;
        }

        public void UpdateInstanceName(string instance)
        {
            rtb_instance.Text = instance;
        }
    }
}
