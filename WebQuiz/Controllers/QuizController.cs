using Microsoft.AspNetCore.Mvc;
using WebQuiz.Domain;
using WebQuiz.Models;

namespace WebQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuestionsRepository questionsRepository;

        public QuizController(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
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