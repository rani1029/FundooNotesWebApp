using FundooModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface ILabelManager
    {
        Task<string> Lable(LabelModel label);
        public Task<string> AddLabelsByUserId(LabelModel label);
        Task<string> RemoveLabel(int labelId);
        IEnumerable<string> GetLabelByUserid(int userId);
    }
}