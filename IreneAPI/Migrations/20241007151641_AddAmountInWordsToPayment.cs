using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IreneAPI.Migrations
{
    public partial class AddAmountInWordsToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AmountInWords",
                table: "Payments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountInWords",
                table: "Payments");
        }
    }
}
