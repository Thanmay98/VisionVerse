using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIAssistantManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject aiChatPanel;
    public Button micButton;                      // Tap to start/stop listening
    public TextMeshProUGUI micButtonLabel;         // Shows "🎤 Tap to Speak" / "🔴 Listening..."
    public TextMeshProUGUI responseDisplayText;
    public TextMeshProUGUI statusText;             // Shows status messages (listening, thinking, error)
    public ScrollRect responseScrollRect;
    public Button closeButton;

    [Header("AI / Voice References")]
    private OllamaAIHelper ollamaHelper;
    private ConditionContextManager contextManager;
    private VoiceInputHandler voiceHandler;

    private bool isWaitingForResponse = false;
    private string currentCondition = "normal";

    private void Start()
    {
        // Get or add required components
        ollamaHelper = GetComponent<OllamaAIHelper>();
        if (ollamaHelper == null) ollamaHelper = gameObject.AddComponent<OllamaAIHelper>();

        voiceHandler = GetComponent<VoiceInputHandler>();
        if (voiceHandler == null) voiceHandler = gameObject.AddComponent<VoiceInputHandler>();

        contextManager = ConditionContextManager.Instance;
        if (contextManager == null)
            Debug.LogError("ConditionContextManager not found in scene!");

        // UI listeners
        if (micButton != null)
            micButton.onClick.AddListener(OnMicButtonClicked);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClicked);

        // Ollama listeners
        ollamaHelper.ResponseReceived += OnOllamaResponseReceived;
        ollamaHelper.ErrorOccurred += OnOllamaError;

        // Voice listeners
        voiceHandler.OnFinalResult += OnVoiceFinalResult;
        voiceHandler.OnHypothesis += OnVoiceHypothesis;
        voiceHandler.OnError += OnVoiceError;
        voiceHandler.OnListeningStopped += OnVoiceListeningStopped;

        ollamaHelper.CheckServerStatus();

        if (aiChatPanel != null)
            aiChatPanel.SetActive(false);
    }

    /// <summary>
    /// Show the AI chat panel (called from "Ask AI" button in Classroom)
    /// </summary>
    public void ShowAIChatPanel()
    {
        if (aiChatPanel != null)
        {
            aiChatPanel.SetActive(true);
            ClearChat();

            if (!voiceHandler.IsSupported())
            {
                SetStatus("⚠ Voice input only works on Windows builds.");
            }
            else
            {
                SetStatus("Tap the mic and ask your question.");
            }
        }
    }

    public void HideAIChatPanel()
    {
        if (voiceHandler != null && voiceHandler.IsListening)
            voiceHandler.StopListening();

        if (aiChatPanel != null)
            aiChatPanel.SetActive(false);
    }

    public void SetCondition(string condition)
    {
        currentCondition = condition;
    }

    private void OnMicButtonClicked()
    {
        if (isWaitingForResponse)
        {
            return; // Don't allow new questions while AI is answering
        }

        if (voiceHandler.IsListening)
        {
            // Tapped again while listening = cancel
            voiceHandler.StopListening();
            SetMicButtonState(false);
            SetStatus("Tap the mic and ask your question.");
        }
        else
        {
            voiceHandler.StartListening();
            SetMicButtonState(true);
            SetStatus("🔴 Listening...");
        }
    }

    private void SetMicButtonState(bool listening)
    {
        if (micButtonLabel != null)
            micButtonLabel.text = listening ? "🔴 Listening..." : "🎤 Tap to Speak";
    }

    /// <summary>
    /// Called continuously while the user is speaking - live preview of what's being heard
    /// </summary>
    private void OnVoiceHypothesis(string partialText)
    {
        SetStatus($"🎤 \"{partialText}...\"");
    }

    /// <summary>
    /// Called when the user finishes speaking and a final transcription is ready
    /// </summary>
    private void OnVoiceFinalResult(string finalText)
    {
        if (string.IsNullOrWhiteSpace(finalText))
            return;

        AskQuestion(finalText);
    }

    private void OnVoiceListeningStopped()
    {
        SetMicButtonState(false);
    }

    private void OnVoiceError(string error)
    {
        SetMicButtonState(false);
        SetStatus($"⚠ {error}");
    }

    /// <summary>
    /// Sends the recognized question to the AI. Public so it can also be wired
    /// to a button manually if needed for testing without voice.
    /// </summary>
    public void AskQuestion(string question)
    {
        if (isWaitingForResponse)
        {
            Debug.LogWarning("Already waiting for a response. Please wait.");
            return;
        }

        if (responseDisplayText != null)
        {
            responseDisplayText.text = $"<color=#4A90E2><b>You asked:</b></color> {question}\n\n";
        }

        SetStatus("⏳ AI is thinking...");

        string context = contextManager != null
            ? contextManager.GetConditionContext(currentCondition)
            : "";

        isWaitingForResponse = true;
        if (micButton != null)
            micButton.interactable = false;

        ollamaHelper.AskOllama(question, context);
    }

    private void OnOllamaResponseReceived(string response)
    {
        isWaitingForResponse = false;

        if (micButton != null)
            micButton.interactable = true;

        SetStatus("Tap the mic to ask another question.");

        if (responseDisplayText != null)
        {
            string sources = contextManager != null
                ? contextManager.GetSourceLinks(currentCondition)
                : "";

            responseDisplayText.text += $"<color=#27AE60><b>AI Assistant:</b></color> {response}\n\n<size=60%><color=#888888>{sources}</color></size>";

            if (responseScrollRect != null)
            {
                Canvas.ForceUpdateCanvases();
                responseScrollRect.verticalNormalizedPosition = 0;
            }
        }
    }

    private void OnOllamaError(string error)
    {
        isWaitingForResponse = false;

        if (micButton != null)
            micButton.interactable = true;

        SetStatus($"⚠ {error}");

        if (responseDisplayText != null)
        {
            responseDisplayText.text += $"\n<color=#E74C3C><b>Error:</b> {error}</color>\n";
        }
    }

    private void OnCloseButtonClicked()
    {
        HideAIChatPanel();
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }

    private void ClearChat()
    {
        if (responseDisplayText != null)
            responseDisplayText.text = "Ask any question about eye conditions and vision. Tap the mic to speak!\n\n";

        SetMicButtonState(false);
    }

    private void OnDestroy()
    {
        if (ollamaHelper != null)
        {
            ollamaHelper.ResponseReceived -= OnOllamaResponseReceived;
            ollamaHelper.ErrorOccurred -= OnOllamaError;
        }

        if (voiceHandler != null)
        {
            voiceHandler.OnFinalResult -= OnVoiceFinalResult;
            voiceHandler.OnHypothesis -= OnVoiceHypothesis;
            voiceHandler.OnError -= OnVoiceError;
            voiceHandler.OnListeningStopped -= OnVoiceListeningStopped;
        }
    }
}
