using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        Task<string> AddLabel(LabelModel label);
        Task<string> RemoveLabel(int labelId);
        IEnumerable<string> GetLabelByUserid(int userId);
        Task<string> AddLabelsByUserId(LabelModel label);
        IEnumerable<LabelModel> GetLabelByNote(int notesId);
        public Task<string> EditLabel(int userId, string labelName, string newLabelName);
        public Task<string> EditLabelWithNoteId(int noteId, string labelName, string newLabelName);
        Task<string> DeleteLabel(int userId, string labelName);
    }
}