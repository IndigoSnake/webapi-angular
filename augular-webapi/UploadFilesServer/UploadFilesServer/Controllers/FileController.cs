using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UploadFilesServer.Models;

namespace UploadFilesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<FileItem> GetFiles()
        {
            List<FileItem> result = new List<FileItem>();
            var folderName = Path.Combine("StaticFiles", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var files = new DirectoryInfo(pathToSave).GetFiles();
            foreach(var item in files){
                var fi = new FileItem();
                fi.Name = item.Name.Substring(0,item.Name.Length-item.Extension.Length);
                if (item.Length < 1024)
                {
                    fi.Size = item.Length.ToString() + "B";
                }
                else if(item.Length >= 1024 && item.Length < 1024 * 1024){
                    fi.Size = (item.Length / 1024).ToString() + "KB";
                }
                else if (item.Length >= 1024 * 1024 && item.Length < 1024 * 1024*1024)
                {
                    fi.Size = (item.Length / 1024 / 1024).ToString() + "MB";
                }
                else if (item.Length >= 1024 * 1024 * 1024)
                {
                    fi.Size = (item.Length / 1024 / 1024).ToString() + "GB";
                }

                fi.UploadedDate = item.LastWriteTime.ToShortDateString() +" "+ item.LastWriteTime.ToLongTimeString();
                if (item.Extension.Substring(1).ToLower() == "png")
                    fi.Type = "image/png";
                else if (item.Extension.Substring(1).ToLower() == "txt")
                    fi.Type = "text/plain";
                else if (item.Extension.Substring(1).ToLower() == "pdf")
                    fi.Type = "application/pdf";

                result.Add(fi);
            }
            return result;
        }
    }
}