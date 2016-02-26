using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MonsterTextured
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Form1 frm = new Form1())
            {
                if (!frm.InitializeDirect3D())
                {
                    MessageBox.Show("Error initizlizing Direct3D");
                    return;
                }
                frm.Show();
                frm.Run();
            }
        }
    }
}
