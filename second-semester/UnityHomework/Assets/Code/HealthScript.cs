using UnityEngine;

/// <summary>
/// Manage hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    public bool isEnemy = true;

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount">damage count</param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            if (isEnemy)
            {
                gameObject.transform.position = new Vector3(Random.Range(6, 9), Random.Range(-2, 7), 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// On bullet hit
    /// </summary>
    /// <param name="otherCollider"></param>
    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
