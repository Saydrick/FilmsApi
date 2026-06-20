using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsApi.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DureeMinute = table.Column<int>(type: "INTEGER", nullable: true),
                    Realisateur = table.Column<string>(type: "TEXT", nullable: true),
                    Annee = table.Column<int>(type: "INTEGER", nullable: false),
                    TmdbId = table.Column<int>(type: "INTEGER", nullable: true),
                    AfficheUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Synopsis = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<double>(type: "REAL", nullable: true),
                    Genres = table.Column<string>(type: "TEXT", nullable: false),
                    Statut = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NbEpisode = table.Column<int>(type: "INTEGER", nullable: true),
                    NbSaison = table.Column<int>(type: "INTEGER", nullable: true),
                    AnneeDebut = table.Column<int>(type: "INTEGER", nullable: true),
                    AnneeFin = table.Column<int>(type: "INTEGER", nullable: true),
                    EnCours = table.Column<bool>(type: "INTEGER", nullable: false),
                    TmdbId = table.Column<int>(type: "INTEGER", nullable: true),
                    AfficheUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Synopsis = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<double>(type: "REAL", nullable: true),
                    Genres = table.Column<string>(type: "TEXT", nullable: false),
                    Statut = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Series");
        }
    }
}
