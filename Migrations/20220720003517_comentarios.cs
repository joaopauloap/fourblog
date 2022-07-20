using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourBlog.Migrations
{
    public partial class comentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_comentario_AspNetUsers_UsuarioId",
                table: "tb_comentario");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "tb_comentario",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostagemId",
                table: "tb_comentario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_comentario_PostagemId",
                table: "tb_comentario",
                column: "PostagemId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_comentario_AspNetUsers_UsuarioId",
                table: "tb_comentario",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_comentario_tb_postagem_PostagemId",
                table: "tb_comentario",
                column: "PostagemId",
                principalTable: "tb_postagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_comentario_AspNetUsers_UsuarioId",
                table: "tb_comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_comentario_tb_postagem_PostagemId",
                table: "tb_comentario");

            migrationBuilder.DropIndex(
                name: "IX_tb_comentario_PostagemId",
                table: "tb_comentario");

            migrationBuilder.DropColumn(
                name: "PostagemId",
                table: "tb_comentario");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "tb_comentario",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_comentario_AspNetUsers_UsuarioId",
                table: "tb_comentario",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
