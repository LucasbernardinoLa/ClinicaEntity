using ClinicaSorrisoEntity.Dados.DAO;
using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;
using Microsoft.Data.SqlClient;

namespace ClinicaSorrisoEntity.Services
{
    // Classe que implementa a interface de serviços do Paciente
    public class PacienteService : IPacienteService, IDisposable
    {
        private PacientesDAO _pacientesDAO { get; set; }

        public PacienteService()
        {
            _pacientesDAO = new PacientesDAO();
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o cadastro pode ou não ser realizado
        public void CadastrarPaciente(CreatePacienteDTO pacienteDTO)
        {
            try
            {
                var existePaciente = ConsultarPacientePorCPF(pacienteDTO.Cpf);

                if (existePaciente != null)
                {
                    throw new ArgumentException("CPF já cadastrado.");
                }

                if (pacienteDTO.Idade < 13)
                {
                    throw new ApplicationException($"paciente só tem {pacienteDTO.Idade} anos.");
                }
                _pacientesDAO.SalvarPaciente(pacienteDTO);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Recebe um paciente e verifica de acordo com as regras de negócio se o paciente pode ou não ser excluído
        public void ExcluirPaciente(Paciente paciente)
        {
            try
            {
                if (paciente == null)
                {
                    throw new ApplicationException("paciente não cadastrado.");
                }

                if (paciente.TemConsultaFutura())
                {
                    throw new ApplicationException($"paciente está agendado para {paciente.ConsultaMarcada.Data:dd/MM/yyyy} as {paciente.ConsultaMarcada.GetHorario(paciente.ConsultaMarcada.HoraInicio)}h.");
                }
                _pacientesDAO.DeletarPaciente(paciente);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Recebe um CPF e retorna um paciente, caso caso o mesmo esteja cadastrado na base de pacientes
        public Paciente ConsultarPacientePorCPF(string cpf)
        {
            try
            {
                return _pacientesDAO.ListarPacientes()
                       .Where(p => p.Cpf == cpf)
                       .SingleOrDefault();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Retorna uma lista da base de pacientes ordenada por CPF
        public List<Paciente> ListarPacientesPorCPF()
        {
            try
            {
                return _pacientesDAO.ListarPacientes().OrderBy(p => p.Cpf).ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Retorna uma lista da base de pacientes ordenada por Nome
        public List<Paciente> ListarPacientesPorNome()
        {
            try
            {
                return _pacientesDAO.ListarPacientes().OrderBy(p => p.Nome).ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _pacientesDAO.Dispose();
        }
    }
}
