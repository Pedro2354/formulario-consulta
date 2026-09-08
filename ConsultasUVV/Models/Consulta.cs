using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultasUVV.Models
{
    /// 
    /// Entidade que representa uma consulta médica/profissional agendada por um usuário.
    ///
    public class Consulta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A especialidade deve ter no máximo 100 caracteres.")]
        [Display(Name = "Especialidade")]
        public string Especialidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data e hora da consulta são obrigatórias.")]
        [Display(Name = "Data e Hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        // Chave estrangeira para o Usuario dono da consulta
        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }
    }
}
