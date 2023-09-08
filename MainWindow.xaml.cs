using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Diagnostics;

namespace BetterPaint
{
    public partial class MainWindow : Window
    {
        public delegate void DetectingMousePositionDelegate(KeyEventArgs e);
        public event DetectingMousePositionDelegate DetectingMousePositionEvent;
        private Bitmap bitmap;
        private double Controller;
        private double DimensionIterationController = 0;
        public double ZoomOutMult = 1;
        DispatcherTimer timer;
        DispatcherTimer FPSUpdateTimer;
        public double AlreadyGeneratedFrames = 0;
        public new System.Windows.Point MousePosition;
        public int OrientatingPointer = 0;
        private Ellipse cursor;

        List<int> FPSInformationList;
        Stopwatch DurationOfFrameGeneration = new Stopwatch();
        private double TimetoComputeFrames;
        private int GenerateZoom;
        private System.Drawing.Color ThreeDColor;


        public double ModuloKey = 0;
        public double RangeKey = 0;
        public double StateMult = 1;
        private bool FillTheCirle = false;

        public MainWindow()
        {
            InitializeComponent();


            //Program p = new Program(ImageControl);



            FPSInformationList = new List<int>();

            DetectingMousePositionEvent += GetMousePosition;

            MouseWheel += MainWindow_MouseWheel;

            MouseUIFrontInfo.HorizontalAlignment = HorizontalAlignment.Right;
            MouseUIFrontInfo.FontSize = 30;
            MouseUIFrontInfo.Foreground = System.Windows.Media.Brushes.White;

            KeyDown += MainWindow_KeyDown;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(0);
            timer.Tick += Timer_Tick;
            timer.Start();

            FPSUpdateTimer = new DispatcherTimer();
            FPSUpdateTimer.Interval = TimeSpan.FromSeconds(1);
            FPSUpdateTimer.Tick += UpdateFPS;
            FPSUpdateTimer.Start();



        }


        async private Task GenerateFrames()
        {
            int width =  500;
            int height = 500;
            // Erstelle ein neues Bitmap
            bitmap = new Bitmap(width, height);

            // Fülle das Bitmap mit Farben
            ItegratePixels(bitmap);
            //await Task.Delay(100);

            // Setze das Bitmap als Quelle für das Image-Control
            ImageControl.Source = ConvertToBitmapSource(bitmap);
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: { MousePosition.Y -= 20; /*GetMousePosition(e);*/ } break;
                case Key.A: { MousePosition.X -= 50;  /*GetMousePosition(e);*/ } break;
                case Key.S: { MousePosition.Y += 20;  /*GetMousePosition(e);*/ } break;
                case Key.D: { MousePosition.X += 50;  /*GetMousePosition(e);*/ } break;
                case Key.Z: { GenerateZoom += 5; } break;

                case Key.Space: { DimensionIterationController += 1; } break;

                case Key.Right:    { RangeKey  += 0.5  * StateMult; } break;
                case Key.Left:     { RangeKey  -= 0.5  * StateMult; } break;
                case Key.Up:       { ModuloKey += 0.05 * StateMult; } break;
                case Key.Down:     { ModuloKey -= 0.05 * StateMult; } break;
                                                     
                case Key.Add:      { StateMult    += 1; } break;
                case Key.Subtract: { StateMult    -= 1; } break;

                case Key.LeftShift: {       if (FillTheCirle == true) { FillTheCirle = false; }        else { FillTheCirle = true; }       } break;

                case Key.M:
                    {
                        if (DetectingMousePositionEvent == null)
                        {
                            DetectingMousePositionEvent += GetMousePosition;
                        }
                        else
                        {
                            DetectingMousePositionEvent -= GetMousePosition;
                        }
                    }
                    break;

                default: { GenerateFrames(); } break;

            }
            MouseUIFrontInfo.Text = $"X : {MousePosition.X} | Y : {MousePosition.Y}";
        }

        private async void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            await Task.Run(() => MainWindow_MouseWheelAsync(e));
        }

        private async Task MainWindow_MouseWheelAsync(MouseWheelEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.Delta > 0)
                {
                    ZoomOutMult -= 0.25 * 100;
                }
                else
                {
                    ZoomOutMult += 0.25 * 100;
                }
            });
        }





        public void GetMousePosition(KeyEventArgs e)
        {
            MousePosition = Mouse.GetPosition(this);
            MouseUIFrontInfo.Text = $"X : {MousePosition.X} | Y : {MousePosition.Y}";
        }



        private void Timer_Tick(object? sender, EventArgs e)
        {
            //IterationControllerUI.Text = $"Modulo Iterations : {IterationController}";
            IterationControllerUI.Text = $"ModuloKey : {ModuloKey}";
            DetectingMousePositionEvent?.Invoke(null);

            //Controller= (Controller+=1) + (Controller*=5);
            //Controller = Controller + 2 * 65;
            Controller += 5000; // Default = 10;


            DurationOfFrameGeneration.Start();
            GenerateFrames();
            DurationOfFrameGeneration.Stop();


            int ElapdesMilliTime = (int)DurationOfFrameGeneration.ElapsedMilliseconds;
            DurationOfFrameGeneration.Reset();
            FPSInformationList.Add(ElapdesMilliTime);

            AlreadyGeneratedFrames++;
        }

        private async void UpdateFPS(object? sender, EventArgs e)
        {
            await Task.Run(UpdateFPSAsync);
        }

        private void UpdateFPSAsync()
        {
            Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < FPSInformationList.Count; i++)
                {
                    //if (FPSInformationList[i] == null) { break; }         // Ist denke überflüssig

                    TimetoComputeFrames += FPSInformationList[i];
                }

            });
            Dispatcher.Invoke(() =>
            {
                AllFPSCompuetdUI.Content = $"All Fps generated : {AlreadyGeneratedFrames}";
                //FramesPerSecondUI.Content = "NaN";

                FramesPerSecondUI.Content = AlreadyGeneratedFrames / (TimetoComputeFrames / 1000);


            });

        }

        private async Task ItegratePixels(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int CenterX = width / 2;
            int CenterY = height / 2;
            int Radius = width / 4;

            for (int y = 0; y < height; y++)
            {


                for (int x = 0; x < width; x++)
                {

                    //double nx = x * x - y * y + cx;
                    //double ny = 2.0 * x * y + cy;
                    //tx = nx;
                    //ty = ny;


                    double Distacesquared1 = Math.Sqrt(Math.Pow(((x*DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2)  * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //double Distacesquared = Math.Sqrt(Math.Pow((x-(MousePosition.X - 350)) - CenterX, 2) * Math.Pow((y - (MousePosition.Y - 200) ) - CenterY, 2));
                    Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);
                    
                    if (Distacesquared1 %ModuloKey > 0.001 && Distacesquared1 %0.05 < 0.015)
                    {
                        
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 0)); // Green
                    }
                    //else if (Distacesquared1 %0.09 > 0.005 && Distacesquared1 % 0.09 < 0.01 * Controller)
                    //{
                    //    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 255));
                    //}


                    else if (Distacesquared1 > 0.02 && Distacesquared1 < 0.025)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255 / 2, 255)); // Pink
                    }
                    else if (Distacesquared1 % 0.06 > 0.025 && Distacesquared1 % 0.06 < 0.050)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 200 - (int)DimensionIterationController, 255)); // LightBlue
                    }
                    else if (Distacesquared1 % 0.08 > 0.055 && Distacesquared1 % 0.08 < 0.08)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 0, 0)); // Red
                    }

                    else
                    {
                        int rnd2 = new Random().Next(x / 50, 255);
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(rnd2, rnd2 / (1 + x), rnd2);
                        bitmap.SetPixel(x, y, color);
                    }
                }
            }
        }

        private async Task Fractal(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int CenterX = width / 2;
            int CenterY = height / 2;
            int Radius = width / 4;

            for (int y = 0; y < height; y++)
            {


                for (int x = 0; x < width; x++)
                {

                    //double nx = x * x - y * y + cx;
                    //double ny = 2.0 * x * y + cy;
                    //tx = nx;
                    //ty = ny;


                    double Distacesquared1 = Math.Sqrt(Math.Pow(x - CenterX, 2) + Math.Pow((y - RangeKey), 2));
                    Distacesquared1 = Distacesquared1 / (Radius * Radius);


                    //System.Drawing.Color color = System.Drawing.Color.FromArgb((int)Distacesquared1/4, (int)ModuloKey, (int)((1 + y) + Math.Pow((1 + y), 2)));




                    if (FillTheCirle)
                    {
                        if (/*Distacesquared1 <= ModuloKey*/ Distacesquared1 % StateMult <= ModuloKey && Distacesquared1 % StateMult >= (ModuloKey / 2))
                        {
                            bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 0, 0));
                        }
                        else
                        {
                            bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 0));
                        }
                    }
                    else
                    {
                        if (Distacesquared1 <= ModuloKey)
                        {
                            bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 0, 0));
                        }
                    }


                   



                }
            }
        }

            public void RegenerateRandomInstance()
            {

            }

            private BitmapSource ConvertToBitmapSource(Bitmap bitmap)
            {
                IntPtr hBitmap = bitmap.GetHbitmap();
                BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromRotation(Rotation.Rotate180));
                return bitmapSource;
            }

       
    }
}
