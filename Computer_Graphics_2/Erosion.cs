using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Computer_Graphics_2
{
    internal class Erosion
    {
        private static Bitmap _bitmap;
        int[,] kernel = new int[,]
        {
            {0,0,1,0,0},
            {0,1,1,1,0},
            {1,1,1,1,1},
            {0,1,1,1,0},
            {0,0,1,0,0}
        };

        public Erosion(BitmapImage bitmapImage)
        {
            _bitmap = BitmapImage2Bitmap(bitmapImage);
        }

        public ImageSource ErodeImage()
        {
            var srcImage = _bitmap;
            int width = srcImage.Width;
            int height = srcImage.Height;

            Rectangle canvas = new Rectangle(0, 0, width, height);
            BitmapData srcData = srcImage.LockBits(canvas, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            srcImage.UnlockBits(srcData);

            float rgb;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }
            int kernelSize = 3;
            int kernelOffset = (kernelSize - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 255;
                    byteOffset = y * srcData.Stride + x * 4;
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (kernel[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * srcData.Stride + xkernel * 4;
                                value = Math.Min(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            Bitmap result = new Bitmap(width, height);
            BitmapData resultData = result.LockBits(canvas, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, bytes);
            result.UnlockBits(resultData);
            return BitmapToImageSource(result);
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
