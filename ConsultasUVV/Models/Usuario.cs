using System.ComponentModel.DataAnnotations;

namespace ConsultasUVV.Models
{
    ///
    /// Entidade que representa um usuário do sistema.
    /// A senha nunca é armazenada em texto puro: apenas o hash (SenhaHash) é persistido.
    /// 
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(150)]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        /// 
        /// Hash da senha (gerado via PasswordHasher). Nunca exponha isso em Views.
        /// 
        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Relacionamento: um usuário pode ter várias consultas
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
