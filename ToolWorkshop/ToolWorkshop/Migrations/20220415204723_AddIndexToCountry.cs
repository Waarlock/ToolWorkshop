using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolWorkshop.Migrations
{
    public partial class AddIndexToCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_countries_Name",
                table: "countries",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_countries_Name",
                table: "countries");
        }
    }
}
