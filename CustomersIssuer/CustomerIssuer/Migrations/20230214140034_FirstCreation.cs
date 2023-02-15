using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerIssuer.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionCardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TransactionApprovalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
