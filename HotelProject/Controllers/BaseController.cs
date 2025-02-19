using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    private readonly RoomService _roomService;

    public BaseController(RoomService roomService)
    {
        _roomService = roomService;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var result2 = _roomService.GetRoomCategories();

        if (result2 != null)
        {
            ViewBag.Categories = result2;
        }
    }
}
