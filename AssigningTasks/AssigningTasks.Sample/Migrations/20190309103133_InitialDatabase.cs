using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssigningTasks.Sample.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Load = table.Column<int>(nullable: false),
                    IsAssigned = table.Column<bool>(nullable: false),
                    TotalTravel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    TargetId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    LastRequest = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.TargetId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(nullable: false),
                    FromTargetId = table.Column<string>(nullable: true),
                    ToCandidateId = table.Column<string>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    RequestAt = table.Column<DateTime>(nullable: false),
                    AssigneeAt = table.Column<DateTime>(nullable: false),
                    FinishAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Targets_FromTargetId",
                        column: x => x.FromTargetId,
                        principalTable: "Targets",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Candidates_ToCandidateId",
                        column: x => x.ToCandidateId,
                        principalTable: "Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromTargetId",
                table: "Transactions",
                column: "FromTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToCandidateId",
                table: "Transactions",
                column: "ToCandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
