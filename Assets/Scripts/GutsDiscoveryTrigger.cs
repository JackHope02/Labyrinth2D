using UnityEngine;

public class GutsDiscoveryTrigger : MonoBehaviour
{
    public DialogueController dialogueController;

    [Header("Dialogue Content")]
    public Sprite icarusPortrait;
    public AudioClip gutsAudio;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogueController.StartDialogue(GetGutsDialogue());
        }
    }

    DialogueLine[] GetGutsDialogue()
    {
        return new DialogueLine[]
        {
            new DialogueLine
            {
                text = "ugh, that's disgusting. I really hope that's not a person’s..",
                portrait = icarusPortrait,
                voiceLine = gutsAudio
            },
            new DialogueLine
            {
                text = "it reeks.",
                portrait = icarusPortrait,
                voiceLine = null // optional second line
            }
        };
    }
}
