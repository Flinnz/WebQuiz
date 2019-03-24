using System;
using System.Linq;
using System.Collections.Generic;

namespace WebQuiz.Domain
{
    public class InMemoryQuestionsRepository : IQuestionsRepository
    {
        private readonly Dictionary<Guid, QuestionEntity> questions;
        public InMemoryQuestionsRepository()
        {
            questions = new Dictionary<Guid, QuestionEntity>();
            this.Insert(new QuestionEntity(Guid.NewGuid(), "Сколько будет 2*2?", "4"));
            this.Insert(new QuestionEntity(Guid.NewGuid(), "Как зовут Вячеслава Жиляева?", "Миша"));
            this.Insert(new QuestionEntity(Guid.NewGuid(), "Итс окей ту би гей?", "Да"));
        }
        public QuestionEntity FindById(Guid id)
        {
            if(questions.ContainsKey(id))
            {
                return questions[id];
            }

            return null;
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