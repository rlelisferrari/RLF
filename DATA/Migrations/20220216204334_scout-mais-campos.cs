using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class scoutmaiscampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assistencia",
                table: "ScoutsGerais",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cartaAmarelo",
                table: "ScoutsGerais",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cartaVermelho",
                table: "ScoutsGerais",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "gol",
                table: "ScoutsGerais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assistencia",
                table: "ScoutsGerais");

            migrationBuilder.DropColumn(
                name: "cartaAmarelo",
                table: "ScoutsGerais");

            migrationBuilder.DropColumn(
                name: "cartaVermelho",
                table: "ScoutsGerais");

            migrationBuilder.DropColumn(
                name: "gol",
                table: "ScoutsGerais");
        }
    }
}
