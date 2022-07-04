using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSorrisoEntity.Models
{
    // Classe que representa um paciente na aplicação
    public class Paciente
    {
        [Key]
        [Required]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo DataNascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public virtual Consulta ConsultaMarcada { get; set; }

        public Paciente()
        {
        }
        public Paciente(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        // Retorna se o paciente possui uma consulta marcada futura
        public bool TemConsultaFutura()
        {
            if (ConsultaMarcada != null)
            {
                return ConsultaMarcada.Data.Date >= DateTime.Now.Date;
            }
            return false;
        }
    }
}
