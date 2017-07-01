using System.Collections.Generic;
using System.Threading.Tasks;
using BlissXamarinApp.Models;

namespace BlissXamarinApp.Interfaces
{
    public interface IBlissApiService
    {
        Task<StatusApi> GetStatusApiAsync();
        Task<List<Question>> GetQuestionsAsync(int offset);
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetQuestionsByFilterAsync(int offset, string filter);
        Task<bool> PostUrlAsync(ShareScreen content);
    }
}