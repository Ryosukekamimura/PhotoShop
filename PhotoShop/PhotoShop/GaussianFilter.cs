using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShop
{
    class GaussianFilter
    {
        public static byte[,] LoadImageGray(string filename)
        {
            Bitmap img = new Bitmap(filename);
            int w = img.Width;
            int h = img.Height;
            byte[,] dst = new byte[w, h];

            //bitmapクラスの画像ピクセル値を配列に挿入
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    dst[j, i] = (byte)((img.GetPixel(j, i).R + img.GetPixel(j, i).G + img.GetPixel(j, i).B) / 3);


                }
            }
            return dst;
        }

        public static byte[,] Filter(byte[,] src, double[,] kernel)
        {
            int w = src.GetLength(0);
            int h = src.GetLength(1);

            int kernelSize = kernel.GetLength(0);

            //出力画像用の配列
            byte[,] dst = new byte[w, h];

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    double sum = 0;
                    for (int k = -kernelSize / 2; k <= kernelSize / 2; k++)
                    {
                        for (int n = -kernelSize / 2; n <= kernelSize / 2; n++)
                        {
                            if (j + n >= 0 && j + n < w && i + k >= 0 && i + k < h)
                            {
                                sum += src[j + n, i + k] * kernel[n + kernelSize / 2, k + kernelSize / 2];

                            }
                        }
                    }
                    dst[j, i] = Byte2Int(sum);

                }


            }
            return dst;
        }
        public static void SaveImagr(byte[,] src, string filename)
        {
            int w = src.GetLength(0);
            int h = src.GetLength(1);
            Bitmap img = new Bitmap(w, h);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    img.SetPixel(j, i, Color.FromArgb(src[j, i], src[j, i], src[j, i]));

                }
            }
            img.Save(filename);
            Console.WriteLine("完了しました。");


        }

        public static byte Byte2Int(double num)
        {
            if (num > 255.0) return 255;
            else if (num < 0) return 0;
            else return (byte)num;

        }



    }
}
