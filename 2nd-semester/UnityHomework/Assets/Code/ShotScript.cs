using UnityEngine;

/// <summary>
/// Manage shot behavior
/// </summary>
public class ShotScript : MonoBehaviour
{
    /// <summary>
    /// Damage inflicted
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile damage player or enemies?
    /// </summary>
    public bool isEnemyShot = false;

    public void Start()
    {
        Destroy(gameObject, 10);
    }
}
