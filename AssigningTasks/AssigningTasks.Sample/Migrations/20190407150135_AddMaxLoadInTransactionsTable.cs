using Microsoft.EntityFrameworkCore.Migrations;

namespace AssigningTasks.Sample.Migrations
{
    public partial class AddMaxLoadInTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxLoad",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLoad",
                table: "Transactions");
        }
    }
}
