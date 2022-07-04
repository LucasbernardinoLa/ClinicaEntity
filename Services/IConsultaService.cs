using System;
using System.Collections.Generic;
using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;

namespace ClinicaSorrisoEntity.Services
{
    // Interface de serviço responsável pelas operações da Consulta entre o controller e o repositorio
    public interface IConsultaService
    {
        List<Consulta> ListarConsultasPorData();
        List<Consulta> ListarConsultasPorPeriodo(DateTime dtInicio, DateTime dtFim);
        List<Consulta> ListarConsultasDoDia(DateTime data);
        Consulta BuscarConsulta(Consulta consulta);
        void CadastrarConsulta(CreateConsultaDTO consulta);
        void ExcluirConsulta(Consulta consulta);
        bool TemConflitoDeHorario(CreateConsultaDTO novaConsulta);
    }
}