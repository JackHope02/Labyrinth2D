using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialogueUI;
    public Image portraitImage;
    public Text dialogueText;
    public AudioSource voiceSource; // Optional — can be null

    [Header("Settings")]
    public float typeSpeed = 0.02f;

    private DialogueLine[] lines;
    private int currentIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0) && !isTyping)
        {
            currentIndex++;
            if (currentIndex < lines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        if (newLines == null || newLines.Length == 0) return;

        lines = newLines;
        currentIndex = 0;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        DialogueLine line = lines[currentIndex];

        // Handle portrait
        if (line.portrait != null)
        {
            portraitImage.sprite = line.portrait;
            portraitImage.enabled = true;
        }
        else
        {
            portraitImage.enabled = false;
        }

        // Stop any playing voice
        if (voiceSource != null && voiceSource.isPlaying)
            voiceSource.Stop();

        // Start typing text (voice plays during typing)
        StopAllCoroutines();
        StartCoroutine(TypeText(line.text, line.voiceLine));
    }

    IEnumerator TypeText(string text, AudioClip voiceLine)
    {
        isTyping = true;
        dialogueText.text = "";

        // Play voice line if available and source assigned
        if (voiceLine != null && voiceSource != null)
        {
            voiceSource.clip = voiceLine;
            voiceSource.Play();
        }

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // Optionally stop voice line at end of text (optional)
        if (voiceSource != null && voiceSource.isPlaying)
        {
            voiceSource.Stop();
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);

        if (voiceSource != null && voiceSource.isPlaying)
            voiceSource.Stop();
    }
}
