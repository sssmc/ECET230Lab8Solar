using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECET230Lab8SolarDesktop
{
    internal class SolarPlotField : IDrawable
    {
        Random random = new Random();
        Color[] colors = new Color[] { Color.FromRgb(255, 0, 0), Color.FromRgb(0, 255, 0), Color.FromRgb(0, 0, 255), Color.FromRgb(255, 0, 255), Color.FromRgb(0,0,0)};

        Color color = Color.FromRgb(255, 0, 0);
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float[][]? _plotDataY = App.solarViewModel.PlotDataY;

            DateTime[] _plotDataX = App.solarViewModel.PlotDataX;

            const int leftLabelWidth = 100;
            const int bottomLabelHeight = 100;

            //Draw a black border around the canvas
            canvas.StrokeSize = 1;
            canvas.StrokeColor = Color.FromRgb(0,0,0);
            canvas.FillColor = Color.FromRgb(255,255,255);
            canvas.DrawRectangle(0,0,dirtyRect.Width, dirtyRect.Height);

            //Draw Vertical Axis Line
            canvas.DrawLine(leftLabelWidth, 0, leftLabelWidth, dirtyRect.Height - bottomLabelHeight/2);

            //We need at least 2 points to draw a plot line
            if(_plotDataX == null || _plotDataY == null)
            {
                return;
            }
            if(_plotDataX.Length > 1 && _plotDataY.Length > 1)
            {

                //Find the min and max values of the Y data
                List<float> minValues = new List<float>();
                foreach (float[] plotData in _plotDataY)
                {
                    minValues.Add(plotData.Min());
                }
                
                float minValue = minValues.Min();

                List<float> maxValues = new List<float>();
                foreach (float[] plotData in _plotDataY)
                {
                    maxValues.Add(plotData.Max());
                }
                float maxValue = maxValues.Max();

                

                //Calculate the vertical span of the data
                float ySpan = maxValue - minValue;

                //Calculate the horizontal span of the data
                TimeSpan xTimeSpan = _plotDataX[_plotDataX.Length - 1] - _plotDataX[0];

                //Calculate the start time of the data
                DateTime startTime = _plotDataX[0];

                //Calculate the number of pixels per millisecond(x axis) and per vertical unit(y axis)
                float pixelsPerMillisecond = (float)((dirtyRect.Width - leftLabelWidth) / xTimeSpan.TotalMilliseconds);
                float pixelsPerVerticalUnit = (dirtyRect.Height - bottomLabelHeight) / ySpan;

                //Draw the horizontal axis line at the zero point of the y axis
                canvas.DrawLine(leftLabelWidth / 2, 
                                dirtyRect.Height - bottomLabelHeight + (minValue * pixelsPerVerticalUnit), 
                                dirtyRect.Width, 
                                dirtyRect.Height - bottomLabelHeight + (minValue * pixelsPerVerticalUnit));

                //Draw Vertical Axis Labels
                canvas.StrokeSize = 1;
                canvas.StrokeColor = Color.FromRgb(0, 0, 0);
                canvas.FillColor = Color.FromRgb(0, 0, 0);
                canvas.FontSize = 20;

                const int virticalLabelCount = 8;

                float verticalLabelInterval = (ySpan / virticalLabelCount);

                for(int i = 0; i <= virticalLabelCount; i++)
                {
                    canvas.DrawString(((int)(i * verticalLabelInterval)).ToString(), 0, dirtyRect.Height - bottomLabelHeight - (((i * verticalLabelInterval) - minValue) * pixelsPerVerticalUnit), leftLabelWidth / 2, bottomLabelHeight, HorizontalAlignment.Center, VerticalAlignment.Top);
                }

                //canvas.DrawString(ySpan.ToString(), 0, dirtyRect.Height - bottomLabelHeight - ((ySpan - minValue) * pixelsPerVerticalUnit), leftLabelWidth / 2, bottomLabelHeight, HorizontalAlignment.Center, VerticalAlignment.Top);

                //Zero
                //canvas.DrawString("0", 0, dirtyRect.Height - bottomLabelHeight + (minValue * pixelsPerVerticalUnit), leftLabelWidth/2, bottomLabelHeight, HorizontalAlignment.Center, VerticalAlignment.Top);

                //For each x point
                for (int i = 0; i < _plotDataX.Length - 1; i++)
                {
                    //For each Y data line
                    for (int j = 0; j< 5;j++) 
                    {
                        //
                        TimeSpan xStartDiff = _plotDataX[i] - startTime;
                        TimeSpan xEndDiff = _plotDataX[i + 1] - startTime;

                        canvas.StrokeSize = 2;
                        canvas.StrokeColor = colors[j];
                        canvas.DrawLine((float)((xStartDiff.TotalMilliseconds * pixelsPerMillisecond) + leftLabelWidth), 
                                        (dirtyRect.Height - ((_plotDataY[j][i] - minValue) * pixelsPerVerticalUnit) - bottomLabelHeight), 
                                        (float)((xEndDiff.TotalMilliseconds * pixelsPerMillisecond) + leftLabelWidth), 
                                        (dirtyRect.Height - ((_plotDataY[j][i + 1] - minValue) * pixelsPerVerticalUnit)) - bottomLabelHeight);
                    }
                }
            }
        }
    }
}
