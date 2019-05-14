using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class FeedbackEntity
    {
        public Guid Id { get; }
        public string Text { get; }

        public FeedbackEntity(Guid id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
