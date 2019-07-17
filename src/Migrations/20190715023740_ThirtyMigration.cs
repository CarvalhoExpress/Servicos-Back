using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class ThirtyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Servico",
                schema: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrestadorDeServico_ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrestadorDeServico_Servico_ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico",
                column: "ServicoId",
                principalSchema: "Servico",
                principalTable: "Servico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrestadorDeServico_Servico_ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico");

            migrationBuilder.DropTable(
                name: "Servico",
                schema: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_PrestadorDeServico_ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                schema: "Servico",
                table: "PrestadorDeServico");
        }
    }
}
