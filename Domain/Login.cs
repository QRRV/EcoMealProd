﻿using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Login
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me")] public bool RememberMe { get; set; }
}