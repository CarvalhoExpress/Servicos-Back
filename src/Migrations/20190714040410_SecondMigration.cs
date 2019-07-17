using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrestadorDeServico",
                schema: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Sobrenome = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Telefone = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Rua = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Numero = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Cep = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Documento = table.Column<byte[]>(nullable: true),
                    Selfie = table.Column<byte[]>(nullable: true),
                    Rg = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Cpf = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestadorDeServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestadorDeServico_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Servico",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrestadorDeServico_UsuarioId",
                schema: "Servico",
                table: "PrestadorDeServico",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrestadorDeServico",
                schema: "Servico");
        }
    }
}
