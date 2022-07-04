using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSorrisoEntity.Dados.DTOs
{
    public class CreatePacienteDTO
    {
        [Key]
        [Required]
        public string Cpf { get; private set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "O campo DataNascimento é obrigatório")]
        public DateTime DataNascimento { get; private set; }
        private int _idade;
        public int Idade
        {
            get
            {
                var dataHoje = DateTime.Today;
                var anos = dataHoje.Year - DataNascimento.Year;

                if (DataNascimento > dataHoje.AddYears(-anos))
                {
                    anos--;
                }
                _idade = anos;
                return _idade;
            }
        }
        public CreatePacienteDTO(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
