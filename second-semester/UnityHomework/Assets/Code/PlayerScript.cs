using UnityEngine;

/// <summary>
/// Manage player behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// Player speed
    /// </summary>
    public Vector2 speed = new Vector2(50, 50);

    /// <summary>
    /// Player movement
    /// </summary>
    private Vector2 movement;

    /// <summary>
    /// Rigidboidy component
    /// </summary>
    private Rigidbody2D rigidbodyComponent;

    /// <summary>
    /// Update player state
    /// </summary>
    public void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack(false);
            }
        }

        Borders();
    }

    /// <summary>
    /// Make sure player in scene borders
    /// </summary>
    public void Borders()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint( new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
          );
    }

    /// <summary>
    /// Fixed update
    /// </summary>
    public void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;
    }

    /// <summary>
    /// On player death
    /// </summary>
    public void OnDestroy()
    {
        var gameOver = FindObjectOfType<GameOverScript>();
        gameOver.ShowButtons();
    }

}
