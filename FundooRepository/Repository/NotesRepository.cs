using FundooModel;
using FundooRepository.Context;
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
                var validUser = this.userContext.Users.Where(x => x.Email == noteData.UserEmail).FirstOrDefault();
                if (validUser != null)
                {
                    if (noteData.MyNote != null && noteData.Title != null)
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
        public string UpdateNotes(NoteModel model)
        {
            try
            {
                var exists = this.userContext.Notes.Where(x => x.NoteId == model.NoteId).SingleOrDefault();
                if (exists != null)
                {
                    exists.MyNote = model.MyNote;
                    exists.Title = model.Title;
                    this.userContext.Notes.Update(exists);
                    this.userContext.SaveChanges();
                    return "Note Updated Successfully !";
                }

                return "No such note found Add new Note";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
