using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobra.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SteamId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<int>(type: "INTEGER", nullable: false),
                    Wallet = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractPlays = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionPlays = table.Column<int>(type: "INTEGER", nullable: false),
                    Trophies = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
