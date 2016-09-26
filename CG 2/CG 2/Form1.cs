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
        Graphics gr;       
        Pen p = new Pen(Color.Lime);            
        SolidBrush fon;   

        public Form1()
        {
            InitializeComponent();
        }
        // рисуем осикоординат
        private void BackGround()
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();  
            fon = new SolidBrush(Color.Black);

            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);  
            gr.DrawLine(p, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
            gr.DrawLine(p, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);
        }
        // обрабатываем входные данные
        private void button1_Click(object sender, EventArgs e)
        {
            BackGround();
            Point Start_Coor = new Point(0, 0);
            Point Point_Coor = new Point(Int32.Parse(X_Box.Text), Int32.Parse(Y_Box.Text));
            int r = Int32.Parse(RBox.Text);
            if ((Point_Coor.X < 0) && (Point_Coor.Y >= Point_Coor.X) && (Point_Coor.Y < 0) && r >= 0)// проверка на корректность
            {
                label4.Text = "";
                SetLine(Start_Coor, Point_Coor);
                int x = Int32.Parse(sentreX_Box.Text);
                int y = Int32.Parse(sentreY_Box.Text);
                SetArc(r, x, y);
            }
            else
            {
                label4.Text = "Некорректные данные!";
            }
        }

        // равертка окружности по алгоритму Брезенхема 
        private void SetArc(int radius, int _x, int _y)
        {
            int x = -radius, y = 0, gap = 0, delta = (2 - 2 * radius);
            while (x <= 0 && x <= y)
            {
                PutPixel(GetX(_x + x), GetY(_y + y));
                PutPixel(GetX(_x + x), GetY(_y - y));
                PutPixel(GetX(_x - x), GetY(_y - y));
                PutPixel(GetX(_x - x), GetY(_y + y));
                PutPixel(GetX(_y + y), GetY(_x + x));
                PutPixel(GetX(_y - y), GetY(_x + x));
                PutPixel(GetX(_y - y), GetY(_x - x));
                PutPixel(GetX(_y + y), GetY(_x - x));

                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }

        }
        // развертка прямой
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
            gr.FillRectangle(Brushes.Aqua, x, y, 1, 1);
        }
        // вернуть координаты по x в пересчете на декартовую систему
        private int GetX(int X)
        {
            return X + pictureBox1.Width/2;
        }
        // вернкть координы по y
        private int GetY(int Y)
        {
            return pictureBox1.Height/2-Y;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
