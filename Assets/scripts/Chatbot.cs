using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Chatbot : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text responseText;

    void Start()
    {
        responseText.text = "Hello!\n\nI'm VisionVerse Assistant.\nAsk me anything about eye health or vision.";
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
            SendMessage();
    }

    public void SendMessage()
    {
        string question = inputField.text.Trim();

        if (string.IsNullOrEmpty(question))
        {
            responseText.text = "Please enter a question.";
            inputField.ActivateInputField();
            return;
        }

        string q = question.ToLower();
        string answer;

        if (q.Contains("myopia") || q.Contains("short sight"))
            answer = "Myopia (short-sightedness) makes distant objects appear blurry.";
        else if (q.Contains("hyperopia") || q.Contains("long sight"))
            answer = "Hyperopia (long-sightedness) makes nearby objects appear blurry.";
        else if (q.Contains("presbyopia"))
            answer = "Presbyopia is an age-related difficulty focusing on nearby objects.";
        else if (q.Contains("astigmatism"))
            answer = "Astigmatism is caused by an irregular cornea or lens and causes blurred vision.";
        else if (q.Contains("color blindness") || q.Contains("colour blindness"))
            answer = "Color blindness is difficulty distinguishing certain colors, commonly red and green.";
        else if (q.Contains("cataract"))
            answer = "A cataract clouds the eye's natural lens and causes blurry vision.";
        else if (q.Contains("glaucoma"))
            answer = "Glaucoma damages the optic nerve, often due to increased eye pressure.";
        else if (q.Contains("conjunctivitis") || q.Contains("pink eye"))
            answer = "Conjunctivitis is inflammation of the conjunctiva causing redness and irritation.";
        else if (q.Contains("retina"))
            answer = "The retina is the light-sensitive layer that sends visual signals to the brain.";
        else if (q.Contains("cornea"))
            answer = "The cornea is the clear front surface of the eye that focuses light.";
        else if (q.Contains("iris"))
            answer = "The iris is the colored part of the eye that controls pupil size.";
        else if (q.Contains("pupil"))
            answer = "The pupil is the opening that lets light into the eye.";
        else if (q.Contains("lens"))
            answer = "The lens focuses light onto the retina.";
        else if (q.Contains("optic nerve"))
            answer = "The optic nerve carries visual information from the eye to the brain.";
        else if (q.Contains("eye care"))
            answer = "Have regular eye checkups, eat healthy food, wear sunglasses and rest your eyes.";
        else if (q.Contains("screen time"))
            answer = "Long screen time may cause eye strain. Take regular breaks.";
        else if (q.Contains("20-20-20"))
            answer = "Every 20 minutes, look 20 feet away for 20 seconds.";
        else if (q.Contains("blue light"))
            answer = "Blue light may contribute to eye strain during prolonged screen use.";
        else if (q.Contains("eye exercise"))
            answer = "Blink often and look into the distance regularly to relax your eyes.";
        else if (q.Contains("healthy food") || q.Contains("foods") || q.Contains("diet"))
            answer = "Leafy greens, carrots, fish, eggs, citrus fruits and nuts support eye health.";
        else if (q.Contains("carrot"))
            answer = "Carrots provide vitamin A, which supports healthy vision.";
        else if (q.Contains("sleep"))
            answer = "Getting 7–9 hours of sleep helps reduce eye strain.";
        else if (q.Contains("eye strain"))
            answer = "Eye strain can cause headaches, tired eyes and blurred vision.";
        else if (q.Contains("glasses"))
            answer = "Prescription glasses correct refractive errors such as myopia and hyperopia.";
        else if (q.Contains("contact"))
            answer = "Clean contact lenses properly and follow your eye care professional's instructions.";
        else if (q.Contains("symptom"))
            answer = "Common symptoms include blurred vision, headaches, double vision and eye strain.";
        else if (q.Contains("treatment"))
            answer = "Treatment depends on the condition and may include glasses, contact lenses, medication or surgery.";
        else if (q.Contains("eye"))
            answer = "The eye detects light and sends visual information to the brain.";
        else
            answer = "Sorry, I can answer questions about eye conditions, eye anatomy, symptoms, treatments and eye care.";

        responseText.text =
            "<b>You:</b> " + question +
            "\n\n<b>VisionVerse Assistant:</b>\n" + answer;

        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
    }
}
