using System.Diagnostics;
using AvansQuinn.Models;
using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealBoxApp.Controllers;

public class HomeController : Controller
{
    private readonly ICanteenRepository _canteenRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly UserManager<IdentityUser> _userManager;
    public HomeController(IMealBoxRepository mealBoxRepository,
        UserManager<IdentityUser> userManager, ICanteenRepository canteenRepository,
        IEmployeeRepository employeeRepository, IStudentRepository studentRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _userManager = userManager;
        _canteenRepository = canteenRepository;
        _employeeRepository = employeeRepository;
        _studentRepository = studentRepository;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        ICollection<MealBox?> mealBoxes;
        var canteens = _canteenRepository.GetCanteens();
        ViewBag.Canteens = canteens;

        if (User.IsInRole("Employee"))
        {
            var userEmployee = _employeeRepository.GetEmployee(_userManager.GetUserName(User.Clone()));
            if (TempData["Button"] != null)
            {
                if (TempData["Button"]!.Equals("Same"))
                {
                    TempData["Canteen"] = userEmployee!.Canteen.City + " " +
                                          char.ToUpper(userEmployee.Canteen.Location[0]) +
                                          userEmployee.Canteen.Location.Substring(1);
                    mealBoxes =  _mealBoxRepository.GetMealBoxesFromCanteen(userEmployee.CanteenId);
                }
                else if (TempData["Button"]!.Equals("Other"))
                {
                    if(userEmployee == null)
                    {
                        return RedirectToAction("Index");
                    }
                    mealBoxes = _mealBoxRepository.GetMealBoxesDifferentCanteen(userEmployee.CanteenId);
                }
                else
                {
                    mealBoxes = _mealBoxRepository.GetAllMealBoxesOrderedByPickUpTime();
                }
            }
            else
            {
                mealBoxes = _mealBoxRepository.GetAllMealBoxesOrderedByPickUpTime();
            }

            return View("IndexEmployees", mealBoxes);
        }

        if (User.IsInRole("Student") && ViewBag.CurrentCanteen == null)
        {
            var student = _studentRepository.GetStudent(_userManager.GetUserName(User.Clone()));
            if(student == null)
            {
                return RedirectToAction("Index");
            }
            var studentCanteen = _canteenRepository.GetStudentCanteen(student.StudyCity);
            return RedirectToAction("Filter", new { type = "None", canteen = studentCanteen?.Id.ToString() ?? "None" });
        }

        mealBoxes = _mealBoxRepository.GetMealBoxesNotReserved();

        return View(mealBoxes);
    }

    [Authorize(Roles = "Employee")]
    public IActionResult All()
    {
        TempData["Button"] = "All";
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Employee")]
    public IActionResult Same()
    {
        TempData["Button"] = "Same";
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Employee")]
    public IActionResult Other()
    {
        TempData["Button"] = "Other";
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Student")]
    public IActionResult Filter(string type, string canteen)
    {
        var student = _studentRepository.GetStudent(_userManager.GetUserName(User.Clone()));
        ViewBag.studentCity = student!.StudyCity;
        var canteens = _canteenRepository.GetCanteens();
        ViewBag.Canteens = canteens;
        ViewBag.CurrentType = type;
        ViewBag.CurrentCanteen = canteen.Equals("None")
            ? null!
            : _canteenRepository.GetCanteen(int.Parse(canteen))!;
        ICollection<MealBox?>? mealBoxes;
        if (type.Equals("None"))
            mealBoxes = canteen.Equals("None")
                ? _mealBoxRepository.GetMealBoxesNotReserved()
                : _mealBoxRepository.GetMealBoxesNotReservedByCanteenId(int.Parse(canteen));
        else
            mealBoxes = canteen.Equals("None")
                ? _mealBoxRepository.GetMealBoxesNotReservedByType(type)
                : _mealBoxRepository.GetMealBoxesNotReservedByTypeAndCanteenId(type, int.Parse(canteen));

        return View("Index", mealBoxes);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}