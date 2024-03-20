using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.Accounts;

public class UpdateRoleDto
{
    [Required]
    public int RoleId { get; set; }
}