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


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        TextBox[] textBoxes;
      
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public Form1()
        {
            InitializeComponent();
            textBoxes = new[] {textBox3, textBox4, textBox8, textBox6, textBox16, textBox14, textBox12, textBox10, textBox32, textBox30, textBox28, textBox26, textBox24, textBox22, textBox20, textBox18, textBox48 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            GetDATA();
        }

        private void GetDATA()
        {
            serverStream = clientSocket.GetStream();
            Byte[] data = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = serverStream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            textBox1.Text += "     " + responseData;
            Parse(responseData);
        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                textBox1.Text = readData;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("127.0.0.1", 9999);
            serverStream = clientSocket.GetStream();
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
                }else if (Char.IsDigit(inputString[i]) || inputString[i] == '.')
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
    }
}
