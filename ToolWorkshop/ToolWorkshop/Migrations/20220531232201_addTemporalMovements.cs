using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolWorkshop.Migrations
{
    public partial class addTemporalMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Details_Temporal_movements_Temporal_MovementId",
                table: "Movement_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Temporal_movements_AspNetUsers_UserId",
                table: "Temporal_movements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temporal_movements",
                table: "Temporal_movements");

            migrationBuilder.RenameTable(
                name: "Temporal_movements",
                newName: "Temporal_Movements");

            migrationBuilder.RenameIndex(
                name: "IX_Temporal_movements_UserId",
                table: "Temporal_Movements",
                newName: "IX_Temporal_Movements_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Temporal_Movements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Temporal_Movements",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ToolId",
                table: "Temporal_Movements",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temporal_Movements",
                table: "Temporal_Movements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Temporal_Movements_ToolId",
                table: "Temporal_Movements",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Details_Temporal_Movements_Temporal_MovementId",
                table: "Movement_Details",
                column: "Temporal_MovementId",
                principalTable: "Temporal_Movements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Temporal_Movements_AspNetUsers_UserId",
                table: "Temporal_Movements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Temporal_Movements_Tools_ToolId",
                table: "Temporal_Movements",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movement_Details_Temporal_Movements_Temporal_MovementId",
                table: "Movement_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Temporal_Movements_AspNetUsers_UserId",
                table: "Temporal_Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Temporal_Movements_Tools_ToolId",
                table: "Temporal_Movements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temporal_Movements",
                table: "Temporal_Movements");

            migrationBuilder.DropIndex(
                name: "IX_Temporal_Movements_ToolId",
                table: "Temporal_Movements");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Temporal_Movements");

            migrationBuilder.DropColumn(
                name: "ToolId",
                table: "Temporal_Movements");

            migrationBuilder.RenameTable(
                name: "Temporal_Movements",
                newName: "Temporal_movements");

            migrationBuilder.RenameIndex(
                name: "IX_Temporal_Movements_UserId",
                table: "Temporal_movements",
                newName: "IX_Temporal_movements_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Temporal_movements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temporal_movements",
                table: "Temporal_movements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movement_Details_Temporal_movements_Temporal_MovementId",
                table: "Movement_Details",
                column: "Temporal_MovementId",
                principalTable: "Temporal_movements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Temporal_movements_AspNetUsers_UserId",
                table: "Temporal_movements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
