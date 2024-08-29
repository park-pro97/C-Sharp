//TempData 활용퀴즈 [ TempData, ViewBag 사용 ]
//Output.cshtml
@{
    ViewData["Title"] = "Output";

    // TempData["temp01"]를 배열로 캐스팅
    string[] foods = TempData["temp01"] as string[];
}

<h1>Output</h1>

<div class="text-center">
    @{
        if (foods != null)
        {
            foreach (var food in foods)
            {
                <h3>@food</h3> <!-- TempData의 데이터를 ViewBag과 동일한 형식으로 출력 -->
            }
        }
        else
        {
            <h3>음식 목록이 없습니다.</h3>
        }

        foreach (var item in ViewBag.data1)
        {
            <h3>@item</h3> <!-- ViewBag 데이터를 출력 -->
        }
    }
</div>
---------------------------------------------------------------------------------------------------
//TempData 활용퀴즈 [ TempData, ViewBag 사용 ]
//MyController.cs
using Microsoft.AspNetCore.Mvc;

namespace _20240829_ViewBagQuiz01.Controllers
{
    public class MyController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Output()
        {
            ViewBag.data1 = new List<string>()
            {
                "C#프로그래밍", "Java 정복", "HTML5", "CSS 하루만에하기"
            };

            string[] foods = { "된장국", "김치찌개", "소금빵", "두루치기" };
            TempData["temp01"] = foods;
            return View();
        }
    }
}
---------------------------------------------------------------------------------------------------
//index.cshtml
@* @model StronTypedView01.Models.Employee *@

@model List<StronTypedView01.Models.Employee>

@{
	ViewData["Title"] = "Home Page";
}

<div class="text-center">
	<h1 class="display-4">Welcome</h1>
	<hr />

	@* <h3>@Model.EmpID</h3>
	<h3>@Model.EmpName</h3>
	<h3>@Model.Designation</h3>
	<h3>@Model.Salary</h3> *@

	<hr />
	@{
		foreach (var item in Model)
		{
			@item.EmpID<br />
			@item.EmpName<br />
			@item.Designation<br />
			@item.Salary<br />
		}
	}

</div>

---------------------------------------------------------------------------------------------------
//ViewImports

List<Student> students = new List<Student>();

Student st1 = new Student();
st1.ID = 1; st1.Name = "홍길동"; st1.HP = "010-1111-1111";
students.Add(st1);
Student st2 = new Student();
st1.ID = 2; st1.Name = "이순신"; st1.HP = "010-2222-2222";
students.Add(st2);
Student st3 = new Student();
st1.ID = 3; st1.Name = "강감찬"; st1.HP = "010-3333-3333";
students.Add(st3);

return View(students);

// 더 쉬운 방식

 List<Student> students = new List<Student>
            {
                new Student{ Id = 1, Name="홍길동", Hp="010-1111-1111"},
                new Student{ Id = 2, Name="이순신", Hp="010-2222-2222"},
                new Student{ Id = 3, Name="강감찬", Hp="010-3333-3333"},

            };
return View(students);
---------------------------------------------------------------------------------------------------
//TagHelpers [ _ViewImports.cshtml 안에 있음 ]
  @{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">TagHelpers</h1>
</div>
<hr/>
<div>

    @* 링크 만들기 
       아래는 전부 같은 표현 *@
    <a href="/Home/Contact">Contact Page 1</a>

    @Html.ActionLink("Contact Page 2", "Contact", "Home") <br />

    <a href="@Url.Action("Contact", "Home")">Contact Page 3</a> <br />

    <a asp-controller="Home" asp-action="Contact">Contact Page 4</a>

</div>
---------------------------------------------------------------------------------------------------
//ImageTagHelpers[ index.cshtml ]
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>

    <img src="~/images/kk001.jpg" asp-append-version="true" 
     width="600" height="400" title="책, Book, 책" />

</div>
---------------------------------------------------------------------------------------------------
//TagHelpers_Form
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
</div>

<div class ="container">
    <div>
        <div class="row">
            <div class="col-sm-4">
                <form class="d-grid" asp-action="Index" asp-controller="Home" method="post">
                    <label>이름 : </label>
                    <input placeholder="이름을 입력해주세요." class="form-control" />
                    <br />
                </form>
            </div>
        </div>
    </div>

</div>
---------------------------------------------------------------------------------------------------
//Student.cs[ 모델에 포함된 ]
namespace TagHelpers_Form.Models
{
    public class Student
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Hp { get; set; }

        public string IsEmployed { get; set; }
        public string Description { get; set; }
    }
    public enum Gender
    {
        남, 여
    }
}
---------------------------------------------------------------------------------------------------
//Enum Gender Class 사용
@model TagHelpers_Form.Models.Student;

@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
</div>

<div class ="container">
    <div>
        <div class="row">
            <div class="col-sm-4">
                <form class="d-grid" asp-action="Index" asp-controller="Home" method="post">
                    <label asp-for="Name">이름 : </label>
                    <input asp-for="Name" placeholder="이름을 입력해주세요." class="form-control" />
                    <br />
                    <select asp-for="Gender" class="form-control" asp-items="Html.GetEnumSelectList<Gender>()">
                        <option value="">성별을 선택해 주세요.</option>
                    </select>
                    <br />
                    <input asp-for="Age" placeholder="나이를 입력해주세요." class="form-control" />
                    <br />
                    <input asp-for="Hp" placeholder="전화번호를 입력하세요." class="form-control" />
                    <br />
                    <label>취업여부</label>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" type="radio" asp-for="IsEmployed" value="취업중">
                        <label class="form-check-label" for="flexRadioDefault1">
                            취업중
                        </label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="radio" type="radio" asp-for="IsEmployed" value="미취업" checked>
                        <label class="form-check-label" for="flexRadioDefault2">
                            미취업
                        </label>
                    </div>
                    <br />
                    <textarea asp-for="Description" class="form-control" rows="5"
                      placeholder="학생 상세정보가 있다면 적어주세요."> </textarea>
                    <br />
                    <input type="submit" value="OK" class="btn btn-outline-primary" />
                </form>
            </div>
        </div>
    </div>

</div>
