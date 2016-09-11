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
   
    public struct point
    {
        public float x;
        public float y;
    }
    public partial class Form1 : Form
    {
        bool IsCorrectData = false;
        int countPens;
        int step;
        double corner;
        point[] points;
        Graphics gr;       //We declare an object - graphics, which will draw
        Pen[] p;            //We declare an object - a pencil, which will draw the contour
        Pen penFon;
        SolidBrush fon;    //We declare an object - fill, to fill the background respectively
        point[] startPoints;
        float firstXFactor = 0;
        float firstYFactor = 0;
        float secondXFactor = 0;
        float secondYFactor = 0;

        /**
         * Set Color for pen
         * 
         * @return void
         * */
        public void SetColorPen()
        {
            penFon = new Pen(Color.Black);
            p = new Pen[3];           
            p[0] = new Pen(Color.Lime);
            p[1] = new Pen(Color.Coral);
            p[2] = new Pen(Color.FloralWhite);
        }

        /**
         * Construct
         * 
         * @return void
         * */
        public Form1()
        {
            countPens = 0;
            InitializeComponent();
            step = 7;
            corner = 231;
            
            startPoints = new point[3];
            points = new point[3];
            SetColorPen();
            points = startPoints;
        }
        /**
         * Аngle calculation
         * 
         * @return double
         * */
        private double Corner(double cor)
        {
            return cor * (Math.PI / 180);
        }
        /**
         * Draw a triangle
         * 
         * @return double
         * */
        private void DrawTriangleFon()
        {
            
            gr.DrawLine(penFon, points[0].x, points[0].y, points[1].x, points[1].y);
            gr.DrawLine(penFon, points[0].x, points[0].y, points[2].x, points[2].y);
            gr.DrawLine(penFon, points[1].x, points[1].y, points[2].x, points[2].y);
        }
        /**
         * Draw a triangle by color fon
         * 
         * @return double
         * */
        private void DrawTriangle()
        {
            gr.DrawLine(p[countPens % 3], points[0].x, points[0].y, points[1].x, points[1].y);
            gr.DrawLine(p[countPens % 3], points[0].x, points[0].y, points[2].x, points[2].y);
            gr.DrawLine(p[countPens % 3], points[1].x, points[1].y, points[2].x, points[2].y);
        }
        /**
         * Moving the pieces
         * 
         * @return void
         * */
        private void Shift()
        {
            point[] tempPoints = points;
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

        /**
        * Button click
        * 
        * @return void
        * */
        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsRightData())
            {
                AnswerLabel.Text = "Неверные данные";
                IsCorrectData = false;
            }
            else
            {
                IsCorrectData = true;
                AnswerLabel.Text = "Данные корректны";
                firstXFactor = startPoints[0].x - startPoints[1].x;
                firstYFactor = startPoints[0].y - startPoints[1].y;
                secondXFactor = startPoints[0].x - startPoints[2].x;
                secondYFactor = startPoints[0].y - startPoints[2].y;

                gr = pictureBox1.CreateGraphics();  //initialize an object of type Graphics
                                                    // tied to a PictureBox

                fon = new SolidBrush(Color.Black); 
                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                timer1.Enabled = true;
            }


        }
        /**
        * Timer tick
        * 
        * @return void
        * */
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsCorrectData)
            {
                //сначала будем очищать область рисования цветом фона(1)
                //  gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);

                // очищение фона(2)
                // gr.Clear(Color.Black);

                // очищение рисованием цвета фона(3)
                DrawTriangleFon();

                Shift();
                DrawTriangle();
            };
        }
        /**
       * Check for correct points
       * 
       * @return bool
       * */
        private bool IsRightData()
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
