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
    struct realPoint
    {
        public realPoint(double X,double Y) { x = X; y = Y; }
        public double x;
        public double y;
    }


    class RobotView
    {
        public const int spaceWidth = 534;
        public const int spaceHeight = spaceWidth;

        public RobotView(PictureBox pictureBox, TextBox[] paramTextBoxes,TextBox debugBox, TextBox[] thrustInputTextBox)
        {
            _pictureBox = pictureBox;
            _listOfParamTextBoxes = paramTextBoxes;
            _graphics = _pictureBox.CreateGraphics();
            _debugText = debugBox;
            _thrustInputTextBox = thrustInputTextBox;
        }
        /// <summary>
        /// Здесь писать логику отрисовки в ГУИ робота и Компании
        /// </summary>
        public void RefreashPictureHandler()//обработчик события прихода сообщения от сервера
        {
            RefreashPicture();
            viewRobot();//отрисовка робота и векторов направлений
            viewCurrentGoalPoint();//отрисовка текущей целевой точки
        }

        public void setCurrentGoalPointHandler(float x,float y)
        {
            _currentGoalPoint_X = x;
            _currentGoalPoint_Y = y;
        }//обработчик события изменения целевой точки
        public void setCurrentThrustVector(float x, float y)
        {
            _currentThrustVector_X = x;
            _currnetThrustVector_Y = y;
            _thrustInputTextBox[0].Text = _currentThrustVector_X.ToString();
            _thrustInputTextBox[1].Text = _currnetThrustVector_Y.ToString();
        }
        private void RefreashPicture()
        {
            _fon = new SolidBrush(Color.LightGoldenrodYellow);
            _graphics.FillRectangle(_fon, 0, 0, _pictureBox.Width, _pictureBox.Height);

            _graphics.DrawLine(_penLines, spaceWidth / 2, 0, spaceWidth / 2, spaceHeight);
            _graphics.DrawLine(_penLines, 0, spaceHeight / 2, spaceWidth, spaceHeight / 2);
        }
        private void viewRobot()
        {
            realPoint origin = new realPoint(spaceWidth / 2, spaceHeight / 2);
            realPoint serverRobotPoint = new realPoint(getParam((int)paramInd.RX), getParam((int)paramInd.RY));
            realPoint robotPoint = convertToPixelCoor(origin, serverRobotPoint);
            double robot_R = getParam((int)paramInd.R);
            robot_R = robot_R *spaceWidth;
            _graphics.DrawEllipse(_penLines, (int)(robotPoint.x - robot_R / 2), (int)(robotPoint.y - robot_R / 2), (float)robot_R, (float)robot_R);
          
            viewVector(robotPoint, new realPoint(getParam((int)paramInd.VRX), getParam((int)paramInd.VRY)), new Pen(Color.Green));
            viewVector(robotPoint,new realPoint(_currentThrustVector_X, _currnetThrustVector_Y), new Pen(Color.Red));
        }
        private void viewCurrentGoalPoint()
        {
            realPoint origin = new realPoint(spaceWidth / 2,spaceHeight/2);
            realPoint serverGP = new realPoint(_currentGoalPoint_X, _currentGoalPoint_Y);
            realPoint pixelGP = convertToPixelCoor(origin, serverGP);
            _graphics.FillRectangle(Brushes.PaleVioletRed, (float)pixelGP.x-2.5f, (float)pixelGP.y-2.5f, 5, 5);
        }
        private double getParam(int i)
        {
            string tx = _listOfParamTextBoxes[i].Text.ToString();
            tx = tx.Replace('.', ',');
            return System.Convert.ToDouble(tx);
        }
        //возвращает данные из текстбокса согласно индексу (как параметр передавать ему значение из enum paramInd
        private void viewVector(realPoint robotPoint, realPoint serverVectorPoint,Pen pen)
        {
            realPoint origin = new realPoint(robotPoint.x, robotPoint.y);
            realPoint pixelPoint = convertToPixelCoor(origin, serverVectorPoint);
            _graphics.DrawLine(pen, new Point((int)robotPoint.x, (int)robotPoint.y),new Point((int)pixelPoint.x, (int)pixelPoint.y));
        
        }
        private realPoint convertToPixelCoor(realPoint origin, realPoint server)
        {
            double x = (origin.x + spaceWidth / 2 * server.x);
            double y = (origin.y - spaceHeight / 2 * server.y);
            return new realPoint(x, y);
        }

        private Graphics _graphics;
        private Pen _penLines = new Pen(Color.Black);
        private Pen _penPoints = new Pen(Color.PaleVioletRed);
        private SolidBrush _fon;
        private PictureBox _pictureBox;

        private TextBox[] _listOfParamTextBoxes;
        private TextBox[] _thrustInputTextBox;
        private TextBox _debugText;


        private float _currentGoalPoint_X;
        private float _currentGoalPoint_Y;

        private float _currentThrustVector_X;
        private float _currnetThrustVector_Y;
    }
}
