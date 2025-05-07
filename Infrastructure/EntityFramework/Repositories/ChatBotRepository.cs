using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Reponse.Chatbot;
using Application.Request.Chatbot;
using Dapper;
using Infrastructure.EntityFramework.Const;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace Infrastructure.EntityFramework.Repositories
{
    public class ChatBotRepository : DapperBase, IChatBotRepository
    {
        public ChatBotRepository(IConfiguration configuration) : base(configuration) { }
        public async Task CreateQuestion(ChatbotQuestion chatbotQuestion)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(chatbotQuestion);
                parameters.Add("@JInput", jInput, DbType.String);

                await connection.ExecuteAsync(
                     StoredExecFunction.CreateQuestion,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return 0;
            });
        }

        public async Task<List<ChatbotQuestionRequest>> GetQuestion()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<ChatbotQuestionRequest>(
                    StoredExecFunction.GetQuestion,
                    commandType: CommandType.Text
                );

                return result.ToList();
            });
        }

        public async Task DeleteQuestion(int id)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(new { questionid = id });
                parameters.Add("@JInput", jInput, DbType.String);
                await connection.ExecuteAsync(
                     StoredExecFunction.DeleteQuestion,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return 0;
            });
        }

        public async Task UpdateQuestion(ChatbotQuestion chatbotQuestion)
        {
            await WithConnection(async connection =>
            {
                var parameters = new DynamicParameters();
                var jInput = JsonConvert.SerializeObject(chatbotQuestion);
                parameters.Add("@JInput", jInput, DbType.String);

                await connection.ExecuteAsync(
                     StoredExecFunction.UpdateQuestion,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return 0;
            });
        }

        public async Task<List<ChatbotQuestion>> GetQuestionAndAnswer()
        {
            return await WithConnection(async connection =>
            {
                var result = await connection.QueryAsync<ChatbotQuestion>(
                    StoredExecFunction.GetAllQuestionsAndAnswer,
                    commandType: CommandType.Text
                );

                return result.ToList();
            });
        }

        public async Task<ChatbotAnswerResponse> GetAnswer(int id)
        {
            return await WithConnection(async connection =>
            {
                var jInput = JsonConvert.SerializeObject(new { questionid = id });
                var parameters = new DynamicParameters();
                parameters.Add("@JInput", jInput, DbType.String);

                var result = await connection.QueryFirstOrDefaultAsync<ChatbotAnswerResponse>(
                    StoredExecFunction.GetAnswer,
                    param: parameters,
                    commandType: CommandType.Text
                );

                return result;
            });
        }

    }
}
