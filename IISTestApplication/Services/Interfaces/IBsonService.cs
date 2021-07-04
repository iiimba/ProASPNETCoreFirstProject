namespace IISTestApplication.Services.Interfaces
{
    public interface IBsonService
    {
        string ToBson<T>(T value);

        T FromBson<T>(string base64data);
    }
}
