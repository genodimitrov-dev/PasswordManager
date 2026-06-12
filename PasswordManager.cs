using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System;


public class PasswordManager
{
    private static List<PasswordEntry> NewEntry = new List<PasswordEntry>();
    private const string SecretKey = "mySecretKey123456789012345678901";
    public static string EncryptPassword(string password)
    {
        using(Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(SecretKey);
            aes.GenerateIV();

            ICryptoTransform newEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memstr_encrypt = new MemoryStream())
            {
                memstr_encrypt.Write(aes.IV, 0, aes.IV.Length);
                using (CryptoStream crptostrEncrypt = new CryptoStream(memstr_encrypt, newEncryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(crptostrEncrypt))
                    {
                        swEncrypt.Write(password);
                    }
                }
                return Convert.ToBase64String(memstr_encrypt.ToArray());
            }
        }
    }
    public static void AddPassword()
    {
        Console.Write("Enter website: ");
        string website = Console.ReadLine();
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        string encryptedPassword = EncryptPassword(password);
        NewEntry.Add(new PasswordEntry(website, username, encryptedPassword));
        Console.WriteLine("Password added successfully!");
        SaveToFile();
    }
    public static string DecryptPassword(string encryptedPassword)
    {
        using(Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(SecretKey);
            byte[] allBytes = Convert.FromBase64String(encryptedPassword);
            byte[] iv = new byte[aes.BlockSize / 8];
            Array.Copy(allBytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            ICryptoTransform newDecryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using(MemoryStream memoryStream_decrypt = new MemoryStream(allBytes))
            {
                memoryStream_decrypt.Seek(iv.Length, SeekOrigin.Begin);
                using (CryptoStream cryptostrDecrypt = new CryptoStream(memoryStream_decrypt, newDecryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptostrDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
    public static void ShowPasswords()
    {
        if(NewEntry.Count == 0)
        {
            Console.WriteLine("No information stored.");
            return;
        }
        foreach (PasswordEntry entry in NewEntry)
        {
            string decryptedPassword = DecryptPassword(entry.Password);
            Console.WriteLine($"Website: {entry.Website}, Username: {entry.Username}, Password: {decryptedPassword}");
        }
    }
    public static void SaveToFile()
    {
        using(StreamWriter swNew = new StreamWriter("passwords.txt"))
        {
            foreach(PasswordEntry entry in NewEntry)
            {
                swNew.WriteLine($"{entry.Website} | {entry.Username} |  {entry.Password}");
            }
        }
    }
    public static void LoadFromFile()
    {
        if(File.Exists("passwords.txt"))
        {
            NewEntry.Clear();
            using(StreamReader srNew = new StreamReader("passwords.txt"))
            {
                string line;
                while((line = srNew.ReadLine())!=null)
                {
                    string[] part = line.Split('|');
                    if(part.Length == 3)
                    {
                        string website = part[0].Trim();
                        string username = part[1].Trim();
                        string encryptedPassword = part[2].Trim();
                        NewEntry.Add(new PasswordEntry(website, username, encryptedPassword));
                    }
                }
            }
        }
    }
    public static void EditPassword_Username()
    {
        Console.Write("Enter website to edit your info for the website: ");
        string website = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(website))
        {
            Console.Write("Please enter a valid website: ");
            website = Console.ReadLine();
        }
        foreach(PasswordEntry entry in NewEntry)
        {
            if(entry.Website.Equals(website, StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine();
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();
                entry.Username = newUsername;
                entry.Password = EncryptPassword(newPassword);
                SaveToFile();
                Console.WriteLine("Information updated successfully!");
                return;
            }
        }
        Console.WriteLine("Website not found.");
    }
    public static void DeleteEntry()
    {
        Console.Write("Enter a website you want to delete your information for: ");
        string website = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(website))
        {
            Console.Write("Please enter a valid website: ");
            website = Console.ReadLine();
        }
        for(int i = 0; i<NewEntry.Count;i++)
        {
            if(NewEntry[i].Website.Equals(website, StringComparison.OrdinalIgnoreCase))
            {
                NewEntry.RemoveAt(i);
                SaveToFile();
                Console.WriteLine("Information deleted successfully!");
                return;
            }
        }
        Console.WriteLine("Website not found.");
    }
    public static void SearchByWebsite()
    {
        Console.Write("Enter a website to search for: ");
        string website = Console.ReadLine();
        while(string.IsNullOrWhiteSpace(website))
        {
            Console.Write("Please enter a valid website: ");
            website = Console.ReadLine();
        }
        foreach(PasswordEntry entry in NewEntry)
        {
            if(entry.Website.Equals(website, StringComparison.OrdinalIgnoreCase))
            {
                string decryptedPassword = DecryptPassword(entry.Password);
                Console.WriteLine($"Website: {entry.Website}, Username: {entry.Username}, Password: {decryptedPassword}");
                return;
            }
        }
        Console.WriteLine("Website not found.");
    }
    public static void ShowMenu()
    {
        Console.WriteLine("1. Add password");
        Console.WriteLine("2. Show passwords");
        Console.WriteLine("3. Edit password/username");
        Console.WriteLine("4. Delete password");
        Console.WriteLine("5. Search by website");
        Console.WriteLine("6. Exit");
    }
}