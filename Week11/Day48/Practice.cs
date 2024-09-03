//Entity Framework 복습
{
    var context = new ProductDbContext();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    Console.WriteLine("데이터베이스 테이블이 생성되었습니다.");
    context.Dispose();
}

// CRUD
static void Main(string[] args)
{
    var context = new ProductDbContext();

    //context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();
    //Console.WriteLine("데이터베이스 테이블이 생성되었습니다.");

    // 데이터 삽입 (No 자동삽입 in MSSQL)
    //var product = new Product()
    //{
    //    Name = "3분카레",
    //    Zone = "경북 상주시"
    //};
    //context.Product.Add(product);
    //context.SaveChanges();
    //Console.WriteLine("데이터 삽입 성공");

    // 데이터 조회
    var list= context.Product.ToList();
    foreach (var item in list)
    {
        Console.WriteLine($"번호 : {item.No}, 제품명 : {item.Name}, 지역 : {item.Zone}");
    }

    context.Dispose();
}
--------------------------------------------------------------------------------
//ntity Framework써서 주소록 프로그램 만들기
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAddressQuiz
{
    public class Players
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ONum { get; set; }
        [MaxLength(10)]
        public int Round { get; set; }
        [MaxLength(10)]
        public int No { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Team { get; set; }
        [MaxLength(50)]
        public string Univ { get; set; }
    }

    public class PlayerDbContext : DbContext
    {
        public DbSet<Players> Players { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; " +
                        "Database = AndongDb; " +
                        "Trusted_Connection = True;" +
                        "Encrypt=False");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PlayerDbContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //Console.WriteLine("데이터베이스 테이블이 생성되었습니다.");
            }

            while (true) // 무한 루프 추가
            {
                // 메뉴 안내 메시지 출력
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("1. 데이터 삽입");
                Console.WriteLine("2. 데이터 삭제");
                Console.WriteLine("3. 데이터 조회");
                Console.WriteLine("4. 데이터 수정");
                Console.WriteLine("0. 종료");
                Console.Write("메뉴 번호를 입력하세요: "); // 사용자에게 메뉴 번호 입력 요청

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        case 1:
                            InsertData();
                            break;
                        case 2:
                            DeleteData();
                            break;
                        case 3:
                            ReadData();
                            break;
                        case 4:
                            UpdateData();
                            break;
                        case 0:
                            Console.WriteLine("프로그램을 종료합니다.");
                            return; // 프로그램 종료
                        default:
                            Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해 주세요.");
                }
            }
        }

        private static void InsertData()
        {
            using (var context = new PlayerDbContext())
            {
                Console.WriteLine("새로운 선수 정보를 입력하세요.");
                Console.Write("Round: ");
                int round = int.Parse(Console.ReadLine());
                Console.Write("No: ");
                int no = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Team: ");
                string team = Console.ReadLine();
                Console.Write("Univ: ");
                string univ = Console.ReadLine();

                var player = new Players { Round = round, No = no, Name = name, Team = team, Univ = univ };
                context.Players.Add(player);
                context.SaveChanges();

                Console.WriteLine("데이터가 삽입되었습니다.");
            }
        }

        private static void DeleteData()
        {
            using (var context = new PlayerDbContext())
            {
                Console.Write("삭제할 선수의 ONum을 입력하세요: ");
                int oNum = int.Parse(Console.ReadLine());

                var player = context.Players.FirstOrDefault(p => p.ONum == oNum);
                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    Console.WriteLine("데이터가 삭제되었습니다.");
                }
                else
                {
                    Console.WriteLine("해당 ONum을 가진 선수를 찾을 수 없습니다.");
                }
            }
        }

        private static void ReadData()
        {
            using (var context = new PlayerDbContext())
            {
                var players = context.Players.ToList();

                // 표의 헤더 출력
                Console.WriteLine("O/N    Round   No      Name           Team           Univ");
                Console.WriteLine("------------------------------------------------------------------");

                // 각 레코드를 표 형식으로 출력
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.ONum,-6} {player.Round,-7} {player.No,-7} {player.Name,-9} {player.Team,-8} {player.Univ}");
                }
            }
        }

        private static void UpdateData()
        {
            using (var context = new PlayerDbContext())
            {
                Console.Write("수정할 선수의 ONum을 입력하세요: ");
                int oNum = int.Parse(Console.ReadLine());

                var player = context.Players.FirstOrDefault(p => p.ONum == oNum);
                if (player != null)
                {
                    Console.Write($"새로운 Round (현재: {player.Round}): ");
                    player.Round = int.Parse(Console.ReadLine());

                    Console.Write($"새로운 No (현재: {player.No}): ");
                    player.No = int.Parse(Console.ReadLine());

                    Console.Write($"새로운 Name (현재: {player.Name}): ");
                    player.Name = Console.ReadLine();

                    Console.Write($"새로운 Team (현재: {player.Team}): ");
                    player.Team = Console.ReadLine();

                    Console.Write($"새로운 Univ (현재: {player.Univ}): ");
                    player.Univ = Console.ReadLine();

                    context.SaveChanges();
                    Console.WriteLine("데이터가 수정되었습니다.");
                }
                else
                {
                    Console.WriteLine("해당 ONum을 가진 선수를 찾을 수 없습니다.");
                }
            }
        }

    }
}
--------------------------------------------------------------------------------
//

--------------------------------------------------------------------------------
//CRUD 비동기의 동기화   [ Appsettings.json ]
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local)\\SQLEXPRESS;Database=AndongDb;Trusted_Connection=True; Encrypt=False;"
  },
  "AllowedHosts": "*"
}

--------------------------------------------------------------------------------
//모델 추가, Program.cs 설정
using Microsoft.EntityFrameworkCore;

namespace WebAddressBookExample.Models
{
    public class PersonDbContext : DbContext
    {
        // 생성자 = DbContext로 받을 땐 재정의로 생성해주기
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
--------------------------------------------------------------------------------
//Program.cs
using WebAddressBookExample.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAddressBookExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //////////////////////////////////////////////////////
            var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddDbContext<PersonDbContext>(item => item.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            //////////////////////////////////////////////////////
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
--------------------------------------------------------------------------------
//Controller
private readonly ILogger<HomeController> logger;
private readonly PersonDbContext context;

public HomeController(PersonDbContext _context, ILogger<HomeController> _logger)
{
    _logger = logger;
    context = _context;
}
--------------------------------------------------------------------------------
//View
@model IEnumerable<WebAddressBookExample.Models.Person>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.HP)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.HP)
            </td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.ID">Edit</a> |
                <a asp-action="Details" asp-route-id="@item.ID">Details</a> |
                <a asp-action="Delete" asp-route-id="@item.ID">Delete</a>
            </td>
        </tr>
}
    </tbody>
</table>
--------------------------------------------------------------------------------
//Create HttpPost
[HttpPost]
public IActionResult Create(Person person)
{
    if (ModelState.IsValid)
    {
        context.Persons.Add(person);
        context.SaveChanges();

        // 앞은 위치, 뒤는 Controller
        return RedirectToAction("Index", "Home");
    }
    return View();
}
--------------------------------------------------------------------------------
//Edit View
public IActionResult Edit(int? id)
{
    if (id == null || context.Persons == null) { return NotFound(); }

    // 정상적인 경우
    var personData = context.Persons.Find(id);
    if (personData == null) { return NotFound(); }

    return View();
}

[HttpPost] // 동기방식 제작
public IActionResult Edit(int? id, Person person)
{
    if (id != person.ID)
    {
        return NotFound();
    }
    if (ModelState.IsValid)
    {
        context.Update(person);
        // SaveChangesAsync = 비동기 방식일때 사용
        context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
    return View(person);

}
--------------------------------------------------------------------------------
//Details View
public IActionResult Details(int? id)
{
    if (id == null || context.Persons == null)
    {
        return NotFound();
    }

    var personData = context.Persons.FirstOrDefault(x => x.ID == id);

    if (personData == null)
    {
        return NotFound();
    }

    return View(personData);
}
--------------------------------------------------------------------------------
//Delete View
public IActionResult Delete(int? id)
{
    if (id == null || context.Persons == null)
    {
        return NotFound();
    }
    // ID 값을 읽어오기
    var personData = context.Persons.FirstOrDefault((x => x.ID == id));

    if (personData == null) { return NotFound(); }
    return View(personData);
}

// 삭제는 Post에서 작업
[HttpPost, ActionName("Delete")]
public IActionResult DeleteConfirmed(int? id)
{
    var personData = context.Persons.Find(id);
    if (personData != null)
    {
        context.Persons.Remove(personData);
    }
    context.SaveChanges();
    return RedirectToAction("Index", "Home");
}
--------------------------------------------------------------------------------
//HomeController.cs
using WebAddressBookQuiz.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace WebAddressBookQuiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly PlayerDbContext context;

        public HomeController(PlayerDbContext _context, ILogger<HomeController> _logger)
        {
            logger = _logger;
            context = _context;
        }

        public IActionResult Index()
        {
            var players = context.Players.ToList();
            
            return View(players);
            //return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                context.Players.Add(player);
                context.SaveChanges();

                // 앞은 위치, 뒤는 Controller
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || context.Players == null) return NotFound(); 

            // 정상적인 경우
            var playerData = context.Players.Find(id);
            if (playerData == null) { return NotFound(); }

            return View(playerData);
        }

        [HttpPost] // 동기방식 제작
        public IActionResult Edit(int? id, Player player)
        {
            if (id != player.ONum)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                context.Update(player);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(player);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || context.Players == null)
            {
                return NotFound();
            }

            var playerData = context.Players.FirstOrDefault(x => x.ONum == id);

            if (playerData == null)
            {
                return NotFound();
            }

            return View(playerData);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || context.Players == null)
            {
                return NotFound();
            }
            // ID 값을 읽어오기
            var playerData = context.Players.FirstOrDefault((x => x.ONum == id));

            if (playerData == null) { return NotFound(); }
            return View(playerData);
        }

        // 삭제는 Post에서 작업
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var playerData = context.Players.Find(id);
            if (playerData != null)
            {
                context.Players.Remove(playerData);
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
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
--------------------------------------------------------------------------------
//Player.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAddressBookQuiz.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ONum { get; set; }
        public int Round { get; set; }
        public int No { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Team { get; set; }
        [MaxLength(50)]
        public string Univ { get; set; }
    }
}
