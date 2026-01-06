using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Data.Dto.V1;
using RestWithAspNet10Scaffold.Services;

namespace RestWithAspNet10Scaffold.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("Bearer")]
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

    [HttpPost("uploadMultipleFile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FileDetailDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFiles([FromForm] MultipleFilesUploadDto multipleFilesUploadDto)
    {
        var fileDetails = await _fileService.SaveFilesToDisk(multipleFilesUploadDto.Files);
        return Ok(fileDetails);
    }

    [HttpGet("download/{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/octet-stream")]
    public IActionResult DownloadFile([FromRoute] string fileName)
    {
        var fileBytes = _fileService.GetFile(fileName);
        var contentType = $"application/{Path.GetExtension(fileName).TrimStart('.')}";
        return File(fileBytes, contentType, fileName);
    }
}