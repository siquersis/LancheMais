using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LancheMais.WebApp.Migrations
{
    public partial class Rename_Table_CarrinhoCompra_For_CarrinhoCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompra");

            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItem",
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
                    table.PrimaryKey("PK_CarrinhoCompraItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItem_Lanche_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanche",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItem_LancheId",
                table: "CarrinhoCompraItem",
                column: "LancheId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItem");

            migrationBuilder.CreateTable(
                name: "CarrinhoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LancheId = table.Column<int>(type: "int", nullable: true),
                    CarrinhoCompraId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
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
    }
}
