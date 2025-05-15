using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Курсовая.Migrations
{
    /// <inheritdoc />
    public partial class NewEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastBy",
                table: "Stamps",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBy",
                table: "Stamps");
        }
    }
}
