using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FullStackWebApp.Models
{
    public class Pessoa
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; } 

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(80,ErrorMessage ="O nome deve conter 80 caracteres")]
        [DisplayName("Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória")]
        [Range(18, int.MaxValue, ErrorMessage = "A idade deve ser maior ou igual a 18 anos")]
        [DisplayName("Idade")]
        public int Idade { get; set; }
        public List<Dependente> Dependentes { get; set; } = new();
    }
}
