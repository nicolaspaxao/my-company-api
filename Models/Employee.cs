using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models; 

[Table("tb_employee")]
public class Employee : IValidatableObject{

    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres.")]
    public string fullName { get; set; }
    public string email { get; set; }
    public string photoUrl { get; set; }
    public string document { get; set; }
    public DateTime birthDay { get; set; }
    public string role { get; set; }
    public string gender { get; set; }  
    public DateTime createdAt { get; set; }
    public DateTime? bloquedAt { get; set; }

    public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext ) {
        if (fullName.IsNullOrEmpty()) {
            yield return new ValidationResult("O nome é requerido" , new [] {
                nameof(this.fullName)
            });
        }
    }
}
