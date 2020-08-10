using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgramAcad.Infra.Data.Data.Migrations.ProgramAcadDb
{
    public partial class ProgramAcadDbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_LINGUAGEM_PROGRAMACAO",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(maxLength: 20, nullable: true),
                    descricao = table.Column<string>(maxLength: 200, nullable: true),
                    num_tipo_compilador = table.Column<int>(nullable: false),
                    api_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LINGUAGEM_PROGRAMACAO", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_NIVEL_DIFICULDADE",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(maxLength: 50, nullable: true),
                    nivel = table.Column<int>(nullable: false),
                    pontos_a_receber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NIVEL_DIFICULDADE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    nome_completo = table.Column<string>(maxLength: 256, nullable: true),
                    nickname = table.Column<string>(maxLength: 32, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    cpf = table.Column<string>(maxLength: 15, nullable: true),
                    cep = table.Column<string>(maxLength: 12, nullable: true),
                    sexo = table.Column<string>(maxLength: 1, nullable: true),
                    role = table.Column<string>(maxLength: 50, nullable: true, defaultValue: "ESTUDANTE"),
                    data_nascimento = table.Column<DateTime>(nullable: true),
                    data_criacao = table.Column<DateTime>(nullable: false),
                    bl_ativo = table.Column<bool>(nullable: false),
                    bl_usuario_externo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_TURMA",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    id_instrutor = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(maxLength: 75, nullable: true),
                    capacidade_alunos = table.Column<int>(nullable: false),
                    url_imagem_turma = table.Column<string>(maxLength: 500, nullable: true),
                    data_criacao = table.Column<DateTime>(nullable: false),
                    data_termino = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TURMA", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_TURMA_TB_USUARIO_id_instrutor",
                        column: x => x.id_instrutor,
                        principalTable: "TB_USUARIO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALGORITMO",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    id_turma = table.Column<Guid>(nullable: false),
                    data_criacao = table.Column<DateTime>(nullable: false),
                    titulo = table.Column<string>(maxLength: 100, nullable: true),
                    html_descricao = table.Column<string>(nullable: true),
                    id_nivel_dificuldade = table.Column<int>(nullable: false),
                    bl_ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALGORITMO", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_ALGORITMO_TB_NIVEL_DIFICULDADE_id_nivel_dificuldade",
                        column: x => x.id_nivel_dificuldade,
                        principalTable: "TB_NIVEL_DIFICULDADE",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALGORITMO_TB_TURMA_id_turma",
                        column: x => x.id_turma,
                        principalTable: "TB_TURMA",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO_PERTENCE_TURMA",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(nullable: false),
                    id_turma = table.Column<Guid>(nullable: false),
                    pontos_usuario = table.Column<int>(nullable: false),
                    data_ingresso = table.Column<DateTime>(nullable: false),
                    aceito = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO_PERTENCE_TURMA", x => new { x.id_usuario, x.id_turma });
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_PERTENCE_TURMA_TB_TURMA_id_turma",
                        column: x => x.id_turma,
                        principalTable: "TB_TURMA",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_PERTENCE_TURMA_TB_USUARIO_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "TB_USUARIO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALGORITMO_LINGUAGEM_DISPONIVEL",
                columns: table => new
                {
                    id_algoritmo = table.Column<Guid>(nullable: false),
                    id_linguagem_programacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALGORITMO_LINGUAGEM_DISPONIVEL", x => new { x.id_algoritmo, x.id_linguagem_programacao });
                    table.ForeignKey(
                        name: "FK_TB_ALGORITMO_LINGUAGEM_DISPONIVEL_TB_ALGORITMO_id_algoritmo",
                        column: x => x.id_algoritmo,
                        principalTable: "TB_ALGORITMO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALGORITMO_LINGUAGEM_DISPONIVEL_TB_LINGUAGEM_PROGRAMACAO_id_linguagem_programacao",
                        column: x => x.id_linguagem_programacao,
                        principalTable: "TB_LINGUAGEM_PROGRAMACAO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_CASO_TESTE",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    entrada_esperada = table.Column<string>(nullable: true),
                    saida_esperada = table.Column<string>(nullable: true),
                    tempo_maximo_execucao = table.Column<int>(nullable: false),
                    id_algoritmo = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CASO_TESTE", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_CASO_TESTE_TB_ALGORITMO_id_algoritmo",
                        column: x => x.id_algoritmo,
                        principalTable: "TB_ALGORITMO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO_RESOLVE_ALGORITMO",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    id_usuario = table.Column<Guid>(nullable: false),
                    id_algoritmo = table.Column<Guid>(nullable: false),
                    id_linguagem_usada = table.Column<int>(nullable: false),
                    data_conclusao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO_RESOLVE_ALGORITMO", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_RESOLVE_ALGORITMO_TB_ALGORITMO_id_algoritmo",
                        column: x => x.id_algoritmo,
                        principalTable: "TB_ALGORITMO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_RESOLVE_ALGORITMO_TB_LINGUAGEM_PROGRAMACAO_id_linguagem_usada",
                        column: x => x.id_linguagem_usada,
                        principalTable: "TB_LINGUAGEM_PROGRAMACAO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_RESOLVE_ALGORITMO_TB_USUARIO_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "TB_USUARIO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TESTE_PRIMEIRA_EXECUCAO",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    id_caso_teste = table.Column<Guid>(nullable: false),
                    id_usuario = table.Column<Guid>(nullable: false),
                    id_linguagem_programacao = table.Column<int>(nullable: false),
                    sucesso = table.Column<bool>(nullable: false),
                    tempo_execucao = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TESTE_PRIMEIRA_EXECUCAO", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_TESTE_PRIMEIRA_EXECUCAO_TB_CASO_TESTE_id_caso_teste",
                        column: x => x.id_caso_teste,
                        principalTable: "TB_CASO_TESTE",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TESTE_PRIMEIRA_EXECUCAO_TB_LINGUAGEM_PROGRAMACAO_id_linguagem_programacao",
                        column: x => x.id_linguagem_programacao,
                        principalTable: "TB_LINGUAGEM_PROGRAMACAO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TESTE_PRIMEIRA_EXECUCAO_TB_USUARIO_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "TB_USUARIO",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALGORITMO_id_nivel_dificuldade",
                table: "TB_ALGORITMO",
                column: "id_nivel_dificuldade");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALGORITMO_id_turma",
                table: "TB_ALGORITMO",
                column: "id_turma");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALGORITMO_LINGUAGEM_DISPONIVEL_id_linguagem_programacao",
                table: "TB_ALGORITMO_LINGUAGEM_DISPONIVEL",
                column: "id_linguagem_programacao");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CASO_TESTE_id_algoritmo",
                table: "TB_CASO_TESTE",
                column: "id_algoritmo");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TESTE_PRIMEIRA_EXECUCAO_id_caso_teste",
                table: "TB_TESTE_PRIMEIRA_EXECUCAO",
                column: "id_caso_teste");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TESTE_PRIMEIRA_EXECUCAO_id_linguagem_programacao",
                table: "TB_TESTE_PRIMEIRA_EXECUCAO",
                column: "id_linguagem_programacao");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TESTE_PRIMEIRA_EXECUCAO_id_usuario",
                table: "TB_TESTE_PRIMEIRA_EXECUCAO",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TURMA_id_instrutor",
                table: "TB_TURMA",
                column: "id_instrutor");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_PERTENCE_TURMA_id_turma",
                table: "TB_USUARIO_PERTENCE_TURMA",
                column: "id_turma");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_RESOLVE_ALGORITMO_id_algoritmo",
                table: "TB_USUARIO_RESOLVE_ALGORITMO",
                column: "id_algoritmo");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_RESOLVE_ALGORITMO_id_linguagem_usada",
                table: "TB_USUARIO_RESOLVE_ALGORITMO",
                column: "id_linguagem_usada");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_RESOLVE_ALGORITMO_id_usuario",
                table: "TB_USUARIO_RESOLVE_ALGORITMO",
                column: "id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ALGORITMO_LINGUAGEM_DISPONIVEL");

            migrationBuilder.DropTable(
                name: "TB_TESTE_PRIMEIRA_EXECUCAO");

            migrationBuilder.DropTable(
                name: "TB_USUARIO_PERTENCE_TURMA");

            migrationBuilder.DropTable(
                name: "TB_USUARIO_RESOLVE_ALGORITMO");

            migrationBuilder.DropTable(
                name: "TB_CASO_TESTE");

            migrationBuilder.DropTable(
                name: "TB_LINGUAGEM_PROGRAMACAO");

            migrationBuilder.DropTable(
                name: "TB_ALGORITMO");

            migrationBuilder.DropTable(
                name: "TB_NIVEL_DIFICULDADE");

            migrationBuilder.DropTable(
                name: "TB_TURMA");

            migrationBuilder.DropTable(
                name: "TB_USUARIO");
        }
    }
}
