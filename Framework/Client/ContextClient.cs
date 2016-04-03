using System;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Client
{
    public abstract class ContextClient<T> where T : class
    {
        private readonly string baseUrl;
        private readonly IPlatformConfig config;
        protected RestClient client;

        protected ContextClient(string baseUrl, IPlatformConfig config)
        {
            this.baseUrl = baseUrl;
            this.config = config;
        }

        public T Use(UserClaims userClaims)
        {
            if (userClaims != null)
            {
                client = new RestClient(baseUrl);
                client.AddDefaultHeader("Authorization", $"Bearer {userClaims.GenerateToken(config.Get(PlatformConfigKeys.SecretKey))}");
            }

            return this as T;
        }

        public T UseWithoutAuthentication()
        {
            client = new RestClient(baseUrl);
            return this as T;
        }

        protected string Expand(string url, params object[] values)
        {
            return string.Format(url, values);
        }

        protected void POST(string url, object model = null)
        {
            var request = new RestRequest(url)
            {
                RequestFormat = DataFormat.Json,
                Method = Method.POST
            };

            if (model != null)
            {
                request.AddJsonBody(model);
            }

            Execute(() => client.Execute(request));
        }

        protected TModel POST<TModel>(string url, object model = null) where TModel : new()
        {
            var request = new RestRequest(url)
            {
                RequestFormat = DataFormat.Json,
                Method = Method.POST
            };

            if (model != null)
            {
                request.AddJsonBody(model);
            }

            var data = default(TModel);

            Execute(() =>
            {
                var result = client.Execute<TModel>(request);
                data = result.Data;
                return result;
            });

            return data;
        }

        protected void DELETE(string url)
        {
            Execute(() => client.Execute(new RestRequest(url, Method.DELETE)));
        }

        protected TModel GET<TModel>(string url, object model = null) where TModel : new()
        {
            var data = default(TModel);
            
            Execute(() =>
            {
                var request = RequestBuilder.GET(url, model);

                var result = client.Execute<TModel>(request);

                data = result.Data;

                return result;
            });

            return data;
        }

        private static void Execute(Func<IRestResponse> func)
        {
            var response = func();

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var error = JObject.Parse(response.Content);

                var type = error.GetValue("type");

                if (type != null)
                {
                    switch (error.GetValue("type").Value<string>())
                    {
                        case "BusinessRuleException":
                            throw new BusinessRuleException(error.GetValue("message").Value<string>());
                        case "ValidationException":
                            throw new ValidationException(error.GetValue("message").Value<string>());
                        default:
                            throw new Exception(error.GetValue("message").Value<string>());
                    }
                }

                throw new Exception(JsonConvert.SerializeObject(response, Formatting.Indented));
            }

            var code = response.StatusCode;

            if (code == HttpStatusCode.Forbidden || code == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException(Streamline(response));
            }

            if (code != HttpStatusCode.OK && code != HttpStatusCode.Created && code != HttpStatusCode.Accepted && code != HttpStatusCode.NoContent)
            {
                throw new Exception(Streamline(response));
            }
        }

        private static string Streamline(IRestResponse response)
        {
            return JsonConvert.SerializeObject(new
            {
                Request = new
                {
                    Method = response.Request?.Method.ToString(),
                    response.Request?.Resource,
                    RequestFormat = response.Request?.RequestFormat.ToString(),
                    response.Request?.Parameters
                },
                Response = new
                {
                    response.Content,
                    response.StatusCode,
                    response.StatusDescription,
                    response.ContentType,
                    response.ErrorMessage
                }
            }, Formatting.Indented);
        }
    }
}
