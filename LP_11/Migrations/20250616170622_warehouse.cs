using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LP_11.Migrations
{
    /// <inheritdoc />
    public partial class warehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "WarehouseProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseProducts_Warehouses_WarehouseId",
                table: "WarehouseProducts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseProducts_Warehouses_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseProducts");
        }
    }
}
