using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicaSorrisoEntity.Migrations
{
    public partial class criacaodetabelaidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Pacientes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Pacientes");
        }
    }
}
