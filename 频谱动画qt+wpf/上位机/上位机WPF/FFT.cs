using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoAudioPlayer
{
    class FFT
    {
        static public float[] AvgValues(float[] data, int transformTo, int multiplyBy)
        {
            float[] averaged = new float[transformTo];
            int iCounter = 0;//计数器
            int sizeOfArray = data.Length/2 ; // halved because there is no second part due to channels being messed up :(
            int divisor = sizeOfArray / transformTo;
            for (int i = 0; i < sizeOfArray; i++)
            {
                averaged[iCounter] += data[i] * multiplyBy;
                if (i % divisor == divisor - 1)
                {
                    averaged[iCounter] += data[i] * multiplyBy;
                    averaged[iCounter] = averaged[iCounter] / divisor;
                    iCounter++;
                }
            }
            return averaged;
        }
        static public float[] GetPowerMag(float[] data,int size)
        {
            float lx, ly;
            float x, y, Mag;
            float[] buffer = new float[size];
            for (int i = 0; i < 128; i++)
            {
                lx = ((int)data[i] << 16) << 16;
                ly = ((int)data[i] >> 16);
                x = 256 * lx;
                y = 256 * ly;
                Mag = (float)Math.Sqrt(x * x + y * y);
                data[i] = Mag;
            }
            return data;
        }
    }
}
