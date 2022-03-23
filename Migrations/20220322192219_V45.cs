using Microsoft.EntityFrameworkCore.Migrations;

namespace nesto.Migrations
{
    public partial class V45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Komentar",
                table: "Popunjavanje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Komentar",
                table: "Popunjavanje",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
