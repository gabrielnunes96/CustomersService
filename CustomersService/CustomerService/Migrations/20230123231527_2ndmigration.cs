using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class _2ndmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(name: "_userName", type: "nvarchar(30)", maxLength: 30, nullable: false),
                    accountType = table.Column<string>(name: "_accountType", type: "nvarchar(max)", nullable: false),
                    idNumber = table.Column<string>(name: "_idNumber", type: "nvarchar(max)", nullable: false),
                    agencyNumber = table.Column<string>(name: "_agencyNumber", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    accountNumber = table.Column<string>(name: "_accountNumber", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    isActive = table.Column<bool>(name: "_isActive", type: "bit", nullable: false)
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
                name: "Clients");
        }
    }
}
