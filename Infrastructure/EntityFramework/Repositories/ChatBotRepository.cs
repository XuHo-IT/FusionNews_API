using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Dapper;
using Infrastructure.EntityFramework.Const;
using Infrastructure.EntityFramework.DataAccess;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class ChatBotRepository : DapperBase, IChatBotRepository
    {
        public async Task CreateQuestion(ChatbotQuestion chatbotQuestionDTO)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(chatbotQuestionDTO);
                parameters.Add("@JInput", jInput, DbType.String);

                await connection.ExecuteAsync(
                     StoredExecFunction.CreateQuestion,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return 0;
            });
        }

        public async Task<List<ChatbotQuestion>> GetQuestion()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<ChatbotQuestion>(
                    StoredExecFunction.GetAllQuestions,
                    commandType: CommandType.Text
                );

                return result.ToList();
            });
        }

    }
}
