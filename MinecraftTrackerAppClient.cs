using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using MinecraftTrackerObjects;

public class MinecraftTrackerAppClient
{
    private readonly HttpClient _httpClient;

    public MinecraftTrackerAppClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MapPoint>> GetAllMapPointsAsync()
    {

        // POST the object to the remote service
        var response = await _httpClient.GetAsync("https://localhost:7199/api/MapPoint/GetAllPoints");

        // Optionally, check the response status
        if (response.IsSuccessStatusCode)
        {
            // Read the response content as a JSON string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON into a list of MapPoint objects
            var mapPoints = JsonSerializer.Deserialize<List<MapPoint>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return mapPoints;
        }
        else
        {
            
            return null;
        }
    }

    public async Task<bool> PostMapPointAsync(MapPoint mapPoint)
    {
        // Serialize the object to JSON
        var jsonContent = JsonSerializer.Serialize(mapPoint);

        // Create an HTTP content object with the JSON and specify the content type
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // POST the object to the remote service
        var response = await _httpClient.PostAsync("https://localhost:7199/api/MapPoint", content);

        // Optionally, check the response status
        if (response.IsSuccessStatusCode)
        {
            // The post was successful
            return true;
        }
        else
        {
            // Handle error responses
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error: {errorMessage}");
            return false;
        }
    }
}
