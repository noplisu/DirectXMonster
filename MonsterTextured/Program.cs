using System;
using System.Windows.Forms;

namespace MonsterTextured
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Form1 frm = new Form1())
            {
                if (!frm.InitializeGraphics())
                {
                    MessageBox.Show("Could not initialize Direct3D.  This tutorial will exit.");
                    return;
                }
                frm.Show();
                while (frm.Created)
                {
                    frm.Render();
                    Application.DoEvents();
                }
            }
        }
    }
}
