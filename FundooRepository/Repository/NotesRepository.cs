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
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext userContext;
        public NotesRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }
        public IConfiguration Configuration { get; }
        public string CreateNote(NoteModel noteData)
        {
            try
            {
                //var validUser = this.userContext.Users.Where(x => x.Email == noteData.Email).FirstOrDefault();
                //if (validUser != null)
                {
                    if (noteData.Description != null && noteData.Title != null)
                    {
                        this.userContext.Add(noteData);
                        this.userContext.SaveChanges();
                        return "Successfully created note";
                    }
                }
                return "Note Creation Failed ";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateNote(NoteModel model)
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
                        this.userContext.SaveChanges();
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

        public bool ArchiveUnArchive(string email, int NoteId)
        {
            //string msg;
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
        public bool PinUnpin(string email, int NoteId)
        {
            //string msg;
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
        public bool TrashUntrash(string email, int NoteId)
        {
            //string msg;
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
        public IEnumerable<NoteModel> GetArchivedNoteList(string email)
        {
            var result = this.userContext.Notes.Where(option => option.Email == email && option.IsArchive == true);
            return result;
        }
        public IEnumerable<NoteModel> GetTrashedNoteList(string email)
        {
            var result = this.userContext.Notes.Where(option => option.Email == email && option.IsTrash == true);
            return result;
        }


        public async Task<string> EditColor(NoteModel noteData)
        {
            try
            {
                var validNoteId = this.userContext.Notes.Where(x => x.NoteId == noteData.NoteId).FirstOrDefault();
                if (validNoteId != null)
                {
                    validNoteId.Colour = noteData.Colour;
                    this.userContext.Update(validNoteId);
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


        //public bool DeleteNote(string email, int id)
        //{
        //    var note = userContext.Notes.FirstOrDefault(option => option.Email == email && option.NoteId == id);
        //    if (note != null)
        //    {
        //        userContext.Notes.Remove(note);
        //        var result = userContext.SaveChanges();
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}








    }
}
