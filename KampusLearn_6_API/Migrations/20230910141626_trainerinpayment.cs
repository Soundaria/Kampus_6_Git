using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusLearn_6.Migrations
{
    /// <inheritdoc />
    public partial class trainerinpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCourse_Trainer_TrainerId",
                table: "CandidateCourse");

            migrationBuilder.DropIndex(
                name: "IX_CandidateCourse_TrainerId",
                table: "CandidateCourse");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "CandidateCourse");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Payment",
                type: "int",
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
                name: "TrainerId",
                table: "Payment");

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "CandidateCourse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourse_TrainerId",
                table: "CandidateCourse",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCourse_Trainer_TrainerId",
                table: "CandidateCourse",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "TrainerId");
        }
    }
}
