using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_behaviour : MonoBehaviour
{

    public GameObject vrRig;

    // variable to store each tree's distance from the player
    public float distance;

    // 2D array to store the distances of all the trees from the player and index
    public string[,] distance_data;

    public string treeName;

    // Vector3 variable to store the position of the tree
    public Vector3 world_treePosition;

    public Vector3 midpoint;
    public Vector3 final_midpoint;

    public TreePrototype[] treePrototypes;

    public string tree_name;
    
    public Vector3 closest_treeposition(){

        Terrain terrain = GetComponent<Terrain>();

        // Get all the tree instances on the terrain
        TreeInstance[] trees = terrain.terrainData.treeInstances;
        // for each tree instance, print position and prototypeIndex
        int treeCount = trees.Length;

        // Get all the tree prototypes
        treePrototypes = terrain.terrainData.treePrototypes;

        distance_data = new string[treeCount, 3];

        for(int i = 0; i<treeCount; i++){
            TreeInstance tree = trees[i];
            int prototypeIndex = tree.prototypeIndex;
            // Get the tree position
            Vector3 tree_pos = Vector3.Scale(tree.position, terrain.terrainData.size);
            // calculate the distance between the tree and the player
            distance = CalculateDistance(tree_pos, vrRig.transform.position);
            // convert the distance to a string
            string distanceString = distance.ToString();

            // store the distance in the distances array and the tree position
            distance_data[i, 0] = distanceString;
            distance_data[i, 1] = tree.position.ToString();
            // store propertyIndex
            distance_data[i, 2] = prototypeIndex.ToString();

            if(i == (treeCount - 1)){
                // find the minimum distance in the distances array and store the data including distance and tree position and prototypeIndex
                string[] closest_tree_data = FindClosestTree(distance_data);
                // convert the tree position string to a Vector3
                world_treePosition = StringToVector3(closest_tree_data[1]);
                world_treePosition = Vector3.Scale(world_treePosition, terrain.terrainData.size);
                // store the tree name
                tree_name = treePrototypes[int.Parse(closest_tree_data[2])].prefab.name;
                // insert the tree name into the closest_tree_data array
                
            }
            
        }

        return world_treePosition;
    }

    // Find the tree with the minimum distance from the player
    string[] FindClosestTree(string[,] distance_data)
    {
        // Initialize the minimum distance to a large value
        float minDistance = 1000000;
        // Initialize the index of the tree with the minimum distance
        int minIndex = 0;

        // Loop through the distances array
        for (int i = 0; i < distance_data.GetLength(0); i++)
        {
            // Get the distance from the distances array
            float distance = float.Parse(distance_data[i, 0]);

            // Check if the current distance is less than the minimum distance
            if (distance < minDistance)
            {
                // Update the minimum distance
                minDistance = distance;
                // Update the index of the tree with the minimum distance
                minIndex = i;
            }
        }

        // Return the data of the tree with the minimum distance
        return new string[] { distance_data[minIndex, 0], distance_data[minIndex, 1], distance_data[minIndex, 2]};
    }

    // Calculate distance between tree and player
    float CalculateDistance(Vector3 treePosition, Vector3 playerPosition)
    {
        return Vector3.Distance(treePosition, playerPosition);
    }
    Vector3 StringToVector3(string s)
    {
        // Remove the parentheses and split the string by commas
        string[] sArray = s.Replace("(", "").Replace(")", "").Split(',');

        // Convert each component from string to float
        float x = float.Parse(sArray[0]);
        float y = float.Parse(sArray[1]);
        float z = float.Parse(sArray[2]);

        // Return the Vector3
        return new Vector3(x, y, z);
    }

}
