//Cv2.NamedWindow 함수로 창의 크기를 이미지에 맞게 자동으로 바꿔줌
using OpenCvSharp;

namespace _2021016_OpenCVWindow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Cv2.GetVersionString());
            Mat image1 = new Mat(300, 400, MatType.CV_8U, new Scalar(255));
            string title1 = "white 창 제어";

            Cv2.NamedWindow(title1, WindowFlags.AutoSize);

            Cv2.ImShow(title1, image1);
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------------------
//윈도우 창의 크기를 바꾸는 코드
using OpenCvSharp;

namespace CV_Window_Resize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat(300, 400, MatType.CV_8UC1, new Scalar(255));
            string title1 = "창 크기변경1 - AUTOSIZE";
            string title2 = "창 크기변경2 - NORMAL";

            Cv2.NamedWindow(title1, WindowFlags.AutoSize);
            Cv2.NamedWindow(title2, WindowFlags.Normal);

            Cv2.ResizeWindow(title1, 500, 200);
            Cv2.ResizeWindow(title2, 500, 200);

            Cv2.ImShow(title1, image);
            Cv2.ImShow(title2, image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
----------------------------------------------------------------------------
//키보드 이벤트 제어
using OpenCvSharp;
using System.Diagnostics.CodeAnalysis;

namespace _20241016_KeyMouse01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Mat image = new Mat(200, 300, MatType.CV_8U, new Scalar(255)))
            {
                Cv2.NamedWindow("키보드 이벤트", WindowFlags.AutoSize);
                Cv2.ImShow("키보드 이벤트", image);

                while (true)
                {
                    int key = Cv2.WaitKeyEx(200);
                    if (key == 27) break; // 'ESC' Key

                    switch (key)
                    {
                        case (int)'a':
                            Console.WriteLine("a키 입력"); break;
                        case (int)'b':
                            Console.WriteLine("b키 입력"); break;
                        case (int)'A':
                            Console.WriteLine("A키 입력"); break;
                        case (int)'B':
                            Console.WriteLine("B키 입력"); break;

                        // 0x250000으로 해도 왼쪽 화살표가 입력, 키보드 배치마다 차이가 있음
                        case 0x250000: Console.WriteLine("왼쪽 화살표 입력"); break;
                        case 0x270000: Console.WriteLine("오른쪽 화살표 입력"); break;
                        case 0x260000: Console.WriteLine("위쪽 화살표 입력"); break;
                        case 0x280000: Console.WriteLine("아래쪽 화살표 입력"); break;

                        default: Console.WriteLine(key); break;
                    }
                }
            }
        }
    }
}
----------------------------------------------------------------------------
//트랙바 이벤트 제어
using OpenCvSharp;

namespace _20241016_TrackBar01
{
    internal class Program
    {
        private static string title = "트랙바 이벤트";
        private static Mat image;
        static void Main(string[] args)
        {
            int value = 130;
            image = new Mat(300, 400, MatType.CV_8UC1, new Scalar(120));

            Cv2.NamedWindow(title, WindowFlags.AutoSize);
            Cv2.CreateTrackbar("밝기값", title, ref value, 255, OnChange);

            Cv2.ImShow(title, image);
            Cv2.WaitKey(0);
        }
        private static void OnChange(int value, IntPtr userdata)
        {
            int add_value = value - 130;
            Console.WriteLine($"추가 화소값 {add_value}");

            // Mat tmp = image + add_value;
            Mat tmp = new Mat();
            Cv2.Add(image, new Scalar(add_value), tmp); // Mat에 스칼라 값 더하기
            Cv2.ImShow(title, tmp);
        }
    }
}
----------------------------------------------------------------------------
//마우스 이벤트 제어
using OpenCvSharp;

namespace _20241016_KeyMouse02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 채널이 3개이기 때문에 Scalar도 3개로 표현(BGR)
            Mat image = new Mat(200, 300, MatType.CV_8UC3, new Scalar(255, 255, 255));

            Cv2.ImShow("마우스 이벤트1", image);
            Cv2.SetMouseCallback("마우스 이벤트1", OnMouse);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }

        // At(@)을 이용하여 매핑해주기
        private static void OnMouse(MouseEventTypes @event, int x, int y, 
            MouseEventFlags flags, IntPtr userdata)
        {
            switch(@event)
            {
                case MouseEventTypes.LButtonDown:
                    Console.WriteLine("마우스 왼쪽 버튼이 눌러졌습니다."); break;
                case MouseEventTypes.LButtonUp:
                    Console.WriteLine("마우스 왼쪽 버튼이 때졌습니다."); break;
                case MouseEventTypes.RButtonDown:
                    Console.WriteLine("마우스 오른쪽 버튼이 눌러졌습니다."); break;
                case MouseEventTypes.RButtonUp:
                    Console.WriteLine("마우스 오른쪽 버튼이 때졌습니다."); break;
            }
        }
    }
}
----------------------------------------------------------------------------
//그리기 함수 [ 원 ]
using OpenCvSharp;

namespace DrawCircle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scalar orange = new Scalar(0, 165, 255);
            Scalar blue = new Scalar(255, 0, 0);
            Scalar magenta = new Scalar(255, 0, 255);

            Mat image = new Mat(300, 500, MatType.CV_8UC3, new Scalar(255, 255, 255));

            Size size = image.Size();
            Point center = new Point(size.Width / 2, size.Height / 2);

            Point pt1 = new Point(70, 50);
            Point pt2 = new Point(350, 220);

            Cv2.Circle(image, center, 100, blue);
            Cv2.Circle(image, pt1, 80, orange, 2);
            Cv2.Circle(image, pt2, 60, magenta, -1);

            Cv2.PutText(image, "center_blue", center, HersheyFonts.HersheyComplex, 1.2, blue);
            Cv2.PutText(image, "pt1_orange", pt1, HersheyFonts.HersheyComplex, 0.8, orange);
            //Cv2.PutText(image, "pt2_magenta", pt2 + Point(2, 2), HersheyFonts.HersheyComplex, 0.5, new Scalar(0, 0, 0), 2);
            Point newPt2 = new Point(pt2.X + 2, pt2.Y + 2);
            Cv2.PutText(image, "pt2_magenta", newPt2, HersheyFonts.HersheyComplex, 0.5, new Scalar(0, 0, 0), 2);
            Cv2.PutText(image, "pt2_magenta", pt2, HersheyFonts.HersheyComplex, 0.5, magenta, 1);

            Cv2.ImShow("원그리기", image);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------------------
//그리기 함수 [ 타원 ]
using OpenCvSharp;

namespace _20241016_DrawingEclipse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scalar orange = new Scalar(0, 165, 255);
            Scalar blue = new Scalar(255, 0, 0);
            Scalar magenta = new Scalar(255, 0, 255);

            Mat image = new Mat(300, 700, MatType.CV_8UC3, new Scalar(255, 255, 255));

            Point pt1 = new Point(120, 150); Point pt2 = new Point(550, 150);
            Cv2.Circle(image, pt1, 1, new Scalar(0), 1);
            Cv2.Circle(image, pt2, 1, new Scalar(0), 1);

            // 타원 그리기
            Cv2.Ellipse(image, pt1, new Size(100, 60), 0, 0, 360, orange, 2);
            Cv2.Ellipse(image, pt1, new Size(100, 60), 0, 30, 270, blue, 4);

            // 호 그리기
            Cv2.Ellipse(image, pt2, new Size(100, 60), 30, 0, 360, orange, 2);
            Cv2.Ellipse(image, pt2, new Size(100, 60), 30, -30, 160, magenta, 2);

            Cv2.ImShow("타원 및 호 그리기", image);
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------------------
//카메라 제어
using OpenCvSharp;

namespace CVBasicCamera
{
    class StringUtil
    {
        public void PutString(Mat frame, string text, Point pt, double value)
        {
            text += value.ToString();
            Point shade = new Point(pt.X + 2, pt.Y + 2);
            int font = (int)HersheyFonts.HersheySimplex;

            // 그림자 효과 
            Cv2.PutText(frame, text, shade, (HersheyFonts)font, 0.7, Scalar.Black, 2);

            // 실제 텍스트 
            Cv2.PutText(frame, text, pt, (HersheyFonts)font, 0.7, new Scalar(120, 200, 90), 2);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 웹캠 연결
            VideoCapture capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                Console.WriteLine("카메라가 연결되지 않았습니다.");
                return;
            }

            // 카메라 속성 출력
            Console.WriteLine("너비: " + capture.Get(VideoCaptureProperties.FrameWidth));
            Console.WriteLine("높이: " + capture.Get(VideoCaptureProperties.FrameHeight));
            Console.WriteLine("노출: " + capture.Get(VideoCaptureProperties.Exposure));
            Console.WriteLine("밝기: " + capture.Get(VideoCaptureProperties.Brightness));

            Mat frame = new Mat();
            while (true)
            {
                // 카메라에서 프레임 읽기
                capture.Read(frame);
                if (frame.Empty())
                    break;

                // 노출 정보 출력
                StringUtil su = new StringUtil();
                su.PutString(frame, "EXPOS: ", new Point(10, 40), capture.Get(VideoCaptureProperties.Exposure));

                Cv2.ImShow("카메라 영상보기", frame);

                // 키 입력 대기 (30ms)
                if (Cv2.WaitKey(30) >= 0)
                    break;
            }
            Cv2.DestroyAllWindows();
        }
    }
}
----------------------------------------------------------------------------
//카메라 제어 [ 흑백처리 + 조금 더 부드러운 프레임으로 ]
using OpenCvSharp;

namespace _20241016_CVBasicCamera01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideoCapture capture = new VideoCapture(0);
            if (!capture.IsOpened()) { Console.WriteLine("카메라가 연결되지 않았습니다."); return; }

            // 카메라 속성
            Console.WriteLine("너비 : " + capture.Get(VideoCaptureProperties.FrameWidth));
            Console.WriteLine("높이 : " + capture.Get(VideoCaptureProperties.FrameHeight));
            Console.WriteLine("노출 : " + capture.Get(VideoCaptureProperties.Exposure));
            Console.WriteLine("밝기 : " + capture.Get(VideoCaptureProperties.Brightness));

            // Key Code
            Mat frame = new Mat();

            while (true)
            {
                // 카메라에서 프레임 읽기
                capture.Read(frame); if(frame.Empty()) { Console.WriteLine("프레임에 문제가 있습니다."); return; }

                // 흑백효과
                Mat black = frame.Clone();
                Cv2.CvtColor(frame, black, ColorConversionCodes.BGR2GRAY);

                // Cv2.ImShow("기본카메라", frame);
                Cv2.ImShow("기본카메라", black);

                // 종료법
                if (Cv2.WaitKey(30) >= 0) break;
            }
            // Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
