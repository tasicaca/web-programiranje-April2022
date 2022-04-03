using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Ploca",
                newName: "Brojnost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brojnost",
                table: "Ploca",
                newName: "Naziv");
        }
    }
}
