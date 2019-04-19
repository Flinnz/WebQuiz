using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebQuiz.Domain;

namespace WebQuiz.Hubs
{
    public class QuizHub: Hub
    {
        private readonly IQuestionsRepository repository;

        public QuizHub(IQuestionsRepository repository)
        {
            this.repository = repository;
        }

        public Task SendQuestion()
        {
            return Clients.All.SendCoreAsync("GetQuestion", new object[] {repository.GetRandomQuestion()});
        }

        public Task ReceiveAnswer(Guid guid, string answer)
        {
            var question = repository.FindById(guid);
            if (question == null || !question.AnswerQuestion(answer))
                return Clients.Caller.SendCoreAsync("Answer", new object[] {false});
            SendQuestion();
            return Clients.Caller.SendCoreAsync("Answer", new object[] {true});
        }
    }
}
