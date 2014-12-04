using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VariantExporterWinGUI
{
    public partial class SplashScreen : Form
    {
        const int MF_BYPOSITION = 0x400;

        [DllImport("User32")]
        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]
        private static extern int GetMenuItemCount(IntPtr hWnd);

        private System.Threading.Thread thread = null;
        public SplashScreen()
        {
            InitializeComponent();

            IntPtr hMenu = GetSystemMenu(this.Handle, false);

            int menuItemCount = GetMenuItemCount(hMenu);

            RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
            
            animateProgressBar();
        }

        public new void Show()
        {
            if (thread == null)
            {
                thread = new System.Threading.Thread(ThreadProc);
                thread.Start();
            }
        }

        public void ThreadProc()
        {
            Application.Run(new SplashScreen());
        }

        public new void Close()
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        public void animateProgressBar()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 10;
        }

        public void SetLabel(string text)
        {
            label1.Text = text;
        }
    }
}
