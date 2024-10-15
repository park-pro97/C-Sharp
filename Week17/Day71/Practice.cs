//컬러 사진을 흑백으로
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241015_CvtColor01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat src = Cv2.ImRead("C:\\Temp\\a001.png", ImreadModes.Color);
            // 경로 에러처리
            if(src.Empty()) 
            { 
                Console.WriteLine("파일 경로가 잘못되었거나, 이미지가 문제가 있습니다.");
                return;
            }

            Mat dst = new Mat();
            Cv2.CvtColor(src, dst, ColorConversionCodes.BGR2GRAY);
            // 저장
            Cv2.ImWrite("C:\\Temp\\dst.png", dst);

            // 출력
            Cv2.ImShow("흑백 사진", dst);
            Cv2.ImShow("원본 사진", src);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
--------------------------------------------------------------------------------------
//C# OpenCV로 변환한 Rect_ 클래스 사용
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241015_RectShow01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Size2d, Point2f 선언
            Size2d sz = new Size2d(100.5, 60.6);
            Point2f pt1 = new Point2f(20f, 30f);
            Point2f pt2 = new Point2f(100f, 200f);

            // Rect 기본 선언 방식
            Rect rect1 = new Rect(10, 10, 30, 50);
            Rect2f rect2 = new Rect2f(pt1.X, pt1.Y, pt2.X-pt1.X, pt2.Y-pt1.Y);
            Rect2d rect3 = new Rect2d(new Point2d(20.5, 10), sz);

            // 간결 선언 & 연산 적용
            Rect rect4 = rect1 + new Point(pt1.X, pt1.Y);
            Rect2f rect5 = rect2 + new Size2f(sz.Width, sz.Height);
            Rect2d rect6 = new Rect2d(rect1.X, rect1.Y, rect1.Width, rect1.Height)
                .Intersect(new Rect2d(rect2.Left, rect2.Top, rect2.Width, rect2.Height));

            // 한글로 결과 출력
            Console.WriteLine($"rect3 = {rect3.X}, {rect3.Y}, {rect3.Width} x {rect3.Height} 크기");
            Console.WriteLine($"rect4의 시작점 = {rect4.Location}, 끝점 = ({rect4.Right}, {rect4.Bottom})");
            Console.WriteLine($"rect5의 크기 = {rect5.Width}x{rect5.Height}");
            Console.WriteLine($"rect6의 교차 영역 = {rect6}");
        }
    }
}
--------------------------------------------------------------------------------------
//Vec_  [ 벡터 코드 ]
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecShow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 기본 선언 및 간결 방식
            Vec2i v1 = new Vec2i(5, 12);
            Vec3d v2 = new Vec3d(40, 130.7, 125.6);
            Vec2b v3 = new Vec2b(10, 10);
            //Vec6f v4 = new Vec6f(40f, 230.25f, 525.6f); //c#에서는 되도록 float는 사용하지 맙시다. Vec6d로 사용하세요.
            Vec3i v5 = new Vec3i(200, 230, 250);

            // 객체 연산 및 형변환
            //Vec3d v6 = v2 + (Vec3d)v5;
            Vec3d v6 = v2 + new Vec3d(v5.Item0, v5.Item1, v5.Item2);
            //Vec2b v7 = (Vec2b)v1 + v3;
            Vec2b v7 = new Vec2b((byte)v1.Item0, (byte)v1.Item1) + v3;

            // Point, Point3 객체 선언
            //Point pt1 = v1 + new Point(v7.Item0, v7.Item1);
            Point pt1 = new Point(v1.Item0, v1.Item1) + new Point(v7.Item0, v7.Item1);
            Point3i pt2 = new Point3i((int)v2.Item0, (int)v2.Item1, (int)v2.Item2);

            // 콘솔창 출력
            Console.WriteLine($"[v3] =  {v3}");
            Console.WriteLine($"[v7] =  {v7}");
            //Console.WriteLine($"[v3 * v7] =  {v3.Mul(v7)}");
            //안타깝지만 Mul이 없다. 직접 계산 ㅠㅠ
            Console.WriteLine($"[v3 * v7] =  ({v3.Item0 * v7.Item0}, {v3.Item1 * v7.Item1})");
            Console.WriteLine($"[v2] =  {v2}");
            Console.WriteLine($"[pt2] =  {pt2}");

            // 한글로 출력
            Console.WriteLine($"[v3] 값은 {v3} 입니다.");
            Console.WriteLine($"[v7] 값은 {v7} 입니다.");
            //Console.WriteLine($"[v3 * v7] 곱셈 결과는 {v3.Mul(v7)} 입니다.");
            //Mul이 없어 직접계산
            Console.WriteLine($"[v3 * v7] 곱셈 결과는 ({v3.Item0 * v7.Item0}, {v3.Item1 * v7.Item1}) 입니다.");
            Console.WriteLine($"[v2] 값은 {v2} 입니다.");
            Console.WriteLine($"[pt2] 값은 {pt2} 입니다.");
        }
    }
}
--------------------------------------------------------------------------------------
//Core 환경 Matrix 생성
using OpenCvSharp;

namespace _20241015_dotnet6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] data =
            {
                1.2, 2.3, 3.2,
                4.5, 5.0, 6.5
            };

            Mat m1 = new Mat(2, 3, MatType.CV_8U);
            // Scalar 화소 값은 8U에서 255가 최대, 즉 16S에서는 300이 가능
            Mat m2 = new Mat(2, 3, MatType.CV_8U, new Scalar(255));
            Mat m3 = new Mat(2, 3, MatType.CV_16S, new Scalar(300));

            Size sz = new Size(3, 2);
            Mat m5 = new Mat(sz, MatType.CV_64F, new Scalar(0));

            Console.WriteLine("m1 : \n" + m1.Dump());
            Console.WriteLine("m2 : \n" + m2.Dump());
            Console.WriteLine("m3 : \n" + m3.Dump());
            Console.WriteLine("m5 : \n" + m5.Dump());

            // 메모리에서 명시적으로 지워주기 위한 코드
            m1.Dispose(); m2.Dispose(); m3.Dispose(); m5.Dispose();
        }
    }
}
--------------------------------------------------------------------------------------
//32F의 Depth = 5, 원소의 크기 별로 다 맞게 나오는 것을 확인
using OpenCvSharp;

namespace _20241015_MatAttr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = new Mat(4, 3, MatType.CV_32FC3, new Scalar(0, 0, 0));

            Console.WriteLine($"m1 : \n{m1.Dump()}");
            Console.WriteLine($"차원 수 : {m1.Dims}");
            Console.WriteLine($"행 개수 : {m1.Rows}");
            Console.WriteLine($"열 개수 : {m1.Cols}");
            Console.WriteLine($"행렬 크기 : {m1.Size()}");

            Console.WriteLine($"전체 원소 개수 : {m1.Total()}");
            Console.WriteLine($"한 원소의 크기 : {m1.ElemSize()}");
            Console.WriteLine($"채널당 한 원소 크기 : {m1.ElemSize1()}");

            Console.WriteLine($"타입 : {m1.Type()}");
            Console.WriteLine($"타입(채널 수 | 깊이) : {m1.Channels()} | {m1.Depth()}");
        }
    }
}
--------------------------------------------------------------------------------------
//Flip 관련 Mat 클래스 코드 [ 예제 ]
using OpenCvSharp;

namespace MatFlip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String path = @"C:/Temp/opencv/flip_test.jpg";
            Mat image = new Mat(path, ImreadModes.Color);
            if (image.Empty())
            {
                Console.WriteLine("경로나 이미지에 문제가 있습니다.");
            }

            Mat x_axis = new Mat();
            Mat y_axis = new Mat();
            Mat xy_axis = new Mat();
            Mat rep_img = new Mat();
            Mat trans_img = new Mat();
            
            Cv2.Flip(image, x_axis, FlipMode.X);
            Cv2.Flip(image, y_axis, FlipMode.Y);
            Cv2.Flip(image, xy_axis, FlipMode.XY);

            Cv2.Repeat(image, 2, 2, rep_img);
            Cv2.Transpose(image, trans_img);

            Cv2.ImShow("image", image);
            Cv2.ImShow("x_axis", x_axis);
            Cv2.ImShow("y_axis", y_axis);
            Cv2.ImShow("xy_axis", xy_axis);
            Cv2.ImShow("rep_img", rep_img);
            Cv2.ImShow("trans_img", trans_img);

            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

        }
    }
}
--------------------------------------------------------------------------------------
//Reshape 사용
using OpenCvSharp;
using System;

namespace OpenCvSharpSample
{
    class CVInfo
    {
        public void PrintMatInfo(string m_name, Mat m)
        {
            Console.WriteLine($"[{m_name} 행렬]");
            Console.WriteLine($"   채널 수 = {m.Channels()}");
            Console.WriteLine($"   행 X 열 = {m.Rows} x {m.Cols}\n");
            Console.WriteLine($"행렬 :\n{m.Dump()}\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mat m1 = new Mat(2, 6, MatType.CV_8U, new Scalar(0, 0, 0));
            Mat m2 = m1.Reshape(2, m1.Cols);
            Mat m3 = m1.Reshape(3, 2);

            CVInfo cvinfo = new CVInfo();
            cvinfo.PrintMatInfo("m1(2, 6)", m1);
            cvinfo.PrintMatInfo("m2 = m1_reshape(2)", m2);
            cvinfo.PrintMatInfo("m3 = m1_reshape(3, 2)", m3);
        }
    }
}
--------------------------------------------------------------------------------------
//그리기 함수 사용
using OpenCvSharp;

namespace _20241015_OpenCVDrawing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 색상 선언, OpenCV는 BGR 순으로 기입
            Scalar blue = new Scalar(255, 0, 0); 
            Scalar red = new Scalar(0, 0, 255);
            Scalar green = new Scalar(0, 255, 0);
            Scalar white = new Scalar(255, 255, 255);
            Scalar yellow = new Scalar(0, 255, 255);

            Mat image = new Mat(400, 600, MatType.CV_8UC3, white);
            Point pt1 = new Point(50, 130);
            Point pt2 = new Point(200, 300);
            Point pt3 = new Point(300, 150);
            Point pt4 = new Point(400, 50);
            // C#에서는 new가 반드시 필요!
            Rect rect = new Rect(pt3, new Size(200, 150));

            // 선 Line
            Cv2.Line(image, pt1, pt2, red, 3, LineTypes.AntiAlias);
            Cv2.Line(image, pt3, pt4, green, 2, LineTypes.AntiAlias);
            Cv2.Line(image, pt3, pt4, green, 3, LineTypes.Link8, 1);
            // 사각형 Rectangle
            Cv2.Rectangle(image, rect, blue, 2, LineTypes.AntiAlias);
            Cv2.Rectangle(image, rect, blue, -1, LineTypes.Link4, 1);
            Cv2.Rectangle(image, pt1, pt2, red, 3, LineTypes.AntiAlias);

            Cv2.ImShow("image", image);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
--------------------------------------------------------------------------------------
//집 모양 도형 그려보기
using OpenCvSharp;
using System.ComponentModel;

namespace _20241015_DrawingQuiz01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scalar blue = new Scalar(255, 0, 0);
            Scalar red = new Scalar(0, 0, 255);
            Scalar green = new Scalar(0, 255, 0);
            Scalar black = new Scalar(0, 0, 0);
            Scalar white = new Scalar(255, 255, 255);

            Mat image = new Mat(800, 600, MatType.CV_8UC3, white);
            Point pt1 = new Point(100, 200);
            Point pt2 = new Point(500, 600);
            Point pt3 = new Point(100, 600);
            Point pt4 = new Point(500, 200);
            Point crm = new Point((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
            Point trm = new Point((pt1.X + pt2.X) / 2, 50);
            Point[] tp = { trm, pt1, pt4 };

            Cv2.Rectangle(image, pt1, pt2, black, 1, LineTypes.AntiAlias);
            Cv2.Line(image, pt1, pt2, black, 1, LineTypes.AntiAlias);
            Cv2.Line(image, pt3, pt4, black, 1, LineTypes.AntiAlias);
            Cv2.FillPoly(image, new[] { tp }, red);
            Cv2.Circle(image, crm, 200, blue, -1, LineTypes.AntiAlias);

            Cv2.ImShow("Image", image);
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
--------------------------------------------------------------------------------------
//

--------------------------------------------------------------------------------------
//

--------------------------------------------------------------------------------------
//
