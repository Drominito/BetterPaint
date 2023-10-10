using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
                    int CursorX = (int)MousePosition.X * 100;
                    int CursorY = (int)MousePosition.Y * 100;


                    double Distacesquared1 = Math.Sqrt(Math.Pow(((x * DimensionIterationController) - (MousePosition.X - 350)) - CenterX, 2) * ZoomOutMult * (Controller) * 100 + Math.Pow(((y * DimensionIterationController) - (MousePosition.Y - 200)) - CenterY, 2) * ZoomOutMult * (Controller) * 100);
                    Distacesquared1 = Distacesquared1 / (Radius * Radius * GenerateZoom);

                    if (x  %(int)(ZoomOutMult / 50) == 0 + CursorX || y % (int)(ZoomOutMult / 50) == 0)
                    {
                        int rnd2 = new Random().Next(x / 50, 255);
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(rnd2, rnd2 / (1 + x), rnd2);
                        bitmap.SetPixel(x, y, color);
                    }
                    else if (x % (int)(ZoomOutMult / 50) == 1 + CursorX ||
                             y % (int)(ZoomOutMult / 50) == 1 
                             )
                             
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
        }
    }
}
