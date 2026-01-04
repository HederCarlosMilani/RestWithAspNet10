namespace RestWithAspNet10Scaffold.Data.Dto.V1;

public class TokenDto
{
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}