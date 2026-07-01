using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

/// <summary>
/// Sends questions to Groq's free chat completions API (OpenAI-compatible format)
/// and returns the AI's response. No local server needed - just an internet connection
/// and a free API key from console.groq.com (no credit card required).
/// </summary>
public class GroqAIHelper : MonoBehaviour
{
    [Header("Groq API Settings")]
    [Tooltip("Get your free API key from https://console.groq.com/keys")]
    private string apiKey = "";

    void Awake()
    {
        // Load key from local file that is git-ignored
        string path = System.IO.Path.Combine(
            System.IO.Directory.GetCurrentDirectory(), 
            "groq_key.txt");
        
        if (System.IO.File.Exists(path))
            apiKey = System.IO.File.ReadAllText(path).Trim();
        else
            Debug.LogError("groq_key.txt not found! Create it in your project root folder.");
    }

    private const string apiUrl = "https://api.groq.com/openai/v1/chat/completions";
    private const string model = "llama-3.3-70b-versatile";

    public delegate void OnResponseReceived(string response);
    public event OnResponseReceived ResponseReceived;

    public delegate void OnError(string error);
    public event OnError ErrorOccurred;

    /// <summary>
    /// Send a question to Groq, optionally with extra context (e.g. condition info),
    /// and get back an AI-generated answer.
    /// </summary>
    public void AskAI(string question, string systemContext = "")
    {
        StartCoroutine(SendToGroq(question, systemContext));
    }

    private IEnumerator SendToGroq(string question, string systemContext)
    {
        if (string.IsNullOrEmpty(apiKey) || apiKey == "PASTE_YOUR_GROQ_API_KEY_HERE")
        {
            ErrorOccurred?.Invoke("No Groq API key set. Paste your key into the GroqAIHelper component in the Inspector.");
            yield break;
        }

        string defaultSystem = "You are VisionVerse Assistant, a helpful expert on eye health and vision conditions. " +
                                "Keep answers clear, accurate, and reasonably concise (3-6 sentences) unless more detail is requested.";

        string systemMessage = string.IsNullOrEmpty(systemContext) ? defaultSystem : systemContext;

        // Build the JSON request body manually to avoid JsonUtility's poor support for
        // nested arrays of objects (messages list).
        string jsonData = "{"
            + "\"model\":\"" + model + "\","
            + "\"messages\":["
                + "{\"role\":\"system\",\"content\":\"" + EscapeJson(systemMessage) + "\"},"
                + "{\"role\":\"user\",\"content\":\"" + EscapeJson(question) + "\"}"
            + "],"
            + "\"temperature\":0.7"
            + "}";

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
            request.timeout = 30; // Groq is fast - 30s is generous, not the 120s Ollama needed

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                string answer = ExtractContentFromResponse(responseText);

                if (!string.IsNullOrEmpty(answer))
                {
                    ResponseReceived?.Invoke(answer);
                }
                else
                {
                    ErrorOccurred?.Invoke("Got a response but couldn't parse it. Raw: " + responseText);
                }
            }
            else
            {
                string error = $"Groq request failed ({request.responseCode}): {request.error}\n{request.downloadHandler.text}";
                Debug.LogError(error);
                ErrorOccurred?.Invoke(error);
            }
        }
    }

    /// <summary>
    /// Extracts the message content from Groq's JSON response without needing
    /// a full JSON library - simple string parsing since the structure is fixed.
    /// Response shape: { "choices": [ { "message": { "content": "..." } } ] }
    /// </summary>
    private string ExtractContentFromResponse(string json)
    {
        const string marker = "\"content\":\"";
        int startIndex = json.IndexOf(marker);
        if (startIndex == -1) return null;

        startIndex += marker.Length;
        int endIndex = startIndex;
        bool escaped = false;

        // Walk forward to find the closing unescaped quote
        while (endIndex < json.Length)
        {
            char c = json[endIndex];
            if (escaped)
            {
                escaped = false;
            }
            else if (c == '\\')
            {
                escaped = true;
            }
            else if (c == '"')
            {
                break;
            }
            endIndex++;
        }

        if (endIndex >= json.Length) return null;

        string raw = json.Substring(startIndex, endIndex - startIndex);
        return UnescapeJson(raw);
    }

    private string EscapeJson(string text)
    {
        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }

    private string UnescapeJson(string text)
    {
        return text
            .Replace("\\n", "\n")
            .Replace("\\r", "\r")
            .Replace("\\t", "\t")
            .Replace("\\\"", "\"")
            .Replace("\\\\", "\\");
    }
}
