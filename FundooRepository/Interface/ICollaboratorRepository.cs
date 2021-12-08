﻿using FundooModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public interface ICollaboratorRepository
    {
        IConfiguration Configuration { get; }
        public Task<string> AddCollaborator(CollaboratorModel collaboratorUser);
        Task<string> DeleteCollaborator(int noteId, string collabMail);
    }
}