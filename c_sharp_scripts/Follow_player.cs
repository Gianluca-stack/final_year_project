using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;
    public Transform petals;
    private float yOffset; // Vertical offset between petals and player

    // Start is called before the first frame update
    void Start()
    {
        // Get references to player and petals
        player = GameObject.FindGameObjectWithTag("player_vr").transform;
        petals = GameObject.FindGameObjectWithTag("petals").transform;

        // Calculate the vertical (Y-axis) offset between the player and the petals
        yOffset = petals.position.y - player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the petals to maintain the same horizontal position as the player
        Vector3 newPosition = player.position;
        newPosition.y += yOffset; // Apply the vertical offset
        petals.position = newPosition;
    }

}
