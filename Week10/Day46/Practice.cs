//날짜출력 Index.cshtml
@{
    ViewData["Title"] = "오늘 날짜";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    

    <h2>오늘은 @ViewData["CurrentDate"] 입니다.</h2>
</div>
------------------------------------------------------------------------
//날짜출력 Controller.cs
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebQuiz1.Models;

namespace WebQuiz1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["CurrentDate"] = DateTime.Now.ToString("yyyy년 M월 d일");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
------------------------------------------------------------------------
//Layout으로 Header와 Footer 디자인 Controller.cs
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebQuiz2.Models;

namespace WebQuiz2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
------------------------------------------------------------------------
//Layout으로 Header와 Footer 디자인 Index.cshtml
@{
    ViewData["Title"] = "Main Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <h1>Main Page</h1>
    <p>qazxc7093@naver.com</p>
</div>
------------------------------------------------------------------------
//Layout으로 Header와 Footer 디자인  Layout.cshtml
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - WebQuiz2</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/WebQuiz2.styles.css" asp-append-version="true" />
</head>
<body>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container-fluid">
            <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">WebQuiz2</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <header style="background-color: lightblue; padding: 10px;">
        <h2>Header</h2>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted" style="background-color: lightgrey; padding: 10px; text-align: center;">
        <div class="container">
            abc@naver.com
        </div>
    </footer>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
------------------------------------------------------------------------
//4칙연산 Index.cshtml
@{
    ViewData["Title"] = "QuizForm";
}

<h2>두 정수를 넣어주세요.</h2>

<form method="post">
    <div>
        <label for="number1">숫자 1:</label>
        <input type="number" id="number1" name="number1" required />
    </div>
    <div>
        <label for="number2">숫자 2:</label>
        <input type="number" id="number2" name="number2" required />
    </div>
    <div>
        <button type="submit">사칙연산</button>
    </div>
</form>

@if (ViewBag.Sum != null)
{
    <div style="margin-top: 20px;">
        <p style="font-size: 1.5em; font-weight: bold;">SUM: @ViewBag.Sum</p>
        <p style="font-size: 1.5em; font-weight: bold;">DIFFERENCE: @ViewBag.Difference</p>
        <p style="font-size: 1.5em; font-weight: bold;">PRODUCT: @ViewBag.Product</p>
        <p style="font-size: 1.5em; font-weight: bold;">DIVIDE: @ViewBag.Divide</p>
    </div>
}
------------------------------------------------------------------------
//4칙연산 Controller.cs
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebQuiz3.Models;

namespace WebQuiz3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int number1, int number2)
        {
            // 사칙연산 수행
            ViewBag.Sum = number1 + number2;
            ViewBag.Difference = number1 - number2;
            ViewBag.Product = number1 * number2;
            ViewBag.Divide = number2 != 0 ? (double)number1 / number2 : double.NaN;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
------------------------------------------------------------------------
//BMI측정 Controllers.cs
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebQuiz4.Models;

namespace WebQuiz4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(double height, double weight, string gender)
        {
            double standardWeight = 0;
            if (gender.ToLower() == "남성")
            {
                standardWeight = (height / 100) * (height / 100) * 22;
            }
            else if (gender.ToLower() == "여성")
            {
                standardWeight = (height / 100) * (height / 100) * 21;
            }

            double pibw = (weight / standardWeight) * 100;
            string weightStatus;

            if (pibw < 90)
                weightStatus = "저체중";
            else if (pibw < 110)
                weightStatus = "정상 체중";
            else if (pibw < 120)
                weightStatus = "과체중";
            else if (pibw < 130)
                weightStatus = "비만";
            else if (pibw < 160)
                weightStatus = "고도 비만";
            else
                weightStatus = "초고도 비만";

            ViewBag.Height = height;
            ViewBag.Weight = weight;
            ViewBag.Gender = gender;
            ViewBag.StandardWeight = standardWeight;
            ViewBag.PIBW = pibw;
            ViewBag.WeightStatus = weightStatus;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
------------------------------------------------------------------------
//BMI측정 Index.cshtml
@{
    ViewData["Title"] = "나의 체질량지수(BMI)";
}

<h2>나의 체질량지수(BMI)</h2>

<form method="post">
    <div>
        <label for="height">당신의 키를 입력해 주세요. (cm):</label>
        <input type="number" id="height" name="height" step="0.1" required />
    </div>
    <div>
        <label for="weight">당신의 몸무게를 입력해 주세요. (kg):</label>
        <input type="number" id="weight" name="weight" step="0.1" required />
    </div>
    <div>
        <label for="gender">당신의 성별을 선택해 주세요:</label>
        <select id="gender" name="gender" required>
            <option value="남성">남성</option>
            <option value="여성">여성</option>
        </select>
    </div>
    <div>
        <button type="submit">BMI 계산</button>
    </div>
</form>

<hr />
<h3>정상 체중 여부</h3>
<table border="1" cellpadding="10" cellspacing="0" style="width: 100%; border-collapse: collapse; text-align: center;">
    <thead style="background-color: #add8e6;">
        <tr>
            <th>정상 체중 여부</th>
            <th>표준 체중 대비 백분율(%)</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>저체중</td>
            <td>90% 미만</td>
        </tr>
        <tr>
            <td>정상 체중</td>
            <td>90% 이상 110% 미만</td>
        </tr>
        <tr>
            <td>과체중</td>
            <td>110% 이상 120% 미만</td>
        </tr>
        <tr>
            <td>경도 비만</td>
            <td>120% 이상 130% 미만</td>
        </tr>
        <tr>
            <td>중정도 비만</td>
            <td>130% 이상 160% 미만</td>
        </tr>
        <tr>
            <td>고도 비만</td>
            <td>160% 이상</td>
        </tr>
    </tbody>
</table>

@if (ViewBag.StandardWeight != null)
{
    <hr />
    <p>당신의 키: @ViewBag.Height cm</p>
    <p>당신의 몸무게: @ViewBag.Weight kg</p>
    <p>당신의 성별: @ViewBag.Gender</p>
    <p>표준 체중: @String.Format("{0:0.00}", ViewBag.StandardWeight) kg</p>
    <p>표준 체중 대비 백분율(PIBW): @String.Format("{0:0.00}", ViewBag.PIBW) %</p>
    <p>결과: 당신은 <strong>@ViewBag.WeightStatus</strong>에 속합니다.</p>
}
