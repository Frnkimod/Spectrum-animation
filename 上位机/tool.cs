using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.BassWasapi;
namespace ArduinoAudioPlayer
{
    public class Tool
    {
        public int deviceld;
        public WASAPIPROC proc;
        public BASS_WASAPI_DEVICEINFO[] infos;
        public BASS_WASAPI_DEVICEINFO info;
        public SerialPort arduinoPort;
        public string comPort;
        public long[] lBufInArray=new long[256];
        public long[] dataFromDevice=new long[128];
        public long[] lBufMagArray=new long[128];
        static public string[] week = new string[] { "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };
        public float[] AverageData(float[] data)
        {
            float[] avrdata=new float[data.Length];

            return avrdata;
        }
        public void setup()
        {
            Bass.BASS_Init(0, 0, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);//初始化Bass
            
            if (GetDevice())
            {
                GetDevice();
                Getproc();//获取更新proc
                if (!BassWasapi.BASS_WASAPI_Init(deviceld, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER | BASSWASAPIInit.BASS_WASAPI_AUTOFORMAT, 1f, 0f, proc, IntPtr.Zero))
                {
                    Console.WriteLine("初始化失败");
                    Environment.Exit(0);
                    return;
                }
                info = BassWasapi.BASS_WASAPI_GetDeviceInfo(deviceld);//获取WasAPI实体信息
                /*启动WASAPI服务*/
                BassWasapi.BASS_WASAPI_SetDevice(deviceld);
                BassWasapi.BASS_WASAPI_Start();
                Console.Clear();
                Console.WriteLine("GetDevice初始化成功");
            }
            else
            {
                Console.WriteLine("初始化失败");
                Environment.Exit(0);
            }
        }


        public void Connect()
        {
            choiceCOM();
            arduinoPort = new SerialPort(comPort, 115200);
            arduinoPort.Open();
        }
        private void choiceCOM()
        {
            for(int i=0;i<GetCOM().Length; i++)
            {
                if (GetCOM()[i] != "default")
                {
                    comPort = GetCOM()[i];
                }
                else
                {
                    Console.WriteLine("未找到串口");
                    Environment.Exit(0);
                }
            }
           

        }
        public string[] GetCOM()
        {
            return SerialPort.GetPortNames();
        }
        public void Write(string data)
        {
            try
            {
                arduinoPort.Write(data.ToString());
            }
            catch(Exception)
            {

            }
        }
        public float[] GetFFTData(BASSData dataType, int size)
        {
            float[] dataFromDevice = new float[size];
            BassWasapi.BASS_WASAPI_GetData(dataFromDevice, (int)dataType);
            return dataFromDevice;
        }
        public double GetLong(int i)
        {
            float[] newAveraged = FFT.AvgValues(GetFFTData(BASSData.BASS_DATA_FFT512, 512), 128, 30000);
            double FFTdata = (newAveraged[6+i]);
            return FFTdata;
        }
        public bool GetDevice()
        {
            int tor = -1;
            infos = BassWasapi.BASS_WASAPI_GetDeviceInfos();
            foreach(BASS_WASAPI_DEVICEINFO info in infos)
            {
                tor++;
                if(info.IsDefault&&!info.IsInput)
                {
                    tor = tor + 1;
                    break;
                }
            }
            if (tor < 0)
                return false;
            deviceld = tor;
            return true;
        }
        
       
        public void Getproc()
        {
            proc = new WASAPIPROC(WasapiCallback);
        }
        private static int WasapiCallback(IntPtr buffer, int length, IntPtr user)
        {
            return 1;
        }
    }
}
