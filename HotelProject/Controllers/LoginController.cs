﻿using BusinessLayer.Services;
using DataAccessLayer;
using HotelProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly LoginService _loginService;
    private readonly SignInManager<ApplicationUser> _signInManager;


    public LoginController(LoginService loginService, SignInManager<ApplicationUser> signInManager)
    {
        _loginService = loginService;
        _signInManager = signInManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _loginService.Login(model);

        if (result == "Giriş başarılı!")
        {
            return Json(new { success = true,message=result, redirectUrl = Url.Action("RoomSet", "Admin") });
        }

        return Json(new { success = false, message = result });
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {  
        var result = await _loginService.Register(model);
        if (result == "Kayıt başarılı!")
        {
            return Json(new { success = true, message = result, redirectUrl = Url.Action("Index", "Login") });
        }

        return Json(new { success = false, message = result });
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
    } 
   


}
