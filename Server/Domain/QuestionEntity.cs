using System;
using MongoDB.Bson;
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
        [BsonElement]
        [BsonRepresentation(BsonType.Document)]
        public DateTime CreationDate { get; }

        [BsonConstructor]
        public QuestionEntity(Guid id, DateTime creationDate, string text, string correctAnswer)
        {
            this.Id = id;
            this.CreationDate = creationDate;
            this.Text = text;
            this.CorrectAnswer = correctAnswer;
        }

        public bool AnswerQuestion(string answer)
        {
            return string.Equals(answer, CorrectAnswer, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}