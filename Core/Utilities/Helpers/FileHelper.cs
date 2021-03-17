using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        /* Guid Hakkında:
         * Guid.NewGuid().ToString(): 00000000-0000-0000-0000-000000000000
         * Guid.NewGuid().ToString("N"): 00000000000000000000000000000000
         * - (tire)'ları kaldırır
         * ToString("D"), ToString("B"), ToString("P"), ToString("X") gibi farklı formatlarda kullanımları vardır.
         * ToString("D") == ToString()
         * ("B"):  { } içine yazar, 
         * ("P"):  ( ) içine yazar,
         * ("X") ise {0x00000000, 0x0000, 0x0000 {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}} gibi bi sonuç döndürür.
         */

        static string directory = Directory.GetCurrentDirectory() + @"\wwwroot\";
        static string path = @"images\";
        public static string Add(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName).ToUpper();
            string newFileName = Guid.NewGuid().ToString("N") + extension;

            if (!Directory.Exists(directory + path))
            {
                Directory.CreateDirectory(directory + path);
            }

            using (FileStream fileStream = File.Create(directory + path + newFileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            return (path + newFileName).Replace("\\", "/");
        }

        public static string Update(IFormFile file, string oldImagePath)
        {
            Delete(oldImagePath);
            return Add(file);
        }

        public static void Delete(string imagePath)
        {
            if (File.Exists(directory + imagePath.Replace("/", "\\"))
                && Path.GetFileName(imagePath) != "logo.jpg")
            {
                File.Delete(directory + imagePath.Replace("/", "\\"));
            }
        }
    }
}