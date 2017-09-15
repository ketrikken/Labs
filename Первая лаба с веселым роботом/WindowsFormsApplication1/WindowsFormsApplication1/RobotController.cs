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

        int count = 0;
        double predTask = -1;
        double pastDistanceForPoint = 400;
        //делегат и событие прихода сообщения от сервера
        public delegate void messageDispatcher();
        public event messageDispatcher messageFromServerEvent;
        //делегат и событие изменения целевой точки
        public delegate void changeGoalPoint(float x, float y,float r);
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
            clearData();
            //_client.Disconnect(true);
            
        }
        private void clearData()
        {
            _connectTextBox.Clear();
            for (int i = 0; i < _listOfParamTextBoxes.Count(); ++i)
                _listOfParamTextBoxes[i].Clear();
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
                _client.BeginReceive(_data, 0, _size, SocketFlags.None, new AsyncCallback(ReceiveData), _client);
            }
            catch (SocketException)
            {
                _connectTextBox.Text = "Error connecting";
            }
        }
        private void ReceiveData(IAsyncResult iar)
        {
           
    
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);
            string stringData = Encoding.ASCII.GetString(_data, 0, recv);
            Parse(stringData);
           // setCurrentGoalPoint();//установка целевой точки согласно сообщения (пока только по второму заданию)
            messageFromServerEvent();
            if (_flagClose != true)
            {
                

                _client.BeginReceive(_data, 0, _size, SocketFlags.None, new AsyncCallback(ReceiveData), _client);

              
            }

        }
        private void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            remote.BeginReceive(_data, 0, _size, SocketFlags.None, new AsyncCallback(ReceiveData), remote);
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
                else if (Char.IsDigit(inputString[i]) || inputString[i] == '.' || inputString[i] == '-')
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
            float r = 0;
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
                   _currentGoalPoint_X = (float)getParam((int)paramInd.TX) ;
                   _currentGoalPoint_Y = (float)(getParam((int)paramInd.TY));
                    //_connectTextBox.Text += "    " + _currentGoalPoint_X.ToString();
                   r = (float)getParam((int)paramInd.TR);
                    break;
            }
            changeCurrentGoalPoint(_currentGoalPoint_X, _currentGoalPoint_Y,r);
        }
        private void setCurrentGoalPointForRouteMode()
        {
            int i = Convert.ToInt32(_listOfParamTextBoxes[(int)paramInd.POINT].Text.ToString());
            switch (i)
            {
                /* case 1: 
                _currentGoalPoint_X = 0; 
                _currentGoalPoint_Y = 0; 
                break;*/
                case 0:
                    _currentGoalPoint_X = 0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 1:
                    _currentGoalPoint_X = 0;
                    _currentGoalPoint_Y = 0.7f;
                    break;
                case 2:
                    _currentGoalPoint_X = -0.3f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 3:
                    _currentGoalPoint_X = -0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                case 4:
                    _currentGoalPoint_X = 0.7f;
                    _currentGoalPoint_Y = 0;
                    break;
                /*case 5:
                    _currentGoalPoint_X = 0.7f;
                    _currentGoalPoint_Y = 0;
                    break;*/
                case 5:
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

        private realPoint ReturnVectorToPoint()
        {
            realPoint point = new realPoint();
            double robot_X_Pos = getParam((int)paramInd.RX);
            double robot_Y_Pos = getParam((int)paramInd.RY);
            double _vx = getParam((int)paramInd.VRX);
            double _vy = getParam((int)paramInd.VRY);

            double vecToPointX =  - robot_X_Pos + _currentGoalPoint_X;
            double vecToPointY = - robot_Y_Pos + _currentGoalPoint_Y;

            double vecFromCentreX = _vx - robot_X_Pos;
            double vecFromCentreY = _vy - robot_Y_Pos;



            point.x = vecToPointX - vecFromCentreX;
            point.y = vecToPointY - vecFromCentreY;
            return point;
        }

        private void headToCurrentPoint()
        {
            realPoint point = new realPoint();
            double robot_X_Pos = getParam((int)paramInd.RX);
            double robot_Y_Pos = getParam((int)paramInd.RY);
            double SpeedVec_PosX = getParam((int)paramInd.VRX);
            double SpeedVec_PosY = getParam((int)paramInd.VRY);
            double _vx = getParam((int)paramInd.VRX);
            double _vy = getParam((int)paramInd.VRY);


            //double vecRast = (robot_X_Pos - _currentGoalPoint_X) * (robot_X_Pos - _currentGoalPoint_X) + (robot_Y_Pos - _currentGoalPoint_Y) * (robot_Y_Pos - _currentGoalPoint_Y);


            double vecToPointX = -robot_X_Pos + _currentGoalPoint_X -_vx ;
            double vecToPointY = -robot_Y_Pos + _currentGoalPoint_Y - _vy;

         
            if ((pastSentToServerX != (-vecToPointX * 32)) && (pastSentToServerY != (-vecToPointY * 32)) )
            sendMessage((float)(-vecToPointX * 32), (float)(-vecToPointY * 32));

            pastSentToServerX = -vecToPointX * 32;
            pastSentToServerY = -vecToPointY * 32;
            time = DateTime.Now;
            //pastLen = vecRast;

        }
        DateTime time;
        double pastSentToServerX = 0;
        double pastSentToServerY = 0;
        //double pastLen = 0;




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
