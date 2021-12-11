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
        public async Task<string> AddLabel(LabelModel label)
        {
            return await repository.AddLabel(label);
        }
        public async Task<string> AddLabelsByUserId(LabelModel label)
        {
            return await repository.AddLabelsByUserId(label);
        }
        public async Task<string> RemoveLabel(int labelId)
        {
            return await repository.RemoveLabel(labelId);

        }
        public async Task<string> DeleteLabel(int userId, string labelName)
        {
            return await repository.DeleteLabel(userId, labelName);
        }

        public IEnumerable<string> GetLabelByUserid(int userId)
        {
            return repository.GetLabelByUserid(userId);
        }
        public IEnumerable<LabelModel> GetLabelByNote(int notesId)
        {
            return repository.GetLabelByNote(notesId);
        }

        public async Task<string> EditLabel(int userId, string labelName, string newLabelName)
        {
            return await repository.EditLabel(userId, labelName, newLabelName);
        }

        public async Task<string> EditLabelWithNoteId(int noteId, string labelName, string newLabelName)
        {
            return await repository.EditLabelWithNoteId(noteId, labelName, newLabelName);
        }
    }

}