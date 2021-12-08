using FundooModels;
using FundooRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> Lable(LabelModel label)
        {
            return await repository.Lable(label);
        }
        public async Task<string> AddLabelsByUserId(LabelModel label)
        {
            return await repository.Lable(label);
        }

    }
}
