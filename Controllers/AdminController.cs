using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Data;
using MyWedding.Models;
using MyWedding.Models.Enums;


namespace MyWedding.Controllers
{
    public class AdminController : Controller
    {
       private readonly ApplicationDbContext _dbContext;

       public AdminController(ApplicationDbContext dbContext)
       {
           _dbContext = dbContext;
       }

        public IActionResult Index()
        {
            return View(_dbContext.Guests.ToList());
        }

        [HttpPost]
        public IActionResult AddGuest([FromForm] string code, string name)
        {
            var guest = new Guest();
            guest.Code = code;
            guest.Name = name;
            _dbContext.Guests.Add(guest);
            _dbContext.SaveChanges();

            return View("Index", _dbContext.Guests.ToList());
        }
    }
}
