using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestWithAspNet10Scaffold.Models.Base;

namespace RestWithAspNet10Scaffold.Models;

[Table("person")]
public class Person : BaseEntity
{
    [Required(ErrorMessage = "First Name is required")]
    [Column("first_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last Name is required")]
    [Column("last_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Address is required")]
    [Column("address", TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Gender is required")]
    [Column("gender", TypeName = "varchar(6)")]
    [MaxLength(6)]
    public string Gender { get; set; }

    [Column("enabled")]
    public bool Enabled { get; set; }
}