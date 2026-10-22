using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Users_UserId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "Membership_Type",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Returned",
                table: "User_Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Returned_Date",
                table: "User_Book",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Memberships",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Fines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Fines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "User_BookId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_User_BookId",
                table: "Fines",
                column: "User_BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_User_Book_User_BookId",
                table: "Fines",
                column: "User_BookId",
                principalTable: "User_Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Users_UserId",
                table: "Fines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_User_Book_User_BookId",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Users_UserId",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_User_BookId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Is_Returned",
                table: "User_Book");

            migrationBuilder.DropColumn(
                name: "Returned_Date",
                table: "User_Book");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "User_BookId",
                table: "Fines");

            migrationBuilder.AddColumn<string>(
                name: "Membership_Type",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Memberships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Users_UserId",
                table: "Fines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
