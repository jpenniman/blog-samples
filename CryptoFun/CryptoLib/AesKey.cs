using System;
using System.Linq;
using System.Security.Cryptography;

namespace CryptoLib
{
    public struct AesKey
    {
        public byte[] Value { get; private set; }

        public static implicit operator AesKey(byte[] bytes) => Create(bytes);
        public static implicit operator byte[](AesKey key) => key.Value;

        public static AesKey Create(byte[] bytes)
        {
            return new AesKey { Value = bytes };
        }
    }
}
