//ROI 영역 내 화소값을 각각 출력함   [ 이미지의 데이터 깊이(depth)와 채널 수를 분석하고, 그에 맞는 자료형 정보 ]
using OpenCvSharp;

namespace ReadImage01
{
    class ImageUtil
    {
        public void PrintMatInfo(string name, Mat img)
        {
            string str;
            int depth = img.Depth();

            if (depth == MatType.CV_8U) str = "CV_8U";
            else if (depth == MatType.CV_8S) str = "CV_8S";
            else if (depth == MatType.CV_16U) str = "CV_16U";
            else if (depth == MatType.CV_16S) str = "CV_16S";
            else if (depth == MatType.CV_32S) str = "CV_32S";
            else if (depth == MatType.CV_32F) str = "CV_32F";
            else if (depth == MatType.CV_64F) str = "CV_64F";
            else str = "Unknown";

            Console.WriteLine($"{name}: depth({depth}) channels({img.Channels()}) -> 자료형: {str}C{img.Channels()}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string filename1 = @"C:/Temp/opencv/add2.jpg";
            Mat gray2gray = Cv2.ImRead(filename1, ImreadModes.Grayscale);
            Mat gray2color = Cv2.ImRead(filename1, ImreadModes.Color);

            if (gray2gray.Empty() || gray2color.Empty())
            {
                Console.WriteLine("이미지를 불러오는 데 실패했습니다.");
                return;
            }

            // ROI 영역 설정 (100, 100 위치의 1x1 픽셀)
            Rect roi = new Rect(100, 100, 1, 1);
            Console.WriteLine("행렬 좌표 (100,100) 화소값");
            Console.WriteLine($"gray2gray: {gray2gray.SubMat(roi).Dump()}");
            Console.WriteLine($"gray2color: {gray2color.SubMat(roi).Dump()}\n");

						// 이미지의 정보 도출
            ImageUtil iu = new ImageUtil();
            iu.PrintMatInfo("gray2gray", gray2gray);
            iu.PrintMatInfo("gray2color", gray2color);

            Cv2.ImShow("gray2gray", gray2gray);
            Cv2.ImShow("gray2color", gray2color);
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
-----------------------------------------------------------------
//행렬을 영상 파일로 저장
using OpenCvSharp;

namespace WriteImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat img8 = Cv2.ImRead(@"C:/Temp/opencv/read_color.jpg", ImreadModes.Color);
            if (img8.Empty())
            {
                Console.WriteLine("이미지를 불러오는 데 실패했습니다.");
                return;
            }

            int[] paramsJpg = { (int)ImwriteFlags.JpegQuality, 50 };  // JPEG 품질 50으로 설정
            int[] paramsPng = { (int)ImwriteFlags.PngCompression, 9 };  // PNG 압축 레벨 9로 설정
                                                                        // JPEG와 PNG 저장 파라미터 설정
            
            //out 폴더를 미리 만들어 주세요. 폴더생성과 예외처리를 넣으면 길어져서 생략해 봅니다.
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test1.jpg", img8); // 기본 설정으로 JPG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test2.jpg", img8, paramsJpg); // 품질 50으로 JPG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test.png", img8, paramsPng); // 압축 레벨 9로 PNG 저장
            Cv2.ImWrite(@"C:/Temp/opencv/out/write_test.bmp", img8); // BMP로 저장

            Console.WriteLine("이미지 저장이 완료되었습니다.");
        }
    }
}
-----------------------------------------------------------------
//압축률을 임의로 조
using OpenCvSharp;

namespace WriteImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat img8 = Cv2.ImRead(@"C:/Temp/cv_imgs/newjeans.jpg", ImreadModes.Color);
            if (img8.Empty())
            {
                Console.WriteLine("이미지를 불러오는 데 실패했습니다.")
                return;
            }

            // 컬러 이미지를 흑백 이미지로 변환
            Mat grayImg = new Mat();

            Cv2.CvtColor(img8, grayImg, ColorConversionCodes.BGR2GRAY);
            int[] paramsJpg = { (int)ImwriteFlags.JpegQuality, 50 };  // JPEG 품질 50으로 설정

            //out 폴더를 미리 만들어 주세요. 폴더생성과 예외처리를 넣으면 길어져서 생략해 봅니다
            Cv2.ImWrite(@"C:/Temp/cv_imgs/out/newjeans.jpg", grayImg, paramsJpg); // 품질 50으로 JPG 저장
            Console.WriteLine("이미지 저장이 완료되었습니다.");
        }
    }
}
-----------------------------------------------------------------
//WinForms에서 OpenCV 구현
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WinFormsCamera01
{
    public partial class Form1 : Form
    {
        private VideoCapture capture; // 카메라 캡처 객체
        private Mat frame; // 프레임 데이터를 담을 객체
        private Bitmap bitmap; // PictureBox에 표시할 비트맵 이미지
        private bool isCameraRunning = false; // 카메라 상태 확인 변수
        private bool isShowingGray = false;  // 흑백 영상 여부
        private bool isShowingColor = false; // 컬러 영상 여부

        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnCamera_Click(object sender, EventArgs e)
        {
            if (!isCameraRunning)
            {
                capture = new VideoCapture(0); // 0은 기본 카메라를 의미
                frame = new Mat();
                isCameraRunning = true;
                isShowingGray = false;
                isShowingColor = false;
                Application.Idle += ProcessFrame; // 카메라 프레임을 처리하는 이벤트
            }
        }

        // 카메라로부터 프레임을 처리하는 메서드
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.IsOpened())
            {
                capture.Read(frame); // 프레임 읽기
                if (!frame.Empty())
                {
                    // 현재 상태에 따라 프레임을 처리
                    if (isShowingGray)
                    {
                        Mat grayFrame = new Mat();
                        Cv2.CvtColor(frame, grayFrame, ColorConversionCodes.BGR2GRAY); // 컬러 영상을 흑백으로 변환
                        bitmap = BitmapConverter.ToBitmap(grayFrame); // 변환된 흑백 프레임을 Bitmap으로 변환
                    }
                    else
                    {
                        bitmap = BitmapConverter.ToBitmap(frame); // 컬러 영상을 Bitmap으로 변환
                    }

                    picMain.Image = bitmap; // PictureBox에 출력
                }
            }
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            if (frame != null && !frame.Empty())
            {
                isShowingGray = true;  // 흑백 모드로 전환
                isShowingColor = false; // 컬러 모드 해제
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (frame != null && !frame.Empty())
            {
                isShowingColor = true; // 컬러 모드로 전환
                isShowingGray = false; // 흑백 모드 해제
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            if (isCameraRunning)
            {
                Application.Idle -= ProcessFrame; // 카메라 프레임 처리 중단
                capture.Release(); // 카메라 해제
                picMain.Image = null; // PictureBox 초기화
                isCameraRunning = false;
            }

            // 프로그램 종료
            Application.Exit();
        }
    }
}
-----------------------------------------------------------------
//Async를 사용해서 카메라 및 색상을 구현
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;

namespace WinFormsCamera01
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;  // 카메라 캡처 객체
        private Mat frame;             // 현재 프레임을 저장할 객체
        private bool isRunning = false;  // 카메라가 실행 중인지 확인하는 변수
        private bool isColor = true;     // 컬러 모드인지 확인하는 변수

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            capture = new VideoCapture(0);  // 카메라 장치 연결
            frame = new Mat();
            capture.Set(VideoCaptureProperties.FrameWidth, 640);  // 프레임 너비 설정
            capture.Set(VideoCaptureProperties.FrameHeight, 480); // 프레임 높이 설정
        }

        private async void btnCamera_Click(object sender, EventArgs e)
        {
            if (isRunning)  // 이미 카메라가 실행 중이면
            {
                isRunning = false;  // 실행 중 상태를 false로 변경
                btnCamera.Text = "Start";  // 버튼 텍스트 변경
                return;
            }

            btnCamera.Text = "Stop";  // 버튼 텍스트 변경
            isRunning = true;  // 실행 중 상태를 true로 변경

            while (isRunning)  // 카메라가 실행 중이면
            {
                if (capture.IsOpened())  // 카메라가 연결되어 있으면
                {
                    capture.Read(frame);  // 프레임 읽기

                    if (!isColor)  // 흑백 모드이면
                    {
                        Cv2.CvtColor(frame, frame, ColorConversionCodes.BGR2GRAY);  // 컬러를 흑백으로 변경
                        Cv2.CvtColor(frame, frame, ColorConversionCodes.GRAY2BGR);  // 흑백을 다시 컬러로 변경 (PictureBox 호환을 위해)
                    }

                    picMain.Image = BitmapConverter.ToBitmap(frame);  // PictureBox에 영상 출력
                }
                await Task.Delay(33);  // 대략 30 fps
            }
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            isColor = false;  // 흑백 모드로 변경
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            isColor = true;   // 컬러 모드로 변경
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            isRunning = false;  // 카메라 중지
            capture.Release();  // 카메라 자원 해제
            this.Close();       // 프로그램 종료
        }
    }
}
-----------------------------------------------------------------
//GRB를 각각의 채널을 만들어 부여함 [ 행렬 ]
using OpenCvSharp;

namespace _20241017_ChannelMerge01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat ch0 = new Mat(3, 4, MatType.CV_8U, new Scalar(10));
            Mat ch1 = new Mat(3, 4, MatType.CV_8U, new Scalar(20));
            Mat ch2 = new Mat(3, 4, MatType.CV_8U, new Scalar(30));

            Mat[] bgr_arr = { ch0, ch1, ch2 };
            Mat bgr = new Mat();
            Cv2.Merge(bgr_arr, bgr);

            Mat[] bgr_vec = Cv2.Split(bgr);

            Console.WriteLine("[ch0] = \n" + ch0.Dump());
            Console.WriteLine("[ch1] = \n" + ch1.Dump());
            Console.WriteLine("[ch2] = \n" + ch2.Dump() + "\n");

            Console.WriteLine("[bgr] = \n" + bgr.Dump() + "\n");
            Console.WriteLine("[bgr_vec[0]] = \n" + bgr_vec[0].Dump());
            Console.WriteLine("[bgr_vec[1]] = \n" + bgr_vec[1].Dump());
            Console.WriteLine("[bgr_vec[2]] = \n" + bgr_vec[2].Dump());
        }
    }
}
-----------------------------------------------------------------
//채널 별로 색상 다르게 출력되는 것을 확
using OpenCvSharp;

namespace ColorChSplit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat(@"C:\\Temp\\opencv\\color.jpg", ImreadModes.Color);
            if (image.Empty())
            {
                throw new System.Exception("이미지를 불러올 수 없습니다.");
            }

            Mat[] bgr = Cv2.Split(image);

            Window windowImage = new Window("image", image);
            Window windowBlue = new Window("blue 채널", bgr[0]);
            Window windowGreen = new Window("green 채널", bgr[1]);
            Window windowRed = new Window("red 채널", bgr[2]);

            Cv2.WaitKey(0);
        }
    }
}
-----------------------------------------------------------------
//채널의 분리 및 합
using OpenCvSharp;

namespace _20241017_MixChannel01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 채널 0, 1, 2 생성 (각각의 값: 10, 20, 30)
            Mat ch0 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(10));
            Mat ch1 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(20));
            Mat ch2 = new Mat(new Size(4, 3), MatType.CV_8UC1, new Scalar(30));
            Mat ch_012 = new Mat();

            // 벡터로 병합
            List<Mat> vec_012 = new List<Mat> { ch0, ch1, ch2 };
            Cv2.Merge(vec_012.ToArray(), ch_012);

            // ch_13: 2채널, ch_2: 1채널
            Mat ch_13 = new Mat(ch_012.Size(), MatType.CV_8UC2);
            Mat ch_2 = new Mat(ch_012.Size(), MatType.CV_8UC1);

            Mat[] outMats = { ch_13, ch_2 };
            int[] from_to = { 0, 0, 2, 1, 1, 2 };

            // mixChannels 함수 사용
            Cv2.MixChannels(new Mat[] { ch_012 }, outMats, from_to);

            // 결과 출력
            Console.WriteLine("[ch_123] = ");
            Console.WriteLine(ch_012.Dump());
            Console.WriteLine();

            Console.WriteLine("[ch_13] = ");
            Console.WriteLine(ch_13.Dump());
            Console.WriteLine();

            Console.WriteLine("[ch_2] = ");
            Console.WriteLine(ch_2.Dump());
        }
    }
}
