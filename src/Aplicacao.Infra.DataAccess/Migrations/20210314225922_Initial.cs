using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aplicacao.Infra.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeCompleto = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "datetime", maxLength: 10, nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "datetime", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClienteId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    peso = table.Column<double>(type: "float", maxLength: 300, nullable: false),
                    sku = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(10,2)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProdutoId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompraId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_Cliente_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    pais = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    estado = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    cidade = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    bairro = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    rua = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    numero = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    complemento = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    cep = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EnderecoId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cliente_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<int>(type: "int", maxLength: 300, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(10,2)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CompraItemId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraItem_Compra_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraItem_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "descricao", "preco", "sku", "peso" },
                values: new object[] { 1, "IPhone 12", 8799.22m, "ae831b1c", 200.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_CustomerId",
                table: "Compra",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_OrderId",
                table: "CompraItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItem_ProductId",
                table: "CompraItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CustomerId",
                table: "Endereco",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraItem");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
