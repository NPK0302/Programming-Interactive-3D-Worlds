using UnityEngine;
using System.Collections.Generic;

public class PlayerFlashlight : MonoBehaviour
{
    // Reference to the player's camera (or flashlight transform if different)
    public Camera playerCamera;

    // How far the flashlight beam can reach
    public float flashlightRange = 20f;

    // The angle of the flashlight beam (like a cone)
    public float flashlightAngle = 30f;

    // A list of all cubes in the scene (you can assign dynamically at runtime too)
    public List<MovingCube> cubes = new List<MovingCube>();

    void Update()
    {
        // For each cube in the scene, we’ll check if it’s lit by the flashlight
        foreach (MovingCube cube in cubes)
        {
            if (cube == null) continue;

            // Direction from flashlight/camera to the cube
            Vector3 dirToCube = (cube.transform.position - transform.position).normalized;

            // Angle between flashlight forward direction and direction to cube
            float angle = Vector3.Angle(transform.forward, dirToCube);

            // If cube is within the cone of the flashlight beam
            if (angle < flashlightAngle)
            {
                // Perform a raycast to check if the cube is actually visible
                if (Physics.Raycast(transform.position, dirToCube, out RaycastHit hit, flashlightRange))
                {
                    // If the raycast hits THIS cube, then it’s lit
                    if (hit.collider.gameObject == cube.gameObject)
                    {
                        cube.SetLit(true);
                        continue;
                    }
                }
            }

            // If not lit, mark as unlit
            cube.SetLit(false);
        }
    }
}
