﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models;

public class UpdatePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}