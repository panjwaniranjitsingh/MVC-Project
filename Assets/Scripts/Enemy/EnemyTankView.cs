using UnityEngine.UI;
using UnityEngine;
using System;

public class EnemyTankView : MonoBehaviour,IDamageable
{
    public EnemyTankController tankController;
    public Rigidbody tankRigidbody;
    private float timeElapsed;
    [SerializeField]private float bulletFireCounter = 1f;
    [SerializeField] Image HealthBar;
    public Transform bulletFirePos;
    public BulletView bulletPrefab;
    [SerializeField] BulletScriptableObjectList bulletSOL;

    void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        
    }
    
    void Start()
    {
        Debug.Log("Enemy Tank created");
        
    }

    void FixedUpdate()
    {
        Vector3 playerPos = TankService.GetInstance().GetPlayerPos();
        if (Vector3.Distance(transform.position, playerPos) > 8f)
            tankController.EnemyMove(tankRigidbody,playerPos);
        if (Vector3.Distance(transform.position, playerPos) < 10f)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > bulletFireCounter)
            {
                timeElapsed = 0;
                FireBullet(bulletFirePos, bulletPrefab);
            }
        }
        
    }

    private void FireBullet(Transform bulletFirePos, BulletView bulletPrefab)
    {
        int selectBulletType = UnityEngine.Random.Range(0, bulletSOL.Bullets.Length);
        BulletService.GetInstance().CreateBullet(bulletPrefab, bulletSOL.Bullets[selectBulletType],gameObject);
        BulletService.GetInstance().FireBullet(bulletFirePos);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<TankView>() != null)
        {
            Debug.Log("Collided with " + other.gameObject.name);
            //tankController.TakeDamage(tankController.TankModel.Damage);
        }
    }

    public void EnemyDie()
    {
        gameObject.SetActive(false);
    }

    public void ChangeHealthBarColor()
    {
        if (tankController.currentHealth < tankController.TankModel.Health/2)
        {
            HealthBar.color = Color.red;
        }
        else if (tankController.currentHealth < tankController.TankModel.Health)
        {
            HealthBar.color = Color.yellow;
        }
        else
        {
            HealthBar.color = Color.green;
        }
    }

    public void TakeDamage(float damage)
    {
        tankController.TakeDamage(damage);
    }

    public void DestroyView()
    {
        Destroy(gameObject);
        tankController = null;
        tankRigidbody = null;
        HealthBar = null;
        bulletFirePos = null;
        bulletPrefab = null;
        bulletSOL = null;
    }
}
