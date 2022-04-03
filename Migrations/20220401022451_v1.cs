using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodavnica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnica", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sara",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sara", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ploca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duzina = table.Column<float>(type: "real", nullable: false),
                    Sirina = table.Column<float>(type: "real", nullable: false),
                    Otpadna = table.Column<bool>(type: "bit", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true),
                    SaraID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ploca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ploca_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ploca_Sara_SaraID",
                        column: x => x.SaraID,
                        principalTable: "Sara",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdavnicaSara",
                columns: table => new
                {
                    ProdavnicaID = table.Column<int>(type: "int", nullable: false),
                    SaraID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdavnicaSara", x => new { x.ProdavnicaID, x.SaraID });
                    table.ForeignKey(
                        name: "FK_ProdavnicaSara_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdavnicaSara_Sara_SaraID",
                        column: x => x.SaraID,
                        principalTable: "Sara",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ploca_ProdavnicaID",
                table: "Ploca",
                column: "ProdavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ploca_SaraID",
                table: "Ploca",
                column: "SaraID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdavnicaSara_SaraID",
                table: "ProdavnicaSara",
                column: "SaraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ploca");

            migrationBuilder.DropTable(
                name: "ProdavnicaSara");

            migrationBuilder.DropTable(
                name: "Prodavnica");

            migrationBuilder.DropTable(
                name: "Sara");
        }
    }
}
