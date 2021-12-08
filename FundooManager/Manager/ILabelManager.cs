using FundooModels;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public interface ILabelManager
    {
        Task<string> Lable(LabelModel label);
        public Task<string> AddLabelsByUserId(LabelModel label);
    }
}