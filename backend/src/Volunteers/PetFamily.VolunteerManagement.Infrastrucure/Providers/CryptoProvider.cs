using System.Security.Cryptography;
using PetFamily.Core.Providers.Crypto;

namespace PetFamily.VolunteerManagement.Infrastrucure.Providers;

public class CryptoProvider
    : ICryptoProvider
{
    public byte[] Sha256(Stream stream)
        => SHA256.HashData(stream);
}