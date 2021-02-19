using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ArduinoAudioPlayer;

using CourseWork;
using Un4seen.Bass;

namespace ArduinoAudioPlayer
{
    public class GUI
    {
        private Canvas root;
        public int width = 500;
        public int height = 300;
        public void GUI_Init(Canvas canvas)
        {
            root = canvas;
        }
        public void FillCircle(double x, double y, int r, Brush Color)
        {
            Path x_Arrow = new Path();//x轴箭头

            x_Arrow.Fill = Color;

            PathFigure x_Figure = new PathFigure();
            x_Figure.IsClosed = true;
            x_Figure.StartPoint = new Point(x, y);//路径的起点
            x_Figure.Segments.Add(new ArcSegment(new Point(x, y + r / 2), new Size(2 * r, 2 * r), 1, true, SweepDirection.Counterclockwise, true));


            PathGeometry x_Geometry = new PathGeometry();

            x_Geometry.Figures.Add(x_Figure);

            x_Arrow.Data = x_Geometry;


            Canvas chartCanvas = new Canvas();
            chartCanvas.Children.Add(x_Arrow);
            root.Children.Add(chartCanvas); 

        }
        
    
        public void DrawingLine(double x, double y, double x1, double y1, Brush color)
        {
            Point startPt = new Point(x, y);
            Point endPt = new Point(x1, y1);
            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = startPt;
            myLineGeometry.EndPoint = endPt;
            Path LinePath = new Path();
            LinePath.Stroke = color;
            LinePath.StrokeThickness = 1;
            LinePath.Data = myLineGeometry;

            root.Children.Add(LinePath);

        }
     

      
        public void Rec(double x, double y, double w, Brush color)
        {
            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(x, height - y, w, y);
            Path myPath = new Path();
            myPath.Data = myRectangleGeometry;
            myPath.Fill = color;
            myPath.StrokeThickness = 0;
            root.Children.Add(myPath);
        }
        public void Fill_Rect(double x, double y, double w,double h, Brush color)
        {
            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(x, y, w, h);
            Path blockTop = new Path();
            blockTop.Fill = color;
            blockTop.StrokeThickness = 0;
            blockTop.Data = myRectangleGeometry;
            root.Children.Add(blockTop);
        }
    }
}
