using System;
using System.Security.Cryptography;
using System.Text;

namespace ClockingSystemReminder.Helpers
{
    public class LocalEncrypter
    {
        byte[] encryptionKey;
        DataProtectionScope scope;

        public LocalEncrypter(string encryptionKey) : this(encryptionKey, DataProtectionScope.CurrentUser) { }
        public LocalEncrypter(string encryptionKey, DataProtectionScope scope) : this(Encoding.ASCII.GetBytes(encryptionKey), scope) { }
        public LocalEncrypter(byte[] encryptionKey, DataProtectionScope scope)
        {
            this.scope = scope;
            this.encryptionKey = encryptionKey;
        }

        public byte[] EncryptText(string text)
        {
            return EncryptText(text, Encoding.ASCII);
        }

        public byte[] EncryptText(string text, Encoding encoding)
        {
            var bytes = encoding.GetBytes(text);
            return Encrypt(bytes);
        }

        public byte[] Encrypt(byte[] data)
        {
            return ProtectedData.Protect(data, encryptionKey, scope);
        }

        public string DecryptText(byte[] encryptedData)
        {
            return DecryptText(encryptedData, Encoding.ASCII);
        }

        public string DecryptText(byte[] encryptedData, Encoding encoding)
        {
            var data = Decrypt(encryptedData);
            return encoding.GetString(data);
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            return ProtectedData.Unprotect(encryptedData, encryptionKey, scope);
        }
    }
}
