using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamplePast1.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Yêu cầu nhập tên máy chủ")]
        [MaxLength(20,ErrorMessage ="Tên máy chủ không vượt quá 20 ký tự")]
        public string ServerName { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập User Id")]
        [MaxLength(10, ErrorMessage = "Tên không vượt quá 10 ký tự")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [MaxLength(10, ErrorMessage = "Mật khẩu không quá 10 ký tự")]
        public string Password { get; set; }
    }
}