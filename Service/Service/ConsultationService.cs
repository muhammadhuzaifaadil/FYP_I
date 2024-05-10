using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Completions;
using OpenAI_API;
using Core.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Core.Data.DTO;
namespace Service.Service
{
    public class ConsultationService
    {
        private readonly ApplicationDbContext context;

        public ConsultationService()
        {
        }

        public ConsultationService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<string> GetConsultation(string response)
        {
            string outputResult = "";
            var openai = new OpenAIAPI("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");
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = response;
            completionRequest.Model = OpenAI_API.Models.Model.ChatGPTTurboInstruct;
            completionRequest.MaxTokens = 1024;

            var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                outputResult += completion.Text;
            }

            return outputResult;

        }

        public async Task<List<ConsultationQuestionModel>> GetQuestion()
        {
            var questions = await context.ConsultationQuestions.ToListAsync();
            return questions;
        }
    }
}
