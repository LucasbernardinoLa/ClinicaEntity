using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;

namespace ClinicaSorrisoEntity.Profiles
{
    public class ConsultaProfile
    {
        public CreateConsultaDTO ConsultaDTO { get; private set; }
        public Consulta  Consulta { get; private set; }

        public static void  Convert(CreateConsultaDTO consultaDTO, Consulta consulta)
        {
            consulta.Paciente = consultaDTO.Paciente;
            consulta.Data = consultaDTO.Data;
            consulta.HoraInicio = consultaDTO.HoraInicio;
            consulta.HoraFim = consultaDTO.HoraFim;
            consulta.TempoDeConsulta = consultaDTO.TempoDeConsulta;
        }
    }
}
