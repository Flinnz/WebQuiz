using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain
{
    public class QuestionEntity
    {
        [BsonElement]
        public Guid Id { get; }
        [BsonElement]
        public string Text { get; }
        [BsonElement]
        public string CorrectAnswer { get; }

        [BsonConstructor]
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