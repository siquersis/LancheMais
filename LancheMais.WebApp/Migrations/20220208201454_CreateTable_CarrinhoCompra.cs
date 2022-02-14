using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LancheMais.WebApp.Migrations
{
    public partial class CreateTable_CarrinhoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LancheId = table.Column<int>(type: "int", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CarrinhoCompraId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompra_Lanche_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanche",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompra_LancheId",
                table: "CarrinhoCompra",
                column: "LancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompra");
        }
    }
}
