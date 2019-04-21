using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Server.Domain;

namespace Server.Hubs
{
    public class QuizHub: Hub
    {
        private readonly IGameRepository gameRepository;

        public QuizHub(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public Task SendQuestion(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null)
                return Clients.Caller.SendCoreAsync("GetQuestion", null);
            return Clients.Caller.SendCoreAsync("GetQuestion", new object[] { game.Questions[game.CurrentQuestion] });
        }

        public Task ReceiveAnswer(Guid gameId, Guid playerId, string answer)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null)
                return Clients.Caller.SendCoreAsync("GetQuestion", null);
            var isQuestionAnswered = game.AnswerQuestion(answer);
            if (isQuestionAnswered)
                SendNewQuestion(gameId);
            return Clients.Caller.SendCoreAsync("Answer", new object[] {isQuestionAnswered});
        }

        public Task SendNewQuestion(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null)
                return Clients.All.SendCoreAsync("GetQuestion", null);
            return Clients.All.SendCoreAsync("GetQuestion", new object[] { game.Questions[game.CurrentQuestion] });
        }
    }
}
