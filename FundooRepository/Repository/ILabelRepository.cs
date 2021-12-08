﻿using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        Task<string> Lable(LabelModel label);
    }
}