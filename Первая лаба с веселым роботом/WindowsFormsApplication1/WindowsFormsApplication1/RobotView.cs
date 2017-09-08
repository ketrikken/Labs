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
    class RobotView
    {
        public const int spaceWidth = 534;
        public const int spaceHeight = spaceWidth;

        public RobotView(PictureBox pb, TextBox[] paramTextBoxes,TextBox debugBox)
        {
            _pb = pb;
            _listOfParamTextBoxes = paramTextBoxes;
            _gr = _pb.CreateGraphics();
            _debugText = debugBox;
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

        private void RefreashPicture()
        {
            _fon = new SolidBrush(Color.LightGoldenrodYellow);
            _gr.FillRectangle(_fon, 0, 0, _pb.Width, _pb.Height);

            _gr.DrawLine(_penLines, spaceWidth / 2, 0, spaceWidth / 2, spaceHeight);
            _gr.DrawLine(_penLines, 0, spaceHeight / 2, spaceWidth, spaceHeight / 2);
        }
        private void viewRobot()
        {
            double robot_X_Pos = getParam((int)paramInd.RX);

            robot_X_Pos = spaceWidth/2 + spaceWidth / 4 * robot_X_Pos;
            double robot_Y_Pos = getParam((int)paramInd.RY);

            robot_Y_Pos = spaceHeight/2 - spaceHeight / 4 * robot_Y_Pos;
            double robot_R = getParam((int)paramInd.R);

            robot_R = robot_R * (spaceWidth/2);
            _gr.DrawEllipse(_penLines, (int)(robot_X_Pos - robot_R / 2), (int)(robot_Y_Pos- robot_R / 2), (float)robot_R, (float)robot_R);
            //_debugText.Text = ((float)robot_R).ToString();
            viewSpeedVector(robot_X_Pos,robot_Y_Pos);
        }
        private void viewCurrentGoalPoint()
        {
            int x = (int)(spaceWidth / 2 + spaceWidth / 4 * _currentGoalPoint_X);
            int y = (int)(spaceWidth / 2 + spaceWidth / 4 * _currentGoalPoint_Y);
            _gr.FillRectangle(Brushes.PaleVioletRed, x-2.5f, y-2.5f, 5, 5);
        }
        private double getParam(int i)
        {
            string tx = _listOfParamTextBoxes[i].Text.ToString();
            tx = tx.Replace('.', ',');
            return System.Convert.ToDouble(tx);
        }//возвращает данные из текстбокса согласно индексу (как параметр передавать ему значение из enum paramInd
        private void viewSpeedVector(double roboX, double roboY)//неправильно
        {
            double vrx = getParam((int)paramInd.VRX);
            vrx = spaceWidth / 2 + spaceWidth / 4 * vrx;
            double vry = getParam((int)paramInd.VRY);
            vry = spaceWidth / 2 + spaceWidth / 4 * vry;
            _gr.DrawLine(_penLines, new Point((int)roboX, (int)roboY),
                new Point((int)vrx, (int)vry));
        }


        private Graphics _gr;
        private Pen _penLines = new Pen(Color.Black);
        private Pen _penPoints = new Pen(Color.PaleVioletRed);
        private SolidBrush _fon;
        private PictureBox _pb;

        private TextBox[] _listOfParamTextBoxes;

        private TextBox _debugText;


        private float _currentGoalPoint_X;
        private float _currentGoalPoint_Y;
        
    }
}
