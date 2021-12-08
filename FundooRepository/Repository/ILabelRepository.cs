using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        Task<string> Lable(LabelModel label);
        Task<string> RemoveLabel(int labelId);
        IEnumerable<string> GetLabelByUserid(int userId);
    }
}