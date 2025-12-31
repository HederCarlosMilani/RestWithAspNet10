using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services.Impl;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly string _basePath;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    private static readonly HashSet<string> _allowedExtensions = new()
    {
        ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt"
    };
    
    public FileService(ILogger<FileService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }
    
    public byte[] GetFile(string fileName)
    {
        _logger.LogInformation("Download File By Name {fileName}", fileName);
        
        var filePath = Path.Combine(_basePath, fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", fileName);
        
        return File.ReadAllBytes(filePath);
    }

    public async Task<FileDetailDto> SaveFileToDisk(IFormFile file)
    {
        _logger.LogInformation("SaveFileToDisk");
        
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            throw new ArgumentException("File type is not allowed");
        
        var documentName = Path.GetFileName(file.FileName);
        var destination = Path.Combine(_basePath, documentName);
        
        var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        
        var fileDetail = new FileDetailDto
        {
            FileName = documentName,
            FileType = extension,
            FileUrl = $"{baseUrl}/file/download/{documentName}"
        };
        
        using var stream = new FileStream(destination, FileMode.Create);
        
        await file.CopyToAsync(stream);

        return fileDetail;
    }

    public async Task<List<FileDetailDto>> SaveFilesToDisk(List<IFormFile> files)
    {
        _logger.LogInformation("Save Multiple Files To Disk");
        
        var results = new List<FileDetailDto>();
        
        foreach (var file in files)
        {
            var fileDetail = SaveFileToDisk(file).Result;
            if (!string.IsNullOrEmpty(fileDetail.FileName))
                results.Add(fileDetail);
        }
        
        return results;
    }
}