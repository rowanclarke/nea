using Connection;
using Core = Connection.Core;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distributor
{
    class LocalDistributor : IDistributor
    {
        public Core.Route GetGeoRoute(Core.RoutePackage task)
        {
            RemoteWorker worker = new RemoteWorker();

            ORSMatrixResponse response = getMatrix(task).Result;

            task.matrix = new Core.AdjacencyMatrix(response.durations);

            return worker.GetRouteSubgraph(task);
        }

        public async Task<ORSMatrixResponse> getMatrix(Core.RoutePackage task)
        {
            var baseAddress = new Uri("https://api.openrouteservice.org");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "5b3ce3597851110001cf6248dc6efc12aaf44311a59270bb75959fb5");

                using (var content = new StringContent($"{{\"locations\":{task.Locations()},\"metrics\":[\"duration\"],\"units\":\"km\"}}", Encoding.UTF8,
                                    "application/json"))
                {
                    using (var response = await httpClient.PostAsync("/v2/matrix/driving-car", content))
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($">>> {responseString}");
                        ORSMatrixResponse responseData = new ORSMatrixResponse();
                        responseData = JsonSerializer.Deserialize<ORSMatrixResponse>(responseString);
                        Console.WriteLine($">>> {responseData.durations}");
                        return responseData;
                    }
                }
            }

        }

    }

    class ORSMatrixResponse
    {
        public List<List<double>> durations { get; set; }
    }
}
