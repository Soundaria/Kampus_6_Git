using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusLearn_6.Migrations
{
    /// <inheritdoc />
    public partial class trainerIdinpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainerName",
                table: "Payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainerName",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
