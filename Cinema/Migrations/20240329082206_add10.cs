using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class add10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_BillStatuses_BillStatusId",
                table: "bills");

            migrationBuilder.DropForeignKey(
                name: "FK_bills_users_UserId",
                table: "bills");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_seatStatuses_SeatStatusId",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_seatTypes_SeatTypeId",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seatStatuses",
                table: "seatStatuses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "bills");

            migrationBuilder.RenameTable(
                name: "seatStatuses",
                newName: "seatsStatus");

            migrationBuilder.RenameColumn(
                name: "ExpiredDateTime",
                table: "refreshTokens",
                newName: "ExpiredTime");

            migrationBuilder.RenameColumn(
                name: "HereImage",
                table: "movies",
                newName: "HeroImage");

            migrationBuilder.RenameColumn(
                name: "IsAtice",
                table: "foods",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsAtive",
                table: "cinemas",
                newName: "IsActive");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SeatTypeId",
                table: "seats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "bills",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "bills",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "BillStatusId",
                table: "bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seatsStatus",
                table: "seatsStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_BillStatuses_BillStatusId",
                table: "bills",
                column: "BillStatusId",
                principalTable: "BillStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_users_UserId",
                table: "bills",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seats_seatTypes_SeatTypeId",
                table: "seats",
                column: "SeatTypeId",
                principalTable: "seatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seats_seatsStatus_SeatStatusId",
                table: "seats",
                column: "SeatStatusId",
                principalTable: "seatsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bills_BillStatuses_BillStatusId",
                table: "bills");

            migrationBuilder.DropForeignKey(
                name: "FK_bills_users_UserId",
                table: "bills");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_seatTypes_SeatTypeId",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_seatsStatus_SeatStatusId",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seatsStatus",
                table: "seatsStatus");

            migrationBuilder.RenameTable(
                name: "seatsStatus",
                newName: "seatStatuses");

            migrationBuilder.RenameColumn(
                name: "ExpiredTime",
                table: "refreshTokens",
                newName: "ExpiredDateTime");

            migrationBuilder.RenameColumn(
                name: "HeroImage",
                table: "movies",
                newName: "HereImage");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "foods",
                newName: "IsAtice");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "cinemas",
                newName: "IsAtive");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeatTypeId",
                table: "seats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "bills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "bills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BillStatusId",
                table: "bills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_seatStatuses",
                table: "seatStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bills_BillStatuses_BillStatusId",
                table: "bills",
                column: "BillStatusId",
                principalTable: "BillStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bills_users_UserId",
                table: "bills",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_seats_seatStatuses_SeatStatusId",
                table: "seats",
                column: "SeatStatusId",
                principalTable: "seatStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seats_seatTypes_SeatTypeId",
                table: "seats",
                column: "SeatTypeId",
                principalTable: "seatTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
