using ClinicaSorrisoEntity.Controllers;

PacienteController pacienteController = new PacienteController();
ConsultaController consultaController = new ConsultaController();
AppController app = new AppController(pacienteController, consultaController);

app.Run();