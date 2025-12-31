using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;
    
    [HttpPost("uploadFile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileDetailDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileUploadDto)
    {
        var fileDetail = await _fileService.SaveFileToDisk(fileUploadDto.File);
        return Ok(fileDetail);
    }
}