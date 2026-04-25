using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodBurger.Api.Models;

[Table("pedidos")]
public class Order
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? SanduicheNome { get; set; }

    public bool TemBatata { get; set; }

    public bool TemRefrigerante { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Desconto { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalFinal { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;
}