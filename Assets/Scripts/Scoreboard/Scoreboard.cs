using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public static class Scoreboard
{
    private const string apiUrl = "https://scrspace23.azurewebsites.net";
    public static List<ScoreRecord> cachedScores;

    public static async Task<List<ScoreRecord>> GetScoreboard()
    {
        Debug.Log("Retrieving scoreboard...");
        using HttpClient client = new HttpClient();
        var response = await client.GetAsync($"{apiUrl}/Score/Scoreboard");
        if (!response.IsSuccessStatusCode)
            Debug.LogError($"Could not retrieve scoreboard (status code: {response.StatusCode})");

        string responseContent = await response.Content.ReadAsStringAsync();
        cachedScores = JsonConvert.DeserializeObject<List<ScoreRecord>>(responseContent);

        Debug.Log("Scoreboard retrieved.");
        return cachedScores;
    }

    public static async Task CreateScoreEntry(ScoreEntry entry)
    {
        Debug.Log("Creating score entry...");
        using HttpClient client = new HttpClient();
        var response = await client.PostAsync($"{apiUrl}/Score/AddScoreEntry", new StringContent(entry.ToString(), Encoding.UTF8, "application/json"));
        if (!response.IsSuccessStatusCode)
            Debug.LogError($"Could not create score entry (status code: {response.StatusCode})");
        Debug.Log("Score entry created.");
    }
}
