using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CG4
{
    public partial class Form1 : Form
    {
        Graphics gr;
    //    Mouse m = new Mouse;
        Pen p = new Pen(Color.Lime);
        Pen ps = new Pen(Color.Aqua);
        SolidBrush fon;
       // Point[] Polygon = new Point[7];//многоугольник
        Point[] TemplateLine = new Point[2];//прямая
        List<Point> TemplatePolygon = new List<Point>(0);
        private void InitPolygon()//пока кривая инициализация
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\1\hello\Labs\CG4\CG4\Template.txt");
            int countTops = int.Parse(file.ReadLine());
            for ( int i=0;i<countTops;++i)
            {
                
                int x = int.Parse(file.ReadLine());
                int y = int.Parse(file.ReadLine());
                Point p = new Point(x, y);
                TemplatePolygon.Add(p);

            }
            /*
            Polygon[0].X = 50;
            Polygon[0].Y = 30;

            Polygon[1].X = 40;
            Polygon[1].Y = 70;

            Polygon[2].X = 70;
            Polygon[2].Y = 50;

            Polygon[3].X = 90;
            Polygon[3].Y = 100;

            Polygon[4].X = 100;
            Polygon[4].Y = 60;

            Polygon[5].X = 150;
            Polygon[5].Y = 90;

            Polygon[6].X = 110;
            Polygon[6].Y = 30;
            */
            TemplateLine[0].X = 40;
            TemplateLine[0].Y = 20;

            TemplateLine[1].X = 90;
            TemplateLine[1].Y = 20;
        }
        private void DrawPolygon(List<Point> Polygon)
        {
            for (int i = 0; i < Polygon.Count; ++i)
            {
                if (i == Polygon.Count - 1)
                {
                    gr.DrawLine(p, Polygon[i], Polygon[0]);
                }
                else
                    gr.DrawLine(p, Polygon[i], Polygon[i + 1]);
            }
        }
        
        private int GetDeterminant(Point[] param)//получить определитель матрицы 3 на 3 
        {
            int[,] matrix = new int[3, 3];
            for (int i=0;i<3;++i)
            {
                matrix[0, i] = param[i].X;
                matrix[1, i] = param[i].Y;
                matrix[2, i] = 1;
            }
            return matrix[0, 0] * matrix[1, 1] * matrix[2, 2] + matrix[2, 0] * matrix[0, 1] * matrix[1, 2] + matrix[1, 0] * matrix[2, 1] * matrix[0, 2] - matrix[2, 0] * matrix[1, 1] * matrix[0, 2] - matrix[0, 0] * matrix[2, 1] * matrix[1, 2] - matrix[1, 0] * matrix[0, 1] * matrix[2, 2];
        }
        private Point GetIntersectionPoint(Point pol1, Point pol2, Point l1, Point l2)//получить точку пересечения двух прямх
        {
            int A1 = pol1.Y - pol2.Y;
            int A2 = l1.Y - l2.Y;
            int B1 = pol2.X - pol1.X;
            int B2 = l2.X - l1.X;
            int C1 = pol1.X * pol2.Y - pol2.X * pol1.Y;
            int C2 = l1.X * l2.Y - l2.X * l1.Y;
            Point ret = new Point();
            ret.X = - ( (C1*B2 - C2*B1) / (A1*B2 - A2*B1) );
            ret.Y = - ( (A1*C2 - A2*C1) / (A1*B2 - A2*B1) );
            return ret;
        }
        public void DrawAmputationLine(Pen p, Point L1, Point L2, List<Point> Polygon)
        {

            if (L1.X > L2.X)
            {
                Point buf = L1;
                L1 = L2;
                L2 = buf;
            }
         

            List<long> determArray = new List<long>(0);//вектор определителей для каждой вершины относительно прямой
            for (int i=0;i<Polygon.Count;++i)
            {
                Point[] set_param = new Point[3] { Polygon[i], L1, L2 };
                determArray.Add(GetDeterminant(set_param));
            }

            int countZDeterm = 0;
            bool ChangeSign =false;
            List<Point> ZeroDetermPoint = new List<Point>(0);
            
            for (int i=0;i<determArray.Count;++i)
            {
                if (determArray[i] == 0)
                {
                    ++countZDeterm;
                    ZeroDetermPoint.Add(Polygon[i]);
                }
                else
                {
                    if (determArray[0] * determArray[i] < 0)
                    {
                        ChangeSign = true;
                        break;
                    }
                }
            }

            if(!ChangeSign && countZDeterm==1)
            {
                if (L1.X <= ZeroDetermPoint[0].X && ZeroDetermPoint[0].X <= L2.X)
                {
                    gr.DrawRectangle(ps, ZeroDetermPoint[0].X, ZeroDetermPoint[0].Y, 1, 1);
                }
            }
            if (!ChangeSign && countZDeterm == 2)
            {
                Point Start;
                Point Finish;
                if ( ZeroDetermPoint[0].X < ZeroDetermPoint[1].X)
                {
                    Start = ZeroDetermPoint[0];
                }
                else
                {
                    Start = ZeroDetermPoint[1];
                }
                if (Start.X < L1.X)
                {
                    Start = L1;
                }
                if (ZeroDetermPoint[0].X > ZeroDetermPoint[1].X)
                {
                    Finish = ZeroDetermPoint[0];
                }
                else
                {
                    Finish = ZeroDetermPoint[1];
                }
                if (Finish.X > L2.X)
                {
                    Finish = L2;
                }
                gr.DrawLine(ps, Start, Finish);
            }
            if (ChangeSign)
            {
                List<Point> IntersectionPointArray = new List<Point>(0);//точки пересечения прямой с гранями
                for (int i = 0; i < determArray.Count; ++i)
                {
                    int next = i + 1;
                    if (i == (determArray.Count - 1))
                    {
                        next = 0;
                    }
                    long d = determArray[i] * determArray[next];
                    if ((determArray[i] * determArray[next]) < 0)
                    {
                        Point IP = GetIntersectionPoint(Polygon[i], Polygon[next], L1, L2);
                        Point IPadd = new Point(0,0);
                        if ( L1.Y > L2.Y)
                        {
                            Point buf = L1;
                            L1 = L2;
                            L2 = buf;
                        }
                        if (IP.X < L1.X)
                        {
                            //IntersectionPointArray.Add(L1);
                           IPadd.X = L1.X;
                        }
                        else
                        {
                            if (IP.X > L2.X)
                            {
                               // IntersectionPointArray.Add(L2);
                                IPadd.X = L2.X;
                            }
                            else
                            {
                               //  IntersectionPointArray.Add(IP);
                                IPadd.X = IP.X;
                                
                            }
                        }
                        if (IP.Y < L1.Y)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.Y = L1.Y;
                        }
                        else
                        {
                            if (IP.Y > L2.Y)
                            {
                                //IntersectionPointArray.Add(L2);
                                IPadd.Y = L2.Y;
                            }
                            else
                            {
                                //IntersectionPointArray.Add(IP);
                                IPadd.Y = IP.Y;
                            }
                        }
                        IntersectionPointArray.Add(IPadd);

                    }
                    if (determArray[i] == 0)
                    {
                        Point IP = GetIntersectionPoint(Polygon[i], Polygon[next], L1, L2);
                        Point IPadd = new Point(0, 0);
                        if (L1.Y > L2.Y)
                        {
                            Point buf = L1;
                            L1 = L2;
                            L2 = buf;
                        }
                        if (IP.X < L1.X)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.X = L1.X;
                        }
                        else
                        {
                            if (IP.X > L2.X)
                            {
                                // IntersectionPointArray.Add(L2);
                                IPadd.X = L2.X;
                            }
                            else
                            {
                                //  IntersectionPointArray.Add(IP);
                                IPadd.X = IP.X;

                            }
                        }
                        if (IP.Y < L1.Y)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.Y = L1.Y;
                        }
                        else
                        {
                            if (IP.Y > L2.Y)
                            {
                                //IntersectionPointArray.Add(L2);
                                IPadd.Y = L2.Y;
                            }
                            else
                            {
                                //IntersectionPointArray.Add(IP);
                                IPadd.Y = IP.Y;
                            }
                        }

                        IntersectionPointArray.Add(IPadd);
                    }

                }

                for (int i = 0; i < IntersectionPointArray.Count - 1; i += 2)
                {
                    gr.DrawLine(p, IntersectionPointArray[i], IntersectionPointArray[i + 1]);
                }
            }
            
        }
        public Form1()
        {
            InitializeComponent();
            //InitPolygon();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AllEntered)
            {
                InitPolygon();
                Color pix = Color.Chocolate;
                gr = pictureBox1.CreateGraphics();
                fon = new SolidBrush(Color.Black);
                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                DrawPolygon(TemplatePolygon);
                DrawAmputationLine(p, TemplateLine[0], TemplateLine[1],TemplatePolygon);
            }
        }
       
        List<Point> Poll = new List<Point>(0);
        bool InLineFlag = false;
        bool AllEntered = false;
        int countPointForLine = 0;
        Point[] nLine = new Point[2];//прямая
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!AllEntered)
            {
               
                Point po = Cursor.Position;
                Point PB = pictureBox1.PointToScreen(new Point());
                Point POF = new Point(po.X - PB.X, po.Y - PB.Y);
                if (!InLineFlag)
                {
                    //   Point coor = Mouse.GetPosition(pictureBox1);

                    Poll.Add(POF);
                    gr.DrawRectangle(p, POF.X, POF.Y, 1, 1);
                }
                else
                {
                    gr.DrawRectangle(ps, POF.X, POF.Y, 1, 1);
                    nLine[countPointForLine] = POF;
                    ++countPointForLine;
                    if (countPointForLine == 2)

                    {
                        InLineFlag = false;
                       // gr.DrawLine(p, nLine[0], nLine[1]);
                        DrawAmputationLine(p, nLine[0], nLine[1], Poll);
                        AllEntered = true;

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!AllEntered)
            {
                Color pix = Color.Chocolate;
                gr = pictureBox1.CreateGraphics();
                fon = new SolidBrush(Color.Black);
                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                DrawPolygon(Poll);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();
            fon = new SolidBrush(Color.Black);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            Poll.Clear();
            AllEntered = false;
            countPointForLine = 0;
        }
     

        private void button3_Click(object sender, EventArgs e)
        {
            InLineFlag = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            AllEntered = false;
            countPointForLine = 0;
            DrawPolygon(Poll);

        }
    }
}
