using UnityEngine;

/// <summary>
/// Manage weapon behavior
/// </summary>
public class WeaponScript : MonoBehaviour
{
    /// <summary>
    /// Shot prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    /// <summary>
    /// Starting shot cooldown
    /// </summary>
    private float shootCooldown;

    /// <summary>
    /// Start
    /// </summary>
    public void Start()
    {
        shootCooldown = 0f;
    }

    /// <summary>
    /// Update object state
    /// </summary>
    public void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Create a new shot if possible
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;
            shotTransform.position = transform.position;
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();

            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right;
            }
        }
    }

    /// <summary>
    /// Checks whether the weapon ready to create a new shot
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
