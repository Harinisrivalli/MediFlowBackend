using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediFlow.Migrations
{
    /// <inheritdoc />
    public partial class RelationShipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlot_doctors_CreateDoctorId",
                table: "AvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreateDoctorId",
                table: "AvailabilitySlot",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "AvailabilitySlot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AvailabilitySlot",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlot_doctors_CreateDoctorId",
                table: "AvailabilitySlot",
                column: "CreateDoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailabilitySlot_doctors_CreateDoctorId",
                table: "AvailabilitySlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AvailabilitySlot");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "AvailabilitySlot",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CreateDoctorId",
                table: "AvailabilitySlot",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailabilitySlot",
                table: "AvailabilitySlot",
                column: "day");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailabilitySlot_doctors_CreateDoctorId",
                table: "AvailabilitySlot",
                column: "CreateDoctorId",
                principalTable: "doctors",
                principalColumn: "Id");
        }
    }
}
