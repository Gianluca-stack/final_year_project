using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        // Find all audio listeners in the scene
        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();

        // If there are more than one audio listener, remove the extras
        if (audioListeners.Length > 1)
        {
            Debug.LogWarning("More than one audio listener found in the scene. Removing extras.");

            // Keep the first audio listener and remove the extras
            for (int i = 1; i < audioListeners.Length; i++)
            {
                Destroy(audioListeners[i]);
            }
        }
        // If there are no audio listeners, add one to the main camera
        else if (audioListeners.Length == 0)
        {
            Debug.LogWarning("No audio listener found in the scene. Adding one.");

            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
            }
            else
            {
                Debug.LogError("No main camera found in the scene. Unable to add audio listener.");
            }
        }
    }
}