using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;
using ClinicaSorrisoEntity.Profiles;
using Microsoft.Data.SqlClient;

namespace ClinicaSorrisoEntity.Dados.DAO
{
    // Implementação da interface do repositório de Pacientes em memória
    public class PacientesDAO : IDAO, IDisposable
    {
        private ClinicaContext _contexto { get; set; }

        public PacientesDAO()
        {
            _contexto = new ClinicaContext();
        }

        // Recebe um Paciente e o exclui da base de pacientes
        public void DeletarPaciente(Paciente paciente)
        {
            try
            {
                _contexto.Pacientes.Remove(paciente);
                _contexto.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        // Retorna uma lista com dos os pacientes da base de pacientes
        public IList<Paciente> ListarPacientes()
        {
            return _contexto.Pacientes.ToList();
        }

        // Recebe um paciente e salva na base de pacientes
        public void SalvarPaciente(CreatePacienteDTO pacienteDTO)
        {
            try
            {
                Paciente paciente = new Paciente();
                PacienteProfile.Convert(paciente, pacienteDTO);
                _contexto.Pacientes.Add(paciente);
                _contexto.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
