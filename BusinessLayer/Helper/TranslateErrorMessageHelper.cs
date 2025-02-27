using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helper
{
    public class TranslateErrorMessageHelper
    {
        public string TranslateErrorMessage(string message)
        {
            switch (message)
            {
                case "Password requires at least 1 digit.":
                    return "Şifre en az 1 rakam içermelidir.";
                case "Passwords must have at least one lowercase ('a'-'z').":
                    return "Şifre en az 1 küçük harf içermelidir.";
                case "Passwords must have at least one uppercase ('A'-'Z').":
                    return "Şifre en az 1 büyük harf içermelidir.";
                case "Passwords must have at least one non alphanumeric character.":
                    return "Şifre en az 1 özel karakter içermelidir.";
                case "Passwords must be at least 8 characters.":
                    return "Şifre en az 8 karakter olmalıdır.";
                case "Email is already taken":
                    return "Bu email adresi zaten alınmış.";
                case "User name is already taken":
                    return "Bu kullanıcı adı zaten alınmış.";
                case "Passwords must use at least 2 different characters.":
                    return "Şifre en az iki farklı karakter içermeli."; 
                case "Passwords must have at least one digit ('0'-'9').":
                    return "Şifreler en az bir rakamdan ('0'-'9') oluşmalıdır";
                default:
                    return message;
            }
        }
    }
}
