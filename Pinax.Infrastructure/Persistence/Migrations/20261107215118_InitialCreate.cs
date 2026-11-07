using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pinax.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progetti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codice = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Colore = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Archiviato = table.Column<bool>(type: "bit", nullable: false),
                    OpInsert = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpUpdate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OpInsert = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpUpdate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ordine = table.Column<int>(type: "int", nullable: false),
                    ProgettoId = table.Column<int>(type: "int", nullable: false),
                    OpInsert = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpUpdate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stati_Progetti_ProgettoId",
                        column: x => x.ProgettoId,
                        principalTable: "Progetti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attivita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ProgettoId = table.Column<int>(type: "int", nullable: false),
                    StatoId = table.Column<int>(type: "int", nullable: false),
                    Ordine = table.Column<int>(type: "int", nullable: false),
                    Scadenza = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priorita = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    OpInsert = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpUpdate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attivita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attivita_Progetti_ProgettoId",
                        column: x => x.ProgettoId,
                        principalTable: "Progetti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attivita_Stati_StatoId",
                        column: x => x.StatoId,
                        principalTable: "Stati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssegnazioniAttivita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttivitaId = table.Column<int>(type: "int", nullable: false),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    OpInsert = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DtInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpUpdate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpDelete = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtDelete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssegnazioniAttivita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssegnazioniAttivita_Attivita_AttivitaId",
                        column: x => x.AttivitaId,
                        principalTable: "Attivita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssegnazioniAttivita_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssegnazioniAttivita_AttivitaId_UtenteId",
                table: "AssegnazioniAttivita",
                columns: new[] { "AttivitaId", "UtenteId" },
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_AssegnazioniAttivita_UtenteId",
                table: "AssegnazioniAttivita",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Attivita_ProgettoId_Numero",
                table: "Attivita",
                columns: new[] { "ProgettoId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attivita_StatoId_Ordine",
                table: "Attivita",
                columns: new[] { "StatoId", "Ordine" });

            migrationBuilder.CreateIndex(
                name: "IX_Progetti_Codice",
                table: "Progetti",
                column: "Codice",
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Stati_ProgettoId_Nome",
                table: "Stati",
                columns: new[] { "ProgettoId", "Nome" },
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Email",
                table: "Utenti",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_Username",
                table: "Utenti",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssegnazioniAttivita");

            migrationBuilder.DropTable(
                name: "Attivita");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Stati");

            migrationBuilder.DropTable(
                name: "Progetti");
        }
    }
}
