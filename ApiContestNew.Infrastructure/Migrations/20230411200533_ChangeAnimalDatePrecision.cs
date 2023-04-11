using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContestNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAnimalDatePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeathDateTime",
                table: "Animals",
                type: "timestamp(6) with time zone",
                precision: 6,
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ChippingDateTime",
                table: "Animals",
                type: "timestamp(6) with time zone",
                precision: 6,
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeathDateTime",
                table: "Animals",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp(6) with time zone",
                oldPrecision: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ChippingDateTime",
                table: "Animals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp(6) with time zone",
                oldPrecision: 6);
        }
    }
}
