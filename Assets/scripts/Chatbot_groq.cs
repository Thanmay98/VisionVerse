using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chatbot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    public TMP_InputField inputField;
    public TMP_Text responseText;
    public ScrollRect scrollRect;       // Assign the ScrollRect wrapping responseText
    public Button sendButton;           // Assign Send button (optional but recommended)

    [Header("AI Reference")]
    public GroqAIHelper groqHelper;     // Assign the GroqAIHelper component

    private bool isWaiting = false;

    void Start()
    {
        responseText.text = "Hello!\n\nI'm VisionVerse Assistant.\nAsk me anything about eye health or vision.";

        // Wire Groq events
        if (groqHelper != null)
        {
            groqHelper.ResponseReceived += OnResponseReceived;
            groqHelper.ErrorOccurred += OnErrorReceived;
        }
        else
        {
            Debug.LogError("GroqAIHelper not assigned on Chatbot script!");
        }

        // Wire send button if assigned
        if (sendButton != null)
            sendButton.onClick.AddListener(SendMessage);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
            SendMessage();
    }

    public void SendMessage()
    {
        if (isWaiting) return;

        string question = inputField.text.Trim();

        if (string.IsNullOrEmpty(question))
        {
            responseText.text = "Please enter a question.";
            inputField.ActivateInputField();
            return;
        }

        // Show waiting state
        isWaiting = true;
        responseText.text = "<b>You:</b> " + question + "\n\n<b>VisionVerse Assistant:</b>\n⏳ Thinking...";

        if (sendButton != null)
            sendButton.interactable = false;

        inputField.text = "";
        inputField.ActivateInputField();

        // Send to Groq
        groqHelper.AskAI(question);
    }

    private void OnResponseReceived(string response)
    {
        isWaiting = false;

        if (sendButton != null)
            sendButton.interactable = true;

        responseText.text = responseText.text.Replace("⏳ Thinking...", response);

        // Scroll to top so user reads from the beginning of the answer
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    private void OnErrorReceived(string error)
    {
        isWaiting = false;

        if (sendButton != null)
            sendButton.interactable = true;

        responseText.text = "<b>You:</b> Question\n\n<color=#E74C3C><b>Error:</b> " + error + "</color>";
        Debug.LogError("Groq error: " + error);
    }

    // Detects clicks on <link> tags for any source links
    public void OnPointerClick(PointerEventData eventData)
    {
        if (responseText == null) return;

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(
            responseText, eventData.position, eventData.pressEventCamera);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = responseText.textInfo.linkInfo[linkIndex];
            string url = linkInfo.GetLinkID();
            if (!string.IsNullOrEmpty(url))
                Application.OpenURL(url);
        }
    }

    private void OnDestroy()
    {
        if (groqHelper != null)
        {
            groqHelper.ResponseReceived -= OnResponseReceived;
            groqHelper.ErrorOccurred -= OnErrorReceived;
        }
    }
}
