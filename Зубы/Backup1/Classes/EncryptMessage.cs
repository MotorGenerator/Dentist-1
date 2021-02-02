using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace EncryptMessage
{
    public class EncryptMessage
    {
        /// <summary>
        /// Зашифровывание сообщения код Microsoft.
        /// </summary>
        /// <param name="message">Текстовое сообщение которое нужно зашифровать</param>
        /// <param name="key">ключ</param>
        /// <param name="IV">вектор IV</param>
        /// <returns></returns>
        public static byte[] Encrypt(string message, byte[] key, byte[] IV)
        {
            // Массив для хранения зашифрованных данных.
            byte[] encrypted;

            // Создадим класс реализации алгоритма шифрования.
            using (RijndaelManaged myAlg = new RijndaelManaged())
            {
                // Передадим в него ключ и вектор IV.
                myAlg.Key = key;
                myAlg.IV = IV;

                // Создадим класс шифрования.
                ICryptoTransform encryptor = myAlg.CreateEncryptor(myAlg.Key, myAlg.IV);

                // Создадим поток который будем использовать для шифрования.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // Запишем текстовое сообщение в поток.
                        using (StreamWriter sWrit = new StreamWriter(cStream))
                        {
                            sWrit.Write(message);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        /// <summary>
        /// Расшифровывет сообщение.
        /// </summary>
        /// <param name="cipherText">зашифрованные данные</param>
        /// <param name="Key">Ключ</param>
        /// <param name="IV">Вектор IV</param>
        /// <returns></returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] IV)
        {
            // Переменная для хранения расшифрованного сообщения.
            string plaintext = null;

            try
            {
                using (RijndaelManaged myAlg = new RijndaelManaged())
                {
                    // Передадим в него ключ и вектор IV.
                    myAlg.Key = key;
                    myAlg.IV = IV;

                    // Создадим класс расшифроввывания.
                    ICryptoTransform decryptor = myAlg.CreateDecryptor(myAlg.Key, myAlg.IV);

                    // Создадим поток для расшивровывания.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                plaintext = "Не верный логин или пароль";
            }
            return plaintext;
        }

        /// <summary>
        /// Генирирует ключ.
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static byte[] GetKey(string strKey)
        {

            byte[] dataKey;

            // Создадим экземпляр реализации хэш алгоритма MD5 которых 
            using (MD5 md5Hash = MD5.Create())
            {
                //Преобразует входную строку в массив байтов и вычисляет хэш.
                dataKey = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(strKey));
            }


            return dataKey;
        }

    }
}
