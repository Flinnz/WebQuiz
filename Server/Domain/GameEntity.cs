using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class GameEntity
    {
        public Guid Id { get; }
        public int MaxPlayerCount { get; }
        public bool IsFinished => Questions.Length == CurrentQuestion;
        public List<PlayerEntity> Players { get; }
        public PlayerEntity HostPlayer { get; }
        public QuestionEntity[] Questions { get; }
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
