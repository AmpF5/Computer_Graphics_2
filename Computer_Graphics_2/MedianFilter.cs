using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Computer_Graphics_2
{
    internal class MedianFilter
    {
        private static Bitmap _bitmap;

        public MedianFilter(BitmapImage bitmapImage)
        {
            _bitmap = BitmapImage2Bitmap(bitmapImage);
        }
        public ImageSource Filtr()
        {
            //int w = _bitmap.Width;
            //int h = _bitmap.Height;
            //for (int x = 0; x < h; x++)
            //{
            //    for (int y = 0; y < w; y++)
            //    {
            //        int i = y;
            //        int j = x;
            //        byte[] medianArr = new byte[9];
            //        Color[] colorsArr = new Color[9];
            //        medianArr[0] = (byte)((_bitmap.GetPixel(i, j).R + _bitmap.GetPixel(i, j).G + _bitmap.GetPixel(i, j).B) / 3);
            //        colorsArr[0] = _bitmap.GetPixel(i, j);


            //        if (i - 1 < 0 || j - 1 < 0)
            //            medianArr[1] = 0;
            //        else
            //        {
            //            medianArr[1] = (byte)((_bitmap.GetPixel(i - 1, j - 1).R + _bitmap.GetPixel(i - 1, j - 1).G + _bitmap.GetPixel(i - 1, j - 1).B) / 3); colorsArr[1] = _bitmap.GetPixel(i - 1, j - 1);
            //        }

            //        if (i - 1 < 0)
            //            medianArr[2] = 0;
            //        else
            //        {
            //            medianArr[2] = (byte)((_bitmap.GetPixel(i - 1, j).R + _bitmap.GetPixel(i - 1, j).G + _bitmap.GetPixel(i - 1, j).B) / 3); colorsArr[2] = _bitmap.GetPixel(i - 1, j);
            //        }

            //        if (i - 1 < 0 || j + 1 > _bitmap.Width - 1)
            //            medianArr[3] = 0;
            //        else
            //        {
            //            medianArr[3] = (byte)((_bitmap.GetPixel(i - 1, j + 1).R + _bitmap.GetPixel(i - 1, j + 1).G + _bitmap.GetPixel(i - 1, j + 1).B) / 3); colorsArr[3] = _bitmap.GetPixel(i - 1, j + 1);
            //        }

            //        if (j - 1 < 0)
            //            medianArr[4] = 0;
            //        else
            //        {
            //            medianArr[4] = (byte)((_bitmap.GetPixel(i, j - 1).R + _bitmap.GetPixel(i, j - 1).G + _bitmap.GetPixel(i, j - 1).B) / 3); colorsArr[4] = _bitmap.GetPixel(i, j - 1);
            //        }

            //        if (j + 1 > _bitmap.Width - 1)
            //            medianArr[5] = 0;
            //        else
            //        {
            //            medianArr[5] = (byte)((_bitmap.GetPixel(i, j + 1).R + _bitmap.GetPixel(i, j + 1).G + _bitmap.GetPixel(i, j + 1).B) / 3); colorsArr[5] = _bitmap.GetPixel(i, j + 1);
            //        }

            //        if (i + 1 > _bitmap.Height - 1 || j - 1 < 0)
            //            medianArr[6] = 0;
            //        else
            //        {
            //            medianArr[6] = (byte)((_bitmap.GetPixel(i + 1, j - 1).R + _bitmap.GetPixel(i + 1, j - 1).G + _bitmap.GetPixel(i + 1, j - 1).B) / 3); colorsArr[6] = _bitmap.GetPixel(i + 1, j - 1);
            //        }

            //        if (i + 1 > _bitmap.Height - 1)
            //            medianArr[7] = 0;
            //        else
            //        {
            //            medianArr[7] = (byte)((_bitmap.GetPixel(i + 1, j).R + _bitmap.GetPixel(i + 1, j).G + _bitmap.GetPixel(i + 1, j).B) / 3); colorsArr[7] = _bitmap.GetPixel(i + 1, j);
            //        }

            //        if (i + 1 > _bitmap.Height - 1 || j + 1 > _bitmap.Width - 1)
            //            medianArr[8] = 0;
            //        else
            //        {
            //            medianArr[8] = (byte)((_bitmap.GetPixel(i + 1, j + 1).R + _bitmap.GetPixel(i + 1, j + 1).G + _bitmap.GetPixel(i + 1, j + 1).B) / 3); colorsArr[8] = _bitmap.GetPixel(i + 1, j + 1);
            //        }

            //        //medianArr.ToList().IndexOf(Median(medianArr));
            //        var medianValue = Median(medianArr);
            //        var colorIndex = medianArr.ToList().IndexOf(medianValue);
            //        var newColor = colorsArr[colorIndex];
            //        _bitmap.SetPixel(i, j ,newColor);
            //        //_bitmap.SetPixel(i, j, colorsArr[medianArr.ToList().IndexOf(Median(medianArr))]);
            //        //Bitmap res_img = new Bitmap(w, h);
            //        //BitmapData res_data = res_img.LockBits(
            //        //    new Rectangle(0, 0, w, h),
            //        //    ImageLockMode.WriteOnly,
            //        //    PixelFormat.Format24bppRgb);
            //        //Marshal.Copy(_bitmap.Arr, 0, res_data.Scan0, h*w);
            //        //Marshal.Copy()
            //        //res_img.UnlockBits(res_data);

            //    }
            //}
            //return BitmapToImageSource(_bitmap);
            var image = _bitmap;
            int w = image.Width;
            int h = image.Height;

            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);
            int r = 1;
            int wres = w - 2 * r;
            int hres = h - 2 * r;

            Bitmap result_image = new Bitmap(wres, hres);
            BitmapData result_data = result_image.LockBits(
                new Rectangle(0, 0, wres, hres),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            int res_bytes = result_data.Stride * result_data.Height;
            byte[] result = new byte[res_bytes];

            for (int x = r; x < w - r; x++)
            {
                for (int y = r; y < h - r; y++)
                {
                    int pixel_location = x * 3 + y * image_data.Stride;
                    int res_pixel_loc = (x - r) * 3 + (y - r) * result_data.Stride;
                    double[] median = new double[3];
                    byte[][] neighborhood = new byte[3][];

                    for (int c = 0; c < 3; c++)
                    {
                        neighborhood[c] = new byte[(int)Math.Pow(2 * r + 1, 2)];
                        int added = 0;
                        for (int kx = -r; kx <= r; kx++)
                        {
                            for (int ky = -r; ky <= r; ky++)
                            {
                                int kernel_pixel = pixel_location + kx * 3 + ky * image_data.Stride;
                                neighborhood[c][added] = buffer[kernel_pixel + c];
                                added++;
                            }
                        }
                    }

                    for (int c = 0; c < 3; c++)
                    {
                        //result[res_pixel_loc + c] = (byte)(neighborhood[c].median());
                        result[res_pixel_loc + c] = (byte)(Median(neighborhood[c]));
                    }
                }
            }

            Marshal.Copy(result, 0, result_data.Scan0, res_bytes);
            result_image.UnlockBits(result_data);
            return BitmapToImageSource(result_image);
        }
        public static byte Median(byte[] a)
        {
            Array.Sort(a);
            return (byte)(a.Length % 2 !=  0 ? a[(a.Length + 1) / 2 - 1] : (a[(a.Length / 2) - 1] + a[(a.Length / 2) + 1] / 2));
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        private static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
