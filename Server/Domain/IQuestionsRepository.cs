using System;

namespace Server.Domain
{
    public interface IQuestionsRepository
    {
        QuestionEntity FindById(Guid id);
        QuestionEntity GetRandomQuestion();
        void Insert(QuestionEntity question);
    }
}