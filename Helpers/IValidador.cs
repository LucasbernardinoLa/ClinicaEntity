using System;
namespace ClinicaSorrisoEntity.Helpers
{
    //Interface para implementacao da validacao dos campos de entrada
    public interface IValidador
    {
        void IniciaValidacao();
        void InicializaDicionarioDeErros();
        bool HasErrors();
        void ExibirErros();
        void ValidarDados();
    }
}
