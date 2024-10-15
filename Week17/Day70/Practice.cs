// C#을 사용할 때 누겟 설치 해야함.
Install-Package OpenCvSharp4 -Version 4.10.0.20240616
Install-Package OpenCvSharp4.runtime.win


//OpenCVTestCode.cs
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp001
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"현재 OpenCV 버전 : {Cv2.GetVersionString()}");
        }
    }
}
----------------------------------------------------------------------------------
//컬러 사진을 흑백으로
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace OpenCVSharp002
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //그림 읽기
            Mat src = Cv2.ImRead("C:\\Temp\\a001.png", ImreadModes.Color);
            if(src.Empty())
            {
                Console.WriteLine("경로가 잘못되었거나 이미지 문제입니다.";
                return;
            }
            //흑백 변환
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            //저장
            Cv2.ImWrite("gray.png", gray);
            //출력
            Cv2.ImShow("칼라 뉴진스", src);
            Cv2.ImShow("흑백 뉴진스", gray);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------------------------
//matrix 표현(예시)
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241014_OpenCVSharp003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = new Mat(300, 400, MatType.CV_8UC1, new Scalar(200));
            Cv2.ImShow("m1 표현", m1);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------------------------
//[ C# 기준 ] Point_ & 매트릭스 관련 코드
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241014_OpenCVSharp004
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point pt = new Point(3, 4);
            Mat m2 = new Mat(200, 300, MatType.CV_8U, new Scalar(200));

            Cv2.ImShow("m2", m2);

            Console.WriteLine($"pt1({pt.X}, {pt.Y})");
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------------------------
//포인트 클래스
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCVSharp004
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point pt1 = new Point(3, 4);
            Point2f pt2 = new Point2f(3.1f, 4.5f);
            Point3d pt3 = new Point3d(100, 200, 300);

            Mat m2 = new Mat(200, 300, MatType.CV_8U, new Scalar(200));

            Cv2.ImShow("m2", m2);

            Console.WriteLine($"pt1({pt1.X}, {pt1.Y})");
            Console.WriteLine($"pt2({pt2.X}, {pt2.Y})");
            Console.WriteLine($"pt3({pt3.X}, {pt3.Y}, {pt3.Z})");

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------------------------
//[ C# ]Size_ 클래스를 이용한 코드
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241014_Size01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Size_ 기본 선언
            Size sz1 = new Size(100, 200);
            Size2f sz2 = new Size2f(192.3f, 25.3f);
            Size2d sz3 = new Size2d(100.2, 30.9);

            Size sz4 = new Size(120, 69);
            Size2f sz5 = new Size2f(0.3f, 0.0f);
            Size2d sz6 = new Size(0.25, 0.6);

            Point2d pt1 = new Point2d(0.25, 0.6);

            // Size sz7 = sz1 + (Size)sz2; 는 Error!
            Size sz7 = new Size(
                sz1.Width + (int)sz2.Width,
                sz2.Height + (int)sz2.Height);

            Size2d sz8 = new Size2d(
                sz3.Width - (double)sz4.Width,
                sz3.Height - (double)sz4.Height);

            // 출력
            Console.Write($"sz1.Width = {sz1.Width},  ");
            Console.WriteLine($"sz2.Height = {sz2.Height}");
            Console.WriteLine($"넓이 : {sz1.Width * sz1.Height}");

            Console.WriteLine($"[sz7] : {sz7},  [sz8] : {sz8}");
        }
    }
}
---------------------------------------------------------------------------------
//RotatedRect 클래스 [ 회전된 사각형을 구현하기 위한 ]
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241014_RotatedRect01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat(300, 500, MatType.CV_8UC3, new Scalar(255, 255, 255));

            // 중심점과 크기 및 각도로 RotatedRect 생성
            Point2f center = new Point2f(250, 150);
            Size2f size = new Size2f(300, 100);
            RotatedRect rotRect = new RotatedRect(center, size, 20);  // 각도를 20도로 설정

            // rotRect의 꼭짓점 가져오기
            Point2f[] pts = rotRect.Points();

            // 회전된 사각형 그리기
            for (int i = 0; i < 4; i++)
            {
                // Point2f를 Point로 변환 (어려움!)
                Point p1 = new Point((int)pts[i].X, (int)pts[i].Y);
                Point p2 = new Point((int)pts[(i + 1) % 4].X, (int)pts[(i + 1) % 4].Y);

                // 꼭짓점을 잇는 선 그리기
                Cv2.Line(image, p1, p2, new Scalar(0), 2);
            }

            // 회전된 사각형의 중심에 원 그리기
            Cv2.Circle(image, new Point((int)rotRect.Center.X, (int)rotRect.Center.Y), 4, new Scalar(0), -1);

            // BoundingRect를 사용해 내접 사각형 그리기 (내접은 회전되지 않음)
            Rect boundRect = rotRect.BoundingRect();
            Cv2.Rectangle(image, boundRect, new Scalar(0), 1);

            Cv2.ImShow("회전 사각형", image);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
