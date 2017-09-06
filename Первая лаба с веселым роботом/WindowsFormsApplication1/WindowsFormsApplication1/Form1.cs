using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //рисовалка
        Graphics gr;
        Pen penLines = new Pen(Color.Black);
        Pen penPoints = new Pen(Color.PaleVioletRed);
        SolidBrush fon;
        int x = 530, y = 530;




        ///////////////////////////////////////////

        public void TipoAlgoritm() // запускаем при старте
        {
            RefreashPicture();


            PutAim(20, 50);



        }

        public void RefreashPicture()
        {
            fon = new SolidBrush(Color.LightGoldenrodYellow);
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);

            gr.DrawLine(penLines, x / 2, 0, x / 2, y);
            gr.DrawLine(penLines, 0, y / 2, x, y / 2);
        }

        private void PutAim(int x, int y)// для отмечания точек
        {
            gr.FillRectangle(Brushes.PaleVioletRed, x, y, 5, 5);
        }

        bool flagClose = false;
        private byte[] data = new byte[1024];
        private int size = 1024;
        private Socket client;
        TextBox[] textBoxes;

     

        public Form1()
        {
            InitializeComponent();
            textBoxes = new[] { textBox3, textBox4, textBox8, textBox6, textBox16, textBox14, textBox12, textBox10, textBox32, textBox30, textBox28, textBox26, textBox24, textBox22, textBox20, textBox18, textBox48 };
            gr = pictureBox1.CreateGraphics();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] message = Encoding.ASCII.GetBytes("{(FRX=0.1)(FRY=1.004)}");
            client.BeginSend(message, 0, message.Length, SocketFlags.None,
                         new AsyncCallback(SendData), client);
        }

    
        private void button2_Click(object sender, EventArgs e)// кнопка старт
        {
            flagClose = false;
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
            TipoAlgoritm();
        }
        void Connected(IAsyncResult iar)
        {
            client = (Socket)iar.AsyncState;
            try
            {
                client.EndConnect(iar);
                textBox1.Text = "Connected to: " + client.RemoteEndPoint.ToString();
                client.BeginReceive(data, 0, size, SocketFlags.None,
                              new AsyncCallback(ReceiveData), client);
            }
            catch (SocketException)
            {
                textBox1.Text = "Error connecting";// просто что бы видеть если ошибка соединения
            }
        }
        void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Parse(stringData);
            if (flagClose != true)
            {
                client.BeginReceive(data, 0, size, SocketFlags.None,
                             new AsyncCallback(ReceiveData), client);
            }
            
        }

        void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            remote.BeginReceive(data, 0, size, SocketFlags.None,
                          new AsyncCallback(ReceiveData), remote);
        }

        private void Parse(String inputString)
        {
            List<String> stringList = new List<string>();
            String temp = String.Empty;
            for (int i = 0; i < inputString.Count() && inputString[i] != '}'; ++i)
            {
                if (inputString[i] == ')')
                {
                    stringList.Add(temp);
                    temp = String.Empty;
                }
                else if (Char.IsDigit(inputString[i]) || inputString[i] == '.')
                {
                    temp += inputString[i];
                }
            }
            for (int i = 0; i < textBoxes.Count(); i++)
            {
                textBoxes[i].Text = stringList[i];
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            flagClose = true;
            client.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
