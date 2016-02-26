using System;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MonsterTextured
{
    public partial class Form1 : Form
    {
        Device device = null;
        VertexBuffer vertexBuffer = null;

        public Form1()
        {
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "D3D Tutorial 01: CreateDevice";
        }

        public bool InitializeGraphics()
        {
            try
            {
                PresentParameters presentParams = new PresentParameters();
                presentParams.Windowed = true;
                presentParams.SwapEffect = SwapEffect.Discard;
                device = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, presentParams);
                OnCreateDevice(device, null);
                return true;
            }
            catch (DirectXException)
            {
                return false;
            }
        }

        public void Render()
        {
            if (device == null)
                return;

            device.Clear(ClearFlags.Target, System.Drawing.Color.Blue, 1.0f, 0);
            device.BeginScene();
            SetupMatrices();

            device.SetStreamSource(0, vertexBuffer, 0);
            device.VertexFormat = CustomVertex.TransformedColored.Format;
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

            device.EndScene();
            device.Present();
        }

        public void OnCreateDevice(object sender, EventArgs e)
        {
            Device dev = (Device)sender;
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.TransformedColored), 3, dev, 0, CustomVertex.TransformedColored.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.OnCreateVertexBuffer);
            this.OnCreateVertexBuffer(vertexBuffer, null);
        }

        public void OnCreateVertexBuffer(object sender, EventArgs e)
        {
            VertexBuffer vb = (VertexBuffer)sender;
            GraphicsStream stm = vb.Lock(0, 0, 0);
            CustomVertex.TransformedColored[] verts = new CustomVertex.TransformedColored[3];

            verts[0].X = 150; verts[0].Y = 50; verts[0].Z = 0.5f; verts[0].Rhw = 1;
            verts[0].Color = System.Drawing.Color.Aqua.ToArgb();

            verts[1].X = 250; verts[1].Y = 250; verts[1].Z = 0.5f; verts[1].Rhw = 1;
            verts[1].Color = System.Drawing.Color.Brown.ToArgb();

            verts[2].X = 50; verts[2].Y = 250; verts[2].Z = 0.5f; verts[2].Rhw = 1;
            verts[2].Color = System.Drawing.Color.LightPink.ToArgb();

            stm.Write(verts);
            vb.Unlock();
        }

        private void SetupMatrices()
        {
            // For our world matrix, we will just rotate the object about the y-axis.

            // Set up the rotation matrix to generate 1 full rotation (2*PI radians) 
            // every 1000 ms. To avoid the loss of precision inherent in very high 
            // floating point numbers, the system time is modulated by the rotation 
            // period before conversion to a radian angle.
            int iTime = Environment.TickCount % 1000;
            float fAngle = iTime * (2.0f * (float)Math.PI) / 1000.0f;
            device.Transform.World = Matrix.RotationY(fAngle);

            // Set up our view matrix. A view matrix can be defined given an eye 
            // point, a point to lookat, and a direction for which way is up. Here, 
            // we set the eye five units back along the z-axis and up three units, 
            // look at the origin, and define "up" to be in the y-direction.

            device.Transform.View = Matrix.LookAtLH(
                new Vector3(0.0f, 3.0f, -5.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f));

            // For the projection matrix, we set up a perspective transform (which
            // transforms geometry from 3D view space to 2D viewport space, with
            // a perspective divide making objects smaller in the distance). To build
            // a perpsective transform, we need the field of view (1/4 pi is common),
            // the aspect ratio, and the near and far clipping planes (which define
            // at what distances geometry should be no longer be rendered).
            device.Transform.Projection = Matrix.PerspectiveFovLH(
                (float)Math.PI / 4,
                1.0f,
                1.0f,
                100.0f);
        }

        public void OnResetDevice(object sender, EventArgs e)
        {
            Device dev = (Device)sender;
            // Turn off culling, so the user sees the front and back of the triangle
            dev.RenderState.CullMode = Cull.None;
            // Turn off Direct3D lighting, since object provides its own vertex colors
            dev.RenderState.Lighting = false;
        }
    }
}
