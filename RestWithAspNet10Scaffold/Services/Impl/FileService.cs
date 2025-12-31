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
        throw new NotImplementedException();
    }

    public Task<FileDetailDto> SaveFileToDisk(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<List<FileDetailDto>> SaveFilesToDisk(List<IFormFile> files)
    {
        throw new NotImplementedException();
    }
}