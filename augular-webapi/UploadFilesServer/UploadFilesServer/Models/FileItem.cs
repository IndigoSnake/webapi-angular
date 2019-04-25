using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFilesServer.Models
{
    public class FileItem
    {
        public FileItem()
        {
            this.User = "";
            this.Type = "";
        }
        public string Name { get; set; }

        public string Size { get; set; }

        public string UploadedDate { get; set; }

        public string User { get; set; }

        public string Type { get; set; }
    }
}
