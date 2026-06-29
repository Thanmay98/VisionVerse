using UnityEngine;
using UnityEngine.Windows.Speech;
using System;

/// <summary>
/// Captures microphone speech and converts it to text using Unity's built-in
/// Windows Speech Recognition (DictationRecognizer). Windows 10/11 only.
/// </summary>
public class VoiceInputHandler : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;

    public bool IsListening { get; private set; } = false;

    // Events the AIAssistantManager will subscribe to
    public event Action<string> OnFinalResult;      // Called when user finishes speaking
    public event Action<string> OnHypothesis;        // Called continuously while speaking (live preview)
    public event Action<string> OnError;             // Called if something goes wrong
    public event Action OnListeningStopped;          // Called when listening ends (timeout or completed)

    /// <summary>
    /// Call this once before first use (e.g. in Start) to check platform support.
    /// </summary>
    public bool IsSupported()
    {
        // Dictation/Phrase recognition is Windows 10/11 only.
        return Application.platform == RuntimePlatform.WindowsPlayer
            || Application.platform == RuntimePlatform.WindowsEditor;
    }

    /// <summary>
    /// Start listening to the microphone. Call this when the user presses the mic button.
    /// </summary>
    public void StartListening()
    {
        if (!IsSupported())
        {
            OnError?.Invoke("Voice input is only supported on Windows.");
            return;
        }

        if (IsListening)
        {
            Debug.LogWarning("Already listening.");
            return;
        }

        try
        {
            dictationRecognizer = new DictationRecognizer();

            dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.Log($"Dictation result: {text} (confidence: {confidence})");
                OnFinalResult?.Invoke(text);
            };

            dictationRecognizer.DictationHypothesis += (text) =>
            {
                OnHypothesis?.Invoke(text);
            };

            dictationRecognizer.DictationComplete += (completionCause) =>
            {
                IsListening = false;
                OnListeningStopped?.Invoke();

                if (completionCause != DictationCompletionCause.Complete)
                {
                    Debug.LogWarning($"Dictation completed unsuccessfully: {completionCause}");

                    // Give a friendlier message for the most common cause: silence timeout
                    if (completionCause == DictationCompletionCause.TimeoutExceeded)
                    {
                        OnError?.Invoke("Didn't hear anything. Tap the mic and try again.");
                    }
                    else
                    {
                        OnError?.Invoke($"Voice recognition stopped: {completionCause}");
                    }
                }
            };

            dictationRecognizer.DictationError += (error, hresult) =>
            {
                IsListening = false;
                Debug.LogError($"Dictation error: {error}; HResult = {hresult}");
                OnError?.Invoke($"Voice recognition error: {error}");
                OnListeningStopped?.Invoke();
            };

            dictationRecognizer.Start();
            IsListening = true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to start dictation: {e.Message}");
            OnError?.Invoke("Could not start voice recognition. Make sure Windows Speech Recognition is set up (Settings > Time & Language > Speech).");
            IsListening = false;
        }
    }

    /// <summary>
    /// Stop listening manually (e.g. user taps mic button again to cancel).
    /// </summary>
    public void StopListening()
    {
        if (dictationRecognizer != null && IsListening)
        {
            dictationRecognizer.Stop();
            IsListening = false;
        }
    }

    private void OnDestroy()
    {
        if (dictationRecognizer != null)
        {
            dictationRecognizer.Dispose();
            dictationRecognizer = null;
        }
    }

    private void OnApplicationQuit()
    {
        if (dictationRecognizer != null)
        {
            dictationRecognizer.Dispose();
            dictationRecognizer = null;
        }
    }
}
