using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
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
                    accountNumber = table.Column<string>(name: "_accountNumber", type: "nvarchar(max)", nullable: true),
                    agencyNumber = table.Column<string>(name: "_agencyNumber", type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(name: "_userName", type: "nvarchar(max)", nullable: false),
                    accountType = table.Column<string>(name: "_accountType", type: "nvarchar(max)", nullable: true),
                    idNumber = table.Column<string>(name: "_idNumber", type: "nvarchar(450)", nullable: true),
                    isActive = table.Column<bool>(name: "_isActive", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients__idNumber",
                table: "Clients",
                column: "_idNumber",
                unique: true,
                filter: "[_idNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
