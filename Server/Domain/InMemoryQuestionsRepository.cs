using System;
using System.Linq;
using System.Collections.Generic;
using SpreadSheetParser.Parsers;

namespace Server.Domain
{
    public class InMemoryQuestionsRepository : IQuestionsRepository
    {
        private readonly Dictionary<Guid, QuestionEntity> questions;
        public InMemoryQuestionsRepository()
        {
            questions = new Dictionary<Guid, QuestionEntity>();
        }
        public QuestionEntity FindById(Guid id)
        {
            return questions.ContainsKey(id) ? questions[id] : null;
        }

        public QuestionEntity GetRandomQuestion()
        {
            var random = new Random();
            var questionsList = questions.Values.ToList();
            return questionsList[random.Next(questionsList.Count)];
        }

        public void Insert(QuestionEntity question)
        {
            questions[question.Id] = question;
        }
    }
}