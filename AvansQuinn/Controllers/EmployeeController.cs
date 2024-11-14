using System.Globalization;
using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealBoxApp.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMealboxProductRepository _mealboxProductRepository;
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<IdentityUser> _userManager;


    public EmployeeController(IMealBoxRepository mealBoxRepository, UserManager<IdentityUser> userManager,
        IEmployeeRepository employeeRepository, IProductRepository productRepository,
        IMealboxProductRepository mealboxProductRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _userManager = userManager;
        _employeeRepository = employeeRepository;
        _productRepository = productRepository;
        _mealboxProductRepository = mealboxProductRepository;
    }

    // GET
    [Authorize(Roles = "Employee")]
    public IActionResult Manage()
    {
        var userEmployee = _employeeRepository.GetEmployee(_userManager.GetUserName(User.Clone()));
        TempData["Canteen"] = userEmployee!.Canteen.City + " " + char.ToUpper(userEmployee.Canteen.Location[0]) +
                              userEmployee.Canteen.Location.Substring(1);
        var mealBoxes = _mealBoxRepository.GetMealBoxesFromCanteen(userEmployee.CanteenId);
        return View(mealBoxes);
    }


    [Authorize(Roles = "Employee")]
    public IActionResult Add()
    {
        var products = _productRepository.GetProducts();
        return View(products);
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public IActionResult Add(string name, string pickUpTimeMin, string pickUpTimeMax, string price, string type, int[] products)
    {
        var userEmployee = _employeeRepository.GetEmployee(_userManager.GetUserName(User.Clone()));
        if (userEmployee == null)
        {
            // Handle error: employee not found
            return RedirectToAction("Manage");
        }

        var mealBox = new MealBox
        {
            Name = name,
            City = userEmployee.Canteen.City,
            CanteenId = userEmployee.CanteenId,
            PickUpTimeMin = DateTime.Parse(pickUpTimeMin),
            PickUpTimeMax = DateTime.Parse(pickUpTimeMax),
            Price = Convert.ToDecimal(price, new CultureInfo("en-US")),
            Type = type
            // Note: Adult flag and MealboxProducts will be set within the service
        };

        _mealBoxRepository.CreateMealBox(mealBox);
        foreach (var product in products)
        {
            _mealboxProductRepository.CreateMealBox_Product(new Mealbox_Product
            {
                MealBoxId = mealBox.Id, ProductId = product
            });
        }

        return RedirectToAction("Manage");
    }


    [HttpPost]
    [Authorize(Roles = "Employee")]
    public IActionResult Edit(string name, string pickUpTimeMin, string pickUpTimeMax, string price, string type,
        int[] products, int id)
    {
        var mealBox = _mealBoxRepository.GetMealBox(id)!;
        var userEmployee = _employeeRepository.GetEmployee(_userManager.GetUserName(User.Clone()));
        var adult = false;
        Console.WriteLine("EYEYEYEYYEYEYEYEYEY" + products[0]);
        foreach (var product in products)
        {
            var p = _productRepository.GetProduct(product);
            if (p != null && p.ContainsAlcohol) adult = true;
        }

        mealBox.Name = name;
        mealBox.City = userEmployee!.Canteen.City;
        mealBox.PickUpTimeMin = DateTime.Parse(pickUpTimeMin);
        mealBox.PickUpTimeMax = DateTime.Parse(pickUpTimeMax);
        mealBox.Adult = adult;
        mealBox.Price = Convert.ToDecimal(price, new CultureInfo("en-US"));
        mealBox.Type = type;
        mealBox.MealboxProducts = new List<Mealbox_Product>();
        foreach (var p in products)
            mealBox.MealboxProducts.Add(new Mealbox_Product
            {
                ProductId = p, MealBoxId = id
            });
        _mealBoxRepository.UpdateMealBox(mealBox);

        return RedirectToAction("Manage");
    }


    [Authorize(Roles = "Employee")]
    public IActionResult Edit(int id)
    {
        ViewBag.Products = _productRepository.GetProducts();
        var mealBox = _mealBoxRepository.GetMealBox(id);
        if (mealBox?.ReservedBy != null) return RedirectToAction("Manage");
        return View(mealBox);
    }

    [Authorize(Roles = "Employee")]
    public IActionResult Delete(int mealboxId)
    {
        var mealBox = _mealBoxRepository.GetMealBox(mealboxId);
        var userEmployee = _employeeRepository.GetEmployee(_userManager.GetUserName(User.Clone()));
        if (mealBox?.ReservedBy != null) return RedirectToAction("Manage");
        if (mealBox != null && userEmployee != null && userEmployee.CanteenId == mealBox.CanteenId &&
            mealBox.ReservedBy == null) _mealBoxRepository.DeleteMealBox(mealBox);

        return RedirectToAction("Manage");
    }
}