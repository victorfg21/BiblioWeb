using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiblioWeb.Migrations
{
    public partial class AlteraImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFotoCapa",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "IdFotoSumario",
                table: "Livro");

            migrationBuilder.AddColumn<int>(
                name: "FotoCapaId",
                table: "Livro",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Imagem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ContentType = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_FotoCapaId",
                table: "Livro",
                column: "FotoCapaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Imagem_FotoCapaId",
                table: "Livro",
                column: "FotoCapaId",
                principalTable: "Imagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Imagem_FotoCapaId",
                table: "Livro");

            migrationBuilder.DropTable(
                name: "Imagem");

            migrationBuilder.DropIndex(
                name: "IX_Livro_FotoCapaId",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "FotoCapaId",
                table: "Livro");

            migrationBuilder.AddColumn<int>(
                name: "IdFotoCapa",
                table: "Livro",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdFotoSumario",
                table: "Livro",
                nullable: false,
                defaultValue: 0);
        }
    }
}
