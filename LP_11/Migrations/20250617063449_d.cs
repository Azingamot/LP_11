using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LP_11.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "INN",
                table: "Clients",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 12,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "INN",
                table: "Clients",
                type: "integer",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
