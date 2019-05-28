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
            //uncomment for google sheets parsing
            var parser = new SheetParser("1FgbLOKoa1FuXnyiDLsweLoAtU60u3i0MlnMLJRCnE38");
            var values = parser.GetValues("B2:C");
            foreach (var value in values)
            {
                if (value.Count > 1)
                    this.Insert(new QuestionEntity(Guid.NewGuid(), value[0].ToString(), value[1].ToString()));
            }
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