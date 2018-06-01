using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BiblioWeb.Migrations
{
    public partial class CamposObrig_Correcao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sinopse",
                table: "Livro",
                maxLength: 3000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.DropColumn(
                name: "Editora",
                table: "Livro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Editora",
                table: "Livro");

            migrationBuilder.AlterColumn<string>(
                name: "Sinopse",
                table: "Livro",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3000);
        }
    }
}
