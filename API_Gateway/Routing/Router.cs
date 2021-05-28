using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API_Gateway
{
    public class Router
    {
        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }

        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);
            Routes = JsonLoader.Deserialize<List<Route>>(
                Convert.ToString(router.routes)
            );
            AuthenticationService = JsonLoader.Deserialize<Destination>(
                Convert.ToString(router.authenticationService)
            );
        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            bool isBasepath = false;
            Destination destination = new Destination();

            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(path)).Destination;
            }
            catch
            {
                isBasepath = true;
            }

            if (isBasepath)
            {
                string basePath = '/' + path.Split('/')[1];

                try
                {
                    destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
                }
                catch
                {
                    return ConstructErrorMessage("The path could not be found.");
                }
            }

            if (request.Method == "PUT" || request.Method == "DELETE")
            {
                destination.RequiresAuthentication = true;
            }

            if (destination.RequiresAuthentication)
            {
                string token = request.Headers["Authorization"].FirstOrDefault();
                if (token == null) return ConstructErrorMessage("Authentication failed.");

                HttpResponseMessage authResponse = await SendAuthRequest(token);
                if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");
            }

            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }

        public async Task<HttpResponseMessage> SendAuthRequest(string token)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage newRequest = new HttpRequestMessage(new HttpMethod("GET"), "https://kwekkeraccountservice.azurewebsites.net/account/verify/");
            client.DefaultRequestHeaders.Add("Authorization", token);
            //newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
            HttpResponseMessage response = await client.SendAsync(newRequest);
            return response;
        }
    }
}
