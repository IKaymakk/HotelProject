using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class BaseController(RoomService roomService, HomeService homeService) : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var result2 = roomService.GetRoomCategories();
        var result3 = homeService.GetContactInformations();
        ViewBag.ContactInfo = result3;
        ViewBag.Categories = result2;
    }
}
