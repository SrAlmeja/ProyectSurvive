using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = transform.parent.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.DestroyBullet();
        }
        else
        {
            Debug.LogWarning("Bullet component not found in parent object.");
        }
    }
}