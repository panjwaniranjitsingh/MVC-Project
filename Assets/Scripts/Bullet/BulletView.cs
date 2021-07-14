using UnityEngine;

public class BulletView : MonoBehaviour
{
    public BulletController bulletController;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        //GameObject bulletEffect = Instantiate(BulletExplosion, transform.position, transform.rotation);
        //Destroy(bulletEffect, 1f);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        //Debug.Log("Damageable=" + damageable);
        if (damageable != null)
        {
            damageable.TakeDamage(bulletController.GetDamage());
        }
        bulletController.DestroyBullet();
    }

    internal void Destroy()
    {
        bulletController = null;
        Destroy(gameObject);
    }
}
