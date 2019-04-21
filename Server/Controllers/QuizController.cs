using Microsoft.AspNetCore.Mvc;
using Server.Domain;
using Server.Models;
using System;
using System.Linq;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuestionsRepository questionsRepository;
        private readonly IGameRepository gameRepository;

        public QuizController(IQuestionsRepository questionsRepository, IGameRepository gameRepository)
        {
            this.questionsRepository = questionsRepository;
            this.gameRepository = gameRepository;
        }

        [HttpPost("game")]
        public IActionResult CreateGame()
        {
            var newGame = new GameEntity(4, new PlayerEntity(), Enumerable.Range(0, 10).Select(x => questionsRepository.GetRandomQuestion()).ToArray());
            gameRepository.Insert(newGame);
            return Ok(new GameToSendDto{
                Guid = newGame.Id,
                PlayerCount = newGame.Players.Length,
                HostPlayerGuid = newGame.HostPlayer.Guid,
                YourPlayerGuid = newGame.HostPlayer.Guid,
            });
        }
        [HttpGet("game")]
        public IActionResult JoinGame([FromQuery]Guid id)
        {
            var game = gameRepository.FindById(id);      
            if (game == null) return NotFound();
            var player = game.Join();
            if (player == null) return Ok("Пошёл нахуй");
            return Ok(new GameToSendDto
            {
                Guid = game.Id,
                PlayerCount = game.Players.Length,
                HostPlayerGuid = game.HostPlayer.Guid,
                YourPlayerGuid = player.Guid,
            });
        }

        /// <summary>
        /// Gets random question from database.
        /// </summary>
        [HttpGet("question")]
        public IActionResult GetNewRandomQuestion()
        {
            var question = questionsRepository.GetRandomQuestion();
            return Ok(new QuestionToSendDto{
                Text = question.Text,
                Id = question.Id          
            });
        }

        /// <summary>
        /// Submits answer.
        /// </summary>
        [HttpPost("answer")]
        public IActionResult AnswerQuestion([FromBody]RecievedAnswerDto answerDto)
        {
            var question = questionsRepository.FindById(answerDto.Id);
            return Ok(question.AnswerQuestion(answerDto.Answer));
        }
    }
}