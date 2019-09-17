using System;
using System.Collections.Generic;
using System.Text;

namespace Palladin.Services.ViewModel.Project
{
    public class JoinProjectViewModel
    {
        public Guid ProjectId { get; set; }
        public Guid VulnerabilityId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<UriViewModel> ListOfUris { get; set; }
        public IEnumerable<FileViewModel> ListOfFiles { get; set; }
    }

    public class UriViewModel
    {
        public string Uri { get; set; }
        public string Port { get; set; }
        public string Method { get; set; }
        public string FormCookie { get; set; }
    }

    public class FileViewModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Base64 { get; set; }
        public DataFileViewModel File { get; set; }
    }

    public class DataFileViewModel
    {
        public DateTime LastModified { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
    }
}
