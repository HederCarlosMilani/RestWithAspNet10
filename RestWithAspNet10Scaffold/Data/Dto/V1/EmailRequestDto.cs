namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class EmailRequestDto
{
    public string to { get; set; } = string.Empty;
    public string subject { get; set; } = string.Empty;
    public string body { get; set; } = string.Empty;
}