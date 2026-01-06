using System.ComponentModel.DataAnnotations;

namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class MultipleFilesUploadDto
{
    [Required]
    public List<IFormFile> Files { get; set; }
}