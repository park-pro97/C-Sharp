//HomeController.cs 코드
using Microsoft.AspNetCore.Mvc;

namespace EmptyQuizApp01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyPage()
        {
            return View();

        }

        [HttpGet]
        public IActionResult InputQuiz()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OutputQuiz(int number)
        {
            // 받은 number를 다시 넘겨주는 역할
            ViewBag.Result = number;
            return View();
        }
    }
}
----------------------------------------------------------------------
//cshtml 코드
// InputQuiz.cshtml

@{
    ViewData["Title"] = "InputQuiz";
}

<h1>InputQuiz</h1>

<form action="Home/OutputQuiz" method="post">
    <label>숫자를 넣어주세요 : </label>
    <input type="number" id="number" name="number" required/>
    <input type="submit" value="계산" />
</form>

// OutputQuiz.cshtml

@{
    ViewData["Title"] = "OutputQuiz";
}

@if (ViewBag.Result != null)
{
    <h3>결과는 : @ViewBag.Result </h3>
}
----------------------------------------------------------------------
// ViewData를 가져오는 코드 @써서
@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<h3>@TempData["person1"]</h3>
<h3>@ViewData["person2"]</h3>
----------------------------------------------------------------------
// Controller 코드
using Microsoft.AspNetCore.Mvc;

namespace ViewDataEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["person1"] = "홍길동";
            ViewData["person2"] = "이순신";

            return View();
        }

        public IActionResult UseViewBag()
        {
            ViewBag.data1 = "data1";
            ViewBag.data2 = 100;
            ViewBag.data3 = DateTime.Now.ToShortDateString();

            string[] arr = { "사과", "배", "딸기" };
            ViewBag.data4 = arr;

            ViewBag.data5 = new List<string>()
            {
                "축구", "야구", "농구"
            };

            return View();
        }
    }
}
----------------------------------------------------------------------
//UseViewBag.cshtml 코드
@{
    ViewData["Title"] = "UseViewBag";
}

<h1>UseViewBag</h1>

<p></p>
<span>첫 번째 데이터 : </span><b>@ViewBag.data1 </b>
<br />
<span>두 번째 데이터 : </span><b>@ViewBag.data2 </b>
<br />
<span>세 번째 데이터 : </span><b>@ViewBag.data3 </b>
<br />

@{
    foreach(var item in ViewBag.data4)
    {
        <h3>@item</h3>
    }
}

@{
    foreach(var item in ViewBag.data5)
    {
        <h3>@item</h3>
    }
}
----------------------------------------------------------------------
//TempData는 저장 기능을 함
  public IActionResult About()
{
    TempData.Keep("data3");
    return View();
}
----------------------------------------------------------------------
//Builder Pattern Console      --Program.cs

namespace BuilderPatternConsole
{
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GraphicsCard { get; set; }

        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}, Storage: {Storage}, Graphics Card: {GraphicsCard}";
        }
    }

    public interface IComputerBuilder
    {
        void SetCPU();
        void SetRAM();
        void SetStorage();
        void SetGraphicsCard();
        Computer GetComputer();
    }
    
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i9";
        }

        public void SetRAM()
        {
            computer.RAM = "32GB";
        }

        public void SetStorage()
        {
            computer.Storage = "2TB SSD";
        }

        public void SetGraphicsCard()
        {
            computer.GraphicsCard = "NVIDIA RTX 3080";
        }

        public Computer GetComputer()
        {
            return computer;
        }
    }

    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core i5";
        }

        public void SetRAM()
        {
            computer.RAM = "16GB";
        }
        public void SetStorage()
        {
            computer.Storage = "512GB SSD";
        }
        public void SetGraphicsCard()
        {
            computer.GraphicsCard = "Integrated Graphics";
        }
        public Computer GetComputer()
        {
            return computer;
        }
    }

    public class InternetCafeComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public void SetCPU()
        {
            computer.CPU = "Intel Core I7";
        }
        public void SetRAM()
        {
            computer.RAM = "32GB";
        }
        public void SetStorage()
        {
            computer.Storage = "1TB SSD";
        }
        public void SetGraphicsCard()
        {
            computer.GraphicsCard = "NVIDIA GTX 1650 Super";
        }
        public Computer GetComputer()
        {
            return computer;
        }
    }

    public class Director
    {
        public Computer BuildComputer(IComputerBuilder builder)
        {
            builder.SetCPU();
            builder.SetRAM();
            builder.SetStorage();
            builder.SetGraphicsCard();
            return builder.GetComputer();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();

            IComputerBuilder gamingBuilder = new GamingComputerBuilder();
            Computer gamingComputer = director.BuildComputer(gamingBuilder);
            Console.WriteLine("게이밍 컴퓨터:");
            Console.WriteLine(gamingComputer);

            IComputerBuilder officeBuilder = new OfficeComputerBuilder();
            Computer officeComputer = director.BuildComputer(officeBuilder);
            Console.WriteLine("오피스 컴퓨터:");
            Console.WriteLine(officeComputer);

            IComputerBuilder internetCafeBuilder = new InternetCafeComputerBuilder();
            Computer internetCafe = director.BuildComputer(internetCafeBuilder);
            Console.WriteLine("PC방 컴퓨터:");
            Console.WriteLine(internetCafe);
        }
    }
}
----------------------------------------------------------------------
//Method Chain 방식으로
namespace BuilderPatternConsole
{
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GraphicsCard { get; set; }

        public override string ToString()
        {
            return $"CPU: {CPU}, RAM: {RAM}, Storage: {Storage}, Graphics Card: {GraphicsCard}";
        }
    }

    public interface IComputerBuilder
    {
        IComputerBuilder SetCPU(string cpu);
        IComputerBuilder SetRAM(string ram);
        IComputerBuilder SetStorage(string storage);
        IComputerBuilder SetGraphicsCard(string graphicsCard);
        Computer Build();
    }

    public class ComputerBuilder : IComputerBuilder
    {
        private Computer computer = new Computer();

        public IComputerBuilder SetCPU(string cpu)
        {
            computer.CPU = cpu;
            return this;
        }

        public IComputerBuilder SetRAM(string ram)
        {
            computer.RAM = ram;
            return this;
        }

        public IComputerBuilder SetStorage(string storage)
        {
            computer.Storage = storage;
            return this;
        }

        public IComputerBuilder SetGraphicsCard(string graphicsCard)
        {
            computer.GraphicsCard = graphicsCard;
            return this;
        }

        public Computer Build()
        {
            return computer;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IComputerBuilder gamingBuilder = new ComputerBuilder();
            Computer gamingComputer = gamingBuilder
                                        .SetCPU("Intel Core i9")
                                        .SetRAM("32GB")
                                        .SetStorage("1TB SSD")
                                        .SetGraphicsCard("NVIDIA RTX 3080")
                                        .Build();
            Console.WriteLine("게이밍 컴퓨터:");
            Console.WriteLine(gamingComputer);

            IComputerBuilder officeBuilder = new ComputerBuilder();
            Computer officeComputer = officeBuilder
                                        .SetCPU("Intel Core i5")
                                        .SetRAM("16GB")
                                        .SetStorage("512GB SSD")
                                        .SetGraphicsCard("Integrated Graphics")
                                        .Build();
            Console.WriteLine("오피스 컴퓨터:");
            Console.WriteLine(officeComputer);
        }
    }
}
----------------------------------------------------------------------
//Model & ViewData 가장 원초적인 코드
using Microsoft.AspNetCore.Mvc;

namespace _20240828_ModelDataMVC.Models
{
    public class StudentModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public string Major { get; set; }
    }
}
----------------------------------------------------------------------
//IStudent.cs [ id만 적어도 데이터 가져옴 ]
public StudentModel getStudentById(int id)
{
    return DataSource().Where(x => x.ID == id).FirstOrDefault();
}

// StudentRepository.cs 아래 DataSource, 이 곳의 1번 아이디가 도출됨을 확인 가능
private List<StudentModel> DataSource()
{

    return new List<StudentModel>
    {
        new StudentModel { ID = 1, Name = "홍길동", HP = "010-1111-1111", Major = "컴퓨터공학"},
        new StudentModel { ID = 2, Name = "이순신", HP = "010-2222-2222", Major = "정보통신공학"},
        new StudentModel { ID = 3, Name = "강감찬", HP = "010-3333-3333", Major = "기계설계"},
    };
}

