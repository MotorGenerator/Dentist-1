using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Стамотология.Classes
{
    /// <summary>
    /// Класс шимфрования id районоа.
    /// </summary>
    public static class Crypto
    {

                //метод шифрования строки
        public static string Shifrovka(string ishText)
        {
            if (string.IsNullOrEmpty(ishText))
                return "";
            string pass = "Мой адрес Советский Союз";
            string sol = "doberman";
            string cryptographicAlgorithm = "SHA1";
               int passIter = 2;
            string initVec = "a8doSuDitOz1hZe#";
               int keySize = 256;
 
            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);
 
            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;
 
            byte[] cipherTextBytes = null;
 
            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }
 
            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

                public static string DeShifrovka(string ciphText)
        {
            if (string.IsNullOrEmpty(ciphText))
                return "";

            string pass = "Мой адрес Советский Союз";
               string sol = "doberman";
               string cryptographicAlgorithm = "SHA1";
               int passIter = 2;
               string initVec = "a8doSuDitOz1hZe#";
               int keySize = 256;
 
            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] cipherTextBytes = Convert.FromBase64String(ciphText);
 
            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
 
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;
 
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;
 
            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
            {
                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }
 
            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }


        /// <summary>
        /// Зашифровывание района.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypting(string id)
        {
             //// Получим ключ.
             //byte[] dataKey = EncryptMessage.EncryptMessage.GetKey("СССР 12.04.1961");//DateTime.Today.ToShortDateString()  //"12.04.1961"

             //// Создаем объект алгоритма симметричного ключа.
             //SymmetricAlgorithm myAlg = new RijndaelManaged();

             //// Запишем ключ.
             //myAlg.Key = dataKey;
             //myAlg.IV = dataKey;

             //// Получим зашифрованный массив с данными.
             //byte[] byteArrayEncrypt = EncryptMessage.EncryptMessage.Encrypt(data, myAlg.Key, myAlg.IV);

             //           //===============================

             //// Преобразуем массив битов в строку.
             //StringBuilder builder = new StringBuilder();

             //for (int i = 0; i < byteArrayEncrypt.Length; i++)
             //{
             //    builder.Append(byteArrayEncrypt[i].ToString("x2"));
             //}

             //// Присвоим переменной зашифрованные данные.
             //return builder.ToString().Trim();

            // Ключ.
            string password = "СССР 12.04.1961";

            // Получим из строки набор байтов которые будем шифровать.
            byte[] id_byte = Encoding.UTF32.GetBytes(id);

            // Алгоритм.
            SymmetricAlgorithm sa_in = Rijndael.Create();

            // Объект для преобразования данных.
            ICryptoTransform ct_in = sa_in.CreateEncryptor(new PasswordDeriveBytes(password, null).GetBytes(16), new byte[16]);

            // Поток.
            MemoryStream ms_in = new MemoryStream();

            // Шифровальщик потока.
            CryptoStream cs_in = new CryptoStream(ms_in, ct_in, CryptoStreamMode.Write);

            // Записываем шифрованные данные в поток.
            cs_in.Write(id_byte, 0, id_byte.Length);
            cs_in.FlushFinalBlock();

            // Выводим зашифрованную строку.
            return Convert.ToBase64String(ms_in.ToArray());

        }

        /// <summary>
        /// Расшифровывем строку.
        /// </summary>
        /// <param name="crypto_str"></param>
        /// <returns></returns>
        public static string DeCrypto(string crypto_str, out bool  flagError)
        {
            string rezult = String.Empty;

            //try
            //{
                // Получим массив байтов.
                byte[] cryp_data = Convert.FromBase64String(crypto_str);

                // Ключ.
                string password = "СССР 12.04.1961";

                // Алгоритм.
                SymmetricAlgorithm sa_out = Rijndael.Create();

                // Объект для преобразования данных.
                ICryptoTransform ct_out = sa_out.CreateDecryptor(new PasswordDeriveBytes(password, null).GetBytes(16), new byte[16]);

                // Поток.
                MemoryStream ms_out = new MemoryStream(cryp_data);

                // Расшифровываем поток.
                CryptoStream cs_out = new CryptoStream(ms_out, ct_out, CryptoStreamMode.Read);

                // Создаем строку.
                StreamReader sr_out = new StreamReader(cs_out);

                flagError = false;

                rezult = sr_out.ReadToEnd();

                // Возвращаем расшифрованную строку.
                return rezult;
            //}
            //catch
            //{
            //    flagError = true;

            //}

            return rezult; 

            //// Получим ключ.
            //byte[] dataKey = EncryptMessage.EncryptMessage.GetKey("СССР 12.04.1961");//DateTime.Today.ToShortDateString()  //"12.04.1961"

            //// Создаем объект алгоритма симметричного ключа.
            ////SymmetricAlgorithm myAlgA = new RijndaelManaged();

            ////// Запишем ключ.
            ////myAlgA.Key = dataKey;
            ////myAlgA.IV = dataKey;

            //string plaintext = string.Empty;

            //using (RijndaelManaged myAlg = new RijndaelManaged())
            //{
            //    // Передадим в него ключ и вектор IV.
            //    myAlg.Key = dataKey; // key;
            //    myAlg.IV = dataKey; // IV;

            //    // Создадим класс расшифроввывания.
            //    ICryptoTransform decryptor = myAlg.CreateDecryptor(myAlg.Key, myAlg.IV);

            //    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);

            //    // Создадим поток для расшивровывания.
            //    using (MemoryStream msDecrypt = new MemoryStream(byteArray))
            //    {
            //        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            //        {
            //            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            //            {

            //                // Read the decrypted bytes from the decrypting stream
            //                // and place them in a string.
            //                plaintext = srDecrypt.ReadToEnd();
            //            }
            //        }
            //    }
            //}

            //return plaintext;
        }
	

    }
}
