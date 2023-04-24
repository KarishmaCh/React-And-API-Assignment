using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatterWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jurisdictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attorneys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JurisdictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attorneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attorneys_Jurisdictions_JurisdictionId",
                        column: x => x.JurisdictionId,
                        principalTable: "Jurisdictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JurisdictionId = table.Column<int>(type: "int", nullable: false),
                    BillingAttorneyId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleAttorneyId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matters_Attorneys_BillingAttorneyId",
                        column: x => x.BillingAttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matters_Attorneys_ResponsibleAttorneyId",
                        column: x => x.ResponsibleAttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matters_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matters_Jurisdictions_JurisdictionId",
                        column: x => x.JurisdictionId,
                        principalTable: "Jurisdictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HoursWorked = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MatterId = table.Column<int>(type: "int", nullable: false),
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Matters_MatterId",
                        column: x => x.MatterId,
                        principalTable: "Matters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attorneys_JurisdictionId",
                table: "Attorneys",
                column: "JurisdictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AttorneyId",
                table: "Invoices",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MatterId",
                table: "Invoices",
                column: "MatterId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_BillingAttorneyId",
                table: "Matters",
                column: "BillingAttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_ClientId",
                table: "Matters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_JurisdictionId",
                table: "Matters",
                column: "JurisdictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_ResponsibleAttorneyId",
                table: "Matters",
                column: "ResponsibleAttorneyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Matters");

            migrationBuilder.DropTable(
                name: "Attorneys");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Jurisdictions");
        }
    }
}
