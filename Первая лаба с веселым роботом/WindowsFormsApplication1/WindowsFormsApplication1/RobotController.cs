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
    public enum paramInd
    {
        T, R, M, RX, RY, VRX, VRY, TR, TX, TY, TASK,
        POINT, FAIL, CRASH, DONE, FUEL, ERROR, IGNORED
    }

    public class RobotController
    {

        public RobotController(TextBox connectTextBox, TextBox[] listOfParamTextBoxes)
        {
            _connectTextBox = connectTextBox;
            _listOfParamTextBoxes = listOfParamTextBoxes;
            messageFromServerEvent += messageHandler;
            _currentGoalPoint_X = 0;
            _currentGoalPoint_Y = 0;
        }

        //делегат и событие прихода сообщения от сервера
        public delegate void messageDispatcher();
        public event messageDispatcher messageFromServerEvent;
        //делегат и событие изменения целевой точки
        public delegate void changeGoalPoint(float x, float y);
        public event changeGoalPoint changeCurrentGoalPoint;
        //делегат и событие изменения вектора тяги робота
        public delegate void changeThrustVector(float x, float y);
        public event changeThrustVector changeCurrentThrustVector;

        public void startCallBack()
        {
            _flagClose = false;
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
        }
        public void closeCallBack()
        {
            _flagClose = true;
            _client.Close();
        }
        /// <summary>
        /// Здесь писать логику отправки управляющих сообщений на сервер!!!
        /// </summary>
        private void messageHandler()
        {
            
            setCurrentGoalPoint();//установка целевой точки согласно сообщения (пока только по второму заданию)
            headToCurrentPoint();//задание направления на текущую точку
        }//обработчик события прихода сообщения от сервера

        private void Connected(IAsyncResult iar)
        {
            _client = (Socket)iar.AsyncState;
            try
            {
                _client.EndConnect(iar);
                _connectTextBox.Text = "Connected to: " + _client.RemoteEndPoint.ToString();
                _client.BeginReceive(_data, 0, _size, SocketFlags.None,
                              new AsyncCallback(ReceiveData), _client);
            }
            catch (SocketException)
            {
                _connectTextBox.Text = "Error connecting";// просто что бы видеть если ошибка соединения
            }
        }
        private void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);
            string stringData = Encoding.ASCII.GetString(_data, 0, recv);
            Parse(stringData);
            messageFromServerEvent();
            if (_flagClose != true)
            {
                _client.BeginReceive(_data, 0, _size, SocketFlags.None,
                             new AsyncCallback(ReceiveData), _client);
            }

        }
        private void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            remote.BeginReceive(_data, 0, _size, SocketFlags.None,
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
            for (int i = 0; i < _listOfParamTextBoxes.Count(); i++)
            {
                _listOfParamTextBoxes[i].Text = stringList[i];
            }

        }
        private void sendMessage(float frx,float fry)
        {
            string frx_s = frx.ToString();
            frx_s = frx_s.Replace(',', '.');
            string fry_s = fry.ToString();
            fry_s = fry_s.Replace(',', '.');
            string messageComand = "{(FRX=" + frx_s + ")(FRY=" + fry_s + ")}";
            byte[] message = Encoding.ASCII.GetBytes(messageComand);
            _client.BeginSend(message, 0, message.Length, SocketFlags.None,
                         new AsyncCallback(SendData), _client);
            changeCurrentThrustVector(frx, fry);
    }
        private void setCurrentGoalPoint()
        {
            int i = Convert.ToInt32(_listOfParamTextBoxes[(int)paramInd.TASK].Text.ToString());
            switch (i)
            {
                case 1:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = 0;
                    break;
                case 2:
                    setCurrentGoalPointForRouteMode();
                    break;
                case 3:
                    _currentGoalPoint_X = Convert.ToInt32(_listOfParamTextBoxes[(int)paramInd.TX].Text.ToString());
                    _currentGoalPoint_Y = Convert.ToInt32(_listOfParamTextBoxes[(int)paramInd.TY].Text.ToString());
                    break;
            }
            changeCurrentGoalPoint(_currentGoalPoint_X, _currentGoalPoint_Y);
        }
        private void setCurrentGoalPointForRouteMode()
        {
            int i = Convert.ToInt32(_listOfParamTextBoxes[(int)paramInd.POINT].Text.ToString());
            switch (i)
            {
                case 0:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = 0;
                    break;
                case 1:
                    _currentGoalPoint_X = 0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 2:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = 0.7f;
                    break;
                case 3:
                    _currentGoalPoint_X = -0.3f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 4:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = -0.7f;
                    break;
                case 5:
                    _currentGoalPoint_X = -0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 6:
                    _currentGoalPoint_X = 0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 7:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = -0.8f;
                    break;
            }
        }
        private double getParam(int i)
        {
            string tx = _listOfParamTextBoxes[i].Text.ToString();
            tx = tx.Replace('.', ',');
            return System.Convert.ToDouble(tx);
        }
        private void headToCurrentPoint()
        {
            double robot_X_Pos = getParam((int)paramInd.RX);
            double robot_Y_Pos = getParam((int)paramInd.RY);
            if (robot_X_Pos > 0)
                sendMessage(0.1f,0f);
            //sendMessage((float)robot_X_Pos, (float)robot_Y_Pos);
            
        }

       




        private bool _flagClose;
        private byte[] _data = new byte[1024];
        private int _size = 1024;
        private Socket _client;
        private TextBox _connectTextBox;
        private TextBox[] _listOfParamTextBoxes;

        private float _currentGoalPoint_X;
        private float _currentGoalPoint_Y;
        
    }
}
