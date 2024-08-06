using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cobra.Server.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTargets",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    WeaponToken = table.Column<int>(type: "INTEGER", nullable: false),
                    OutfitToken = table.Column<int>(type: "INTEGER", nullable: false),
                    AmmoType = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialSituation = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    DisplayId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    LevelIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckpointIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    Difficulty = table.Column<int>(type: "INTEGER", nullable: false),
                    ExitId = table.Column<int>(type: "INTEGER", nullable: false),
                    WeaponToken = table.Column<int>(type: "INTEGER", nullable: false),
                    OutfitToken = table.Column<int>(type: "INTEGER", nullable: false),
                    Target1Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    Target2Id = table.Column<uint>(type: "INTEGER", nullable: true),
                    Target3Id = table.Column<uint>(type: "INTEGER", nullable: true),
                    Restrictions = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTargets_Target1Id",
                        column: x => x.Target1Id,
                        principalTable: "ContractTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTargets_Target2Id",
                        column: x => x.Target2Id,
                        principalTable: "ContractTargets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTargets_Target3Id",
                        column: x => x.Target3Id,
                        principalTable: "ContractTargets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoresSniper",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresSniper", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ScoresSniper_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoresStory",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false),
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresStory", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_ScoresStory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoresTutorial",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresTutorial", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ScoresTutorial_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFriends",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SteamId = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriends", x => new { x.UserId, x.SteamId });
                    table.ForeignKey(
                        name: "FK_UserFriends_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserContracts",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    ContractId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Queued = table.Column<bool>(type: "INTEGER", nullable: false),
                    Plays = table.Column<int>(type: "INTEGER", nullable: true),
                    Score = table.Column<int>(type: "INTEGER", nullable: true),
                    Liked = table.Column<bool>(type: "INTEGER", nullable: true),
                    LastPlayedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContracts", x => new { x.UserId, x.ContractId });
                    table.ForeignKey(
                        name: "FK_UserContracts_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserContracts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DisplayId",
                table: "Contracts",
                column: "DisplayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Target1Id",
                table: "Contracts",
                column: "Target1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Target2Id",
                table: "Contracts",
                column: "Target2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Target3Id",
                table: "Contracts",
                column: "Target3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                table: "Contracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTargets_Name_WeaponToken_OutfitToken_AmmoType_SpecialSituation",
                table: "ContractTargets",
                columns: ["Name", "WeaponToken", "OutfitToken", "AmmoType", "SpecialSituation"],
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoresStory_UserId",
                table: "ScoresStory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContracts_ContractId",
                table: "UserContracts",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContracts_UserId",
                table: "UserContracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContracts_UserId_Queued",
                table: "UserContracts",
                columns: ["UserId", "Queued"]);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Country",
                table: "Users",
                column: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoresSniper");

            migrationBuilder.DropTable(
                name: "ScoresStory");

            migrationBuilder.DropTable(
                name: "ScoresTutorial");

            migrationBuilder.DropTable(
                name: "UserContracts");

            migrationBuilder.DropTable(
                name: "UserFriends");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractTargets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
