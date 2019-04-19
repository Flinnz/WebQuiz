using System;

namespace WebQuiz.Domain
{
    public class QuestionEntity
    {
        public Guid Id { get; }
        public string Text { get; }
        public string CorrectAnswer { get; }

        public QuestionEntity(Guid id, string text, string correctAnswer)
        {
            this.Id = id;
            this.Text = text;
            this.CorrectAnswer = correctAnswer;
        }

        public bool AnswerQuestion(string answer)
        {
            return string.Equals(answer, CorrectAnswer, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}