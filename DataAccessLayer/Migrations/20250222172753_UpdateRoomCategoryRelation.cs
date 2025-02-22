using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    

            migrationBuilder.RenameTable(
                name: "RoomFeatures",
                newName: "tbl_RoomFeatures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_RoomFeatures",
                table: "tbl_RoomFeatures",
                column: "FeatureId");

            migrationBuilder.CreateTable(
                name: "RoomRoomCategory",
                columns: table => new
                {
                    RoomCategoriesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRoomCategory", x => new { x.RoomCategoriesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_RoomRoomCategory_tbl_RoomCategory_RoomCategoriesId",
                        column: x => x.RoomCategoriesId,
                        principalTable: "tbl_RoomCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomRoomCategory_tbl_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "tbl_Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RoomFeatures_RoomId",
                table: "tbl_RoomFeatures",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRoomCategory_RoomsId",
                table: "RoomRoomCategory",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_RoomFeatures_tbl_Rooms_RoomId",
                table: "tbl_RoomFeatures",
                column: "RoomId",
                principalTable: "tbl_Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_RoomFeatures_tbl_Rooms_RoomId",
                table: "tbl_RoomFeatures");

            migrationBuilder.DropTable(
                name: "RoomRoomCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_RoomFeatures",
                table: "tbl_RoomFeatures");

            migrationBuilder.DropIndex(
                name: "IX_tbl_RoomFeatures_RoomId",
                table: "tbl_RoomFeatures");

        }
    }
}
