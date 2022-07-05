using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;
using ClinicaSorrisoEntity.Profiles;
using Microsoft.Data.SqlClient;

namespace ClinicaSorrisoEntity.Dados.DAO
{
    // Implementação da interface do repositório de Consultas em memória
    public class ConsultaDAO : IDAO, IDisposable
    {
        private ClinicaContext _contexto { get; set; }

        public ConsultaDAO()
        {
            _contexto = new ClinicaContext();
        }

        // Recebe uma Consulta e salva na base de consultas
        public void SalvarConsulta(CreateConsultaDTO createConsultaDTO)
        {
            try
            {
                Consulta consulta = new Consulta();
                ConsultaProfile.Convert(createConsultaDTO, consulta);
                _contexto.Consultas.Add(consulta);
                _contexto.SaveChanges();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Recebe uma Consulta e exclui da base de consultas
        public void DeletarConsulta(Consulta entity)
        {
            try
            {
                _contexto.Consultas.Remove(entity);
                _contexto.SaveChanges();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Retorna uma lista com todas as consultas da base de consultas
        public IList<Consulta> ListarConsulta()
        {
            try
            {
                return _contexto.Consultas.ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public IList<Paciente> ListarPacientes()
        {
            try
            {
                return _contexto.Pacientes.ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}