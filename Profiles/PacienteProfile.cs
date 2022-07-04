using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;

namespace ClinicaSorrisoEntity.Profiles
{
    public class PacienteProfile
    {
        public CreatePacienteDTO PacienteDTO { get; private set; }
        public Paciente Paciente { get; private set; }


        public static void Convert(Paciente paciente, CreatePacienteDTO pacienteDTO)
        {
            paciente.Cpf = pacienteDTO.Cpf;
            paciente.Nome = pacienteDTO.Nome;
            paciente.DataNascimento = pacienteDTO.DataNascimento;
            paciente.Idade = pacienteDTO.Idade;       
        }
    }
}
