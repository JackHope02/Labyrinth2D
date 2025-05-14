using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class TriggerCutscene : MonoBehaviour
{
    [Header("Cutscene Type")]
    public Sprite cutsceneImage;
    public VideoClip cutsceneVideo;

    [Header("Cutscene Display References")]
    public Image cutsceneImageDisplay;
    public RawImage cutsceneVideoDisplay;
    public VideoPlayer cutsceneVideoPlayer;
    public RenderTexture cutsceneRenderTexture;

    [Header("Player Control")]
    public GameObject player;
    public MonoBehaviour playerMovementScript;

    [Header("After Cutscene")]
    public UnityEvent onCutsceneEnd;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCutscene();
        }
    }

    void StartCutscene()
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        if (cutsceneVideo != null)
        {
            cutsceneImageDisplay.gameObject.SetActive(false);
            cutsceneVideoDisplay.gameObject.SetActive(true);
            cutsceneVideoPlayer.clip = cutsceneVideo;
            cutsceneVideoPlayer.targetTexture = cutsceneRenderTexture;
            cutsceneVideoPlayer.loopPointReached += EndCutscene;
            cutsceneVideoPlayer.Play();
        }
        else if (cutsceneImage != null)
        {
            cutsceneVideoDisplay.gameObject.SetActive(false);
            cutsceneImageDisplay.sprite = cutsceneImage;
            cutsceneImageDisplay.gameObject.SetActive(true);
            Invoke("EndCutscene", 4f); // Hold image for 4 seconds
        }
        else
        {
            EndCutscene(); // No media? Skip cutscene
        }
    }

    void EndCutscene(VideoPlayer vp) => EndCutscene();

    void EndCutscene()
    {
        cutsceneImageDisplay.gameObject.SetActive(false);
        cutsceneVideoDisplay.gameObject.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        onCutsceneEnd?.Invoke(); // Trigger additional outcome
    }
}
