using System;
using System.Linq;
using RestSharp;

namespace Trackwane.Framework.Client
{
    internal class RequestBuilder
    {
        public static RestRequest GET(string url, object model)
        {
            var request = new RestRequest(url, Method.GET);

            if (model != null)
            {
                var properties = model.GetType().GetProperties()
                    .Where(x => x.CanRead)
                    .Where(x => x.GetValue(model, null) != null)
                    .ToDictionary(x => x.Name, x => x.GetValue(model, null));

                foreach (var property in properties)
                {
                    var value = property.Value;

                    if (value is DateTime)
                    {
                        var datetime = (DateTime) value;
                        request.AddQueryParameter(property.Key, datetime.ToString("o"));
                    }
                    else
                    {
                        request.AddQueryParameter(property.Key, value.ToString());
                    }
                }
            }
            return request;
        }

    }
}
