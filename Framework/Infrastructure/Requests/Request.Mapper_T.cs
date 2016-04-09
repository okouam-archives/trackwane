using System;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public class RequestMapper<T> : IAmAMessageMapper<T> where T : Common.Request
    {
        private static MessageType GetMessageType(Type t)
        {
            return t.IsSubclassOf(typeof (SystemCommand)) ? MessageType.MT_COMMAND : MessageType.MT_EVENT;
        }

        public Message MapToMessage(T request)
        {
            var header = new MessageHeader(request.Id, typeof(T).Name, GetMessageType(typeof(T)));
            var body = new MessageBody(JsonConvert.SerializeObject(request));
            return new Message(header, body);
        }

        public T MapToRequest(Message message)
        {
            return JsonConvert.DeserializeObject<T>(message.Body.Value);
        }
    }
}
