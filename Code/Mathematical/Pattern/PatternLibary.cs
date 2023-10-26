using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Color = System.Drawing.Color;

namespace BetterPaint.Code.Mathematical.Pattern
{
    class PatternLibary
    {
        //public class PatternLibaryGhost
        //{
        //    public PatternLibary()
        //    {
                
        //    }
        //}

        public Bitmap bitmap                      { get; set; }
        public double Controller                  { get; set; }
        public System.Windows.Point MousePosition { get; set; }
        public int GenerateZoom                   { get; set; }
        public double DimensionIterationController   { get; set; }

        public double ZoomOutMult                 { get; set; }
        public bool FillTheCirle                  { get; set; }
        public double ModuloKey                   { get; set; }
        public double RangeKey                       { get; set; }
        public double StateMult                   { get; set; }

        public PatternLibary( Bitmap bitmap, double controller, System.Windows.Point mouseposition,
                              int generatezoom, double dimensioniterationcontroller, double zoomoutmult)
        {
            this.bitmap   = bitmap;
            Controller    = controller;
            MousePosition = mouseposition;
            GenerateZoom = generatezoom;
            DimensionIterationController = dimensioniterationcontroller;
            ZoomOutMult = zoomoutmult;
            
        }



        public PatternLibary( Bitmap bitmap, double controller, bool fillthecircle,
                              double modulokey, double rangekey, double statemult)
        {
            this.bitmap  = bitmap;
            Controller   = controller;
            FillTheCirle = fillthecircle;
            ModuloKey    = modulokey;
            RangeKey     = rangekey;
            StateMult = statemult;
        }



        
        public PatternLibary( Bitmap bitmap, double controller, System.Windows.Point mouseposition,
                              int generatezoom, double dimensioniterationcontroller,
                              double zoomoutmult,bool fillthecircle, double modulokey, double rangekey, double statemult )
        {
            this.bitmap = bitmap;
            Controller = controller;
            MousePosition = mouseposition;
            GenerateZoom = generatezoom;
            DimensionIterationController = dimensioniterationcontroller;
            ZoomOutMult = zoomoutmult;

            FillTheCirle = fillthecircle;
            ModuloKey = modulokey;
            RangeKey = rangekey;
            StateMult = statemult;
        }




        public void ModuloCircle()
        {
            int Width = bitmap.Width, Height = bitmap.Height;

            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width / 4;

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {

                    //double nx = x * x - y * y + cx;
                    //double ny = 2.0 * x * y + cy;
                    //tx = nx;
                    //ty = ny;


                    double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //double Distacesquared = Math.Sqrt(Math.Pow((x - (MousePosition.X - 350)) - CenterX, 2) * Math.Pow((y - (MousePosition.Y - 200)) - CenterY, 2));
                    Distacesquared1 = Distacesquared1 / (Radius * Radius * Radius * GenerateZoom);
                    //Distacesquared1 /= Radius;

                    //if (x*y %3 == new Random().Next((x*y / 50000) + (y*x / 50000)))
                    //{
                    //}
                    if (Distacesquared1 % ModuloKey > 0.001 && Distacesquared1 % 0.05 < 0.015)
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
            
        public void ModuloCircle2()
        {
            int Width = bitmap.Width, Height = bitmap.Height;

            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width / 4;



            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {

                    //double nx = x * x - y * y + cx;
                    //double ny = 2.0 * x * y + cy;
                    //tx = nx;
                    //ty = ny;


                    double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //double Distacesquared = Math.Sqrt(Math.Pow((x-(MousePosition.X - 350)) - CenterX, 2) * Math.Pow((y - (MousePosition.Y - 200) ) - CenterY, 2));
                    Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (x+Controller / y*Controller != 0            && x > 5 + (Controller/500) && y > 1)
                    {

                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 255)); // Green
                    }
                    else if (x+Controller / y+Controller == 0)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, 255, 255));

                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(255, 0 , 0));
                    }
                }
            }
        }

        
        public void QRCodeStar()
        {
            int Width = bitmap.Width, Height = bitmap.Height;

            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width / 4;

            //await Task.Run(() =>
            //{
            //});
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    double Distacesquared = /*Math.Sqrt*/Math.Pow(x - CenterX, 4) + Math.Pow(y - CenterY, 4) * 16;
                    //Distacesquared = Distacesquared1 / (Radius * Radius);


                    if (Distacesquared <= (Radius * Radius))
                    {
                            bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 0));
                    }
                    else if (x / y == 0)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 0));

                    }

                }   
            }
        }


        public async Task Fractal()
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int CenterX = width / 2;
            int CenterY = height / 2;
            int Radius = width / 4;

            await Task.Run(() =>
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {


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
            });
        }

        public void Grid()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width / 4;

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X / (int)((225) * (ZoomOutMult * 100));
                    int CursorY = (int)MousePosition.Y / (int)((225) * (ZoomOutMult * 100));
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    int MinXX = Math.Min(255, x * x);
                    int MinYY = Math.Min(255, y * y);
                    int MinXY = Math.Min(255, x * y) ;
                    if ( x > 0 && y > 0 )
                    {
                        if (x %ScalingRenderFactor / GridThickness == CursorX || y %ScalingRenderFactor / GridThickness == CursorY)
                        {
                            int rnd2 = new Random().Next(x / 50, 255);
                            Color color = Color.White;
                            //color = System.Drawing.Color.FromArgb(rnd2, rnd2 / x, rnd2);
                            //color = System.Drawing.Color.FromArgb(MinXX, MinYY / MinXX, MinXY);
                            bitmap.SetPixel(x, y, color);
                        }
                        else if (x % (int)(ZoomOutMult) == 1 + CursorX ||
                                 y % (int)(ZoomOutMult) == 1 + CursorY
                                 )

                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                    }
                }
            }
        }

        
        public void Cell()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width * 8;

            

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X ;
                    int CursorY = (int)MousePosition.Y ;
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    double Amplitude = Math.PI * Math.PI;
                    double Speed = Math.Pow(10, 2) ;
                    double AdvancedDistanceSquare = (Math.Pow(Math.Tanh((Controller / Speed) + (x / Amplitude)) + x - (CenterX - CursorX), 2) + Math.Pow(Math.Cos((Controller / Speed) + (y / Amplitude)) + y - (CenterY - CursorY), 2));
                    AdvancedDistanceSquare = AdvancedDistanceSquare + (Math.Cos(AdvancedDistanceSquare/(100*RangeKey)) * 1000);


                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (x > 0 && y > 0)
                    {
                        //if (AdvancedDistanceScuared <= Radius)
                        //{
                        //    bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                        //}
                        if (AdvancedDistanceSquare >= Radius / /*x+x*y+y/*/(ModuloKey * 10)/* || AdvancedDistanceSquare <= Radius / (x + x * y + y / 4)*/)
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 255));

                        }
                        //if (AdvancedDistanceSquare <= Radius / (x + x * y + y / 4))
                        //{
                        //    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                        //}
                        
                    }
                }
            }
        }
        public void MathMonsterICenterCreepy()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width * 8;

            

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X ;
                    int CursorY = (int)MousePosition.Y ;
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    double Amplitude = Math.PI * Math.PI;
                    double Speed = Math.Pow(10, 2) ;
                    int MiniDistanceSquared = (int)Math.Sqrt(x * x + y * y);
                    double AdvancedDistanceSquare = (Math.Pow(Math.Sin((Controller / Speed) + (x / Amplitude)) + x - (CenterX - CursorX), 2) + Math.Pow(Math.Cos((Controller / Speed) + (y / Amplitude)) + y - (CenterY - CursorY), 2));
                    AdvancedDistanceSquare = AdvancedDistanceSquare + (Math.Sin(AdvancedDistanceSquare / (100 *  RangeKey)) * 500*(ModuloKey*10) /** (ZoomOutMult/20)*/);
                    

                    //AdvancedDistanceSquare = AdvancedDistanceSquare + (ZoomOutMult* ZoomOutMult* ZoomOutMult);

                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (x > 0 && y > 0)
                    {
                        int AntiAliasing = (int)(180 * Math.Sin(AdvancedDistanceSquare / Math.Pow(10, (ZoomOutMult/4))) / StateMult);
                        AntiAliasing *= 2;
                        if (/*x %ZoomOutMult/4 == 0 && y %ZoomOutMult/4 == 0*/ 0 == 0)
                        {
                            //if (AdvancedDistanceScuared <= Radius)
                            //{
                            //    bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                            //}
                            if (AdvancedDistanceSquare >= Radius + y*y)
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(0, 0 , 50));
                                AntiAliasing *= 50;

                            }

                            if (AdvancedDistanceSquare <= Radius / ((x * x + Math.Sin(y * y*DimensionIterationController))) / 4) // Roter Kreis -> Auge
                            {
                                try
                                {
                                    if (AntiAliasing < 0)   { AntiAliasing = 0; }
                                    if (AntiAliasing > 255) { AntiAliasing = 255; }

                                    bitmap.SetPixel(x, y, Color.FromArgb(AntiAliasing, 0, 0));
                                    AntiAliasing++;
                                }
                                catch
                                {
                                    
                                }
                            }



                            //else
                            //{
                            //    int rnd_r = new Random().Next(50);
                            //    int rnd_g = new Random().Next((50 + (rnd_r)));
                            //    int rnd_b = new Random().Next(50 + (rnd_g + rnd_r));
                            //    bitmap.SetPixel(x, y, Color.FromArgb(rnd_r, rnd_g, rnd_b));
                            //}
                        }
                        else
                        {
                            AntiAliasing /= 2;
                        }
                        
                    }
                }
            }
        }
        public void UnderstandingPythagoras()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width * 8;

            

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X ;
                    int CursorY = (int)MousePosition.Y ;
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    double Amplitude = Math.PI * Math.PI;
                    double Speed = Math.Pow(10, 2) ;
                    int MiniDistanceSquared = (int)Math.Sqrt(x * x + y * y);
                    double AdvancedDistanceSquare = (Math.Pow(Math.Sin((Controller / Speed) + (x / Amplitude)) + x - (CenterX - CursorX), 2)
                            
                                                   + Math.Pow(Math.Cos((Controller / Speed) + (y / Amplitude)) + y - (CenterY - CursorY), 2));

                    AdvancedDistanceSquare += AdvancedDistanceSquare + (Math.Sin(AdvancedDistanceSquare / (100 * RangeKey)) * 500 * (ModuloKey * 10) /** (ZoomOutMult/20)*/);


                    //AdvancedDistanceSquare = AdvancedDistanceSquare + (ZoomOutMult* ZoomOutMult* ZoomOutMult);

                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (x > 0 && y > 0)
                    {
                            //if (AdvancedDistanceScuared <= Radius)
                            //{
                            //    bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                            //}
                            //if (AdvancedDistanceSquare >= Radius + y*y)
                            //{
                            //    bitmap.SetPixel(x, y, Color.FromArgb(0, 0 , 50));

                            //}

                            //if (AdvancedDistanceSquare / Math.Pow(y*y*x*x, (0.5))<= Radius / ( ( x * x + Math.Sin(y * y / 100) * 100 ) / 4) ) // Roter Kreis -> Auge
                            //{
                            //    try
                            //    {

                            //        bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                            //    }
                            //    catch
                            //    {
                                    
                            //    }
                            //}
                            if (Math.Sqrt(Math.Pow(x - (/*CenterX +*/ CursorX), 2) * Math.Pow(y - (/*CenterY +*/ CursorY), 2) - ZoomOutMult) <= ((y * x + Math.Sin(y *y)))) // Roter Kreis -> Auge
                           {
                                try
                                {

                                    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                                }
                                catch
                                {
                                    
                                }
                            }



                        
                    }
                }
            }
        }
        public void MathMonsterIFunny()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width * 8;
            bool RandomColorfulImage = false;
            int RndColorfulImageINt = new Random().Next(2);
            
            if (RndColorfulImageINt == 2)
            {
                RandomColorfulImage = true;
            }

            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X ;
                    int CursorY = (int)MousePosition.Y ;
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    double Amplitude = Math.PI * Math.PI;
                    double Speed = Math.Pow(10, 2) ;
                    int MiniDistanceSquared = (int)Math.Sqrt(x * x + y * y);
                    double AdvancedDistanceSquare = (Math.Pow(Math.Sin((Controller / Speed) + (x / Amplitude)) + x - (CenterX - CursorX), 2)
                            
                                                   + Math.Pow(Math.Cos((Controller / Speed) + (y / Amplitude)) + y - (CenterY - CursorY), 2));

                    AdvancedDistanceSquare += AdvancedDistanceSquare + (Math.Sin(AdvancedDistanceSquare / (100 * RangeKey)) * 500 * (ModuloKey * 10) /** (ZoomOutMult/20)*/);


                    //AdvancedDistanceSquare = AdvancedDistanceSquare + (ZoomOutMult* ZoomOutMult* ZoomOutMult);

                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (RandomColorfulImage)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb((int)Math.Sqrt(x * x + y * y), 255 - (int)Math.Sqrt(x * x + y * y), (int)Math.Sin(Math.Sqrt(x * x + y * y))));
                    }
                    else
                    {
                        if (x > 0 && y > 0)
                        {
                            //if (AdvancedDistanceScuared <= Radius)
                            //{
                            //    bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                            //}
                            //if (AdvancedDistanceSquare >= Radius + y*y)
                            //{
                            //    bitmap.SetPixel(x, y, Color.FromArgb(0, 0 , 50));

                            //}

                            //if (AdvancedDistanceSquare / Math.Pow(y*y*x*x, (0.5))<= Radius / ( ( x * x + Math.Sin(y * y / 100) * 100 ) / 4) ) // Roter Kreis -> Auge
                            //{
                            //    try
                            //    {

                            //        bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                            //    }
                            //    catch
                            //    {

                            //    }
                            //}
                            if (Math.Sqrt(Math.Pow(x - (/*CenterX +*/ CursorX), 2) + Math.Pow(y - (/*CenterY +*/ CursorY), 2) - ZoomOutMult) >= Radius / (x * x - (ZoomOutMult * 100))) // Roter Kreis -> Auge
                            {
                                try
                                {

                                    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                                }
                                catch
                                {

                                }
                            }

                            if (Math.Sqrt(Math.Pow(x - (/*CenterX +*/ CursorX), 2) + Math.Pow(y - (/*CenterY +*/ CursorY), 2) - ZoomOutMult) >= Radius / (x * y)) // Roter Kreis -> Auge
                            {
                                try
                                {

                                    bitmap.SetPixel(x, y, Color.FromArgb(255, 0, 255));
                                }
                                catch
                                {

                                }
                            }
                            if (Math.Sqrt(Math.Pow(x - (/*CenterX +*/ CursorX), 2) + Math.Pow(y - (/*CenterY +*/ CursorY), 2) - ZoomOutMult) >=  Radius / (x * y) + Math.Sin(x*x))
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(255, 255, (Math.Min(256, 255 -  (int)(Math.Sqrt(x*x+y*y + Math.Sin(x+y/100)*10))/(2)))));

                            }





                        }
                    }


                    
                }
            }
        }
        

        public void SimpleCube()
        {
            int Width = bitmap.Width, Height = bitmap.Height;
            int CenterX = Width / 2, CenterY = Height / 2;
            int Radius = Width * 8;
            double Gravitation = 0;


            for (int y = 0; y < Height; y++)
            {


                for (int x = 0; x < Width; x++)
                {
                    int CursorX = (int)MousePosition.X / (int)((225) * (ZoomOutMult * 100));
                    int CursorY = (int)MousePosition.Y / (int)((225) * (ZoomOutMult * 100));
                    int ScalingRenderFactor = (int)ZoomOutMult / 5;
                    double GridThickness = ModuloKey;

                    

                    //double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    //Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    
                    if (x > 0 && y > 0)
                    {
                        if (x + MousePosition.X <= CenterX  && x + MousePosition.X >= CenterX/2 )
                        {
                            if (y - (Gravitation / 4) + MousePosition.Y <= CenterY  && y + MousePosition.Y - Gravitation >= CenterY / 2)
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 255));
                            }
                            else
                            {
                                bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                            }
                        }
                        else
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                        }
                    }
                    if (MousePosition.Y < 600)
                    {
                        Gravitation = Gravitation - (Controller/10000000);
                    }
                }
            }
        }

    }
}
