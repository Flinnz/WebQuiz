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
        private readonly IQuestionsRepository questionsRepository;
        private readonly Dictionary<string, PlayerEntity> playerManager;

        public QuizHub(IGameRepository gameRepository, IQuestionsRepository questionsRepository, Dictionary<string, PlayerEntity> playerManager)
        {
            this.gameRepository = gameRepository;
            this.questionsRepository = questionsRepository;
            this.playerManager = playerManager;
        }

        public override Task OnConnectedAsync()
        {
            var connectionGuid = this.Context.ConnectionId;
            playerManager.Add(connectionGuid, new PlayerEntity(connectionGuid));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionGuid = this.Context.ConnectionId;
            var player = playerManager[connectionGuid];
            var game = gameRepository.FindById(player.GameId);
            game?.Leave(player);
            playerManager.Remove(connectionGuid);          
            return base.OnDisconnectedAsync(exception);
        }

        public Task Tick()
        {
            return Clients.Caller.SendCoreAsync("Tick", new object[0]);
        }

        public Task CreateGame()
        {
            var game = new GameEntity(4, playerManager[this.Context.ConnectionId], Enumerable.Range(0, 10).Select(q => questionsRepository.GetRandomQuestion()).ToArray());
            this.gameRepository.Insert(game);

            return Clients.Caller.SendCoreAsync("CreateGame", new object[] {game.Id}).ContinueWith((t) => SendQuestion(game.Id));
        }

        public Task FindGame()
        {
            var game = gameRepository.FindNotFullGame();
            if (game == null) return Clients.Caller.SendCoreAsync("FindGame", new object[0]);
            var connectionGuid = this.Context.ConnectionId;
            var player = playerManager[connectionGuid];
            if (!game.Join(player)) return Clients.Caller.SendCoreAsync("FindGame", new object[0]);
            return Clients.Caller.SendCoreAsync("FindGame", new object[] { game.Id }).ContinueWith(t => SendQuestion(game.Id));
        }

        public Task JoinGame(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null) return Clients.Caller.SendCoreAsync("JoinGame", new object[0]);
            var connectionGuid = this.Context.ConnectionId;
            var player = playerManager[connectionGuid];
            if (!game.Join(player)) return Clients.Caller.SendCoreAsync("JoinGame", new object[0]);
            return Clients.Caller.SendCoreAsync("JoinGame", new object[] {game.Id}).ContinueWith(t => SendQuestion(game.Id));
        }

        public Task LeaveGame(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null) return Clients.Caller.SendCoreAsync("LeaveGame", new object[0]);
            var connectionGuid = this.Context.ConnectionId;
            var player = playerManager[connectionGuid];
            game.Leave(player);
            return Task.CompletedTask;
        }

        public Task SendQuestion(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null)
                return Clients.Caller.SendCoreAsync("GetQuestion", new object[0]);
            return Clients.Caller.SendCoreAsync("GetQuestion", new object[] { game.Questions[game.CurrentQuestion] });
        }

        public Task ReceiveAnswer(Guid gameId, string answer)
        {
            var game = gameRepository.FindById(gameId);
            var connectionId = this.Context.ConnectionId;
            if (game == null)
                return Clients.Caller.SendCoreAsync("GetQuestion", new object[0]);
            var isQuestionAnswered = game.AnswerQuestion(answer);
            if (isQuestionAnswered)
            {
                playerManager[connectionId].Score++;
                SendNewQuestion(gameId);
            }

            return Clients.Caller.SendCoreAsync("Answer", new object[] {isQuestionAnswered});
        }

        public Task SendNewQuestion(Guid gameId)
        {
            var game = gameRepository.FindById(gameId);
            if (game == null)
                return Clients.All.SendCoreAsync("GetQuestion", new object[0]);
            return Clients.All.SendCoreAsync("GetQuestion", new object[] { game.Questions[game.CurrentQuestion] });
        }
    }
}
