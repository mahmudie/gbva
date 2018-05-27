using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rmc.Migrations
{
    public partial class gbv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rptCode",
                table: "gbvCase");

            migrationBuilder.AlterColumn<string>(
                name: "patientName",
                table: "gbvCase",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "patientName",
                table: "gbvCase",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "rptCode",
                table: "gbvCase",
                maxLength: 50,
                nullable: true);
        }
    }
}
