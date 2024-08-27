//
namespace MVCEmptyApp01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // MVC로 넘어가는 중간단계 처리
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            //app.MapGet("/greet", () => "<h1>안녕하세요!</h1>");
            //app.MapGet("/help", () => "도와주세요~");

            // 매핑
            app.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                );
            app.Run();
        }
    }
}
-----------------------------------------------------------------------------
//라우팅
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetProductById(int id)
    {
        // /api/products/{id} 경로로 응답한다.
        return Ok();
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateProduct()
    {
        // /api/products/create 경로로 응답한다.
        return Ok();
    }
}
-----------------------------------------------------------------------------
//라우팅 어노테이션으로 숫자 전달
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    public class HelpController : ControllerBase
    {
        // /Help 또는 /Help/{id} 경로로 요청을 처리한다.
        [Route("/Help/{id?}")]
        public int Help(int? id)
        {
            if (id.HasValue)
            {
                // id가 전달된 경우, 해당 ID를 반환한다.
                return id.Value;
            }
            else
            {
                // id가 전달되지 않은 경우, 기본값으로 0을 반환한다.
                return 0;
            }
        }
    }
}  
-----------------------------------------------------------------------------
 //HomeController밖에서 Route적용하고 적용할 Route 설정
using Microsoft.AspNetCore.Mvc;

namespace _20240827_MVCEmptyApp02.Controllers
{
		[Route("[Controller]")]
    public class HomeController : Controller
    {
		    [Route("~/")]
				[Route("/Home")]
				[Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
        public int Help(int? id)
        {
		        return id?? 100;
        }
    }
}
-----------------------------------------------------------------------------
//
namespace MVCEmptyApp02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Home", async (context) =>
                {
                    context.Response.ContentType = "text/plain; charset=utf-8"; //한글문제 해법
                    await context.Response.WriteAsync("홈페이지 입니다. - Get");
                });
                endpoints.MapPost("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Post");
                });
                endpoints.MapDelete("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Delete");
                });
                endpoints.MapPut("/Home", async (context) =>
                {
                    await context.Response.WriteAsync("홈페이지 입니다. - Put");
                });

            });

            //app.MapGet("/Home", () => "Hello World! --- GET");
            //app.MapPost("/Home", () => "Hello World! --- POST");
            //app.MapPut("/Home", () => "Hello World! --- PUT");
            //app.MapDelete("/Home", () => "Hello World! --- DELETE");

            app.Run(async (HttpContext context) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("페이지를 찾을 수 없습니다.");
            });

            app.Run();
        }
    }
}
-----------------------------------------------------------------------------
//로그인화면 웹앱
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>로그인 페이지</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .login-container {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
            margin: 0 20px; /* 좌우 여백을 균형 있게 설정 */
            box-sizing: border-box; /* 패딩 포함 계산 */
        }

        h2 {
            text-align: center;
            color: #333;
        }

        .input-group {
            margin: 15px 0;
        }

        .input-group label {
            display: block;
            margin-bottom: 5px;
            color: #555;
        }

        .input-group input {
            width: 100%; /* 100% 너비로 조정 */
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box; /* 패딩 포함 계산 */
        }

        .btn {
            width: 100%; /* 버튼도 100%로 설정 */
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            box-sizing: border-box; /* 패딩 포함 계산 */
        }

        .btn:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>

    <div class="login-container">
        <h2>로그인</h2>
        <div class="input-group">
            <label for="username">아이디</label>
            <input type="text" id="username" placeholder="아이디를 입력하세요">
        </div>
        <div class="input-group">
            <label for="password">비밀번호</label>
            <input type="password" id="password" placeholder="비밀번호를 입력하세요">
        </div>
        <button class="btn">로그인</button>
    </div>

</body>
</html>
-----------------------------------------------------------------------------
//계산기 웹앱
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>계산기</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .calculator-container {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
            margin: 0 20px;
            box-sizing: border-box;
        }

        h2 {
            text-align: center;
            color: #333;
        }

        .input-group {
            margin: 15px 0;
        }

        .input-group label {
            display: block;
            margin-bottom: 5px;
            color: #555;
        }

        .input-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box;
        }

        .input-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box;
        }

        .btn {
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            box-sizing: border-box;
        }

        .btn:hover {
            background-color: #45a049;
        }

        .result {
            margin-top: 20px;
            font-size: 18px;
            text-align: center;
            color: #333;
        }
    </style>
</head>
<body>

    <div class="calculator-container">
        <h2>계산기</h2>
        <div class="input-group">
            <label for="num1">정수 1</label>
            <input type="number" id="num1" placeholder="첫 번째 정수를 입력하세요">
        </div>
        <div class="input-group">
            <label for="num2">정수 2</label>
            <input type="number" id="num2" placeholder="두 번째 정수를 입력하세요">
        </div>
        <div class="input-group">
            <label for="operation">연산 선택</label>
            <select id="operation">
                <option value="add">더하기</option>
                <option value="subtract">빼기</option>
                <option value="multiply">곱하기</option>
                <option value="divide">나누기</option>
            </select>
        </div>
        <button class="btn" onclick="calculate()">계산하기</button>
        <div class="result" id="result"></div>
    </div>

    <script>
        function calculate() {
            // 입력 값 가져오기
            const num1 = parseFloat(document.getElementById('num1').value);
            const num2 = parseFloat(document.getElementById('num2').value);
            const operation = document.getElementById('operation').value;
            let result = '';

            // 계산 로직
            if (isNaN(num1) || isNaN(num2)) {
                result = "올바른 숫자를 입력하세요.";
            } else {
                if (operation === 'add') {
                    result = num1 + num2;
                } else if (operation === 'subtract') {
                    result = num1 - num2;
                } else if (operation === 'multiply') {
                    result = num1 * num2;
                } else if (operation === 'divide') {
                    if (num2 === 0) {
                        result = "0으로 나눌 수 없습니다.";
                    } else {
                        result = num1 / num2;
                    }
                }
            }

            // 결과 출력
            document.getElementById('result').innerText = "결과: " + result;
        }
    </script>

</body>
</html>


