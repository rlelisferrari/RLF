using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class scoutidbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ScoutsGerais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ScoutsGerais");
        }
    }
}
