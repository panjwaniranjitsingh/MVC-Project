using UnityEngine.UI;
using UnityEngine;

public class EnemyTankView : MonoBehaviour
{
    public EnemyTankController tankController;
    public Rigidbody tankRigidbody;
    private float timeElapsed;
    [SerializeField]private float bulletFireCounter = 1f;
    private float BulletForce = 1000f;
    [SerializeField] Image HealthBar;
    public Transform bulletFirePos;
    public GameObject bulletPrefab;
   

    void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        
    }
    
    void Start()
    {
        Debug.Log("Tank View created");
        
    }

    void FixedUpdate()
    {
        Vector3 playerPos = TankService.GetInstance().GetPlayerPos();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.parent.GetComponent<TankView>() != null)
        {
            Debug.Log("Bullet Fired by " + other.gameObject.transform.parent.name);
            Destroy(other.gameObject);
            tankController.TankHit();
        }
    }

    public void FireBullet(Transform bulletFirePos, GameObject bulletPrefab)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFirePos.position, bulletFirePos.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Rigidbody>().AddForce(bulletFirePos.forward * BulletForce);
        Destroy(bullet, 2f);
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
}
