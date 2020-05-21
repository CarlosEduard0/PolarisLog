using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolarisLog.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    AmbienteId = table.Column<Guid>(nullable: false),
                    NivelId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Origem = table.Column<string>(nullable: true),
                    ArquivadoEm = table.Column<DateTime>(nullable: true),
                    CadastradoEm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Ambientes_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Niveis_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Niveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ambientes",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("e5676e01-88ef-47a2-b8f8-397dab18feb7"), "Produção" },
                    { new Guid("7dae489b-abf7-47da-8c90-7fe2b34cc024"), "Homologação" },
                    { new Guid("03341d8f-2064-4ce9-ad13-a247760ab110"), "Dev" }
                });

            migrationBuilder.InsertData(
                table: "Niveis",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { new Guid("0b84331d-3d3f-4ded-b7c2-35b6e3511ff8"), "Verbose" },
                    { new Guid("f917899a-85d6-4b1a-ad5b-7573522bdd00"), "Debug" },
                    { new Guid("53a2b01f-7ba7-4873-b7ac-4e543396625a"), "Information" },
                    { new Guid("31197b8c-bd8b-43b1-bdc5-8b7e8f078a3e"), "Warning" },
                    { new Guid("da19754d-9deb-48f6-830b-76ce2e69c6c8"), "Error" },
                    { new Guid("240fd2ac-e94f-44f7-a11e-fcf2ca69f57c"), "Fatal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_AmbienteId",
                table: "Logs",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_NivelId",
                table: "Logs",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UsuarioId",
                table: "Logs",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Ambientes");

            migrationBuilder.DropTable(
                name: "Niveis");
        }
    }
}
