using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebQuiz.Domain
{
    public class GameEntity
    {
        public PlayerEntity[] Players { get; }
        public PlayerEntity HostPlayer { get; }
        public QuestionEntity[] Questions { get; }
        public GameEntity(int playerCount, QuestionEntity[] questions)
        {
            this.Players = new PlayerEntity[playerCount];
            this.Questions = questions;
        }

    }
}
