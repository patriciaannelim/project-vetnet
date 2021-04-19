using Microsoft.EntityFrameworkCore.Migrations;

namespace Vet.Net.Migrations
{
    public partial class UpdatedReservationFormandAddedMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Animal",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicationID",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MedicationID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MedicationID",
                table: "Reservations",
                column: "MedicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Medications_MedicationID",
                table: "Reservations",
                column: "MedicationID",
                principalTable: "Medications",
                principalColumn: "MedicationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Medications_MedicationID",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_MedicationID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Animal",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "MedicationID",
                table: "Reservations");
        }
    }
}
