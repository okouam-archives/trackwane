using System.Collections.Generic;

namespace Trackwane.Framework.Common
{
    public class ResponsePage<T>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IList<T> Items { get; set; } = new List<T>();

        public int Total { get; set; }
    }
}
