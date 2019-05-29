using System;
using System.Linq;
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
         questionCollection.Indexes.CreateOne(new CreateIndexModel<QuestionEntity>(
                Builders<QuestionEntity>.IndexKeys.Descending(u => u.CreationDate),
                new CreateIndexOptions { Unique = false }));
         //some shit happens with date FIX FIX FIX ASAP should be last date
         var lastQuestionFromDB = questionCollection
            .Aggregate()
            .SortByDescending(x => x.CreationDate)
            .FirstOrDefault();
         ParseQuestions(lastQuestionFromDB == null ? DateTime.MinValue : lastQuestionFromDB.CreationDate);
      }

      private void ParseQuestions(DateTime lastDateFromDB)
      {
         var parser = new SheetParser("1FgbLOKoa1FuXnyiDLsweLoAtU60u3i0MlnMLJRCnE38");
         var values = parser.GetValues("A2:C");

         foreach (var value in values.Where(x => x.Count > 2))
         {
            var currentDate = DateTime.Parse(value[0].ToString());
            if (currentDate > lastDateFromDB)
               this.Insert(new QuestionEntity(Guid.NewGuid(), currentDate, value[1].ToString(), value[2].ToString()));
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