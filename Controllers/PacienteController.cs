using System;
using ClinicaSorrisoEntity.Services;
using ClinicaSorrisoEntity.Views;
using Microsoft.Data.SqlClient;

namespace ClinicaSorrisoEntity.Controllers
{
    //Classe que recebe e envia os dados PacienteView e interage com o model Paciente e os servicos da aplicação
    public class PacienteController
    {
        public PacienteController()
        {
        }

        // Obtém a opção selecionada pelo usuario no Menu do Paciente e executa a respectiva funcionalidade
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
                        ListarPorCpf();
                        break;
                    case '4':
                        Console.Clear();
                        ListarPorNome();
                        break;
                    case '5':
                        Console.Clear();
                        MenuView.MenuPrincipal();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        PacienteView.MenuPaciente();
                        break;
                }

            }

        }

        // Executa a lógica de cadastro de um paciente
        public void Cadastrar()
        {
            using (var repo = new PacienteService())
            {
                try
                {
                    repo.CadastrarPaciente(PacienteView.Cadastrar());
                    PacienteView.CadastroRealizado();
                }
                catch (ArgumentException ex)
                {
                    PacienteView.MensagemErro(ex.Message);
                }
                catch (ApplicationException ex)
                {
                    PacienteView.MensagemErro(ex.Message);
                }
                catch (SqlException ex)
                {
                    PacienteView.MensagemErro(ex.Message);
                }
            }
        }

        // Executa a lógica de exclusão de um paciente de acordo com o CPF informado
        public void Excluir()
        {
            using (var repo = new PacienteService())
            {
                try
                {
                    var pacienteSalvo = repo.ConsultarPacientePorCPF(PacienteView.ConsultarCpf());
                    repo.ExcluirPaciente(pacienteSalvo);
                    PacienteView.PacienteExcluido();
                }
                catch (ApplicationException ex)
                {
                    PacienteView.MensagemErro(ex.Message);
                }
                catch (SqlException ex)
                {
                    PacienteView.MensagemErro(ex.Message);
                }
            }
        }

        // Obtém a lista de pacientes ordenadas por CPF do repositório e envia para ser exibida na PacienteView
        public void ListarPorCpf()
        {
            try
            {
                using (var repo = new PacienteService())
                {
                    PacienteView.ListarPacientes(repo.ListarPacientesPorCPF());
                }
            }
            catch (SqlException ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
        }

        // Obtém a lista de pacientes ordenadas por Nome do repositório e envia para ser exibida na PacienteView
        public void ListarPorNome()
        {
            try
            {
                using (var repo = new PacienteService())
                {
                    PacienteView.ListarPacientes(repo.ListarPacientesPorNome());
                }
            }
            catch (SqlException ex)
            {
                PacienteView.MensagemErro(ex.Message);
            }
        }
    }
}
