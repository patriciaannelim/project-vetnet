using Microsoft.EntityFrameworkCore.Migrations;

namespace Vet.Net.Migrations
{
    public partial class FinalProfileandBooklet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booklets_Profiles_ProfileID",
                table: "Booklets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booklets",
                table: "Booklets");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileID",
                table: "Booklets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookletID",
                table: "Booklets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booklets",
                table: "Booklets",
                column: "BookletID");

            migrationBuilder.CreateIndex(
                name: "IX_Booklets_ProfileID",
                table: "Booklets",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booklets_Profiles_ProfileID",
                table: "Booklets",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booklets_Profiles_ProfileID",
                table: "Booklets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booklets",
                table: "Booklets");

            migrationBuilder.DropIndex(
                name: "IX_Booklets_ProfileID",
                table: "Booklets");

            migrationBuilder.DropColumn(
                name: "BookletID",
                table: "Booklets");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileID",
                table: "Booklets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booklets",
                table: "Booklets",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booklets_Profiles_ProfileID",
                table: "Booklets",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
