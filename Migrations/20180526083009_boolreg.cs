using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rmc.Migrations
{
    public partial class boolreg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "serviceRefSafeHouse",
                table: "registration",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "serviceRefLegal",
                table: "registration",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "servicePsycho",
                table: "registration",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "serviceMedical",
                table: "registration",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "serviceRefSafeHouse",
                table: "registration",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "serviceRefLegal",
                table: "registration",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "servicePsycho",
                table: "registration",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "serviceMedical",
                table: "registration",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
