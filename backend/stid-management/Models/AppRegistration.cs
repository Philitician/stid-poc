using System.ComponentModel.DataAnnotations;

namespace stid_management.Models;

public class AppRegistration
{
    [Key]
    public string ClientId { get; set; }
}