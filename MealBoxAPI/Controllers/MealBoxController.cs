using System.Text.Json;
using System.Text.Json.Serialization;
using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MealBoxAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MealBoxController : ControllerBase
{
    private readonly ILogger<MealBoxController> _logger;
    private readonly IMealBoxRepository _mealBoxRepository;

    public MealBoxController(ILogger<MealBoxController> logger, IMealBoxRepository mealBoxRepository)
    {
        _logger = logger;
        _mealBoxRepository = mealBoxRepository;
    }

    [HttpGet(Name = "GetReservations")]
    public string Get()
    {
        var mealBoxes = _mealBoxRepository.GetMealBoxesNoNest();

        return JsonConvert.SerializeObject(new { message = mealBoxes, code = 200 });
    }

    [HttpPut(Name = "GetReservations")]
    public StatusCodeResult Put()
    {
        return StatusCode(501);
    }

    [HttpDelete(Name = "GetReservations")]
    public StatusCodeResult Delete()
    {
        return StatusCode(501);
    }

    [HttpPost(Name = "GetReservations")]
    public string Post(int mealBoxId, int studentId)
    {
        var mealBox = _mealBoxRepository.GetMealBox(mealBoxId);
        string response;
        if (mealBox == null)
        {
            response = JsonConvert.SerializeObject(new { message = "mealbox doesn't exist!", code = 409 });
        }
        else
        {
            if (mealBox.ReservedBy != null)
            {
                response = JsonConvert.SerializeObject(new { message = "mealbox is already reserved!", code = 404 });
            }
            else
            {
                bool success = _mealBoxRepository.ReserveMealBox(mealBox, studentId);
                if (!success)
                {
                    response = JsonConvert.SerializeObject(new { message = "reservation failed!", code = 500 });
                }
                else
                {
                    response = JsonConvert.SerializeObject(new { message = "mealbox has been reserved!", code = 200 });
                }
            }
        }


        return response;
    }
}