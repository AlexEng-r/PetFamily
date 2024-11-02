namespace PetFamily.Core.Providers.Crypto;

public interface ICryptoProvider
{
    byte[] Sha256(Stream stream);
}