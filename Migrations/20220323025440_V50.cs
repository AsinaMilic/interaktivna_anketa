using Microsoft.EntityFrameworkCore.Migrations;

namespace nesto.Migrations
{
    public partial class V50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Korisnicko_ime = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Anketa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entitet = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: true),
                    Info = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(99)", maxLength: 99, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anketa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fakultet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Info = table.Column<string>(type: "nvarchar(666)", maxLength: 666, nullable: true),
                    administratorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fakultet_Administrator_administratorID",
                        column: x => x.administratorID,
                        principalTable: "Administrator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdministratorAnketa",
                columns: table => new
                {
                    AdminAnetaID = table.Column<int>(type: "int", nullable: false),
                    AnketaAdministratorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministratorAnketa", x => new { x.AdminAnetaID, x.AnketaAdministratorID });
                    table.ForeignKey(
                        name: "FK_AdministratorAnketa_Administrator_AnketaAdministratorID",
                        column: x => x.AnketaAdministratorID,
                        principalTable: "Administrator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdministratorAnketa_Anketa_AdminAnetaID",
                        column: x => x.AdminAnetaID,
                        principalTable: "Anketa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pitanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst_pitanja = table.Column<string>(type: "nvarchar(666)", maxLength: 666, nullable: false),
                    tip_pitanja = table.Column<int>(type: "int", nullable: false),
                    Moguci_odgovori = table.Column<string>(type: "nvarchar(666)", maxLength: 666, nullable: true),
                    anketaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pitanje_Anketa_anketaID",
                        column: x => x.anketaID,
                        principalTable: "Anketa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Popunjavanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Popunjena = table.Column<bool>(type: "bit", nullable: false),
                    anketaID = table.Column<int>(type: "int", nullable: true),
                    studentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Popunjavanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Popunjavanje_Anketa_anketaID",
                        column: x => x.anketaID,
                        principalTable: "Anketa",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Popunjavanje_Student_studentID",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Odgovor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst_odgovora = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(666)", maxLength: 666, nullable: true),
                    pitanjeID = table.Column<int>(type: "int", nullable: true),
                    studentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odgovor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odgovor_Pitanje_pitanjeID",
                        column: x => x.pitanjeID,
                        principalTable: "Pitanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odgovor_Student_studentID",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministratorAnketa_AnketaAdministratorID",
                table: "AdministratorAnketa",
                column: "AnketaAdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_AnketaFakultet_FakultetAnketeID",
                table: "AnketaFakultet",
                column: "FakultetAnketeID");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultet_administratorID",
                table: "Fakultet",
                column: "administratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_pitanjeID",
                table: "Odgovor",
                column: "pitanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_studentID",
                table: "Odgovor",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Pitanje_anketaID",
                table: "Pitanje",
                column: "anketaID");

            migrationBuilder.CreateIndex(
                name: "IX_Popunjavanje_anketaID",
                table: "Popunjavanje",
                column: "anketaID");

            migrationBuilder.CreateIndex(
                name: "IX_Popunjavanje_studentID",
                table: "Popunjavanje",
                column: "studentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministratorAnketa");

            migrationBuilder.DropTable(
                name: "AnketaFakultet");

            migrationBuilder.DropTable(
                name: "Odgovor");

            migrationBuilder.DropTable(
                name: "Popunjavanje");

            migrationBuilder.DropTable(
                name: "Fakultet");

            migrationBuilder.DropTable(
                name: "Pitanje");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Anketa");
        }
    }
}
