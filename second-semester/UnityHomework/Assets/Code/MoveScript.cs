using UnityEngine;

/// <summary>
/// Manage object movement
/// </summary>
public class MoveScript : MonoBehaviour
{
    /// <summary>
    /// Object speed
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Movement
    /// </summary>
    private Vector2 movement;

    /// <summary>
    /// Rigidbody component
    /// </summary>
    private Rigidbody2D rigidbodyComponent;

    /// <summary>
    /// Update object state
    /// </summary>
    public void Update()
    {
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    /// <summary>
    /// Fixed update
    /// </summary>
    public void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = movement;
    }
}
