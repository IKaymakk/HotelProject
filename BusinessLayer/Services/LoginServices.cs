using BusinessLayer.Helper;
using DataAccessLayer;
using HotelProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.Services
{
    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        TranslateErrorMessageHelper errorMessageHelper = new TranslateErrorMessageHelper();
        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> Login(LoginViewModel model)
        {
            if (model == null)
                return "Geçersiz giriş.";

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return "Bu email adresi ile bir kullanıcı bulunamadı.";

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
                return "Şifre yanlış!";

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return "Giriş başarılı!";
            }
            else if (result.IsLockedOut)
            {
                return "Hesabınız çok fazla hatalı giriş yapıldığı için kilitlendi. Lütfen daha sonra tekrar deneyin.";
            }
            else if (result.IsNotAllowed)
            {
                return "Giriş yapabilmeniz için hesabınız onaylanmalıdır.";
            }
            else if (result.RequiresTwoFactor)
            {
                return "İki adımlı doğrulama gereklidir.";
            }

            return "Bilinmeyen bir hata oluştu. Lütfen tekrar deneyin.";
        }


        public async Task<string> Register(RegisterViewModel model)
        {
            if (model == null)
                return "Geçersiz giriş.";

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return "Kayıt başarılı!";
            }

            var errorMessages = result.Errors
             .Select(e => errorMessageHelper.TranslateErrorMessage(e.Description))
             .SelectMany(desc => desc.Split('.').Where(s => !string.IsNullOrWhiteSpace(s))) 
             .ToList();

            return string.Join("\n", errorMessages);
        }

        public async Task<string> ChangePassword(PasswordChangeModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return "Kullanıcı bulunamadı.";
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return "Şifre başarıyla değiştirildi.";
            }
            else
            {

                var errorMessages = result.Errors
                 .Select(e => errorMessageHelper.TranslateErrorMessage(e.Description))
                 .SelectMany(desc => desc.Split('.').Where(s => !string.IsNullOrWhiteSpace(s)))
                 .ToList();
                return "<div style='text-align:center'>" +
                       "<h6 style='text-align:left;'>Şifre değiştirilirken bir hata oluştu:</h6>" +  // Başlık sola yaslanır
                       "<div>" +
                       "<ul>" +
                       string.Join("", errorMessages.Select(msg => $"<li style='float:left'>{msg}</li>")) +
                       "</ul>" +
                       "</div></div>";
            }
        }


    }
}
