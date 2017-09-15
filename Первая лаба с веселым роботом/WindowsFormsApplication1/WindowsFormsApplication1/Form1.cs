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
using System.Net.Sockets;
using System.Net;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        RobotController rc;
        RobotView rv;
        TextBox[] textBoxes;

        public Form1()
        {
            InitializeComponent();
            textBoxes = new[] { textBox_T, textBox_R, textBox_M, textBox_RX, textBox_RY, textBox_VRX, textBox_VRY,
                textBox_TR, textBox_TX, textBox_TY, textBox_TASK,  textBox_point, textBox_FALL, textBox_CRASH, textBox_FUEL,textBox_DONE, textBox_ERROR, textBox_IGNORED };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rc = new RobotController(textBox_connectInfo, textBoxes);
            rv = new RobotView(pictureBox1, textBoxes, textBox_connectInfo, new[] { textBox_FRX, textBox_FRY });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

    
        private void button2_Click(object sender, EventArgs e)// кнопка старт
        {
            rc = new RobotController(textBox_connectInfo, textBoxes);
            rv = new RobotView(pictureBox1, textBoxes, textBox_connectInfo, new[] { textBox_FRX, textBox_FRY });
            rc.startCallBack();
            rc.messageFromServerEvent += rv.RefreashPictureHandler; // добавление обработчика события когда пришло сообщение от сервера
            rc.changeCurrentGoalPoint += rv.setCurrentGoalPointHandler;//добавление обработчика события когда поменялась текущая целевая точка, к которой должен лететь робот
            rc.changeCurrentThrustVector += rv.setCurrentThrustVector;
        }
       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            rc.closeCallBack();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
