using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class TipoEquipamento_e_EquipamentoTipoEquipamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoEquipamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipamentoTipoEquipamento",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(nullable: false),
                    TipoEquipamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipamentoTipoEquipamento", x => new { x.EquipamentoId, x.TipoEquipamentoId });
                    table.ForeignKey(
                        name: "FK_EquipamentoTipoEquipamento_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipamentoTipoEquipamento_TipoEquipamento_TipoEquipamentoId",
                        column: x => x.TipoEquipamentoId,
                        principalTable: "TipoEquipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipamentoTipoEquipamento_TipoEquipamentoId",
                table: "EquipamentoTipoEquipamento",
                column: "TipoEquipamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipamentoTipoEquipamento");

            migrationBuilder.DropTable(
                name: "TipoEquipamento");
        }
    }
}
