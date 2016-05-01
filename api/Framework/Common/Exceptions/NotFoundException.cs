using System;

namespace Trackwane.Framework.Common.Exceptions
{
    [Serializable]
    public class NotFoundException<T> : Exception
    {
        public string Id { get; set; }

        public NotFoundException(string id) : base(CreateMessage(typeof(T), id))
        {
            Id = id;
        }

        private static string CreateMessage(Type type, string id)
        {
            return $"The {type.ToString().ToLower()} with ID <{id}> cannot be found";
        }
    }
}
