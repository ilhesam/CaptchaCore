using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CaptchaCore.Providers.ImageCreator
{
    public class CaptchaImageCreator : ICaptchaImageCreator
    {
        public virtual Bitmap Create(string captchaCode, int width, int height)
        {
            if (string.IsNullOrEmpty(captchaCode))
            {
                throw new ArgumentNullException(nameof(captchaCode));
            }

            if (width < 10 || width > 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < 10 || height > 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            using var baseMap = new Bitmap(width, height);
            using var graph = Graphics.FromImage(baseMap);

            var random = new Random();

            graph.Clear(GetRandomLightColor());

            DrawCaptchaCode();
            DrawDisorderLine();
            AdjustRippleEffect();

            return baseMap;

            int GetFontSize(int imageWidth, int codeLength)
            {
                var averageSize = imageWidth / codeLength;
                return Convert.ToInt32(averageSize);
            }

            Color GetRandomDeepColor()
            {
                var redLow = 160;
                var greenLow = 100;
                var blueLow = 160;
                return Color.FromArgb(random.Next(redLow), random.Next(greenLow), random.Next(blueLow));
            }

            Color GetRandomLightColor()
            {
                var low = 180;
                var high = 255;

                var nRend = random.Next(high) % (high - low) + low;
                var nGreen = random.Next(high) % (high - low) + low;
                var nBlue = random.Next(high) % (high - low) + low;

                return Color.FromArgb(nRend, nGreen, nBlue);
            }

            void DrawCaptchaCode()
            {
                var fontBrush = new SolidBrush(Color.Black);
                var fontSize = GetFontSize(width, captchaCode.Length);
                var font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                for (int i = 0; i < captchaCode.Length; i++)
                {
                    fontBrush.Color = GetRandomDeepColor();

                    var shiftPx = fontSize / 6;

                    float x = i * fontSize + random.Next(-shiftPx, shiftPx) + random.Next(-shiftPx, shiftPx);
                    var maxY = height - fontSize;

                    if (maxY < 0)
                    {
                        maxY = 0;
                    }

                    float y = random.Next(0, maxY);

                    graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                }
            }

            void DrawDisorderLine()
            {
                var linePen = new Pen(new SolidBrush(Color.Black), 3);

                for (int i = 0; i < random.Next(3, 5); i++)
                {
                    linePen.Color = GetRandomDeepColor();

                    var startPoint = new Point(random.Next(0, width), random.Next(0, height));
                    var endPoint = new Point(random.Next(0, width), random.Next(0, height));
                    graph.DrawLine(linePen, startPoint, endPoint);
                }
            }

            void AdjustRippleEffect()
            {
                var nWave = 6;
                var nWidth = baseMap.Width;
                var nHeight = baseMap.Height;

                var pt = new Point[nWidth, nHeight];

                for (int x = 0; x < nWidth; ++x)
                {
                    for (int y = 0; y < nHeight; ++y)
                    {
                        var xo = nWave * Math.Sin(2.0 * 3.1415 * y / 128.0);
                        var yo = nWave * Math.Cos(2.0 * 3.1415 * x / 128.0);

                        var newX = x + xo;
                        var newY = y + yo;

                        if (newX > 0 && newX < nWidth)
                        {
                            pt[x, y].X = (int)newX;
                        }
                        else
                        {
                            pt[x, y].X = 0;
                        }


                        if (newY > 0 && newY < nHeight)
                        {
                            pt[x, y].Y = (int)newY;
                        }
                        else
                        {
                            pt[x, y].Y = 0;
                        }
                    }
                }

                var bSrc = (Bitmap)baseMap.Clone();

                var bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                var bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                var scanLine = bitmapData.Stride;

                var scan0 = bitmapData.Scan0;
                var srcScan0 = bmSrc.Scan0;

                unsafe
                {
                    var p = (byte*)(void*)scan0;
                    var pSrc = (byte*)(void*)srcScan0;

                    var nOffset = bitmapData.Stride - baseMap.Width * 3;

                    for (int y = 0; y < nHeight; ++y)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            var xOffset = pt[x, y].X;
                            var yOffset = pt[x, y].Y;

                            if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                            {
                                if (pSrc != null)
                                {
                                    p[0] = pSrc[yOffset * scanLine + xOffset * 3];
                                    p[1] = pSrc[yOffset * scanLine + xOffset * 3 + 1];
                                    p[2] = pSrc[yOffset * scanLine + xOffset * 3 + 2];
                                }
                            }

                            p += 3;
                        }

                        p += nOffset;
                    }
                }

                baseMap.UnlockBits(bitmapData);
                bSrc.UnlockBits(bmSrc);
                bSrc.Dispose();
            }
        }
    }
}