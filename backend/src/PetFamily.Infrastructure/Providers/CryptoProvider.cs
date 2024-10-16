using System.Security.Cryptography;
using PetFamily.Application.Providers.Crypto;

namespace PetFamily.Infrastructure.Providers;

public class CryptoProvider
    : ICryptoProvider
{
    public byte[] Sha256(Stream stream)
        => SHA256.HashData(stream);
}