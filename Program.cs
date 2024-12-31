using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Alegeți o opțiune:");
        Console.WriteLine("1. Calculați hash pentru un șir de caractere");
        Console.WriteLine("2. Calcularea hash-ului pentru o matrice de octeți");
        Console.WriteLine("3. Calcularea hash-ului pentru un fișier");
        Console.Write("\nAlegerea dvs: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Introduceți șirul: ");
                string input = Console.ReadLine();
                string stringHash = await CalcHash(Encoding.UTF8.GetBytes(input));
                Console.WriteLine($"Hash: {stringHash}");
                break;

            case "2":
                Console.Write("Introduceți matricea de octeți (separați prin virgulă): ");
                string byteArrayInput = Console.ReadLine();
                byte[] byteArray = Array.ConvertAll(byteArrayInput.Split(','), byte.Parse);
                string byteArrayHash = await CalcHash(byteArray);
                Console.WriteLine($"Hash: {byteArrayHash}");
                break;

            case "3":
                Console.Write("Introduceți path-ul fișierului: ");
                string filePath = @"" + Console.ReadLine();
                if (File.Exists(filePath))
                {
                    byte[] fileContent = await File.ReadAllBytesAsync(filePath);
                    string fileHash = await CalcHash(fileContent);
                    Console.WriteLine($"Hash: {fileHash}");
                }
                else
                {
                    Console.WriteLine("Fișierul nu a fost găsit.");
                }
                break;

            default:
                Console.WriteLine("Alegere invalidă.");
                break;
        }
    }

    private static async Task<string> CalcHash(byte[] data)
    {
        return await Task.Run(() =>
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(data);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        });
    }
}
