using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface ILabelManager
    {
        Task<string> AddLabel(LabelModel label);
        public Task<string> AddLabelsByUserId(LabelModel label);
        Task<string> RemoveLabel(int labelId);
        IEnumerable<string> GetLabelByUserid(int userId);
        IEnumerable<LabelModel> GetLabelByNote(int notesId);
        public Task<string> EditLabel(int userId, string labelName, string newLabelName);
        Task<string> DeleteLabel(int userId, string labelName);
    }
}