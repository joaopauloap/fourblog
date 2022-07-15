using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourBlog.Migrations
{
    public partial class correcaoTbPostagemUsuarioId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_postagem_AspNetUsers_UsuarioId1",
                table: "tb_postagem");

            migrationBuilder.DropIndex(
                name: "IX_tb_postagem_UsuarioId1",
                table: "tb_postagem");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "tb_postagem");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "tb_postagem",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_tb_postagem_UsuarioId",
                table: "tb_postagem",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_postagem_AspNetUsers_UsuarioId",
                table: "tb_postagem",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_postagem_AspNetUsers_UsuarioId",
                table: "tb_postagem");

            migrationBuilder.DropIndex(
                name: "IX_tb_postagem_UsuarioId",
                table: "tb_postagem");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "tb_postagem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "tb_postagem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_postagem_UsuarioId1",
                table: "tb_postagem",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_postagem_AspNetUsers_UsuarioId1",
                table: "tb_postagem",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
