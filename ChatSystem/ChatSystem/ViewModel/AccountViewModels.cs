using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatSystem.ViewModel
{


    public class LoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل ضروریست")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن کلمه عبور ضروریست")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا بیاد داشته باش؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "وارد کردن ایمیل ضروریست")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن کلمه عبور ضروریست")]
        [StringLength(100, ErrorMessage = "تعداد حروف کلمه عبور شما باید در رنج [6-100] باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمات عبور وارد شده یکی نیستند")]
        public string ConfirmPassword { get; set; }
    }


}
