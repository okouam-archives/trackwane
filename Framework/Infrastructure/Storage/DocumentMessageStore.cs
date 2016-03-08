using System;
using paramore.brighter.commandprocessor;
using Raven.Client;

namespace Trackwane.Framework.Infrastructure.Storage
{
    public class DocumentMessageStore : IAmAMessageStore<Message>
    {
        private readonly IDocumentStore documentStore;

        public DocumentMessageStore(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void Add(Message message, int messageStoreTimeout = -1)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(message);
                session.SaveChanges();
            }
        }

        public Message Get(Guid messageId, int messageStoreTimeout = -1)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Load<Message>(messageId.ToString());
            }
        }
    }
}
