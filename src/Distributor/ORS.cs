using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core = Connection.Core;

namespace Distributor.ORS
{

    public static class ORS
    {

        public static async Task<MatrixResponse> GetMatrix(Core.RoutePackage task)
        {
            string responseString = await UseORS("/v2/matrix/driving-car", $"{{\"locations\":{task.reference.Locations()},\"metrics\":[\"duration\"],\"units\":\"km\"}}");
            MatrixResponse responseData = JsonSerializer.Deserialize<MatrixResponse>(responseString);
            return responseData;
        }

        public static async Task<DirectionResponse> GetGeometry(Core.Route route)
        {
            string responseString = await UseORS("/v2/directions/driving-car/geojson", $"{{\"coordinates\":{route.Locations()},\"instructions\":\"false\"}}");
            DirectionResponse responseData = JsonSerializer.Deserialize<DirectionResponse>(responseString);
            return responseData;
        }

        public static async Task<string> UseORS(string url, string contentString)
        {
            var baseAddress = new Uri("https://api.openrouteservice.org");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "5b3ce3597851110001cf6248dc6efc12aaf44311a59270bb75959fb5");

                using (var content = new StringContent(contentString, Encoding.UTF8, "application/json"))
                {
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        return responseString;
                    }
                }
            }
        }
    }

    public class MatrixResponse
    {
        public List<List<float>> durations { get; set; }

        public float[,] ToFloat()
        {
            float[,] d = new float[durations.Count, durations[0].Count];

            for (int i = 0; i < durations.Count; i++)
            {
                for (int j = 0; j < durations[0].Count; j++)
                {
                    d[i, j] = durations[i][j];
                }
            }

            return d;
        }
    }

    public class DirectionResponse
    {
        public List<Feature> features { get; set; }
    }

    public class Feature
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public List<List<float>> coordinates { get; set; }

        public float[,] ToFloat()
        {
            float[,] f = new float[coordinates.Count, coordinates[0].Count];

            for (int i = 0; i < coordinates.Count; i++)
            {
                for (int j = 0; j < coordinates[0].Count; j++)
                {
                    f[i, j] = coordinates[i][j];
                }
            }

            return f;
        }
    }

    
}
