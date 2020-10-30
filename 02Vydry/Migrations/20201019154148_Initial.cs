using Microsoft.EntityFrameworkCore.Migrations;

namespace _02Vydry.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => new { x.Name, x.LocationId });
                    table.ForeignKey(
                        name: "FK_Places_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vydras",
                columns: table => new
                {
                    TattooID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MotherId = table.Column<int>(nullable: true),
                    PlaceName = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vydras", x => x.TattooID);
                    table.ForeignKey(
                        name: "FK_Vydras_Vydras_MotherId",
                        column: x => x.MotherId,
                        principalTable: "Vydras",
                        principalColumn: "TattooID");
                    table.ForeignKey(
                        name: "FK_Vydras_Places_PlaceName_LocationId",
                        columns: x => new { x.PlaceName, x.LocationId },
                        principalTable: "Places",
                        principalColumns: new[] { "Name", "LocationId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Area", "Name" },
                values: new object[] { 111, 33233, "NP Šumava" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Area", "Name" },
                values: new object[] { 128, 13165, "CHKO Jizerské hory" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationID", "Area", "Name" },
                values: new object[] { 666, 15432, "CHKO Čeký Les" });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Name", "LocationId" },
                values: new object[,]
                {
                    { "U Studánky", 111 },
                    { "U Buku", 111 },
                    { "Černé Jezero", 128 },
                    { "U Studánky", 128 },
                    { "Na Čihadlech", 128 },
                    { "U Studánky", 666 },
                    { "Český Pařez", 666 }
                });

            migrationBuilder.InsertData(
                table: "Vydras",
                columns: new[] { "TattooID", "Color", "LocationId", "MotherId", "Name", "PlaceName" },
                values: new object[] { 1, "hnědá jako hodně", 111, null, "Velká Máti", "U Studánky" });

            migrationBuilder.InsertData(
                table: "Vydras",
                columns: new[] { "TattooID", "Color", "LocationId", "MotherId", "Name", "PlaceName" },
                values: new object[] { 2, "Hnědá taky", 111, 1, "První Dcera", "U Studánky" });

            migrationBuilder.InsertData(
                table: "Vydras",
                columns: new[] { "TattooID", "Color", "LocationId", "MotherId", "Name", "PlaceName" },
                values: new object[] { 3, "Hnědá trochu", 128, 1, "ZBloudilka", "Černé Jezero" });

            migrationBuilder.CreateIndex(
                name: "IX_Places_LocationId",
                table: "Places",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vydras_MotherId",
                table: "Vydras",
                column: "MotherId");

            migrationBuilder.CreateIndex(
                name: "IX_Vydras_PlaceName_LocationId",
                table: "Vydras",
                columns: new[] { "PlaceName", "LocationId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vydras");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
