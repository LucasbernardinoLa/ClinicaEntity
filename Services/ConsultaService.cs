using ClinicaSorrisoEntity.Dados.DAO;
using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;
using Microsoft.Data.SqlClient;

namespace ClinicaSorrisoEntity.Services
{
    public class ConsultaService : IConsultaService, IDisposable
    {
        private ConsultaDAO _consultaDAO { get; set; }

        public ConsultaService()
        {
           _consultaDAO =  new ConsultaDAO();
        }

        public Paciente ConsultarPacientePorCPF(string cpf)
        {
            try
            {
                return _consultaDAO.ListarPacientes().Where(p => p.Cpf == cpf)
                                                 .SingleOrDefault();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Recebe uma consulta e salva no repositorio
        public void CadastrarConsulta(CreateConsultaDTO consulta)
        {
            try
            {
                _consultaDAO.SalvarConsulta(consulta);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        // Recebe uma consulta e exclui do repositorio
        public void ExcluirConsulta(Consulta consulta)
        {
            try
            {
                _consultaDAO.DeletarConsulta(consulta);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //Retorna uma lista com todas as consultas do repositorio ordenadas por data
        public List<Consulta> ListarConsultasPorData()
        {
            try
            {
                return _consultaDAO.ListarConsulta()
                               .OrderBy(c => c.Data)
                               .ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //Recebe uma data e retorna uma lista com todas as consultas agendadas para este dia
        public List<Consulta> ListarConsultasDoDia(DateTime dataAgendamento)
        {
            try
            {
                return _consultaDAO.ListarConsulta()
                               .Where(c => c.Data.Date == dataAgendamento.Date)
                               .ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //Recebe uma consulta e retorna se há conflito de horário desta consulta com as demais consultas do mesmo dia
        public bool TemConflitoDeHorario(CreateConsultaDTO novaConsulta)
        {
            var consultasDoDia = ListarConsultasDoDia(novaConsulta.Data);
            foreach (var consulta in consultasDoDia)
            {
                if (VerificaSeTemConflito(novaConsulta,consulta))
                {
                    return true;
                }
            }
            return false;
        }
        public bool VerificaSeTemConflito(CreateConsultaDTO consultaDTO, Consulta consulta)
        {
            int.TryParse(consulta.HoraInicio, out int horaInicio);
            int.TryParse(consulta.HoraFim, out int horaFim);
            int.TryParse(consultaDTO.HoraInicio, out int consultaHoraInicio);
            int.TryParse(consultaDTO.HoraFim, out int consultaHoraFim);

            return !(horaInicio >= consultaHoraFim || horaFim <= consultaHoraInicio);
        }

        //Recebe uma data inicial e final e retorna uma lista com todas as consultas agendadas nesse periodo
        public List<Consulta> ListarConsultasPorPeriodo(DateTime dtInicio, DateTime dtFim)
        {
            try
            {
                return _consultaDAO.ListarConsulta()
                               .Where(consulta => (consulta.Data.Date >= dtInicio.Date) &
                                                   consulta.Data.Date <= dtFim.Date)
                               .ToList();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //Recebe uma consulta e caso exista no repositorio, retorna ela
        public Consulta BuscarConsulta(Consulta consulta)
        {
            try
            {
                return _consultaDAO.ListarConsulta()
                               .Where(c => c.Paciente.Cpf == consulta.Paciente.Cpf &
                               c.Data.Date == consulta.Data.Date &
                               c.HoraInicio == consulta.HoraInicio)
                               .SingleOrDefault();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public void Dispose()
        {
            _consultaDAO.Dispose();
        }
    }
}
