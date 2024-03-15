namespace MOHU.Integration.Contracts.Interface.Cache
{
    public interface ICacheKeyGeneratorService
    {
        string GenerateKey(string key, string language);
    }
}
