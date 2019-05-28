using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SpreadSheetParser.Parsers;

namespace Server.Domain
{
   public class MongoQuestionsRepository : IQuestionsRepository
   {
      private readonly IMongoCollection<QuestionEntity> questionCollection;
      public const string CollectionName = "questions";

      public MongoQuestionsRepository(IMongoDatabase database)
      {
         questionCollection = database.GetCollection<QuestionEntity>(CollectionName);
         //USE ONCE
         //ParseQuestions();
      }

      private void ParseQuestions()
      {
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
         return questionCollection.Find(u => u.Id == id).SingleOrDefault();
      }

      public QuestionEntity GetRandomQuestion()
      {
         return questionCollection.AsQueryable().Sample(1).First();
      }

      public void Insert(QuestionEntity question)
      {
         questionCollection.InsertOne(question);
      }
   }
}