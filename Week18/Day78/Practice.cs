//침식 연산 실습
using OpenCvSharp;

namespace MorphologyErosion
{
    internal class Program
    {
        // 마스크 원소와 마스크 범위 입력화소 간의 일치 여부 체크
        static bool CheckMatch(Mat img, Point start, Mat mask, bool mode = false)
        {
            for (int u = 0; u < mask.Rows; u++)
            {
                for (int v = 0; v < mask.Cols; v++)
                {
                    Point pt = new Point(v, u);
                    int m = mask.At<byte>(pt.Y, pt.X);  // 마스크 계수
                    int p = img.At<byte>(start.Y + pt.Y, start.X + pt.X);  // 해당 위치 입력화소

                    bool ch = (p == 255);  // 일치 여부 비교
                    if (m == 1 && ch == mode) return false;
                }
            }
            return true;
        }

        static void Erosion(Mat img, Mat dst, Mat mask)
        {
            dst.SetTo(0);   //좌표의 모든 값을 0으로 채움

            if (mask.Empty())
                mask = Mat.Ones(3, 3, MatType.CV_8UC1);

            Point maskCenterCoord = new Point(mask.Cols / 2, mask.Rows / 2);

            // mask의 중심 좌표를 기준으로 이미지를 순회
            // (이미지의 가장자리는 mask가 벗어나지 않도록 하기 위해 mask의 중심을 기준으로 순회 시작 및 종료)
            for (int i = maskCenterCoord.Y; i < img.Rows - maskCenterCoord.Y; i++)
            {
                for (int j = maskCenterCoord.X; j < img.Cols - maskCenterCoord.X; j++)
                {
                    // 현재 픽셀을 중심으로 마스크를 적용할 시작 좌표 계산
                    Point start = new Point(j, i) - maskCenterCoord;
                    // 이미지의 해당 부분과 마스크가 일치하는지 확인
                    bool check = CheckMatch(img, start, mask, false);
                    // 일치하면 결과 이미지의 해당 픽셀을 255로 설정, 그렇지 않으면 0으로 설정
                    dst.Set<byte>(i, j, check ? (byte)255 : (byte)0);
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("C:\\Temp\\opencv\\morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat thImg = new Mat();
            //128이상의 픽셀값이면 모두 255로 변환하라!! 128보다 작으면? 모두 0으로 변환 즉, 0과 255로 이진화하라.
            Cv2.Threshold(image, thImg, 128, 255, ThresholdTypes.Binary);

            Mat mask = new Mat(3, 3, MatType.CV_8UC1); // 3x3 크기의 Mat 생성
            mask.Set(0, 1, 1); // 중간 값(0, 1)을 1로 설정
            mask.Set(1, 0, 1);
            mask.Set(1, 1, 1);
            mask.Set(1, 2, 1);
            mask.Set(2, 1, 1);

            Mat dst1 = image.Clone(); //전달할 때 크기를 설정 후 전달.
            Mat dst2 = new Mat();
            Erosion(thImg, dst1, mask); //침식연산 - 우리가 만든 함수
            Cv2.MorphologyEx(thImg, dst2, MorphTypes.Erode, mask); //OpenCV에 있는 침식연산 앞으로 이 함수를 사용하세요.

            Cv2.ImShow("image", image);
            Cv2.ImShow("이진 영상", thImg);
            Cv2.ImShow("User_erosion", dst1);
            Cv2.ImShow("OpenCV_erosion", dst2);

            Cv2.WaitKey(); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------
//팽창 연산
using OpenCvSharp;

namespace MorphologyDilation
{
    internal class Program
    {
        static bool CheckMatch(Mat img, Point start, Mat mask, int mode)
        {
            for (int u = 0; u < mask.Rows; u++)
            {
                for (int v = 0; v < mask.Cols; v++)
                {
                    int m = mask.At<byte>(u, v); // 마스크 계수
                    int p = img.At<byte>(start.Y + u, start.X + v); // 해당 위치 입력화소

                    bool ch = (p == 255); // 일치 여부 비교
                    if (m == 1 && ch == (mode == 1)) return false;
                }
            }
            return true;
        }

        // 팽창 연산을 수행하는 함수
        static void Dilation(Mat img, Mat dst, Mat mask)
        {
            // 결과 이미지 초기화
            dst.SetTo(0);

            // 마스크가 제공되지 않았다면 기본 3x3 마스크 생성
            if (mask.Empty())
                mask = Mat.Ones(3, 3, MatType.CV_8UC1);

            // 마스크의 중심 좌표 계산
            Point maskCenter = new Point(mask.Cols / 2, mask.Rows / 2);

            // 이미지 순회
            for (int i = maskCenter.Y; i < img.Rows - maskCenter.Y; i++)
            {
                for (int j = maskCenter.X; j < img.Cols - maskCenter.X; j++)
                {
                    // 현재 픽셀을 중심으로 마스크를 적용할 시작 좌표 계산
                    Point start = new Point(j, i) - maskCenter;

                    // 이미지의 해당 부분과 마스크가 일치하는지 확인
                    bool check = CheckMatch(img, start, mask, 1);

                    // 팽창 연산 수행
                    dst.Set<byte>(i, j, check ? (byte)0 : (byte)255);
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead("c:\\Temp\\opencv\\morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
                throw new Exception("이미지를 불러올 수 없습니다.");

            Mat thImg = new Mat();
            Cv2.Threshold(image, thImg, 128, 255, ThresholdTypes.Binary);

            Mat mask = new Mat(3, 3, MatType.CV_8UC1); // 3x3 크기의 Mat 생성
            mask.Set(0, 1, 1); // 중간 값(0, 1)을 1로 설정
            mask.Set(1, 0, 1);
            mask.Set(1, 1, 1);
            mask.Set(1, 2, 1);
            mask.Set(2, 1, 1);

            Mat dst1 = image.Clone();
            Dilation(thImg, dst1, mask);

            Mat dst2 = new Mat();
            Cv2.MorphologyEx(thImg, dst2, MorphTypes.Dilate, mask);

            Cv2.ImShow("image", image);
            Cv2.ImShow("팽창 사용자정의 함수", dst1);
            Cv2.ImShow("OpenCV 팽창 함수", dst2);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//열림 연산 및 닫힘 연산
using OpenCvSharp;

namespace MorphologyOpenClose
{
    internal class Program
    {
        class CvUtils
        {
            // 두 이미지와 마스크가 일치하는지 여부를 확인하는 함수
            public bool CheckMatch(Mat img, Point start, Mat mask, int mode = 0)
            {
                for (int u = 0; u < mask.Rows; u++)
                {
                    for (int v = 0; v < mask.Cols; v++)
                    {
                        Point pt = new Point(v, u);
                        byte m = mask.Get<byte>(u, v); // 마스크 계수
                        byte p = img.Get<byte>(start.Y + u, start.X + v); // 해당 위치 입력화소

                        bool ch = (p == 0); // 일치 여부 비교 (검은 바탕에 하얀 글씨)
                        if (m == 1 && ch == (mode == 0))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            // 침식 연산 함수
            public void Erosion(Mat img, Mat dst, Mat mask)
            {
                dst.Create(img.Size(), MatType.CV_8UC1);
                dst.SetTo(Scalar.Black);
                Point h_m = new Point(mask.Cols / 2, mask.Rows / 2);

                for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
                {
                    for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                    {
                        Point start = new Point(j, i) - h_m;
                        bool check = CheckMatch(img, start, mask, 0);
                        dst.Set<byte>(i, j, (byte)(check ? 255 : 0));
                    }
                }
            }

            // 팽창 연산 함수
            public void Dilation(Mat img, Mat dst, Mat mask)
            {
                dst.Create(img.Size(), MatType.CV_8UC1);
                dst.SetTo(Scalar.White);
                Point h_m = new Point(mask.Cols / 2, mask.Rows / 2);

                for (int i = h_m.Y; i < img.Rows - h_m.Y; i++)
                {
                    for (int j = h_m.X; j < img.Cols - h_m.X; j++)
                    {
                        Point start = new Point(j, i) - h_m;
                        bool check = CheckMatch(img, start, mask, 1);
                        dst.Set<byte>(i, j, (byte)(check ? 0 : 255));
                    }
                }
            }

            // 열기 연산 함수
            public void Opening(Mat img, Mat dst, Mat mask)
            {
                Mat tmp = new Mat();
                Erosion(img, tmp, mask);
                Dilation(tmp, dst, mask);
            }

            // 닫기 연산 함수
            public void Closing(Mat img, Mat dst, Mat mask)
            {
                Mat tmp = new Mat();
                Dilation(img, tmp, mask);
                Erosion(tmp, dst, mask);
            }
        }

        static void Main(string[] args)
        {
            // 이미지 읽기 - 그림의 경로는 "C:/Temp/opencv/"로 지정
            Mat image = Cv2.ImRead(@"C:/Temp/opencv/morph_test1.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("이미지를 불러올 수 없습니다. 경로를 확인해주세요.");
                return;
            }

            // 이진화 작업
            Mat th_img = new Mat();
            Cv2.Threshold(image, th_img, 128, 255, ThresholdTypes.Binary);

            // 마스크 행렬 정의 (3x3 형태)
            Mat mask = new Mat(3, 3, MatType.CV_8UC1, new Scalar(0));
            mask.Set(0, 1, 1);
            mask.Set(1, 0, 1);
            mask.Set(1, 1, 1);
            mask.Set(1, 2, 1);
            mask.Set(2, 1, 1);

            // 열기 연산 수행 (사용자 정의)
            Mat dst1 = new Mat();
            CvUtils cvUtils = new CvUtils();
            cvUtils.Opening(th_img, dst1, mask);

            // 닫기 연산 수행 (사용자 정의)
            Mat dst2 = new Mat();
            cvUtils.Closing(th_img, dst2, mask);

            // OpenCV 제공 열기 연산 함수 사용
            Mat dst3 = new Mat();
            Cv2.MorphologyEx(th_img, dst3, MorphTypes.Open, mask);

            // OpenCV 제공 닫기 연산 함수 사용
            Mat dst4 = new Mat();
            Cv2.MorphologyEx(th_img, dst4, MorphTypes.Close, mask);

            // 결과 이미지 출력
            Cv2.ImShow("User_opening", dst1);
            Cv2.ImShow("User_closing", dst2);
            Cv2.ImShow("OpenCV_opening", dst3);
            Cv2.ImShow("OpenCV_closing", dst4);
            Cv2.WaitKey(); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------
//차량 번호판 경계 추출
using OpenCvSharp;

namespace DefectCarPlate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("차량 영상 번호( 0:종료 ) : ");
                if (!int.TryParse(Console.ReadLine(), out int no) || no == 0)
                    break;

                string fname = $"C:/Temp/opencv/test_car/{no:D2}.jpg"; //두 자리로 꼭 사용하세요. 숫자 01, 11, 12 등
                Mat image = new Mat(fname, ImreadModes.Color);

                if (image.Empty())
                {
                    Console.WriteLine($"{no}번 영상 파일이 없습니다.");
                    continue;
                }

                Mat gray = new Mat();
                Mat sobel = new Mat();
                Mat thImg = new Mat();
                Mat morph = new Mat();

                // 닫힘 연산 마스크 생성
                Mat kernel = Mat.Ones(new Size(31, 5), MatType.CV_8UC1);

                // 명암도 영상 변환
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                // 블러링을 통한 전처리
                Cv2.Blur(gray, gray, new Size(5, 5));

                // 소벨 알고리즘으로 경계(에지 검출한다)
                Cv2.Sobel(gray, sobel, MatType.CV_8U, 1, 0, 3);

                // 이진화를 수행한다.
                Cv2.Threshold(sobel, thImg, 120, 255, ThresholdTypes.Binary);

                // 닫음 연산 - 팽창 후 침식을 통해 경계를 명확히 분리한다
                Cv2.MorphologyEx(thImg, morph, MorphTypes.Close, kernel);

                // 결과 표시
                Cv2.ImShow("image", image);
                Cv2.MoveWindow("image", 0, 0);

                Cv2.ImShow("이진 영상", thImg);
                Cv2.MoveWindow("이진 영상", 500, 0);

                Cv2.ImShow("닫음 연산", morph);
                Cv2.MoveWindow("닫음 연산", 1000, 0);

                Cv2.WaitKey(2000); 
            }
        }
    }
}
----------------------------------------------------------------
//기하학 처리
using OpenCvSharp;

namespace Scaling
{
    internal class Program
    {
        static void Scaling(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < img.Rows; i++)
            {
                for (int k = 0; k < img.Cols; k++)
                {
                    int x = (int)(k * ratioX);
                    int y = (int)(i * ratioY);
                    dst.Set(y, x, img.At<byte>(i, k));
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/scaling_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1, dst2;
            Scaling(image, out dst1, new Size(150, 200)); // 크기변경 수행 - 축소
            Scaling(image, out dst2, new Size(300, 400)); // 크기변경 수행 - 확대

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-축소", dst1);
            Cv2.ImShow("dst2-확대", dst2);
            Cv2.ResizeWindow("dst1-축소", 200, 200); // 윈도우 크기 확장
            Cv2.WaitKey(); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------
//최근접 이웃 보간
using System;
using OpenCvSharp;

namespace NearSetScaling
{
    internal class Program
    {
        static void Scaling(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < img.Rows; i++)
            {
                for (int k = 0; k < img.Cols; k++)
                {
                    int x = (int)(k * ratioX);
                    int y = (int)(i * ratioY);
                    dst.Set(y, x, img.At<byte>(i, k));
                }
            }
        }

        static void ScalingNearest(Mat img, out Mat dst, Size size)
        {
            dst = new Mat(size, MatType.CV_8U, new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    int x = (int)Math.Round(j / ratioX);
                    int y = (int)Math.Round(i / ratioY);
                    dst.Set(i, j, img.At<byte>(y, x));
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/interpolation_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1, dst2;
            Scaling(image, out dst1, new Size(300, 300)); // 크기변경 - 기본
            ScalingNearest(image, out dst2, new Size(300, 300)); // 크기변경 – 최근접 이웃

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-순방향사상", dst1);
            Cv2.ImShow("dst2-최근접 이웃보간", dst2);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//양선형 보간
using OpenCvSharp;

namespace BilinearScaling
{
    internal class Program
    {
        static void ScalingNearest(Mat img, out Mat dst, Size size) // 최근접 이웃 보간
        {
            dst = new Mat(size, MatType.CV_8U, new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    int x = (int)Math.Round(j / ratioX);
                    int y = (int)Math.Round(i / ratioY);
                    dst.Set(i, j, img.At<byte>(y, x));
                }
            }
        }

        static byte BilinearValue(Mat img, double x, double y) // 단일 화소 양선형 보간
        {
            if (x >= img.Cols - 1) x--;
            if (y >= img.Rows - 1) y--;

            Point pt = new Point((int)x, (int)y);
            byte A = img.At<byte>(pt.Y, pt.X);  // 왼쪽상단 화소
            byte B = img.At<byte>(pt.Y + 1, pt.X);     // 왼쪽하단 화소
            byte C = img.At<byte>(pt.Y, pt.X + 1);     // 오른쪽상단 화소
            byte D = img.At<byte>(pt.Y + 1, pt.X + 1);     // 오른쪽하단 화소

            double alpha = y - pt.Y;
            double beta = x - pt.X;
            int M1 = A + (int)Math.Round(alpha * (B - A));  // 1차 보간
            int M2 = C + (int)Math.Round(alpha * (D - C));
            int P = M1 + (int)Math.Round(beta * (M2 - M1)); // 2차 보간

            return (byte)(P < 0 ? 0 : (P > 255 ? 255 : P));
        }

        static void ScalingBilinear(Mat img, out Mat dst, Size size) // 크기변경 – 양선형 보간
        {
            dst = new Mat(size, img.Type(), new Scalar(0));
            double ratioY = (double)size.Height / img.Rows;
            double ratioX = (double)size.Width / img.Cols;

            for (int i = 0; i < dst.Rows; i++) // 목적영상 순회 - 역방향 사상
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    double y = i / ratioY;
                    double x = j / ratioX;
                    dst.Set(i, j, BilinearValue(img, x, y)); // 화소 양선형 보간
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"c:/Temp/opencv/interpolation_test.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Image load failed!");
                return;
            }

            Mat dst1 = new Mat();
            Mat dst2 = new Mat();
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();
            ScalingBilinear(image, out dst1, new Size(300, 300)); // 크기변경 – 양선형 보간
            ScalingNearest(image, out dst2, new Size(300, 300));  // 크기변경 – 최근접 보간
            Cv2.Resize(image, dst3, new Size(300, 300), 0, 0, InterpolationFlags.Linear); // OpenCV 함수 적용 - 양선형 보간
            Cv2.Resize(image, dst4, new Size(300, 300), 0, 0, InterpolationFlags.Nearest); // OpenCV 함수 적용 - 최근접 보간

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-양선형", dst1);
            Cv2.ImShow("dst2-최근접이웃", dst2);
            Cv2.ImShow("OpenCV-양선형", dst3);
            Cv2.ImShow("OpenCV-최근접이웃", dst4);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//어파인 변환
using OpenCvSharp;

namespace AffineTransform
{
    internal class Program
    {
        // 주어진 좌표 (x, y)에서 픽셀의 양선형 보간 값을 계산합니다.
        // 이 함수는 주변 네 개의 픽셀을 사용하여 그 사이의 값을 결정합니다.
        static byte BilinearValue(Mat img, double x, double y)
        {
            if (x >= img.Cols - 1) x--;
            if (y >= img.Rows - 1) y--;

            // 4개 화소 가져옴
            Point pt = new Point((int)x, (int)y);
            int A = img.At<byte>((int)pt.Y, (int)pt.X);
            int B = img.At<byte>((int)(pt.Y + 1), (int)pt.X);
            int C = img.At<byte>((int)pt.Y, (int)(pt.X + 1));
            int D = img.At<byte>((int)(pt.Y + 1), (int)(pt.X + 1));

            // 1차 보간
            double alpha = y - pt.Y;
            double beta = x - pt.X;
            int M1 = A + (int)Math.Round(alpha * (B - A));
            int M2 = C + (int)Math.Round(alpha * (D - C));

            // 2차 보간
            int P = M1 + (int)Math.Round(beta * (M2 - M1));
            return (byte)(P < 0 ? 0 : (P > 255 ? 255 : P));
        }

        // 주어진 변환 행렬을 사용하여 입력 이미지에 어파인 변환을 적용합니다.
        // 이 함수는 변환 행렬을 수동으로 반전하고, 양선형 보간법을 사용하여 출력 픽셀 값을 계산합니다.
        static void AffineTransform(Mat img, out Mat dst, Mat map)
        {
            dst = new Mat(img.Size(), img.Type(), Scalar.All(0));
            Rect rect = new Rect(new Point(0, 0), img.Size());

            Mat invMap = new Mat();
            Cv2.InvertAffineTransform(map, invMap);

            for (int i = 0; i < dst.Rows; i++)
            {
                for (int j = 0; j < dst.Cols; j++)
                {
                    Mat coordinateMatrix = new Mat(3, 1, MatType.CV_64F);
                    double[] values = { j, i, 1.0 };
                    for (int k = 0; k < 3; k++)
                    {
                        coordinateMatrix.Set(k, 0, values[k]);
                    }
                    Mat xy = invMap * coordinateMatrix;
                    Point2d pt = new Point2d(xy.At<double>(0), xy.At<double>(1));

                    if (rect.Contains(new Point((int)pt.X, (int)pt.Y)))
                    {
                        dst.Set(i, j, BilinearValue(img, pt.X, pt.Y));
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Mat image = Cv2.ImRead(@"C:/Temp/opencv/affine_test3.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                throw new Exception("Image not found!");
            }

            Point2f[] pt1 = { new Point2f(10, 200), new Point2f(200, 150), new Point2f(280, 280) };
            Point2f[] pt2 = { new Point2f(10, 10), new Point2f(250, 10), new Point2f(280, 280) };

            Point2f center = new Point2f(200, 100);
            double angle = 30.0;
            double scale = 1;

            Mat affMap = Cv2.GetAffineTransform(pt1, pt2);
            Mat rotMap = Cv2.GetRotationMatrix2D(center, angle, scale);

            Mat dst1, dst2, dst3 = new Mat(), dst4 = new Mat();
            AffineTransform(image, out dst1, affMap);
            AffineTransform(image, out dst2, rotMap);

            Cv2.WarpAffine(image, dst3, affMap, image.Size(), InterpolationFlags.Linear);
            Cv2.WarpAffine(image, dst4, rotMap, image.Size(), InterpolationFlags.Linear);

            Cv2.CvtColor(image, image, ColorConversionCodes.GRAY2BGR);
            Cv2.CvtColor(dst1, dst1, ColorConversionCodes.GRAY2BGR);

            for (int i = 0; i < 3; i++)
            {
                Cv2.Circle(image, (Point)pt1[i], 3, new Scalar(0, 0, 255), 2);
                Cv2.Circle(dst1, (Point)pt2[i], 3, new Scalar(0, 0, 255), 2);
            }

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-어파인", dst1);
            Cv2.ImShow("dst2-어파인 회전", dst2);
            Cv2.ImShow("dst3-어파인_OpenCV", dst3);
            Cv2.ImShow("dst4-어파인-회전_OpenCV", dst4);

            Cv2.WaitKey();
        }
    }
}
----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//

----------------------------------------------------------------
//
