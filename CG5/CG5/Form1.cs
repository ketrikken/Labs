using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG5
{
    public partial class Form1 : Form
    {
        Graphics gr;
        SolidBrush fon;
        Pen MainPolygonBrush = new Pen(Color.Gray);
        Pen InterseptPolygonBrush = new Pen(Color.GreenYellow);

        public struct TopPoint
        {
            public TopPoint(Point cr) { coor = cr;  IntersectionPoints = new List<Point>(0); IntersectMainTop = new List<int>(0); }
            public Point coor;
            public List<Point> IntersectionPoints;
            public List<int> IntersectMainTop;
        }
       /* public struct IntersctPoint
        {
            public IntersctPoint(Point cr) { coor = cr;  mainTop = -1; }
            public Point coor;
            public int mainTop;
        }*/
        List<TopPoint> MainPolygon = new List<TopPoint>(0);
        List<TopPoint> InterceptPolygon = new List<TopPoint>(0);

        int InPutMode = 0;
        /*****************************************************************************/

        private int GetDeterminant(Point[] param)//получить определитель матрицы 3 на 3 
        {
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < 3; ++i)
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
            ret.X = -((C1 * B2 - C2 * B1) / (A1 * B2 - A2 * B1));
            ret.Y = -((A1 * C2 - A2 * C1) / (A1 * B2 - A2 * B1));
            return ret;
        }
        public void GetAmputationLine( TopPoint L1, TopPoint L2, List<TopPoint> Polygon,int z)
        {
            bool swapDetector = false;
            List<long> determArray = new List<long>(0);//вектор определителей для каждой вершины относительно прямой
            for (int i = 0; i < Polygon.Count; ++i)
            {
                Point[] set_param = new Point[3] { Polygon[i].coor, L1.coor, L2.coor };
                determArray.Add(GetDeterminant(set_param));
            }

            int countZDeterm = 0;
            bool ChangeSign = false;
            List<Point> ZeroDetermPoint = new List<Point>(0);

            for (int i = 0; i < determArray.Count; ++i)
            {
                if (determArray[i] == 0)
                {
                    ++countZDeterm;
                    ZeroDetermPoint.Add(Polygon[i].coor);
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

            if (!ChangeSign && countZDeterm == 1)
            {
                if (L1.coor.X <= ZeroDetermPoint[0].X && ZeroDetermPoint[0].X <= L2.coor.X)
                {
                     gr.DrawRectangle(InterseptPolygonBrush, ZeroDetermPoint[0].X, ZeroDetermPoint[0].Y, 1, 1);
                   // Polygon[i].IntersectionPoints.Add(ZeroDetermPoint[0]);
                    //L1.IntersectionPoints.Add(IPadd);

                }
            }
            if (!ChangeSign && countZDeterm == 2)
            {
                Point Start;
                Point Finish;
                if (ZeroDetermPoint[0].X < ZeroDetermPoint[1].X)
                {
                    Start = ZeroDetermPoint[0];
                }
                else
                {
                    Start = ZeroDetermPoint[1];
                }
                if (Start.X < L1.coor.X)
                {
                    Start = L1.coor;
                }
                if (ZeroDetermPoint[0].X > ZeroDetermPoint[1].X)
                {
                    Finish = ZeroDetermPoint[0];
                }
                else
                {
                    Finish = ZeroDetermPoint[1];
                }
                if (Finish.X > L2.coor.X)
                {
                    Finish = L2.coor;
                }
                gr.DrawLine(InterseptPolygonBrush, Start, Finish);
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
                        Point IP = GetIntersectionPoint(Polygon[i].coor, Polygon[next].coor, L1.coor, L2.coor);
                        Point IPadd = new Point(0, 0);
                        if (L1.coor.X > L2.coor.X)
                        {
                            Point buf = L1.coor;
                            L1 = L2;
                            L2.coor = buf;
                            swapDetector = true;
                        }
                        if (IP.X < L1.coor.X)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.X = L1.coor.X;
                        }
                        else
                        {
                            if (IP.X > L2.coor.X)
                            {
                                // IntersectionPointArray.Add(L2);
                                IPadd.X = L2.coor.X;
                            }
                            else
                            {
                                //  IntersectionPointArray.Add(IP);
                                IPadd.X = IP.X;

                            }
                        }
                        if (L1.coor.Y > L2.coor.Y)
                        {
                            Point buf = L1.coor;
                            L1 = L2;
                            L2.coor = buf;
                            swapDetector = true;
                        }
                        if (IP.Y < L1.coor.Y)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.Y = L1.coor.Y;
                        }
                        else
                        {
                            if (IP.Y > L2.coor.Y)
                            {
                                //IntersectionPointArray.Add(L2);
                                IPadd.Y = L2.coor.Y;
                            }
                            else
                            {
                                //IntersectionPointArray.Add(IP);
                                IPadd.Y = IP.Y;
                            }
                        }
                        IntersectionPointArray.Add(IPadd);
                        //Polygon[i].IntersectionPoints.Add(IPadd);
                        InterceptPolygon[z].IntersectionPoints.Add(IPadd);
                        if (IPadd != L1.coor && IPadd != L2.coor)
                        {
                            InterceptPolygon[z].IntersectMainTop.Add(i);
                        }
                        else
                            InterceptPolygon[z].IntersectMainTop.Add(-1);
                            
                        //gr.DrawRectangle(InterseptPolygonBrush, IPadd.X, IPadd.Y, 1, 1);
                    }
                    if (determArray[i] == 0)
                    {
                        Point IP = GetIntersectionPoint(Polygon[i].coor, Polygon[next].coor, L1.coor, L2.coor);
                        Point IPadd = new Point(0, 0);
                        if (L1.coor.Y > L2.coor.Y)
                        {
                            Point buf = L1.coor;
                            L1 = L2;
                            L2.coor = buf;
                        }
                        if (IP.X < L1.coor.X)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.X = L1.coor.X;
                        }
                        else
                        {
                            if (IP.X > L2.coor.X)
                            {
                                // IntersectionPointArray.Add(L2);
                                IPadd.X = L2.coor.X;
                            }
                            else
                            {
                                //  IntersectionPointArray.Add(IP);
                                IPadd.X = IP.X;

                            }
                        }
                        if (IP.Y < L1.coor.Y)
                        {
                            //IntersectionPointArray.Add(L1);
                            IPadd.Y = L1.coor.Y;
                        }
                        else
                        {
                            if (IP.Y > L2.coor.Y)
                            {
                                //IntersectionPointArray.Add(L2);
                                IPadd.Y = L2.coor.Y;
                            }
                            else
                            {
                                //IntersectionPointArray.Add(IP);
                                IPadd.Y = IP.Y;
                            }
                        }

                        IntersectionPointArray.Add(IPadd);
                       /* Polygon[i].IntersectionPoints.Add(IPadd);
                        L1.IntersectionPoints.Add(IPadd);*/
                        //gr.DrawRectangle(InterseptPolygonBrush, IPadd.X, IPadd.Y, 1, 1);
                    }

                }

                for (int j = 1; j < IntersectionPointArray.Count; ++j)
                {
                    Point key = IntersectionPointArray[j];
                    int i = j - 1;
                    while ((i >= 0) && ((IntersectionPointArray[i].X > key.X) || (IntersectionPointArray[i].X == key.X && IntersectionPointArray[i].Y > key.Y)))
                    {
                        IntersectionPointArray[i + 1] = IntersectionPointArray[i];
                        --i;
                    }
                    IntersectionPointArray[i + 1] = key;
                }
                //
                for (int i = 0; i < IntersectionPointArray.Count - 1; i += 2)
                {
                    gr.DrawLine(InterseptPolygonBrush, IntersectionPointArray[i], IntersectionPointArray[i + 1]);
                }
            }

        }/****************************************************************************/
        private void PolygonsProcessing()
        {
            for(int i=0;i<InterceptPolygon.Count;++i)
            {
                int next = i + 1;
                if (next == InterceptPolygon.Count)
                    next = 0;
                GetAmputationLine(InterceptPolygon[i], InterceptPolygon[next], MainPolygon,i);
            }
        }

        private void DrawPolygon(List<TopPoint> Polygon,Pen p)
        {
            for (int i = 0; i < Polygon.Count; ++i)
            {
                if (i == Polygon.Count - 1)
                {
                    
                    gr.DrawLine(p, Polygon[i].coor, Polygon[0].coor);
                }
                else
                    gr.DrawLine(p, Polygon[i].coor, Polygon[i + 1].coor);
            }
        }

       /* List<List<Point>> InPoll = new List<List<Point>>(0);
        void Obhod()
        {
            List<Point> pol = new List<Point>(0);
            
            Point next = new Point(-1, -1);
            Point start = InterceptPolygon[0].IntersectionPoints[0];

            while (next!= start)
            {

            }
        }*/
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InPutMode == 0)
            {
                gr = pictureBox1.CreateGraphics();
                fon = new SolidBrush(Color.Black);
                gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
                InPutMode = 1;
            }
            else
            {
                DrawPolygon(MainPolygon, MainPolygonBrush);
                InPutMode = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InPutMode == 2)
            {
               // DrawPolygon(InterceptPolygon, InterseptPolygonBrush);
                PolygonsProcessing();
                InPutMode = 0;
            }
            else
                InPutMode = 2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point po = Cursor.Position;
            Point PB = pictureBox1.PointToScreen(new Point());
            Point POF = new Point(po.X - PB.X, po.Y - PB.Y);
            TopPoint tp = new TopPoint(POF);
            if(InPutMode == 1)
            {
                
                MainPolygon.Add(tp);
                gr.DrawRectangle(MainPolygonBrush, POF.X, POF.Y, 1, 1);
            }
            if (InPutMode == 2)
            {
                InterceptPolygon.Add(tp);
                gr.DrawRectangle(InterseptPolygonBrush, POF.X, POF.Y, 1, 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();
            fon = new SolidBrush(Color.Black);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            MainPolygon.Clear();
            InterceptPolygon.Clear();
            InPutMode = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();
            fon = new SolidBrush(Color.Black);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            DrawPolygon(MainPolygon, MainPolygonBrush);
            InterceptPolygon.Clear();
            InPutMode = 0;
        }
    }





}
