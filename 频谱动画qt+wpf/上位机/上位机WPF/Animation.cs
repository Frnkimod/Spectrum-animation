using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArduinoAudioPlayer
{
    public class Animation :GUI
    {
        private Random rand = new Random();
        private Tool tool = new Tool();
        private const int MINDMAX = 25;
        private const int SPECTRUMMAX = 64;

        private int[] a = new int[SPECTRUMMAX];
        public double[] y_m = new double[SPECTRUMMAX];
        public double[] y_c = new double[SPECTRUMMAX];
        private double speed = 0.2f;
        private enum State : int
        {
            OK = 0,
            ERROR = 1,
            BUSY = 2,
            IDLE = 3,
        }
        private struct MTMOVMIND
        {
            public double x;
            public double y;
            public double dirx;
            public double diry;
            public double r;
            public Brush color;
        }
        private MTMOVMIND[] mtmovmind = new MTMOVMIND[MINDMAX];
        private State MovMind(int Index)
        {
            if (mtmovmind[Index].x <= 3)
            {
                mtmovmind[Index].x = 4;

                return State.IDLE;
            }
            else if (mtmovmind[Index].x >= width - 2)
            {
                mtmovmind[Index].x = width - 3;
                return State.IDLE;
            }
            else if (mtmovmind[Index].y <= 0)
            {
                mtmovmind[Index].y = 1;
                return State.IDLE;
            }
            else if (mtmovmind[Index].y >= height - 2)
            {
                mtmovmind[Index].y = height - 3;
                return State.IDLE;
            }
            else
            {
                mtmovmind[Index].x += mtmovmind[Index].dirx;
                mtmovmind[Index].y += mtmovmind[Index].diry;
            }
            return State.BUSY;
        }

        private void Motion_MindInit()
        {
            for (int i = 0; i < SPECTRUMMAX; i++)
            {
                y_m[i] = 250;
                y_c[i] = 0;
            }
            for (int i = 0; i < MINDMAX; i++)
            {

                mtmovmind[i].color = new SolidColorBrush(Color.FromRgb((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                mtmovmind[i].x = rand.Next(width);
                mtmovmind[i].y = rand.Next(height);
                mtmovmind[i].r = 2;
                mtmovmind[i].dirx = rand.Next(-15, 15) * speed;
                mtmovmind[i].diry = rand.Next(-15, 15) * speed;
                if (mtmovmind[i].dirx < 0.2 && mtmovmind[i].dirx > -0.2)
                {
                    mtmovmind[i].dirx = 0.5f;
                }
                if (mtmovmind[i].diry < 0.2 && mtmovmind[i].diry > -0.2)
                {
                    mtmovmind[i].diry = 0.5f;
                }
            }
        }
        public void Motion_Mind(Brush color)
        {
            //更新小球UI
            int i, j;
            double cheap = 4;
            for (i = 0; i < MINDMAX; i++)
            {
                if (mtmovmind[i].y <= 5)
                {
                    mtmovmind[i].diry = rand.Next(-15, 15) * speed;
                }
                if (mtmovmind[i].x <= 5)
                {
                    mtmovmind[i].dirx = rand.Next(-15, 15) * speed;
                }
                if (MovMind(i) == State.IDLE)
                {
                    //IDLE状态下
                    mtmovmind[i].color = new SolidColorBrush(Color.FromRgb((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                    mtmovmind[i].dirx = rand.Next(-15, 15) * speed;
                    mtmovmind[i].diry = rand.Next(-15, 15) * speed;
                    //防卡位
                    if (mtmovmind[i].dirx < 0.2 && mtmovmind[i].dirx > -0.2)
                    {
                        mtmovmind[i].dirx = 0.5f;
                    }
                    if (mtmovmind[i].diry < 0.2 && mtmovmind[i].diry > -0.2)
                    {
                        mtmovmind[i].diry = 0.5f;
                    }

                }
            }

            for (i = 0; i < MINDMAX; i++)
            {

                for (j = 0; j < MINDMAX; j++)
                {

                    if ((mtmovmind[i].x - mtmovmind[j].x) * (mtmovmind[i].x - mtmovmind[j].x) + (mtmovmind[i].y - mtmovmind[j].y) * (mtmovmind[i].y - mtmovmind[j].y) < 6000)
                    {
                       DrawingLine(mtmovmind[i].x - cheap, mtmovmind[i].y, mtmovmind[j].x - cheap, mtmovmind[j].y,color);
                    }
                }

            }
            for (i = 0; i < MINDMAX; i++)
            {
               FillCircle(mtmovmind[i].x, mtmovmind[i].y, 2, mtmovmind[i].color);
            }
        }
        private void column(double x, double y, int i,Brush color)
        {

            double FFTdata = tool.GetLong(i);
            double value = 1.5;
            if (FFTdata == 0)
            {
                return;
            }
            Rec(x, 3 + y_c[i], 5, color);
            if (y_c[i] > FFTdata && y_c[i] != 5)
            {
                y_c[i] -= value;
            }
            else
            {

                y_c[i] += value;
            }
            return;
        }
        private void block(double x, int i,Brush color)
        {
            double FFTdata = tool.GetLong(i);
            if (250 - (y_m[i] + 5) - FFTdata < 10)
            {
                y_m[i] = height - 5 - FFTdata - 3;
            }
            Fill_Rect(x, y_m[i], 5, 5, color);
        }
        public void Moation_Spectrum(Brush color)
        {
            int x = 0;
            for (int i = 0; i < SPECTRUMMAX; i++)
            {
                x = i << 3;
                y_m[i] += 6;
                column(x, 10, i,color);
            }
            UDP.SendUdpString(string.Join(",",y_c));
            tool.Write(string.Join(",", y_c));
        }
        public void Moation_Init(Canvas main)
        {
            tool.setup();
            tool.Connect();
            GUI_Init(main);
            Motion_MindInit();
        }
    }
}
