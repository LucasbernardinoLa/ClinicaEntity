using ClinicaSorrisoEntity.Dados.DTOs;
using ClinicaSorrisoEntity.Models;
using ClinicaSorrisoEntity.Services;
using ClinicaSorrisoEntity.Views;

namespace ClinicaSorrisoEntity.Controllers
{
    //Classe que recebe e envia os dados ConsultaView e interage com o model Consulta e Paciente, e os servicos da aplicação
    public class ConsultaController
    {
        public ConsultaController()
        {
        }

        // Obtém a opção selecionada pelo usuario no Menu da Agenda e executa a respectiva funcionalidade
        public void LeituraOpcao()
        {
            bool exit = false;

            while (!exit)
            {
                var opcao = Console.ReadKey();
                switch (opcao.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Cadastrar();
                        break;
                    case '2':
                        Console.Clear();
                        Excluir();
                        break;
                    case '3':
                        Console.Clear();
                        ListarAgenda();
                        break;
                    case '4':
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        ConsultaView.MenuAgenda();
                        break;
                }

            }
        }

        // Executa a lógica de agendamento de uma consulta
        public void Cadastrar()
        {
            using (var repo = new ConsultaService())
            {
                var pacienteExistente = repo.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());

                if (pacienteExistente is null)
                {
                    PacienteView.MensagemErro("paciente não cadastrado.");
                    return;
                }

                if (pacienteExistente.TemConsultaFutura())
                {
                    string mensagemErro = $" o paciente já possui consulta marcada para " +
                        $"{pacienteExistente.ConsultaMarcada.Data:dd/MM/yyyy} as " +
                        $"{pacienteExistente.ConsultaMarcada.GetHorario(pacienteExistente.ConsultaMarcada.HoraInicio)}h";

                    PacienteView.MensagemErro(mensagemErro);
                    return;
                }

                var dadosConsulta = ConsultaView.ObterDadosConsulta();

                var novaConsulta = new CreateConsultaDTO(pacienteExistente,
                                                DateTime.Parse(dadosConsulta.DataConsulta),
                                                dadosConsulta.HoraInicio,
                                                dadosConsulta.HoraFim);

                if (repo.TemConflitoDeHorario(novaConsulta))
                {
                    PacienteView.MensagemErro("já existe uma consulta agendada nesta data/hora.");
                    return;
                }
                else
                {
                    repo.CadastrarConsulta(novaConsulta);
                    ConsultaView.AgendamentoRealizado();
                }
            }
        }

        // Executa a lógica de cancelamento de uma consulta
        public void Excluir()
        {
            using(var repo = new ConsultaService())
            {
                var pacienteExistente = repo.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                if (pacienteExistente is null)
                {
                    PacienteView.MensagemErro("paciente não cadastrado.");
                    return;
                }

                var dadosConsulta = ConsultaView.ObterDadosConsulta();

                var consultaExcluir = new Consulta(pacienteExistente,
                                                DateTime.Parse(dadosConsulta.DataConsulta),
                                                dadosConsulta.HoraInicio,
                                                dadosConsulta.HoraFim);

                var consultaExistente = repo.BuscarConsulta(consultaExcluir);

                if (consultaExistente is null)
                {
                    ConsultaView.MensagemErro("agendamento não encontrado.");
                    return;
                }
                repo.ExcluirConsulta(consultaExistente);
                ConsultaView.ConsultaExcluida();
            }
        }

        //Obtem o tipo de listagem escolhida e passa para a View a lista correspondente
        public void ListarAgenda()
        {
            using(var repo = new ConsultaService())
            {
                var opcaoListagem = ConsultaView.ObterOpcaoListagem();
                Console.Clear();
                if (opcaoListagem == 'T' || opcaoListagem == 't')
                {
                    ConsultaView.ListarAgenda(repo.ListarConsultasPorData());
                }
                if (opcaoListagem == 'P' || opcaoListagem == 'p')
                {
                    var datasPeriodo = ConsultaView.ObterPeriodoListagem();
                    ConsultaView.ListarAgenda(repo.ListarConsultasPorPeriodo(datasPeriodo[0], datasPeriodo[1]));
                }
                else
                {
                    Console.WriteLine();
                }
            }          
        }
    }
}
