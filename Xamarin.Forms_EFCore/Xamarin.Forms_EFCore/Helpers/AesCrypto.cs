using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class AesCrypto
    {
        AesCryptoServiceProvider crypt_provider;

        public AesCrypto() {

            crypt_provider = new AesCryptoServiceProvider();

            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.GenerateIV();
            crypt_provider.GenerateKey();
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;


        }


        public void CreateKeys() {

            crypt_provider = new AesCryptoServiceProvider();

            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.GenerateIV();
            crypt_provider.GenerateKey();
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;

            string key = Convert.ToBase64String(crypt_provider.Key);
            string IV = Convert.ToBase64String(crypt_provider.IV);

        }

        public string Encrypt(string clear_text, string key, string IV)
        {

            byte[] plaintextbytes = Convert.FromBase64String(clear_text);
            crypt_provider = new AesCryptoServiceProvider();
            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.Key  = Convert.FromBase64String(key);
            crypt_provider.IV = Convert.FromBase64String(IV);
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = crypt_provider.CreateEncryptor(crypt_provider.Key, crypt_provider.IV);

            byte[] encrypted_bytes = transform.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
            transform.Dispose();
            string str = Convert.ToBase64String(encrypted_bytes);
            return str;

            
        }

        public string Decrypt(string cipher_text, string key, string IV)
        {

            byte[] enc_bytes = Convert.FromBase64String(cipher_text);
            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.Key = Convert.FromBase64String(key);
            crypt_provider.IV = Convert.FromBase64String(IV);
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = crypt_provider.CreateDecryptor(crypt_provider.Key, crypt_provider.IV);
            byte[] descrypted_bytes = transform.TransformFinalBlock(enc_bytes, 0, enc_bytes.Length);
            transform.Dispose();    

            string str = Convert.ToBase64String(descrypted_bytes);

            return str;


        }

        public byte[] GetKey()
        {

            return crypt_provider.Key;

        }

        public byte[] GetIV()
        {

            return crypt_provider.IV;

        }


    }
}
