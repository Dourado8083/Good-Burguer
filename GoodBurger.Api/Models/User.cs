using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodBurger.Api.Models;

[Table("usuario")]
public class User
{
    [Key]
    public int Id { get; set; }
    public bool isAdmin { get; set; }
}