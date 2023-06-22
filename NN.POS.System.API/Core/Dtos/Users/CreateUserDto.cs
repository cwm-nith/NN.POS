﻿using System.ComponentModel.DataAnnotations;

namespace NN.POS.System.API.Core.Dtos.Users;

public class CreateUserDto : BaseDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
}