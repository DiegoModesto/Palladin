using System;
using System.ComponentModel.DataAnnotations;

namespace Palladin.Services.ViewModel.Project
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(80, ErrorMessage = "Nome deve conter no máximo 80 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Data inicial é obrigatória.")]
        public DateTime InitialDate { get; set; }
        [Required(ErrorMessage = "Data final é obrigatória.")]
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Tipo de projeto é obrigatório.")]
        public string ProjectType { get; set; }
        [MaxLength(100, ErrorMessage = "Nome da subsidiária precisa ter no máximo 100 letras")]
        public string Subsidiary { get; set; }
        [Required(ErrorMessage = "É necessário preencher o cliente")]
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
    }
}
