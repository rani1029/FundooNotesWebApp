using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepository.Migrations
{
    public partial class labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Notes_noteModelNoteId",
                table: "Collaborators");

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

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorModel",
                table: "CollaboratorModel",
                column: "CollaboratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorModel_Notes_noteModelNoteId",
                table: "CollaboratorModel",
                column: "noteModelNoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorModel_Notes_noteModelNoteId",
                table: "CollaboratorModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorModel",
                table: "CollaboratorModel");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Notes_noteModelNoteId",
                table: "Collaborators",
                column: "noteModelNoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
