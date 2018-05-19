using UnityEngine;

/// <summary>
/// Manage enemy  behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    /// <summary>
    /// Move script
    /// </summary>
    private MoveScript moveScript;

    /// <summary>
    /// Weapon components
    /// </summary>
    private WeaponScript[] weapons;

    /// <summary>
    /// Colider component
    /// </summary>
    private Collider2D coliderComponent;

    /// <summary>
    /// Renderer component
    /// </summary>
    private SpriteRenderer rendererComponent;

    /// <summary>
    /// Awake
    /// </summary>
    public void Awake()
    {
        weapons = GetComponentsInChildren<WeaponScript>();
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Update object state
    /// </summary>
    public void Update()
    {
        foreach (WeaponScript weapon in weapons)
        {
            if (weapon != null && weapon.enabled && weapon.CanAttack)
            {
                weapon.Attack(true);
            }
        }

        if (rendererComponent.IsVisibleFrom(Camera.main) == false)
        {
            gameObject.transform.position = new Vector3(Random.Range(6, 9), Random.Range(-2, 7), 0);
        }
    }
}
