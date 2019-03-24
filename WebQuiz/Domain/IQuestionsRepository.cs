

using System;

namespace WebQuiz.Domain
{
    public interface IQuestionsRepository
    {
        QuestionEntity FindById(Guid id);
        QuestionEntity GetRandomQuestion();
        void Insert(QuestionEntity question);
    }
}