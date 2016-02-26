using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MonsterTextured
{
    public partial class Form1 : Form
    {
        Device device = null;

        public Form1()
        {
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "D3D Tutorial 01: CreateDevice";
        }

        public bool InitializeDirect3D()
        {
            PresentParameters pp = new PresentParameters();

            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            try
            {
                device = new Device(0,
                        DeviceType.Hardware,
                        this,
                        CreateFlags.SoftwareVertexProcessing,
                        pp);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Run()
        {
            while (this.Created)
            {
                GameLogic();
                Render();
                Application.DoEvents();
            }
        }

        private void GameLogic()
        {
            // TODO: add your game logic here
        }

        private void Render()
        {
            device.Clear(ClearFlags.Target,
                        Color.Blue, 1.0f, 0);
            device.Present();
        }

    }
}
