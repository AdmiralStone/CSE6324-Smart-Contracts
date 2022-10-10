using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NethereumWebUI.Models
{
    public class ContractModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string FileType { get; set; }
        public string ModifiedOn { get; set; }
        public string UploadedBy { get; set; }
    }
}