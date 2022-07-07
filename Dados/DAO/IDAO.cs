using ClinicaSorrisoEntity.Models;
using System.Collections.Generic;

namespace ClinicaSorrisoEntity.Dados.DAO
{
    // Interface do repositório de dados
    public interface IDAO
    {
        IEnumerable<Paciente> ListarPacientes();
    }
}
