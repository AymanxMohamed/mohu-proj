using System.Security.Cryptography.X509Certificates;

namespace MOHU.Integration.WebApi.Common.Security.Certificates;

public static class CertificatesFactory
{
    public static X509Certificate2 GetByThumbprint(string certificateThumbprint)
    {
        if (string.IsNullOrWhiteSpace(certificateThumbprint))
        {
            throw new ArgumentException(
                "Certificate thumbprint must not be null or empty.",
                nameof(certificateThumbprint));
        }

        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);

        return certificateCollection.Count > 0 
            ? certificateCollection[0] 
            : throw new InvalidOperationException($"Certificate with thumbprint '{certificateThumbprint}' not found.");
    }
}