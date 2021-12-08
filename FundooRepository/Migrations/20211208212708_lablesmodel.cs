using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepository.Migrations
{
    public partial class lablesmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorModel_Notes_noteModelNoteId",
                table: "CollaboratorModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorModel",
                table: "CollaboratorModel");

            migrationBuilder.RenameTable(
                name: "CollaboratorModel",
                newName: "Collaborators");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorModel_noteModelNoteId",
                table: "Collaborators",
                newName: "IX_Collaborators_noteModelNoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collaborators",
                table: "Collaborators",
                column: "CollaboratorID");

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    LabelName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Labels_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Labels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NoteId",
                table: "Labels",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_UserId",
                table: "Labels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Notes_noteModelNoteId",
                table: "Collaborators",
                column: "noteModelNoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Notes_noteModelNoteId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collaborators",
                table: "Collaborators");

            migrationBuilder.RenameTable(
                name: "Collaborators",
                newName: "CollaboratorModel");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_noteModelNoteId",
                table: "CollaboratorModel",
                newName: "IX_CollaboratorModel_noteModelNoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorModel",
                table: "CollaboratorModel",
                column: "CollaboratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorModel_Notes_noteModelNoteId",
                table: "CollaboratorModel",
                column: "noteModelNoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
