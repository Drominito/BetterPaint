using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace BetterPaint.Code.Mathematical.Pattern
{
    internal class RenderPatternLibary
    {
        public Canvas GDIWindow { get; set; }
        public double Controller { get; set; }
        public System.Windows.Point MousePosition { get; set; }
        public int GenerateZoom { get; set; }
        public double DimensionIterationController { get; set; }

        public double ZoomOutMult { get; set; }
        public bool FillTheCirle { get; set; }
        public double ModuloKey { get; set; }
        public double RangeKey { get; set; }
        public double StateMult { get; set; }
        public int Resolution { get; set; }

        public RenderPatternLibary(Canvas _GDIWindow, double controller, System.Windows.Point mouseposition,
                              int generatezoom, double dimensioniterationcontroller, double zoomoutmult)
        {
            GDIWindow = _GDIWindow;
            Controller = controller;
            MousePosition = mouseposition;
            GenerateZoom = generatezoom;
            DimensionIterationController = dimensioniterationcontroller;
            ZoomOutMult = zoomoutmult;

        }



        public RenderPatternLibary(Canvas _GDIWindow, double controller, bool fillthecircle,
                              double modulokey, double rangekey, double statemult)
        {
            GDIWindow = _GDIWindow;
            Controller = controller;
            FillTheCirle = fillthecircle;
            ModuloKey = modulokey;
            RangeKey = rangekey;
            StateMult = statemult;
        }




        public RenderPatternLibary(Canvas _GDIWindow, double controller, System.Windows.Point mouseposition,
                              int generatezoom, double zoomoutmult, double modulokey, double rangekey, double statemult, int _Resoltuion)
        {
            GDIWindow = _GDIWindow;
            Controller = controller;
            MousePosition = mouseposition;
            GenerateZoom = generatezoom;
            ZoomOutMult = zoomoutmult;

            ModuloKey = modulokey;
            RangeKey = rangekey;
            StateMult = statemult;
            Resolution = _Resoltuion;

        }

        public void Fractal()
        {
            int width = Resolution;
            int height = width;

            int centerX = width / 2;
            int centerY = height / 2;

            double cursorX = centerX - MousePosition.X;
            double cursorY = centerY - MousePosition.Y;

            int iterations = 250;
            int moduloKeyInt = (int)ModuloKey;

            int pixelSize = 100;

            GeometryGroup geometryGroup = new GeometryGroup();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distanceSquared = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                    double realPart = (x + cursorX) / ZoomOutMult;
                    double imaginaryPart = (y + cursorY) / ZoomOutMult;

                    Complex complexC = new Complex(realPart, imaginaryPart);
                    Complex complexZ = new Complex(0, 0);
                    int iteration = 0;

                    while (complexZ.Magnitude < 2 && iteration < iterations)
                    {
                        complexZ = (complexZ * complexZ) + complexC;
                        iteration++;
                    }

                    if (iteration != iterations)
                    {
                        // Erstellen Sie eine EllipseGeometry für jeden Punkt, der nicht den maximalen Iterationswert erreicht hat
                        EllipseGeometry ellipseGeometry = new EllipseGeometry(new Point(x, y), pixelSize, pixelSize);
                        geometryGroup.Children.Add(ellipseGeometry);
                    }
                   
                }
            }

            // Hier können Sie die GeometryGroup zur Visualisierung verwenden, z.B. indem Sie sie einer Shape-Instanz zuweisen oder direkt zum UI hinzufügen
            // Zum Beispiel:
            Path path = new Path();
            path.Stroke = Brushes.Yellow;
            path.StrokeThickness = 1;
            path.Data = geometryGroup;


            // Fügen Sie path dem UI-Element hinzu oder weisen Sie es einer Shape-Instanz zu, abhängig von Ihrer Anwendung.

            //CubeList = null;
        }
        
        private void CreateVirtualPixel(int x, int y, int CenterX, int CenterY, Color color, Rectangle[,] CubeList, int PixelSize)
        {
            CubeList[x, y] = new Rectangle()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(color)
            };
            Canvas.SetLeft(CubeList[x, y], x);
            Canvas.SetTop(CubeList[x, y],  y);
            GDIWindow.Children.Add(CubeList[x, y]);
        }
    }
}
