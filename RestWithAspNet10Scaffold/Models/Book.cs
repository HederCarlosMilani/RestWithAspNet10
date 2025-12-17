using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestWithAspNet10Scaffold.Models.Base;

namespace RestWithAspNet10Scaffold.Models;

[Table("Books")]
public class Book : BaseEntity
{
    [Required(ErrorMessage = "Title is required")]
    [Column("title", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Author is required")]
    [Column("author", TypeName = "varchar(MAX)")]
    [MaxLength]
    public string Author { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Column("price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Launch Date is required")]
    [Column("launch_date", TypeName = "datetime2(6)")]
    public DateTime LaunchDate { get; set; }
}