using System;

namespace Palladin.Services.ViewModel.Project
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProjectType { get; set; }
        public string Subsidiary { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
    }
}
