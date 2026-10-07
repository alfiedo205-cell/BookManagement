using Microsoft.AspNetCore.Mvc;
using BookManagement.Models;

namespace BookManagement.Controllers
{
    public class BookController : Controller
    {
        // Danh sách sách mẫu
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "Lập trình C#",
                Price = 120000,
                Description = "Sách học lập trình C#"
            },

            new Book
            {
                Id = 2,
                Name = "ASP.NET Core MVC",
                Price = 150000,
                Description = "Sách học ASP.NET Core MVC"
            },

            new Book
            {
                Id = 3,
                Name = "Cơ sở dữ liệu",
                Price = 100000,
                Description = "Sách học SQL Server"
            }
        };

        // GET: /Book
        public IActionResult Index()
        {
            return View(books);
        }

        // GET: /Book/Detail/1
        public IActionResult Detail(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: /Book/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Book/Create
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            book.Id = books.Count + 1;
            books.Add(book);

            return RedirectToAction("Index");
        }
    }
}