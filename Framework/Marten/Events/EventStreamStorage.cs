using System;
using Baseline;
using Marten.Schema;
using Marten.Services;
using Npgsql;
using NpgsqlTypes;

namespace Marten.Events
{
    public class EventStreamStorage : IDocumentStorage, IdAssignment<EventStream>
    {
        private readonly EventGraph _graph;

        private FunctionName AppendEventFunction => new FunctionName(_graph.DatabaseSchemaName, "mt_append_event");

        public EventStreamStorage(EventGraph graph)
        {
            _graph = graph;
        }

        public Type DocumentType { get; } = typeof (EventStream);
        public NpgsqlDbType IdType { get; } = NpgsqlDbType.Uuid;

        public NpgsqlCommand LoaderCommand(object id)
        {
            throw new NotImplementedException();
        }

        public NpgsqlCommand DeleteCommandForId(object id)
        {
            throw new NotImplementedException();
        }

        public NpgsqlCommand DeleteCommandForEntity(object entity)
        {
            throw new NotImplementedException();
        }

        public NpgsqlCommand LoadByArrayCommand<TKey>(TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public object Identity(object document)
        {
            return document.As<EventStream>().Id;
        }

        public void RegisterUpdate(UpdateBatch batch, object entity)
        {
            var stream = entity.As<EventStream>();

            var streamTypeName = stream.AggregateType == null ? null : _graph.AggregateFor(stream.AggregateType).Alias;

            stream.Events.Each(@event =>
            {
                var mapping = _graph.EventMappingFor(@event.Body.GetType());

                batch.Sproc(AppendEventFunction)
                    .Param("stream", stream.Id)
                    .Param("stream_type", streamTypeName)
                    .Param("event_id", @event.Id)
                    .Param("event_type", mapping.EventTypeName)
                    .JsonEntity("body", @event.Body);
            });
        }

        public void RegisterUpdate(UpdateBatch batch, object entity, string json)
        {
            throw new NotSupportedException();
        }

        public void Remove(IIdentityMap map, object entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IIdentityMap map, object id)
        {
            throw new NotImplementedException();
        }

        public void Store(IIdentityMap map, object id, object entity)
        {
            //EventStreams are not stored in entity map
        }

        public object Assign(EventStream document, out bool assigned)
        {
            assigned = false;
            return document.Id;
        }
    }
}