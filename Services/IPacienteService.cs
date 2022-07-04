using System.Collections.Generic;
using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;

namespace ClinicaSorrisoEntity.Services
{
    // Interface de serviço responsável pelas operações do Paciente entre o controller e o repositorio
    public interface IPacienteService
    {
        List<Paciente> ListarPacientesPorCPF();
        List<Paciente> ListarPacientesPorNome();
        Paciente ConsultarPacientePorCPF(string cpf);
        void CadastrarPaciente(CreatePacienteDTO pacienteDTO);
        void ExcluirPaciente(Paciente paciente);
    }
}