using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusLearn_6.Migrations
{
    /// <inheritdoc />
    public partial class trainerNameinpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainer_Payment_PaymentId",
                table: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_Trainer_PaymentId",
                table: "Trainer");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Trainer");

            migrationBuilder.AddColumn<string>(
                name: "TrainerName",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TrainerId",
                table: "Payment",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Trainer_TrainerId",
                table: "Payment",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Trainer_TrainerId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_TrainerId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "TrainerName",
                table: "Payment");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Trainer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_PaymentId",
                table: "Trainer",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainer_Payment_PaymentId",
                table: "Trainer",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");
        }
    }
}
