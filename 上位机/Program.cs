using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.BassWasapi;
using System.IO.Ports;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Windows;
using CourseWork;

namespace ArduinoAudioPlayer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            BassNet.Registration(ConfigurationManager.AppSettings["bass_email"], ConfigurationManager.AppSettings["bass_key"]);
            Tool tool = new Tool();
            OLEDwin win = new OLEDwin();
            Application app = new Application();
            
            
            /*串口初始化*/
            
            app.Run(win);
            Bass.BASS_Free();
            
        }

    }
}
