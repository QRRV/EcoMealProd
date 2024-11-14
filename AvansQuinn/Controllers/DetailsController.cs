using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealBoxApp.Controllers;

public class DetailsController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public DetailsController(IMealBoxRepository mealBoxRepository, UserManager<IdentityUser> userManager,
        IStudentRepository studentRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _userManager = userManager;
        _studentRepository = studentRepository;
    }

    [Authorize(Roles = "Student")]
    public IActionResult Details(int id)
    {
        var student = _studentRepository.GetStudent(_userManager.GetUserName(User.Clone()));
        var mealBox = _mealBoxRepository.GetMealBox(id);
        var studentMealBoxes = _mealBoxRepository.GetMealBox(_userManager.GetUserName(User.Clone()));
        var hasReservation = false;
        foreach (var studentMealBox in studentMealBoxes)
            if (mealBox != null &&
                studentMealBox != null &&
                studentMealBox.PickUpTimeMin.Year == mealBox.PickUpTimeMin.Year &&
                studentMealBox.PickUpTimeMin.Month == mealBox.PickUpTimeMin.Month && studentMealBox.PickUpTimeMin.Day ==
                mealBox.PickUpTimeMin.Day)
                hasReservation = true;

        ViewBag.hasReservation = hasReservation;
        ViewBag.Adult = mealBox != null && student != null && student.BirthDate.AddYears(18) <= mealBox.PickUpTimeMin;
        if (mealBox?.ReservedBy != null)
            ViewBag.ReservedBy = mealBox.StudentId == student?.Id ? "This" : "Other";
        else
            ViewBag.ReservedBy = "None";

        return View(mealBox);
    }

    [Authorize(Roles = "Employee")]
    public IActionResult DetailsEmployee(int id)
    {
        var mealBox = _mealBoxRepository.GetMealBox(id);
        return View(mealBox);
    }

    [Authorize(Roles = "Student")]
    public IActionResult Reserve(int id)
    {
        var mealBox = _mealBoxRepository.GetMealBox(id);
        var student = _studentRepository.GetStudent(_userManager.GetUserName(User.Clone()));
        if (mealBox == null || student == null) return RedirectToAction("Details", new { id });
        var success = _mealBoxRepository.ReserveMealBox(mealBox, student);
         if (!success)
        {
            TempData["msg"] =
                "<script> window.onload = function () {alert('Deze mealbox is al gereserveerd!');};</script>";
            return RedirectToAction("Index", "Home");
        }


        return RedirectToAction("Details", new { id });
    }

    [Authorize(Roles = "Student")]
    public IActionResult Cancel(int id)
    {
        var mealBox = _mealBoxRepository.GetMealBox(id);
        var student = _studentRepository.GetStudent(_userManager.GetUserName(User.Clone()));
        if (mealBox?.ReservedBy == null || student == null) return RedirectToAction("Details", new { id });
        if (mealBox.ReservedBy.Id != student.Id) return RedirectToAction("Details", new { id });
        mealBox.StudentId = null;
        mealBox.ReservedBy = null;
        _mealBoxRepository.UpdateMealBox(mealBox);

        return RedirectToAction("Details", new { id });
    }
}