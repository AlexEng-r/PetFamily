namespace PetFamily.Application.Providers.Crypto;

public interface ICryptoProvider
{
    byte[] Sha256(Stream stream);
}