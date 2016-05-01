using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;

namespace Trackwane.Framework.Client
{
    public abstract class ContextClient<T> : ContextClient where T : class
    { 
        protected ContextClient(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey) : base(server, protocol, apiPort, secretKey, metricsPort, applicationKey)
        {
        }

        public T Use(UserClaims userClaims)
        {
            if (userClaims != null)
            {
                apiClient = CreateRestClientForApplication(applicationKey, baseUrl);
                apiClient.AddDefaultHeader("Authorization", $"Bearer {userClaims.GenerateToken(secretKey)}");
            }

            return this as T;
        }

        public T UseWithoutAuthentication()
        {
            apiClient = CreateRestClientForApplication(applicationKey, baseUrl);
            return this as T;
        }

        private static RestClient CreateRestClientForApplication(string appKey, string uri)
        {
            var client = new RestClient(uri);
            client.AddDefaultHeader(Constants.HTTP_TRACKWANE_APPLICATION_KEY, appKey);
            return client;
        }
    }

    public abstract class ContextClient 
    {
        protected readonly string baseUrl;
        protected readonly string secretKey;
        protected readonly string applicationKey;
        protected readonly RestClient metricsClient;
        protected RestClient apiClient;

        protected ContextClient(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey)
        {
            baseUrl = protocol + "://" + server + ":" + apiPort;
            this.secretKey = secretKey;
            this.applicationKey = applicationKey;
            metricsClient = new RestClient(protocol + "://" + server + ":" + metricsPort);
        }

        public string Expand(string url, params object[] values)
        {
            return string.Format(url, values);
        }

        public string GetMetrics()
        {
            return metricsClient.Get(new RestRequest("/metrics")).Content;
        }

        public void POST(string url, object model = null)
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

            Execute(() => apiClient.Execute(request));
        }

        public TModel POST<TModel>(string url, object model = null) where TModel : new()
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
                var result = apiClient.Execute<TModel>(request);
                data = result.Data;
                return result;
            });

            return data;
        }

        public void DELETE(string url)
        {
            Execute(() => apiClient.Execute(new RestRequest(url, Method.DELETE)));
        }

        public TModel GET<TModel>(string url, object model = null) where TModel : new()
        {
            var data = default(TModel);

            Execute(() =>
            {
                var request = RequestBuilder.GET(url, model);

                var result = apiClient.Execute<TModel>(request);

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
