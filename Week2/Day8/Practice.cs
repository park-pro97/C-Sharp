// 상속, 다향성 연습
namespace OOPApp07
{
    class Shape
    {
        public int vertex; //멤버변수
        public Shape() //Default Constructor(디폴트 생성자)
        {
            vertex = 0;
        }
        public void ShowVertex()
        {
            Console.WriteLine(vertex);
        }
        public void ShowVertex(string msg)
        {
            Console.WriteLine(msg + " "+ vertex);
        }
        public void ShowVertex(string msg, string position, int repeat)
        {
            Console.WriteLine(msg + " " + vertex + " 현재 지역은 " + position + " 반복 횟수는 " + repeat);
        }
        public virtual void ShowName()
        {
            Console.WriteLine("도형입니다.");
        }
    }
        class Triangle : Shape
        {
            public Triangle()
            {
                vertex = 3;
            }

        public override void ShowName()
        {
            Console.WriteLine();
        }
    }
        class Circle : Shape
        {
            
        }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            triangle.ShowVertex();
            triangle.ShowVertex("곡짓점의 개수: ");
            triangle.ShowVertex("곡짓점의 개수: ", "안동,", 3);

            triangle.ShowName();
            Circle circle = new Circle();
            //
        }
    }
}


----------------------------------------------------------------------------------------------------------------------------
//Quiz 상속, 다향성
