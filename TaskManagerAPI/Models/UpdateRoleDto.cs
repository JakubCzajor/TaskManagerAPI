using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models;

public class UpdateRoleDto
{
    [Required]
    public int RoleId { get; set; }
}