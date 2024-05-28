using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerVR : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject player;

    private Vector3 respawnLocation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player_vr"); // find player in scene

        spawnLocation = GameObject.FindGameObjectWithTag("SpawnPoint"); // find spawn point in scene

        respawnLocation = player.transform.position; // set respawn location to player's initial position

        MovePlayerToSpawnPoint(); // Move player to spawn point
        
    }

    private void MovePlayerToSpawnPoint()
    {
        if (player != null && spawnLocation != null)
        {
            player.transform.position = spawnLocation.transform.position; // Move player to spawn point
            // rotate 180 degrees to the left
            // get current scene name 
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if(sceneName == "Home"){
                // turn 90 degrees to the left
                player.transform.Rotate(0, 90, 0);
            }else if(sceneName == "Nature's Discovery"){
                // turn 180 degrees to the left
                player.transform.Rotate(0, 180, 0);
            }

        }
        else
        {
            Debug.LogError("Player or spawn location not found.");
        }
    }
}