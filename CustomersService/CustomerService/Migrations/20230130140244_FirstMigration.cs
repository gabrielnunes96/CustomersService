using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    cardDueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardCVC = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agencyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalLimit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currentLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isBlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    accountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agencyNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    accountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
