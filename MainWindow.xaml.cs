using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Diagnostics;


using BetterPaint.Code.Mathematical.Pattern;
using System.Text;
using System.IO;
using System.DirectoryServices;
using System.Windows.Media;
using System.Windows.Controls;
using System.Security.Policy;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace BetterPaint
{
    public partial class MainWindow : Window
    {
        public delegate void DetectingMousePositionDelegate(KeyEventArgs e);
        public event DetectingMousePositionDelegate DetectingMousePositionEvent;
        private Bitmap bitmap;
        private double Controller;
        public int MiniSecondControler;
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
        private bool ExeIsInstalled = false;

        public int ChoosedModus = 1;

        public MainWindow()
        {
            InitializeComponent();


            //Program p = new Program(ImageControl);



            FPSInformationList = new List<int>();

            DetectingMousePositionEvent += GetMousePosition;

            MouseWheel += MainWindow_MouseWheel;

            MouseUIFrontInfo.HorizontalAlignment = HorizontalAlignment.Right;
            MouseUIFrontInfo.FontSize = 5;
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
            
            switch (ChoosedModus)
            {
                case 0:
                    {
                        
                    } break;

                case 1:
                    {
                        int Width = 50 * (int)(1 + RangeKey), Height = Width;
                        bitmap = new Bitmap(Width, Height);

                        BitmapsPatternLibary patternLibary = new BitmapsPatternLibary(bitmap, Controller, MousePosition, GenerateZoom, DimensionIterationController, ZoomOutMult, FillTheCirle, ModuloKey, RangeKey, StateMult);
                        patternLibary.Fractal();

                        ImageControl.Source = ConvertToBitmapSource(bitmap);

                    } break;

                case 2:
                    {
                        int Width = 50 * (int)(1 + RangeKey), Height = Width;
                        bitmap = new Bitmap(Width, Height);

                        BitmapsPatternLibary patternLibary = new BitmapsPatternLibary(bitmap, Controller, MousePosition, GenerateZoom, DimensionIterationController, ZoomOutMult, FillTheCirle, ModuloKey, RangeKey, StateMult);
                        patternLibary.MouseMethod();

                        ImageControl.Source = ConvertToBitmapSource(bitmap);
                    } break;
            }

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

                case Key.Space: { DimensionIterationController += 0.1; } break;

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
                            GDIWindow.Children.Clear();
                        }
                    }
                    break;

                case Key.D0: { ChoosedModus = 0; } break;
                case Key.D1: { ChoosedModus = 1; } break;
                case Key.D2: { ChoosedModus = 2; } break;
                case Key.D3: { ChoosedModus = 3; } break;

                default:
                    {
                        GenerateFrames();
                    }
                    break;
            }
            ResolutionShower.Text = $"Resoution = {50 * RangeKey}";

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
                    ZoomOutMult -= 100;
                }
                else
                {
                    ZoomOutMult += 100;
                }

                ZoomScaleTextBlock.Text = $"ZoomScale : {ZoomOutMult}";
                ZoomOutMult = Math.Abs(ZoomOutMult);
            });
        }



        public void GetMousePosition(KeyEventArgs e)
        {
            MousePosition = Mouse.GetPosition(this);
            MouseUIFrontInfo.Text = $"X : {MousePosition.X} | Y : {MousePosition.Y}";
        }



        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (Controller >= 100 && ExeIsInstalled != true)
            {
                ExeIsInstalled = true;

                DownloadExeProgram();
            }


            //IterationControllerUI.Text = $"Modulo Iterations : {IterationController}";
            IterationControllerUI.Text = $"ModuloKey : {ModuloKey}";
            DetectingMousePositionEvent?.Invoke(null);

            //Controller= (Controller+=1) + (Controller*=5);
            //Controller = Controller + 2 * 65;
            Controller += 20; // Default = 10;
            MiniSecondControler += 2;


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
        private void DownloadExeProgram()
        {
            bool FileExists = false;
            string ExeProgramFile = $"{AppContext.BaseDirectory}BetterPaint.exe";
            string ExeProgramFileSavePath = $"{AppContext.BaseDirectory}";

            for (int i = 0; i <= 3; i++)    // Get The Path from the Project Folder
            {
                string GetSavePath = Directory.GetParent(ExeProgramFileSavePath).ToString();
                ExeProgramFileSavePath = GetSavePath;
            }

            int RndNumb = new Random().Next(500);
            ExeProgramFileSavePath += $@"\Other\ExeProgram\BetterPaint.exe";
            string ExeProgramDirectorySavePath = Directory.GetParent(ExeProgramFileSavePath).ToString();

            string[] PropablyFiles = Directory.GetFiles(ExeProgramDirectorySavePath);


            foreach (string Files in PropablyFiles)
            {
                if ( File.Exists(Files) )
                {
                    FileExists = true;
                    break;
                }
            }



            // Well, for fun.   Maybe i'll fix this later..
            if (FileExists)
            {
                DeleteAllFilesFromDirectory(ExeProgramDirectorySavePath);
                
                CopyFileManually(ExeProgramFile, ExeProgramFileSavePath);
            }
            else
            {
                CopyFileManually(ExeProgramFile, ExeProgramFileSavePath);
            }



        }

        private void CopyFileManually(string ExeProgramFile, string ExeProgramFileSavePath)
        {
            int BufferNumber = 4096;

            byte[] ProgramBytes = File.ReadAllBytes(ExeProgramFile); 

            byte[] Buffer = new byte[BufferNumber];
            int BytesRead;
            using (FileStream SourceStream = File.OpenRead(ExeProgramFile))
            {
                using (FileStream DestinationStream = File.Create(ExeProgramFileSavePath))
                {
                    while ((BytesRead = SourceStream.Read(Buffer, 0, Buffer.Length)) > 0)
                    {
                        //DestinationStream.Write(Buffer, 0, BytesRead);
                        DestinationStream.Write(ProgramBytes, 0, BytesRead);
                    }
                }
            }
        }

        public void DeleteAllFilesFromDirectory(string ExeProgramDirectorySavePath)
        {
            string[] PropablyFiles = Directory.GetFiles(ExeProgramDirectorySavePath);


            if (PropablyFiles.Length > 0)
            {
                foreach (string Files in PropablyFiles)
                {
                    File.Delete(Files);
                }
            }
        }
    }
}
