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
        private readonly IFeedbackRepository feedbackRepository;

        public QuizController(IQuestionsRepository questionsRepository, IGameRepository gameRepository, IFeedbackRepository feedbackRepository)
        {
            this.questionsRepository = questionsRepository;
            this.gameRepository = gameRepository;
            this.feedbackRepository = feedbackRepository;
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

        /// <summary>
        /// Recieve feedback.
        /// </summary>
        [HttpPost("feedback")]
        public IActionResult SendFeedback([FromBody] RecivedFeedbackDto note)
        {
            var feedback = feedbackRepository.Insert(note.Text);
            return Ok(feedback);
        }

    }
}