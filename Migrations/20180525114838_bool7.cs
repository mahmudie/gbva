using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rmc.Migrations
{
    public partial class bool7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "insertDate",
                table: "Authorization");

            migrationBuilder.DropColumn(
                name: "lastUpdate",
                table: "Authorization");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "Authorization");

            migrationBuilder.AlterColumn<bool>(
                name: "islessthan18",
                table: "Authorization",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isSigned",
                table: "Authorization",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "islessthan18",
                table: "Authorization",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "isSigned",
                table: "Authorization",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "insertDate",
                table: "Authorization",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdate",
                table: "Authorization",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "Authorization",
                maxLength: 50,
                nullable: true);
        }
    }
}
