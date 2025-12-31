using System.ComponentModel.DataAnnotations;

namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class FileUploadDto
{
    [Required]
    public IFormFile File { get; set; }
}