using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binning.D2;


namespace Test
{
    public partial class Form1 : Form
    {
        public Bin bin = null;
        public Sku sku = null;
        public Binning1Sku binning;

        public Bitmap bmp = new Bitmap(650, 450);
        public Graphics gph;

        public Form1()
        {
            InitializeComponent();
            tb_bl.Text = "600";
            tb_bw.Text = "400";
            tb_bh.Text = "300";

            tb_sl.Text = "100";
            tb_sw.Text = "80";
            tb_sh.Text = "20";
            gph = Graphics.FromImage(bmp);

    }

        private void lb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int l, w, h = 0;
            l = int.Parse(tb_bl.Text);
            w = int.Parse(tb_bw.Text);
            h = int.Parse(tb_bh.Text);
            if (bin == null)
            {
                bin = new Bin(l, w, h);
            }
            else
            {
                bin.Cube.SetSize(l, w, h);
            }
            if (bin.Cube.IsValid())
            {
                lb1.Items.Add(DateTime.Now.ToString() + ": Create Bin Size Length " + bin.Cube.Length + ", Width " + bin.Cube.Width + ", Height " + bin.Cube.Height);
            }
            else
            {
                lb1.Items.Add(DateTime.Now.ToString() + ": Invalid Bin Data ");
            }

            l = int.Parse(tb_sl.Text);
            w = int.Parse(tb_sw.Text);
            h = int.Parse(tb_sh.Text);
            if (sku == null)
            {
                sku = new Sku(l, w, h);
            }
            else
            {
                sku.UpdateSize(l,w,h);
            }
            if (sku.Cube.IsValid())
            {
                lb1.Items.Add(DateTime.Now.ToString() + ": Create Sku Size Length " + sku.Cube.Length + ", Width " + sku.Cube.Width + ", Height " + sku.Cube.Height);
            }
            else
            {
                lb1.Items.Add(DateTime.Now.ToString() + ": Invalid Sku Data ");
            }

            gph.Clear(Color.White);
            Pen pen = new Pen(Color.Red,3);
            gph.DrawRectangle(pen, new Rectangle(0, 0, bin.Cube.Length, bin.Cube.Width));

            pb1.Image = bmp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            lb1.Items.Clear();

            int c1, c2;
            int direct;
            if (binning == null)
            {
                binning = new Binning1Sku(bin, sku);
            }
            binning.LinearFill((int)bin.Cube.Width, (int)sku.Cube.Width, (int)sku.Cube.Length, out c1, out c2);
            lb1.Items.Add(DateTime.Now.ToString() + ": Vertical: " + c1 + " / " + c2);

            binning.LinearFill((int)bin.Cube.Length, (int)sku.Cube.Length, (int)sku.Cube.Width, out c1, out c2);
            lb1.Items.Add(DateTime.Now.ToString() + ": Horizontol: " + c1 + " / " + c2);

            

            binning.GeneratePattern();

            lb1.Items.Add(DateTime.Now.ToString() + ": Waste Space: " + (bin.Cube.Length * bin.Cube.Width - sku.Cube.Length * sku.Cube.Width *binning.FillPattern.RectList.Count));

            for (int i = 0; i < binning.FillPattern.RectList.Count(); i++)
            {
                if (binning.FillPattern.RectList[i].Width == sku.Cube.Length)
                    direct = 0;
                else
                    direct = 1;
                lb1.Items.Add(DateTime.Now.ToString() + ": SKU No: "+i + " : position : " + 
                    binning.FillPattern.RectList[i].X +", "+ binning.FillPattern.RectList[i].Y+" , "+ direct);
            }

            Pen pen = new Pen(Color.Blue);
            gph.DrawRectangles(pen, binning.FillPattern.RectList.Values.ToArray<Rectangle>());
            pb1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lb1.Items.Clear();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
 
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            lb1.Width = this.Width - 40;
            lb1.Height = this.Height / 3 - 20;
            lb1.Left = 10;
            lb1.Top = this.Height / 3*2 - 20;
        }
    }
}
