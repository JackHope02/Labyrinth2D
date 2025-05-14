using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [Tooltip("Name of the scene to load")]
    public string targetSceneName = "EndScene";

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
