using ClinicaSorrisoEntity.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSorrisoEntity.Dados.DTOs
{
    public class CreateConsultaDTO
    {
        [Required(ErrorMessage = "O campo Data é obrigatório")]
        public DateTime Data { get; private set; }

        [Required(ErrorMessage = "O campo HoraInicio é obrigatório")]
        public string HoraInicio { get; private set; }

        [Required(ErrorMessage = "O campo HoraFim é obrigatório")]
        public string HoraFim { get; private set; }
        public  Paciente Paciente { get; private set; }
        public string PacienteId { get; private set; }
        public TimeSpan TempoDeConsulta { get;  set; }

        public CreateConsultaDTO(Paciente paciente, DateTime data, string horaInicio, string horaFim)
        {
            Paciente = paciente;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
    }
}
