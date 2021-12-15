using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModel;
using FundooRepository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    /// <summary>
    /// repository class of notes
    /// </summary>
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext userContext;
        public NotesRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// method to create new note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns> string successful or not</returns>
        public string CreateNote(NoteModel noteData)
        {
            try
            {
                if (noteData.Description != null && noteData.Title != null)
                {
                    this.userContext.Add(noteData);
                    this.userContext.SaveChanges();
                    return "Successfully created note";
                }

                return "Note Creation Failed ";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// updates existing note title or description
        /// </summary>
        /// <param name="model"></param>
        /// <returns> note updation status</returns>
        public async Task<string> UpdateNote(NoteModel model)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NoteId == model.NoteId).SingleOrDefault();
                if (exists != null)
                {
                    if (model != null)
                    {
                        exists.Description = model.Description == null ? exists.Description : model.Description;
                        exists.Title = model.Title == null ? exists.Title : model.Title;
                        this.userContext.Notes.Update(exists);
                        await this.userContext.SaveChangesAsync();
                        return "Note Updated Successfully!";
                    }
                }

                return "No such note found Add new Note";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Archives and UnArchives note
        /// </summary>
        /// <param name="email"></param>
        /// <param name="NoteId"></param>
        /// <returns> boolean value</returns>
        public bool ArchiveUnArchive(string email, int NoteId)
        {
            try
            {
                var note = this.userContext.Notes.FirstOrDefault(option => option.Email == email && option.NoteId == NoteId);
                if (note != null)
                {
                    bool doArchiUnArchive = note.IsArchive == true ? note.IsArchive = false : note.IsArchive = true;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// pin and unpin the note 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="NoteId"></param>
        /// <returns> boolean value</returns>
        public bool PinUnpin(string email, int NoteId)
        {
            try
            {
                var note = this.userContext.Notes.FirstOrDefault(option => option.Email == email && option.NoteId == NoteId);
                if (note != null)
                {
                    bool doPinUnpine = note.IsPin == true ? note.IsPin = false : note.IsPin = true;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Trash and Untrash note
        /// </summary>
        /// <param name="email"></param>
        /// <param name="NoteId"></param>
        /// <returns> Boolean value</returns>
        public bool TrashUntrash(string email, int NoteId)
        {
            try
            {
                var note = this.userContext.Notes.FirstOrDefault(option => option.Email == email && option.NoteId == NoteId);
                if (note != null)
                {
                    bool doTrashUntrash = note.IsTrash == true ? note.IsPin = false : note.IsPin = true;
                    this.userContext.Notes.Update(note);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// permanantly deletes the note 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns> boolean value</returns>
        public bool DeleteNote(string email, int id)
        {
            var note = this.userContext.Notes.FirstOrDefault(option => option.Email == email && option.NoteId == id);
            if (note != null)
            {
                this.userContext.Notes.Remove(note);
                var result = this.userContext.SaveChanges();
                if (result == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<NoteModel> GetNote(int Noteid)
        {
            try
            {
                var result = this.userContext.Notes.Where(option => option.NoteId == Noteid && option.IsArchive == false && option.IsPin == false);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// gets all Archived notes
        /// </summary>
        /// <param name="email"></param>
        /// <returns> all archived note </returns>
        public IEnumerable<NoteModel> GetArchivedNoteList(string email)
        {
            try
            {
                var result = this.userContext.Notes.Where(option => option.Email == email && option.IsArchive == true);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// gets all trashed notes
        /// </summary>
        /// <param name="email"></param>
        /// <returns> all trashed notes</returns>
        public IEnumerable<NoteModel> GetTrashedNoteList(string email)
        {
            try
            {
                var result = this.userContext.Notes.Where(option => option.Email == email && option.IsTrash == true);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// changes color of the note
        /// </summary>
        /// <param name="noteData"></param>
        /// <returns>color change successful or not </returns>
        public async Task<string> EditColor(int noteId, string color)
        {
            try
            {
                var validNoteId = this.userContext.Notes.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (validNoteId != null)
                {
                    validNoteId.Colour = color;
                    this.userContext.Notes.Update(validNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Successfully change color";
                }
                return "Unsuccessful to change color";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// adds remind me feature to note
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="remind"></param>
        /// <returns> string value added or not </returns>
        public async Task<string> AddReminder(int notesId, string remind)
        {
            try
            {
                var UserNoteId = this.userContext.Notes.Where(x => x.NoteId == notesId).SingleOrDefault();
                if (UserNoteId != null)
                {
                    UserNoteId.Reminder = remind;
                    this.userContext.Notes.Update(UserNoteId);
                    await this.userContext.SaveChangesAsync();
                    return "Reminder added successfully";
                }
                else
                {
                    return "This note does not exist";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// adds image to note using cloudnary
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="form"></param>
        /// <returns>string value image added or not</returns>
        public async Task<string> AddImage(int noteId, IFormFile form)
        {
            try
            {
                var availNote = this.userContext.Notes.Where(x => x.NoteId == noteId).SingleOrDefault();
                if (availNote != null)
                {
                    var cloudinary = new Cloudinary(
                                                new Account(
                                                "fundoonotesrani",
                                                "316831696271792",
                                                "jUp3Itn31BOO84pjuCs0m1jtSCw"));
                    var addingImage = new ImageUploadParams()
                    {
                        File = new FileDescription(form.FileName, form.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(addingImage);
                    var uploadPath = uploadResult.Url;
                    availNote.Image = uploadPath.ToString();
                    this.userContext.Notes.Update(availNote);
                    await this.userContext.SaveChangesAsync();
                    return "Image added Successfully";
                }
                else
                {
                    return "This note doesn't Exists";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<NoteModel> GetAllNotes(int userId)
        {
            try
            {
                IEnumerable<NoteModel> dataFromAllNotes = (from notes in this.userContext.Notes
                                                           where notes.registerModel.UserId == userId && notes.IsArchive == false &&
                                                           notes.IsTrash == false
                                                           select notes);
                if (dataFromAllNotes != null)
                {
                    return dataFromAllNotes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}











