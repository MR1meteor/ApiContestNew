using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContestNew.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAnimalVisitedLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalAnimalVisitedLocation");

            migrationBuilder.AddColumn<long>(
                name: "AnimalId",
                table: "AnimalVisitedLocations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalVisitedLocations_AnimalId",
                table: "AnimalVisitedLocations",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalVisitedLocations_Animals_AnimalId",
                table: "AnimalVisitedLocations",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalVisitedLocations_Animals_AnimalId",
                table: "AnimalVisitedLocations");

            migrationBuilder.DropIndex(
                name: "IX_AnimalVisitedLocations_AnimalId",
                table: "AnimalVisitedLocations");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "AnimalVisitedLocations");

            migrationBuilder.CreateTable(
                name: "AnimalAnimalVisitedLocation",
                columns: table => new
                {
                    AnimalsId = table.Column<long>(type: "bigint", nullable: false),
                    VisitedLocationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalAnimalVisitedLocation", x => new { x.AnimalsId, x.VisitedLocationsId });
                    table.ForeignKey(
                        name: "FK_AnimalAnimalVisitedLocation_AnimalVisitedLocations_VisitedL~",
                        column: x => x.VisitedLocationsId,
                        principalTable: "AnimalVisitedLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalAnimalVisitedLocation_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalAnimalVisitedLocation_VisitedLocationsId",
                table: "AnimalAnimalVisitedLocation",
                column: "VisitedLocationsId");
        }
    }
}
