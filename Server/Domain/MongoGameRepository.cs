using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Server.Domain
{
    public class MongoGameRepository : IGameRepository
    {
        private readonly IMongoCollection<GameEntity> gameCollection;
        public const string CollectionName = "games";

        public MongoGameRepository(IMongoDatabase db)
        {
            gameCollection = db.GetCollection<GameEntity>(CollectionName);
        }

        public GameEntity FindById(Guid id)
        {
            return gameCollection.Find(g => g.Id == id).SingleOrDefault();
        }

        public void Insert(GameEntity gameEntity)
        {
            gameCollection.InsertOne(gameEntity);
        }

        public void Update(GameEntity gameEntity)
        {
            gameCollection.ReplaceOne(g => g.Id == gameEntity.Id, gameEntity);
        }

        public void Upsert(Guid id, GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }

        public GameEntity FindNotFullGame()
        {
            return gameCollection.Find(g => !g.IsFull).FirstOrDefault();
        }
    }
}
