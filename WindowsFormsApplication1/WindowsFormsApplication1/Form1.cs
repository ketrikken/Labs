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
    
    public struct popo
    {
        public float x;
        public float y;
    }
    public partial class Form1 : Form
    {
        bool IsCorrectData = false;
        int countPens;
        string[] color;
        int step;
        double corner;
        popo[] points;
        Graphics gr;       //объявляем объект - графику, на которой будем рисовать
        Pen[] p;             //объявляем объект - карандаш, которым будем рисовать контур
        SolidBrush fon;    //объявляем объект - заливки, для заливки соответственно фона
        SolidBrush fig;    //и внутренности рисуемой фигуры
        popo[] startPoints;
        Random rand;      // объект, для получения случайных чисел
        ///новые изменения, комит от 10,09,2016 @Миша
        float firstXFactor = 0;
        float firstYFactor = 0;
        float secondXFactor = 0;
        float secondYFactor = 0;

        //
        public Form1()
        {
            countPens = 0;
            InitializeComponent();
            step = 10;
            corner = 15;
            
            startPoints = new popo[3];
          /*  startPoints[0].x = 0;
            startPoints[0].y = 0;
            startPoints[1].x = 0;
            startPoints[1].y = -30;
            startPoints[2].x = 40;
            startPoints[2].y = 0;*/
            points = new popo[3];
            points = startPoints;
        }
        private double Corner(double aa)
        {
            return aa * (Math.PI / 180);
        }
        private void DrawRectangle()
        {
            gr.DrawLine(p[countPens%3], points[0].x, points[0].y, points[1].x, points[1].y);
            gr.DrawLine(p[countPens%3], points[0].x, points[0].y, points[2].x, points[2].y);
            gr.DrawLine(p[countPens%3], points[1].x, points[1].y, points[2].x, points[2].y);
        }
        private void Shift()
        {
            popo[] tempPoints = points;
            int tempStep = step;
            bool flag = true;

            while (flag == true)
            {
                points[0].x = startPoints[0].x + step;
                points[0].y = (startPoints[0].y + (float)Math.Tan(Corner(corner)) * step);
                points[1].x = points[0].x - firstXFactor;
                points[1].y = points[0].y - firstYFactor;
                points[2].x = points[0].x - secondXFactor;
                points[2].y = points[0].y - secondYFactor;
                flag = false;
                for (int i = 0; i < 3; ++i)
                {

                    if (points[i].x >= ClientSize.Width || points[i].x <= 0)
                    {
                        countPens = ++countPens % 3;
                        step *= (-1);
                        corner *= (-1);
                        flag = true;
                        break;
                    }
                    if (points[i].y >= ClientSize.Height - 92 || points[i].y <= 0)
                    {
                        countPens = ++countPens % 3;
                        corner *= (-1);
                        flag = true;
                        break;
                    }

                }
            }

            startPoints = tempPoints;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsRightData())
            {
                AnswerLabel.Text = "EROR!Again!";
                IsCorrectData = false;
            }
            else
            {
                IsCorrectData = true;
                AnswerLabel.Text = "OK";
               // startPoints = new popo[3];

                /*startPoints[0].x = Convert.ToInt32(textBox1.Text);
                startPoints[0].y = int.Parse(textBox2.Text);
                startPoints[1].x = int.Parse(textBox3.Text);
                startPoints[1].y = int.Parse(textBox4.Text);
                startPoints[2].x = int.Parse(textBox5.Text);
                startPoints[2].y = int.Parse(textBox6.Text);*/
                firstXFactor = startPoints[0].x - startPoints[1].x;
                firstYFactor = startPoints[0].y - startPoints[1].y;
                secondXFactor = startPoints[0].x - startPoints[2].x;
                secondYFactor = startPoints[0].y - startPoints[2].y;

                gr = pictureBox1.CreateGraphics();  //инициализируем объект типа графики
                                                    // привязав  к PictureBox

                p = new Pen[3];           // задали цвет для карандаша
                p[0] = new Pen(Color.Green);
                p[1] = new Pen(Color.Coral);
                p[2] = new Pen(Color.FloralWhite);

                fon = new SolidBrush(Color.Black); // и для заливки
                fig = new SolidBrush(Color.Purple);

                rand = new Random();               //инициализируем объект для рандомных числе

                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                timer1.Enabled = true;
            }


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsCorrectData)
            {
                //сначала будем очищать область рисования цветом фона
                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                Shift();
                DrawRectangle();
            };

        }

        ///новые изменения, комит от 10,09,2016 @Миша
        bool IsRightData()
        {
            int i = 0;
            int j = 1;
            foreach (TextBox c in Controls.OfType<TextBox>())
            {
                int result;
                if (String.IsNullOrEmpty(c.Text) || !Int32.TryParse(c.Text, out result))
                {

                    return false;
                }
                else
                {
                    if (j % 2 == 1)
                    {
                        
                        if (Int32.Parse(c.Text) < -9 || Int32.Parse(c.Text) >= 501)
                            return false;
                        else
                            startPoints[i].x = Int32.Parse(c.Text);
                        
                    }
                    else
                    {
                        if (Int32.Parse(c.Text) < -2 || Int32.Parse(c.Text) >= 250)
                            return false;
                        else
                            startPoints[i].y = Int32.Parse(c.Text);
                        ++i;
                    }
                    ++j;
                }
            }
            return true;
        }
    }
}
