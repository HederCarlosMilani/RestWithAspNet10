using RestWithAspNet10Scaffold.Data.Dto.V1;

namespace RestWithAspNet10Scaffold.Services;

public interface IFileService
{
    byte[] GetFile(string fileName);
    Task<FileDetailDto> SaveFileToDisk(IFormFile file);
    Task<List<FileDetailDto>> SaveFilesToDisk(List<IFormFile> files);
}