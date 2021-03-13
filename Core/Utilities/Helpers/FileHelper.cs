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

        public static string Add(IFormFile file)
        {
            var sourcepath = Path.GetTempFileName();
            if (file.Length > 0)
            {
                using (var uploading = new FileStream(sourcepath, FileMode.Create)) // Dosya okuma yazma
                {
                    file.CopyTo(uploading);
                }
            }

            var result = NewPath(file);

            File.Move(sourcepath, result);

            return result;
        }

        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public static string Update(string sourcePath, IFormFile file)
        {
            var result = NewPath(file).ToString();
            if (sourcePath.Length > 0)
            {
                using (var stream = new FileStream(result, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            File.Delete(sourcePath);

            return result;
        }

        public static string NewPath(IFormFile file)
        { // Dosya yolu oluşturma
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;

            string path = Environment.CurrentDirectory + @"\wwwroot\uploads";
            var newPath = Guid.NewGuid().ToString("N") +  fileExtension; // Kendi verdiğim GUID ile dosyalanacak

            string result = $@"{path}\{newPath}";
            return result;
        }
    }
}