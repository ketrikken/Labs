using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Pen p = new Pen(Color.Lime);
        SolidBrush fon;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();
            fon = new SolidBrush(Color.Black);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            string word;
            word = textBox1.Text;
            int y = Int32.Parse(Pointer_X.Text);
            int x = Int32.Parse(Pointer_Y.Text);
            for (int i=0;i<word.Length;++i)
            {
                if (word[i] == 'A')
                {
                    PrintA(x, y);
                }
                if (word[i] == 'B')
                {
                    PrintB(x, y);
                }
                x += 8;
            }
        }    
        private void PrintA(int x,int y)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\A.txt");
            

            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);

                for (int i = 0; i < line.Length; ++i)
                {
                    if (line[i] == '1')
                    {
                        PutPixel(x+i, y);

                    }
                }
                ++y;
            }
        }
        public void PrintB(int x, int y)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\B.txt");


            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);

                for (int i = 0; i < line.Length; ++i)
                {
                    if (line[i] == '1')
                    {
                        PutPixel(x+i, y);

                    }
                }
                ++y;
            }
        }
        private void PutPixel(int x, int y)
        {
            gr.FillRectangle(Brushes.Aqua, x, y, 1, 1);
        }
    }
}
