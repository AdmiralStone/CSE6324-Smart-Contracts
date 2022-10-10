using System;
using System.Collections.Generic;

namespace NethereumWebUI.Data
{
    public partial class Contract
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = null!;
        public string? Size { get; set; }
        public string FileType { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? UploadedBy { get; set; }
    }
}
