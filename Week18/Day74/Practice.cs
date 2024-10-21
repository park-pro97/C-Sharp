//Console에서 카메라 구현
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

            Mat frame = new Mat();
            while (true)
            {
                // 카메라에서 프레임 읽기
                capture.Read(frame);
                if (frame.Empty())
                    break;

                // 채널 분리
                Mat[] channels = Cv2.Split(frame);

                // 각 채널에 대해 최소값, 최대값을 찾고 정규화
                for (int i = 0; i < channels.Length; i++)
                {
                    double minVal, maxVal;
                    Cv2.MinMaxIdx(channels[i], out minVal, out maxVal);
                    double ratio = (maxVal - minVal) / 255.0;

                    channels[i].ConvertTo(channels[i], MatType.CV_64F);  // 정밀한 계산을 위해 CV_64F로 변환
                    Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]);  // 최소값 빼기
                    Cv2.Divide(channels[i], new Scalar(ratio), channels[i]);  // 정규화
                    channels[i].ConvertTo(channels[i], MatType.CV_8U);  // 다시 CV_8U로 변환
                }

                // 채널 병합하여 개선된 이미지 생성
                Cv2.Merge(channels, frame);

                // 노출 정보 출력
                StringUtil su = new StringUtil();
                su.PutString(frame, "EXPOS: ", new Point(10, 40), capture.Get(VideoCaptureProperties.Exposure));

                // 결과 영상 보여주기
                Cv2.ImShow("카메라 영상보기", frame);

                // 키 입력 대기 (30ms)
                if (Cv2.WaitKey(30) >= 0)
                    break;
            }
            Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------
//WinForm에서 카메라 구현
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20241021_CVCameraWinFormUp
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
        private bool isCameraRunning = false;
        private bool isEnhanced = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            if (!isCameraRunning)
            {
                capture = new VideoCapture(0); // 카메라 ID 0 (기본 웹캠)
                frame = new Mat();
                isCameraRunning = true;
                Application.Idle += ProcessFrame; // 카메라 영상 실시간 처리
            }
        }

        // 실시간으로 카메라 프레임을 처리하는 메소드
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capture != null && capture.IsOpened())
            {
                capture.Read(frame); // 카메라로부터 프레임 읽기

                if (!frame.Empty())
                {
                    if (isEnhanced)
                    {
                        // 화질 개선 코드
                        Mat[] channels = Cv2.Split(frame);

                        for (int i = 0; i < channels.Length; i++)
                        {
                            double minVal, maxVal;
                            Cv2.MinMaxLoc(channels[i], out minVal, out maxVal); // 최소, 최대 값 찾기

                            // 밝기 및 대비 조정 (비율을 통해 조정)
                            double ratio = (maxVal - minVal) / 255.0;
                            Cv2.Subtract(channels[i], new Scalar(minVal), channels[i]); // 최소값 빼기
                            Cv2.Divide(channels[i], new Scalar(ratio), channels[i]);    // 비율로 나누기

                            // 밝기 조정 (너무 어두운 경우를 방지)
                            Cv2.Add(channels[i], new Scalar(20), channels[i]); // 20 정도 밝기 증가
                            channels[i].ConvertTo(channels[i], MatType.CV_8U);  // 다시 8비트로 변환
                        }

                        // 채널 병합
                        Mat adjustedFrame = new Mat();
                        Cv2.Merge(channels, adjustedFrame);

                        // 개선된 이미지를 PictureBox에 출력
                        image = BitmapConverter.ToBitmap(adjustedFrame);
                    }
                    else
                    {
                        // 원본 이미지 출력
                        image = BitmapConverter.ToBitmap(frame);
                    }

                    picMain.Image = image; // PictureBox에 이미지 출력
                }
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            if (isCameraRunning)
            {
                Application.Idle -= ProcessFrame;
                capture.Release(); // 카메라 리소스 해제
                isCameraRunning = false;

                // PictureBox를 빈 화면으로 설정 (화면 초기화)
                picMain.Image = null;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // 버튼을 눌러 화질 개선 상태를 전환
            isEnhanced = !isEnhanced;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isCameraRunning)
            {
                Application.Idle -= ProcessFrame; // 이벤트 핸들러 제거
                capture.Release(); // 카메라 리소스 해제
            }
            this.Close(); // 프로그램 종료
        }
    }
}
---------------------------------------------------------------
//화소 밝기 변환
using OpenCvSharp;
using System.Net;

namespace GrayscaleImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 50행, 512열 영상을 생성
            Mat image1 = new Mat(50, 512, MatType.CV_8UC1, new Scalar(0));
            Mat image2 = new Mat(50, 512, MatType.CV_8UC1, new Scalar(0));

            for (int i=0; i<image1.Rows; i++)
            {
                for (int j = 0; j < image1.Cols; j++)
                {
                    image1.Set<byte>(i, j, (byte)Math.Min(j / 2, 255));
                    image2.Set<byte>(i, j, (byte)Math.Min((j / 20) * 10, 255));
                }
            }

            Cv2.ImShow("image1", image1); Cv2.ImShow("image2", image2);
            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------
//밝게, 어둡게, 반전, 효과를 나타내보기
using OpenCvSharp;

namespace BrightDark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat("c:\\Temp\\opencv\\bright.jpg", ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("영상을 읽지 못 했습니다.");
                Environment.Exit(1);
            }

            // 밝게 만드는 연산 (image + 100)
            Mat dst1 = new Mat();
            Cv2.Add(image, new Scalar(100), dst1);

            // 어둡게 만드는 연산 (image - 100)
            Mat dst2 = new Mat();
            Cv2.Subtract(image, new Scalar(100), dst2);

            Mat dst3 = new Mat(image.Size(), image.Type(), Scalar.All(255)) - image;

            Mat dst4 = new Mat(image.Size(), image.Type());
            Mat dst5 = new Mat(image.Size(), image.Type());

            for (int i = 0; i < image.Rows; i++)
            {
                for (int j = 0; j < image.Cols; j++)
                {
                    dst4.At<byte>(i, j) = (byte)(Math.Min(image.At<byte>(i, j) + 100, 255));
                    dst5.At<byte>(i, j) = (byte)(255 - image.At<byte>(i, j));
                }
            }

            Cv2.ImShow("원 영상", image);
            Cv2.ImShow("dst1 - 밝게", dst1);
            Cv2.ImShow("dst2 - 어둡게", dst2);
            Cv2.ImShow("dst3 - 반전", dst3);
            Cv2.ImShow("dst4 - 밝게", dst4);
            Cv2.ImShow("dst5 - 반전", dst5);
            Cv2.WaitKey();
        }
    }
}
---------------------------------------------------------------
//행렬을 이용한 영상 합성 [ 덧셈, 곱셈 ]
using OpenCvSharp;

namespace ImageSynthesis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image1 = new Mat("C:\\Temp\\opencv\\add2.jpg", ImreadModes.Grayscale);
            Mat image2 = new Mat("C:\\Temp\\opencv\\add1.jpg", ImreadModes.Grayscale);
            if (image1.Empty() || image2.Empty())
            {
                Console.WriteLine("영상을 읽지 못 했습니다.");
                Environment.Exit(1);
            }

            double alpha = 0.5, beta = 0.85; // 황금비율 가설, 곱셈 비율
            Mat add_img1 = image1 + image2; // 영상 합성
            Mat add_img2 = image1 * 0.5 + image2 * 0.5;
            Mat add_img3 = image1 * alpha + image2 * (1 - alpha);

            Mat add_img4 = new Mat();
            Cv2.AddWeighted(image1, alpha, image2, beta, 0, add_img4);

            Cv2.ImShow("image1", image1); Cv2.ImShow("image2", image2);
            Cv2.ImShow("add_image1", add_img1); Cv2.ImShow("add_image2", add_img2);
            Cv2.ImShow("add_image3", add_img3); Cv2.ImShow("add_image4", add_img4);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
---------------------------------------------------------------
//명암 대비
using OpenCvSharp;

namespace Contrast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mat image = new Mat("C:\\Temp\\opencv\\contrast_test.jpg", ImreadModes.Grayscale);

            if (image.Empty())
            {
                Console.WriteLine("영상을 읽지 못 했습니다.");
                Environment.Exit(1);
            }

            //Scalar avg = Cv2.Mean(image) / 2.0;
            Scalar meanValue = Cv2.Mean(image);
            double avg = meanValue.Val0 / 2.0;

            Mat dst1 = image * 0.5;
            Mat dst2 = image * 2.0;
            //Mat dst3 = image * 0.5 + avg[0];
            //Mat dst4 = image * 2.0 - avg[0];
            Mat dst3 = new Mat();
            Mat dst4 = new Mat();

            Cv2.AddWeighted(image, 0.5, Mat.Ones(image.Size(), image.Type()) * avg, 1, 0, dst3);
            Cv2.AddWeighted(image, 2.0, Mat.Ones(image.Size(), image.Type()) * -avg, 1, 0, dst4);

            Cv2.ImShow("image", image);
            Cv2.ImShow("dst1-대비감소", dst1);
            Cv2.ImShow("dst2-대비증가", dst2);
            Cv2.ImShow("dst3-평균이용 대비감소", dst3);
            Cv2.ImShow("dst4-평균이용 대비증가", dst4);

            Cv2.WaitKey(0); Cv2.DestroyAllWindows();
        }
    }
}
