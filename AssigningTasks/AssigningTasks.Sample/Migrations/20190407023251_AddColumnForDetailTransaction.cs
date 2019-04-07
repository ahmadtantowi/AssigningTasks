using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssigningTasks.Sample.Migrations
{
    public partial class AddColumnForDetailTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Algorithm",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AlgorithmExecutionTime",
                table: "Transactions",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Candidates",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Algorithm",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AlgorithmExecutionTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Candidates",
                table: "Transactions");
        }
    }
}
