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
        string line;
        char[][,] BitMapFontArray = new char[5][,];
        Dictionary<char, List<Point>> myMap = new Dictionary<char, List<Point>>();
        public void BitMapFontInit()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Алатиэль\Desktop\cg\Labs\CG3\CG3\BitmapFont.txt");
            for (int i = 0; i < BitMapFontArray.Length; ++i)
            {
                BitMapFontArray[i] = new char[8,8];
                for (int j = 0; j < 8; ++j)
                {
                    line = file.ReadLine();
                    System.Console.WriteLine(line);
                    for (int z = 0; z < 8; ++z)
                    {
                        BitMapFontArray[i][j, z] = line[z];
                    }
                }
            }
        }
        
        private void VectorPointsChars()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Алатиэль\Desktop\cg\Labs\CG3\CG3\points.txt");
            while( !file.EndOfStream )
            {
                line = file.ReadLine();
                System.Console.WriteLine(line);
                List<Point> tempList = new List<Point>();
                char tempChar = line[0];
                int count = line[2] - '0';
                int cur = 4;
                for (int j = 0; j < count * 2; ++j)
                {
                    int xx = line[cur] - '0';
                    if (line[cur + 1] != ' ')
                    {
                        int ii = cur + 1;
                        int cnt = 10;
                        while (line[ii] != ' ')
                        {
                            xx = xx * cnt + line[ii] - '0';
                            ii++;
                            cnt *= 10;
                        }
                        cur = ii + 1;
                    }
                    else cur+=2;
                   
                    int yy = line[cur] - '0';
                    if (line.Length > cur + 1 && line[cur + 1] != ' ')
                    {
                        int ii = cur + 1;
                        int cnt = 10;
                        while (line[ii] != ' ')
                        {
                            yy = yy * cnt + line[ii] - '0';
                            ii++;
                            cnt *= 10;
                        }
                        cur = ii + 1;
                    }
                   else cur += 2;


                    Point temp = new Point(xx, yy);
                   
                    tempList.Add(temp);
                }
                myMap[tempChar] = tempList;
            }
        }
        public Form1()
        {
            InitializeComponent();
            BitMapFontInit();
            VectorPointsChars();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Color pix = Color.Chocolate;
            gr = pictureBox1.CreateGraphics();
            fon = new SolidBrush(Color.Black);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);

            string word;
            word = textBox1.Text;
            string word2 = textBox2.Text;
            if (IsRightData()==true)
            {
                int x = GetX(Int32.Parse(Pointer_X.Text));
                int y = GetY(Int32.Parse(Pointer_Y.Text));
                PrintRastr(word, x, y);
                PrintVector(word2, x, y + 10);

            }
        }   
        private void PrintVector(string word, int x, int y)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                if (word[i] >= 'A' && word[i] <= 'D')
                {
                    int index = 0;
                    for(int ii = 0; ii < myMap[word[i]].Count / 2; ++ii)
                    {
                        gr.DrawLine(p, myMap[word[i]][index].X + x, myMap[word[i]][index].Y + y, myMap[word[i]][index + 1].X + x, myMap[word[i]][index + 1].Y + y);
                        index += 2;
                    }
                    x += 10;
                }
            }
        }
        private void PrintRastr(string word, int x, int y)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                int ID = -1;

                if (word[i] == 'А')
                {
                    ID = 0;
                }
                if (word[i] == 'Б')
                {
                    ID = 1;
                }
                if (word[i] == 'В')
                {
                    ID = 2;
                }
                if (word[i] == 'Г')
                {
                    ID = 3;
                }
                if (word[i] == 'Д')
                {
                    ID = 4;
                }
                if (ID >= 0)
                    PrintRastrSymb(ID, x, y);
                x += 8;
            }
        }
        private void PrintRastrSymb(int ID,int x,int y)
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (BitMapFontArray[ID][i, j] == '1')
                    {
                        PutPixel(x + j, y);
                    }
                }
                ++y;
            }
        } 
        private int GetX(int x)
        {
            return x;
        }
        private int GetY(int y)
        {
            return pictureBox1.Height - y - 10;
        }
        private void PutPixel(int x, int y)
        {
            gr.FillRectangle(Brushes.Aqua, x, y, 1, 1);
        }
        private bool IsRightData()
        {
            int result;
            bool ret = true;
            if (String.IsNullOrEmpty(Pointer_X.Text) || !Int32.TryParse(Pointer_X.Text, out result) || (Int32.Parse(Pointer_X.Text)<0) || (Int32.Parse(Pointer_X.Text)>=(pictureBox1.Width - textBox1.Text.Length*8)))
            {
                ans_line.Text = "Некорректный Х";
                ret = false;
            }
            else
            {
                ans_line.Text = "OK";
            }
            if (String.IsNullOrEmpty(Pointer_Y.Text) || !Int32.TryParse(Pointer_Y.Text, out result)||(Int32.Parse(Pointer_Y.Text)<0)||(Int32.Parse(Pointer_Y.Text)>pictureBox1.Height-8))
            {
                ans_line.Text = "Некорректный Y";
                ret = false;
            }

            
            return ret;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
