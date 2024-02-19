namespace MOHU.Integration.Contracts.Interface.Cache
{
    public interface ICacheKeyGeneratorService
    {
        Task<string> GenerateKey(string key, string language);
    }
}
