using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;


public class OllamaAIHelper : MonoBehaviour
{
    private string ollamaUrl = "http://localhost:11434/api/generate";
    private string model = "phi3:mini";
    
    public delegate void OnResponseReceived(string response);
    public event OnResponseReceived ResponseReceived;
    
    public delegate void OnError(string error);
    public event OnError ErrorOccurred;

    /// <summary>
    /// Send a question to Ollama and get response
    /// </summary>
    public void AskOllama(string question, string conditionContext = "")
    {
        StartCoroutine(SendToOllama(question, conditionContext));
    }

    private IEnumerator SendToOllama(string question, string conditionContext)
    {
        // Build the prompt with context
        string fullPrompt = question;
        if (!string.IsNullOrEmpty(conditionContext))
        {
            fullPrompt = $"Context: {conditionContext}\n\nQuestion: {question}";
        }

        // Create request JSON
        string jsonData = JsonUtility.ToJson(new OllamaRequest
        {
            model = model,
            prompt = fullPrompt,
            stream = false,
            temperature = 0.7f
        });

        using (UnityWebRequest request = new UnityWebRequest(ollamaUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 120; // 2 minutes timeout for long responses

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                OllamaResponse response = JsonUtility.FromJson<OllamaResponse>(responseText);
                
                if (ResponseReceived != null)
                {
                    ResponseReceived(response.response);
                }
            }
            else
            {
                string error = $"Error: {request.error}";
                Debug.LogError(error);
                
                if (ErrorOccurred != null)
                {
                    ErrorOccurred(error);
                }
            }
        }
    }

    /// <summary>
    /// Check if Ollama server is running
    /// </summary>
    public void CheckServerStatus()
    {
        StartCoroutine(CheckStatus());
    }

    private IEnumerator CheckStatus()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:11434/api/tags"))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Ollama server is running and Mistral is available");
            }
            else
            {
                Debug.LogError("❌ Ollama server is not running. Please run 'ollama serve' in Command Prompt");
                if (ErrorOccurred != null)
                {
                    ErrorOccurred("Ollama server not running. Please start it with: ollama serve");
                }
            }
        }
    }
}

[System.Serializable]
public class OllamaRequest
{
    public string model;
    public string prompt;
    public bool stream;
    public float temperature;
}

[System.Serializable]
public class OllamaResponse
{
    public string response;
    public bool done;
    public long created_at;
}
