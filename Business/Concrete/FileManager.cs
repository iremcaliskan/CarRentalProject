using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileManager : IFileProcess
    {
        private readonly IHostingEnvironment _hostEnvironment;
        string FileDirectory;

        public FileManager(IHostingEnvironment environment)
        {
            _hostEnvironment = environment;
            FileDirectory = environment.ContentRootPath + "/images/";
        }

        // Upload to server's assets folder
        // <param name="fileName">Guid File Name</param>
        // <param name="file">Image</param>
        public async Task<IResult> Upload(string fileName, IFormFile file)
        {
            using (var fileStream = new FileStream(Path.Combine(FileDirectory, fileName.ToString() + ".jpg"), FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }
            return new SuccessResult("The file is uploaded in success!");
        }

        // Delete file from given path
        // <param name="path">Guid file path</param>
        public IResult Delete(string path)
        {
            var roadpath = Path.Combine(FileDirectory, path + ".jpg");
            if (File.Exists(roadpath))
            {
                File.Delete(roadpath);
            }
            return new SuccessResult();
        }
    }
}
