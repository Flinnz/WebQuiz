using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain
{
    public class GameEntity
    {
        [BsonConstructor]
        public GameEntity(Guid id, int maxPlayerCount, List<PlayerEntity> players, PlayerEntity hostPlayer, QuestionEntity[] questions, int currentQuestion)
        {
            Id = id;
            MaxPlayerCount = maxPlayerCount;
            Players = players;
            HostPlayer = hostPlayer;
            Questions = questions;
            CurrentQuestion = currentQuestion;
        }

        [BsonElement]
        public Guid Id { get; }
        [BsonElement]
        public int MaxPlayerCount { get; }
        public bool IsFinished => Questions.Length == CurrentQuestion;
        public bool IsStarted => true;
        [BsonElement]
        public bool IsFull => Players.Count == MaxPlayerCount;
        public List<PlayerEntity> Players { get; set; }
        [BsonElement]
        public PlayerEntity HostPlayer { get; }
        public QuestionEntity[] Questions { get; set; }
        [BsonElement]
        public int CurrentQuestion { get; private set; }
        public GameEntity(int maxPlayerCount, PlayerEntity hostPlayer, QuestionEntity[] questions)
        {
            MaxPlayerCount = maxPlayerCount;
            this.CurrentQuestion = 0;
            this.Id = Guid.NewGuid();
            this.Players = new List<PlayerEntity> {hostPlayer};
            this.HostPlayer = hostPlayer;
            this.Questions = questions;
        }



        public bool Join(PlayerEntity playerEntity)
        {
            if (Players.Count >= MaxPlayerCount) return false;
            playerEntity.GameId = this.Id;
            Players.Add(playerEntity);
            return true;
        }

        public void Leave(PlayerEntity playerEntity)
        {
            playerEntity.GameId = Guid.Empty;
            Players.RemoveAll(p => p.Id == playerEntity.Id);
        }
        
        public bool AnswerQuestion(string answer)
        {
            if (IsFinished) return false;
            var currentQuestion = Questions[CurrentQuestion];
            var isRightAnswer = currentQuestion.AnswerQuestion(answer);
            if (isRightAnswer) CurrentQuestion++;
            return isRightAnswer;
        }

    }
}
