using UnityEngine;

public class MovingCube : MonoBehaviour
{
    // Reference to the player (the camera/player that cubes move toward)
    public Transform player;

    // Movement speed of the cube when unlit
    public float moveSpeed = 2f;

    // How many seconds of flashlight exposure are required to destroy the cube
    public float timeToDestroy = 3f;

    // Tracks how long this cube has been lit continuously
    private float litTimer = 0f;

    // Internal state: is the cube currently lit by the flashlight?
    private bool isLit = false;

    void Start()
    {
        // If no player assigned, default to the main camera
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (isLit)
        {
            // If lit, increase the timer by the time passed since last frame
            litTimer += Time.deltaTime;

            // If the cube has been lit long enough, destroy it
            if (litTimer >= timeToDestroy)
            {
                Destroy(gameObject); // removes the cube from the scene
            }
        }
        else
        {
            // If not lit, reset the timer back to 0
            litTimer = 0f;

            // Continue moving toward the player
            if (player != null)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    // Called from PlayerFlashlight.cs to set the lighting state
    public void SetLit(bool lit)
    {
        isLit = lit;
    }
}
