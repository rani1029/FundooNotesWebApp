using FundooModels;
using FundooRepository.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext userContext;
        public LabelRepository(IConfiguration configuration, UserContext userContext)
        {
            this.Configuration = configuration;
            this.userContext = userContext;
        }
        public IConfiguration Configuration { get; }

        public async Task<string> AddLabel(LabelModel label)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x => x.UserId == label.UserId && x.LabelName != label.LabelName && x.NoteId == null).SingleOrDefault();
                if (validLabel == null)
                {
                    // Add the data to the database
                    this.userContext.Labels.Add(label);
                    await this.userContext.SaveChangesAsync();
                    return "Label is added Successfully";
                }
                else
                {
                    return "Label name already Exits";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> AddLabelsByUserId(LabelModel label)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x => x.UserId == label.UserId && x.NoteId == label.NoteId).SingleOrDefault();
                if (validLabel == null)
                {
                    // Add the data to the database
                    this.userContext.Add(label);
                    await this.userContext.SaveChangesAsync();
                    return "Label is added Successfully";
                }
                else
                {
                    return "Label name already Exits";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> RemoveLabel(int labelId)
        {
            try
            {
                var validLabel = this.userContext.Labels.Where(x => x.LabelId == labelId).SingleOrDefault();
                if (validLabel != null)
                {
                    this.userContext.Labels.Remove(validLabel);
                    await this.userContext.SaveChangesAsync();
                    return "Deleted Label From Note";
                }

                return "No such label Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            try
            {
                var exists = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == userId).FirstOrDefault();
                if (exists != null)
                {
                    this.userContext.Labels.Remove(exists);
                    await this.userContext.SaveChangesAsync();
                    return "Deleted Label";
                }

                return "No Label Present";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<string> GetLabelByUserid(int userId)
        {
            try
            {
                IEnumerable<string> validLabel = this.userContext.Labels.Where(x => x.UserId == userId).Select(x => x.LabelName);
                if (validLabel != null)
                {
                    return validLabel;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<LabelModel> GetLabelByNote(int notesId)
        {
            try
            {
                IEnumerable<LabelModel> validLabel = this.userContext.Labels.Where(x => x.NoteId == notesId);
                if (validLabel != null)
                {
                    return validLabel;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> EditLabel(int userId, string labelName, string newLabelName)
        {
            try
            {
                var exist = this.userContext.Labels.Where(x => x.LabelName == labelName && x.UserId == userId).SingleOrDefault();
                if (exist != null)
                {
                    exist.LabelName = newLabelName;
                    this.userContext.Labels.Update(exist);
                    await this.userContext.SaveChangesAsync();

                    return "Updated Label";
                }

                return "Label not present";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> EditLabelWithNoteId(int noteId, string labelName, string newLabelName)
        {
            try
            {
                var exist = this.userContext.Labels.Where(x => x.LabelName == labelName && x.NoteId == noteId).SingleOrDefault();
                if (exist != null)
                {
                    exist.LabelName = newLabelName;
                    this.userContext.Labels.Update(exist);
                    await this.userContext.SaveChangesAsync();

                    return "Updated Label";
                }

                return "Label not present";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
