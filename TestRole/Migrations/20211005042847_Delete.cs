using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRole.Migrations
{
    public partial class Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_StudentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Accounts_TrainerId",
                table: "Trainers");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_StudentId",
                table: "Students",
                column: "StudentId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classrooms",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Accounts_TrainerId",
                table: "Trainers",
                column: "TrainerId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_StudentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Accounts_TrainerId",
                table: "Trainers");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_StudentId",
                table: "Students",
                column: "StudentId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classrooms",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Accounts_TrainerId",
                table: "Trainers",
                column: "TrainerId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
