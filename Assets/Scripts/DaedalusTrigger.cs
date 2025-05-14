using UnityEngine;

public class DaedalusTrigger : MonoBehaviour
{
    public DialogueController dialogueController;

    [Header("Portraits")]
    public Sprite icarusNeutralPortrait;
    public Sprite icarusPanickedPortrait;

    [Header("Voice Clips – Discovery")]
    public AudioClip fatherAudio;
    public AudioClip noAudio;
    public AudioClip disbeliefAudio;
    public AudioClip monsterAudio;

    [Header("Voice Clips – Map Realisation")]
    public AudioClip noRepeatAudio;
    public AudioClip bloodAudio;
    public AudioClip screamAudio;
    public AudioClip panicAudio;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogueController.StartDialogue(GetFullDaedalusSequence());
        }
    }

    DialogueLine[] GetFullDaedalusSequence()
    {
        return new DialogueLine[]
        {
            // --- Scene 1: Discovery of the body ---
            new DialogueLine
            {
                text = "Father...",
                portrait = icarusNeutralPortrait,
                voiceLine = fatherAudio
            },
            new DialogueLine
            {
                text = "No...",
                portrait = icarusNeutralPortrait,
                voiceLine = noAudio
            },
            new DialogueLine
            {
                text = "I can't believe you're really dead.",
                portrait = icarusNeutralPortrait,
                voiceLine = disbeliefAudio
            },
            new DialogueLine
            {
                text = "I need to get out of here before I get caught by that monster.",
                portrait = icarusNeutralPortrait,
                voiceLine = monsterAudio
            },
            new DialogueLine
            {
                text = "*look for map*",
                portrait = icarusNeutralPortrait,
                voiceLine = noRepeatAudio
            },

            // --- Scene 2: Finding the map covered in blood ---
            new DialogueLine
            {
                text = "No, no no no no",
                portrait = icarusPanickedPortrait,
                voiceLine = noRepeatAudio
            },
            new DialogueLine
            {
                text = "It’s covered in blood.",
                portrait = icarusPanickedPortrait,
                voiceLine = bloodAudio
            },
            new DialogueLine
            {
                text = "HOW WILL I GET OUT NOW!",
                portrait = icarusPanickedPortrait,
                voiceLine = screamAudio
            },
            new DialogueLine
            {
                text = "I need to keep moving, I can’t get caught here.",
                portrait = icarusPanickedPortrait,
                voiceLine = panicAudio
            }
        };
    }
}
