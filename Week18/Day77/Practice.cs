//블러링 실습
using OpenCvSharp;

namespace Blurring01
{
    internal class Program
    {
        static void Filter(Mat img, out Mat dst, Mat mask)
        {
            dst = new Mat(img.Size(), MatType.CV_32F, Scalar.All(0));
            Point h_m = new Point(mask.Width / 2, mask.Height / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int k = h_m.X; k < img.Cols - h_m.X; k++)
                {
                    float sum = 0;
                    for (int u = 0; u < mask.Rows; u++)
                    {
                        for (int v = 0; v < mask.Cols; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = k + v - h_m.X;
                            sum += mask.At<float>(u, v) * img.At<Vec3b>(y, x)[0];  // 그레이스케일 단순화
                        }
                    }
                    dst.Set<float>(i, k, sum);
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("C:\\Temp\\opencv\\filter_blur.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 로드할 수 없습니다.");
                return;
            }

            float[] data1 =
            {
                1/9f, 1/9f, 1/9f,
                1/9f, 1/9f, 1/9f,
                1/9f, 1/9f, 1/9f
            };
            float[] data2 =
            {
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f,
                1/25f, 1/25f, 1/25f, 1/25f, 1/25f
            };

            //Mat mask = new Mat(3, 3, MatType.CV_32F, data1); //Error
            Mat mask = new Mat(5, 5, MatType.CV_32F);

            for (int i = 0; i < mask.Rows; i++)
            {
                for (int k = 0; k < mask.Cols; k++)
                {
                    mask.Set<float>(i, k, data2[i * mask.Cols + k]);
                }
            }

            Filter(image, out Mat blur, mask);

            blur.ConvertTo(blur, MatType.CV_8U);

            Cv2.ImShow("image", image);
            Cv2.ImShow("blur", blur);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//가우시안 블러
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluringDirect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_blur.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("이미지 문제발생");

            Mat blur = new Mat();

            // OpenCV 함수인 GaussianBlur를 사용하여 블러링 처리
            Cv2.GaussianBlur(src, blur, new OpenCvSharp.Size(5, 5), 0); //Size 함수의 3을 5로 바꾸면 5 * 5 필터가 되고 1/25f가 입력됨

            Cv2.ImShow("src", src);
            Cv2.ImShow("blur", blur);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//샤프닝 실습
using OpenCvSharp;

namespace Sharpening01
{
    internal class Program
    {
        // 회선함수 (Filter) 동일
        static void Filter(Mat img, out Mat dst, Mat mask)
        {
            dst = new Mat(img.Size(), MatType.CV_32F, Scalar.All(0));
            Point h_m = new Point(mask.Width / 2, mask.Height / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int k = h_m.X; k < img.Cols - h_m.X; k++)
                {
                    float sum = 0;
                    for (int u = 0; u < mask.Rows; u++)
                    {
                        for (int v = 0; v < mask.Cols; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = k + v - h_m.X;
                            sum += mask.At<float>(u, v) * img.At<byte>(y, x);
                        }
                    }
                    dst.Set<float>(i, k, sum);
                }
            }
        }

        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/filter_sharpen.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("Failed to load image");

            float[] data1 =
            {
                0, -1, 0,
                -1, 5, -1,
                0, -1, 0
            };

            float[] data2 =
            {
                -1, -1, -1,
                -1, 9, -1,
                -1, -1, -1
            };

            Mat mask1 = new Mat(3, 3, MatType.CV_32F);
            Mat mask2 = new Mat(3, 3, MatType.CV_32F);

            // data1 값을 mask1에 설정
            for (int i = 0; i < mask1.Rows; i++)
            {
                for (int j = 0; j < mask1.Cols; j++)
                {
                    mask1.Set<float>(i, j, data1[i * mask1.Cols + j]);
                }
            }

            // data2 값을 mask2에 설정
            for (int i = 0; i < mask2.Rows; i++)
            {
                for (int j = 0; j < mask2.Cols; j++)
                {
                    mask2.Set<float>(i, j, data2[i * mask2.Cols + j]);
                }
            }

            Filter(src, out Mat sharpen1, mask1);
            Filter(src, out Mat sharpen2, mask2);

            sharpen1.ConvertTo(sharpen1, MatType.CV_8U);
            sharpen2.ConvertTo(sharpen2, MatType.CV_8U);

            Cv2.ImShow("sharpen1", sharpen1);
            Cv2.ImShow("sharpen2", sharpen2);
            Cv2.ImShow("src", src);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//로버츠 마스크 실습
using System;
using OpenCvSharp;

namespace RobertsMask
{
    internal class Program
    {
        static void DifferOp(Mat img, out Mat dst, int maskSize)
        {
            dst = new Mat(img.Size(), MatType.CV_8U, Scalar.All(0));
            Point h_m = new Point(maskSize / 2, maskSize / 2);

            for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
            {
                for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                {
                    List<byte> mask = new List<byte>();

                    for (int u = 0; u < maskSize; u++)
                    {
                        for (int v = 0; v < maskSize; v++)
                        {
                            int y = i + u - h_m.Y;
                            int x = j + v - h_m.X;
                            mask.Add(img.At<byte>(y, x));
                        }
                    }

                    byte max = 0;
                    for (int k = 0; k < mask.Count / 2; k++)
                    {
                        int start = mask[k];
                        int end = mask[mask.Count - k - 1];

                        byte difference = (byte)Math.Abs(start - end);
                        if (difference > max) max = difference;
                    }

                    dst.Set<byte>(i, j, max);
                }
            }
        }

        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/edge_test.jpg";
            Mat src = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (src.Empty())
                throw new Exception("Failed to load image");

            Mat edge;
            DifferOp(src, out edge, 3);

            // 결과 출력
            Cv2.ImShow("src", src);
            Cv2.ImShow("edge", edge);

            Cv2.WaitKey(); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------
//소벨(Sobel) 마스크 실습
using OpenCvSharp;

namespace SobelMask
{
    internal class Program
    {
        static void Differential(Mat image, out Mat dst, float[] data1, float[] data2)
        {
            Mat mask1 = new Mat(3, 3, MatType.CV_32F);
            Mat mask2 = new Mat(3, 3, MatType.CV_32F);

            // data1 값을 mask1에 설정
            for (int i = 0; i < mask1.Rows; i++)
            {
                for (int j = 0; j < mask1.Cols; j++)
                {
                    mask1.Set<float>(i, j, data1[i * mask1.Cols + j]);
                }
            }

            // data2 값을 mask2에 설정
            for (int i = 0; i < mask2.Rows; i++)
            {
                for (int j = 0; j < mask2.Cols; j++)
                {
                    mask2.Set<float>(i, j, data2[i * mask2.Cols + j]);
                }
            }

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            dst = new Mat();

            Cv2.Filter2D(image, dst1, MatType.CV_32F, mask1);  // OpenCV 제공 회선 함수
            Cv2.Filter2D(image, dst2, MatType.CV_32F, mask2);
            Cv2.Magnitude(dst1, dst2, dst);
            dst.ConvertTo(dst, MatType.CV_8U);

            Cv2.ConvertScaleAbs(dst1, dst1);  // 절대값 및 형변환 동시 수행 함수
            Cv2.ConvertScaleAbs(dst2, dst2);
            Cv2.ImShow("dst1 - 수직 마스크", dst1);
            Cv2.ImShow("dst2 - 수평 마스크", dst2);
        }

        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/edge_test1.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("Failed to load image");

            float[] data1 =
            {
                -1, 0, 1,
                -2, 0, 2,
                -1, 0, 1
            };
            float[] data2 =
            {
                -1, -2, -1,
                0, 0, 0,
                1, 2, 1
            };

            Mat dst;
            Differential(image, out dst, data1, data2);  // 두 방향 소벨 회선 및 크기 계산

            // OpenCV 제공 소벨 에지 계산
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();

            Cv2.Sobel(image, dst3, MatType.CV_32F, 1, 0, 3);  // x방향 미분 - 수직 마스크
            Cv2.Sobel(image, dst4, MatType.CV_32F, 0, 1, 3);  // y방향 미분 - 수평 마스크
            Cv2.ConvertScaleAbs(dst3, dst3);  // 절대값 및 uchar 형변환
            Cv2.ConvertScaleAbs(dst4, dst4);

            Cv2.ImShow("image", image);
            Cv2.ImShow("소벨에지", dst);
            Cv2.ImShow("dst3-수직_OpenCV", dst3);
            Cv2.ImShow("dst4-수평_OpenCV", dst4);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//LoG & DoG
using OpenCvSharp;

namespace LoGDoGTest
{
    internal class Program
    {
        static Mat GetLoGMask(Size size, double sigma)
        {
            double ratio = 1 / (Math.PI * Math.Pow(sigma, 4.0));
            int center = size.Height / 2;
            Mat dst = new Mat(size, MatType.CV_64F);

            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    int x2 = (j - center) * (j - center);
                    int y2 = (i - center) * (i - center);

                    double value = (x2 + y2) / (2 * sigma * sigma);
                    dst.Set(i, j, -ratio * (1 - value) * Math.Exp(-value));
                }
            }

            double scale = (center * 10 / ratio);
            dst *= scale;
            return dst;
        }

        static void Main(string[] args)
        {
            string path = @"C:/Temp/opencv/laplacian_test.jpg";
            Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);

            if (image.Empty())
                throw new Exception("Failed to load image");

            //sigma값이 작으면 필터는 더 날카로워지고 커지면 더 부드럽게 되어 넓은 영역을 흐리게 함 
            double sigma = 1.4;
            Mat logMask = GetLoGMask(new Size(9, 9), sigma);

            Console.WriteLine(logMask);
            Console.WriteLine(Cv2.Sum(logMask));

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            Mat gausImg = new Mat();

            // LoG 필터 적용
            Cv2.Filter2D(image, dst1, MatType.CV_32F, logMask);
            Cv2.GaussianBlur(image, gausImg, new Size(9, 9), sigma, sigma);
            Cv2.Laplacian(gausImg, dst2, MatType.CV_32F, 5);

            // DoG 계산
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();
            Cv2.GaussianBlur(image, dst3, new Size(1, 1), 0.0);
            Cv2.GaussianBlur(image, dst4, new Size(9, 9), 1.6);
            Mat dstDoG = new Mat();
            Cv2.Subtract(dst3, dst4, dstDoG);

            Cv2.Normalize(dstDoG, dstDoG, 0, 255, NormTypes.MinMax);

            // 결과 출력
            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1 - LoG_filter2D", dst1);
            Cv2.ImShow("dst2 - LOG_OpenCV", dst2);
            Cv2.ImShow("dst_DoG - DOG_OpenCV", dstDoG);
            Cv2.WaitKey();
        }
    }
}
