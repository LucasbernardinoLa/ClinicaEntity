using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSorrisoEntity.Models
{
    // Classe que representa uma consulta na aplicação
    public class Consulta
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    
        [Required(ErrorMessage = "O campo Data é obrigatório")]
        public DateTime Data { get; set; }          
        
        [Required(ErrorMessage = "O campo HoraInicio é obrigatório")]
        public string HoraInicio { get; set; }
        
        [Required(ErrorMessage = "O campo HoraFim é obrigatório")]
        public string HoraFim { get; set; }
        public virtual Paciente Paciente { get; set; }
        [Required]
        public string PacienteId { get; set; }
        
        private TimeSpan _tempo;
        public TimeSpan TempoDeConsulta
        {
            get
            {
                return _tempo;
            }
            set
            {
                var horaInicio = new TimeSpan(int.Parse(HoraInicio.Substring(0, 2)), int.Parse(HoraInicio.Substring(2)), 0);
                var horaFim = new TimeSpan(int.Parse(HoraFim.Substring(0, 2)), int.Parse(HoraFim.Substring(2)), 0);
                _tempo = horaFim - horaInicio;
            }
        }
        public Consulta()
        {
        }

        public Consulta(Paciente paciente, DateTime data, string horaInicio, string horaFim)
        {
            Paciente = paciente;
            Data = data;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        //Recebe uma consulta e retorna se há um conflito de horário com a mesma
        public bool TemConflitoDeHorario(Consulta consulta)
        {
            int.TryParse(HoraInicio, out int horaInicio);
            int.TryParse(HoraFim, out int horaFim);
            int.TryParse(consulta.HoraInicio, out int consultaHoraInicio);
            int.TryParse(consulta.HoraFim, out int consultaHoraFim);

            return !(horaInicio >= consultaHoraFim || horaFim <= consultaHoraInicio);
        }        

        //Retorna o horário no formato HH:MM
        public string GetHorario(string horarioStr)
        {
            return horarioStr.Insert(2, ":");
        }
        
    }
}
