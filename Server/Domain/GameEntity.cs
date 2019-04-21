using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class GameEntity
    {
        public Guid Id { get; }
        private int currentPlayerCount;
        public bool IsFinished => Questions.Length == CurrentQuestion;
        public PlayerEntity[] Players { get; }
        public PlayerEntity HostPlayer { get; }
        public QuestionEntity[] Questions { get; }
        public int CurrentQuestion { get; private set; }
        public GameEntity(int playerCount, PlayerEntity hostPlayer, QuestionEntity[] questions)
        {
            currentPlayerCount = 1;
            this.CurrentQuestion = 0;
            this.Id = Guid.NewGuid();
            this.Players = new PlayerEntity[playerCount];
            this.HostPlayer = hostPlayer;
            this.Questions = questions;
        }

        public PlayerEntity Join()
        {
            if (Players.Length == currentPlayerCount) return null;
            var newPlayer = new PlayerEntity();
            Players[currentPlayerCount] = newPlayer;
            currentPlayerCount++;
            return newPlayer;
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
