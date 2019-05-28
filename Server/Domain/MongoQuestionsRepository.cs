using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
                Builders<QuestionEntity>.IndexKeys.Ascending(u => u.Id),
                new CreateIndexOptions { Unique = true }));
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