using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public interface IFeedbackRepository
    {
        FeedbackEntity Insert(string text);
    }

    public class InMemoryFeedbackRepository : IFeedbackRepository
    {
        private readonly Dictionary<Guid, FeedbackEntity> allFeedback = new Dictionary<Guid, FeedbackEntity>();

        public FeedbackEntity Insert(string text)
        {
            var id = new Guid();
            var entity = new FeedbackEntity(id, text);
            allFeedback[id] = entity;
            return new FeedbackEntity(id, entity.Text);
        }
    }
}