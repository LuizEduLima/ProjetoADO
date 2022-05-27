using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADO.Data.Migrations
{
    public partial class Criacao_Bd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cadastrado_em = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    CPF = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(100)", nullable: true),
                    Nascimento = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosCursos",
                columns: table => new
                {
                    Aluno_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Curso_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosCursos", x => x.Aluno_id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cadastrado_em = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cadastrado_em = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true),
                    TotalHoras = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_CategoriaId",
                table: "Cursos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "AlunosCursos");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
