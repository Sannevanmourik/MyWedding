using Microsoft.AspNetCore.Mvc;
using MyWedding.Data;
using System.Linq;
using MyWedding.Models.Enums;

namespace MyWedding.Controllers
{
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public GuestController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Index([FromForm] string code)
        {
            var guest = _dbContext.Guests.FirstOrDefault(x => x.Code == code);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        [HttpPost]
        public IActionResult SaveResponse([FromForm] int id, bool isAttending, EMealType mealType, string comments)
        {
            var guest = _dbContext.Guests.FirstOrDefault(x => x.Id == id);

            guest.IsAttending = isAttending;
            guest.MealType = mealType;
            guest.Comments = comments;
            guest.HasResponded = true;

            _dbContext.Guests.Update(guest);
            _dbContext.SaveChanges();

            return View();
        }
    }
}
