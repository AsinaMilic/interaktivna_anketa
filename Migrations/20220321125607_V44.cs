using Microsoft.EntityFrameworkCore.Migrations;

namespace nesto.Migrations
{
    public partial class V44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnketaFakultet",
                columns: table => new
                {
                    AnketaFakultetiID = table.Column<int>(type: "int", nullable: false),
                    FakultetAnketeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnketaFakultet", x => new { x.AnketaFakultetiID, x.FakultetAnketeID });
                    table.ForeignKey(
                        name: "FK_AnketaFakultet_Anketa_FakultetAnketeID",
                        column: x => x.FakultetAnketeID,
                        principalTable: "Anketa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnketaFakultet_Fakultet_AnketaFakultetiID",
                        column: x => x.AnketaFakultetiID,
                        principalTable: "Fakultet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnketaFakultet_FakultetAnketeID",
                table: "AnketaFakultet",
                column: "FakultetAnketeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnketaFakultet");
        }
    }
}
