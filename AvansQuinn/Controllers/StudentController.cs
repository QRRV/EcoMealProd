using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealBoxApp.Controllers;

public class StudentController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;

    private readonly UserManager<IdentityUser> _userManager;

    // GET
    public StudentController(IMealBoxRepository mealBoxRepository, UserManager<IdentityUser> userManager)
    {
        _mealBoxRepository = mealBoxRepository;
        _userManager = userManager;
    }

    [Authorize(Roles = "Student")]
    public IActionResult Reserved()
    {
        var mealBoxes = _mealBoxRepository.GetMealBox(_userManager.GetUserName(User.Clone()));
        return View(mealBoxes);
    }
}