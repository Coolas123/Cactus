﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cactus.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Неверный формат почты")]
        [Display(Name = "Почта")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
