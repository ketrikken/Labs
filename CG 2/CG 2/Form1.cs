using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
namespace CG_2
{
    public partial class Form1 : Form
    {
        Graphics gr;       //We declare an object - graphics, which will draw
        Pen p = new Pen(Color.Lime);            //We declare an object - a pencil, which will draw the contour
        Pen penFon;
        SolidBrush fon;    //We declare an object - fill, to fill the background respectively
       

        public Form1()
        {
           
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();  //initialize an object of type Graphics
            fon = new SolidBrush(Color.Black);

            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);                                   // tied to a PictureBox
            gr.DrawLine(p, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            gr.DrawLine(p, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
            Point Start_Coor = new Point(0, 0);
            Point Point_Coor = new Point(Int32.Parse(X_Box.Text), Int32.Parse(Y_Box.Text));
            if ((Point_Coor.X < 0) && (Point_Coor.Y > Point_Coor.X) && (Point_Coor.Y < 0))
            {
                SetLine(Start_Coor, Point_Coor);
            }
            else
            {
                ans_line.Text = "Некорректные данные!";
            }
            //PutPixel(Point_Coor.X, Point_Coor.Y);
            
            
        }
            
        private void SetLine( Point start, Point finish)
        {
            int x = start.X;
            int y = start.Y;
            int dx = finish.X - start.X;
            int dy = finish.Y - start.Y;
            int D = -dx;
            int DX = dx >> 1;
            int DY = dy >> 1;
            while (x > finish.X)
            {
                PutPixel(GetX(x), GetY(y));
                --x;
                D += DY;
                if (D < 0)
                {
                    --y;
                    D -= DX;
                }
            }
        }
        private void PutPixel(int x,int y)
        {
            gr.FillRectangle(Brushes.Azure, x, y, 1, 1);
        }
        private int GetX(int X)
        {
         
            return X + pictureBox1.Width/2;
        }
        private int GetY(int Y)
        {
           
            return pictureBox1.Height/2-Y;
        }



    }
}
