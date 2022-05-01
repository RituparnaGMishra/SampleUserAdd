using Microsoft.AspNetCore.Mvc;
using SampleUserAdd.Models;
using SampleUserAdd.Services;

namespace SampleUserAdd.Controllers
{
    public class UserEntityController : Controller
    {
        private readonly IDbService _dbService;

        public UserEntityController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserEntityModel user)
        {
            string? message;
            if (ModelState.IsValid)
            {
               
                try
                {
                    if (_dbService.IsExistingEmail(user.Email) > 0)
                    {
                        ModelState.AddModelError("", "Email id duplicate");
                        return View();
                    }
                    else
                    {
                        _dbService.AddUser(user);
                        message = $"User {user.Email} saved successfully";
                    }
                }
                catch (Exception ex)
                {
                    message = $"User could not be saved because of following exception :{ex.Message}";
                }

            }
            else
            {
                return View();
            }
            TempData["message"] = message;
            return RedirectToAction("Create");
        }
    }
}
