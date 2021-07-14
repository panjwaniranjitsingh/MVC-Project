
using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour,IDamageable
{
    public TankController tankController;
    public Rigidbody tankRigidbody;
    [SerializeField] float horizontal, vertical;
    const string HORIZONTAL = "Horizontal1";
    const string VERTICAL = "Vertical1";
    [SerializeField] Image HealthBar;

    public Transform bulletFirePos;
    public BulletView bulletPrefab;
    [SerializeField] Text NameText;
    [SerializeField] Text HealthText;
    [SerializeField] BulletScriptableObjectList bulletSOL;

    void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }

    private void DisplayTank()
    {
        GameObject TankRenderers = gameObject.transform.GetChild(0).gameObject;
        for (int i = 0; i < TankRenderers.transform.childCount; i++)
        {
            TankRenderers.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = tankController.TankModel.TankColor;
        }
        gameObject.name = tankController.TankModel.Name;
        NameText.text = gameObject.name;
        HealthText.text = "Health:" + tankController.currentHealth.ToString();
    }

    void Start()
    {
        Debug.Log("Player Tank created");
        DisplayTank();
        
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw(HORIZONTAL); 

        vertical = Input.GetAxisRaw(VERTICAL);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int selectBulletType = UnityEngine.Random.Range(0, bulletSOL.Bullets.Length);
            BulletService.GetInstance().CreateBullet(bulletPrefab, bulletSOL.Bullets[selectBulletType],gameObject);
            BulletService.GetInstance().FireBullet(bulletFirePos);
        }

        tankController.PlayerMovement(horizontal, vertical);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyTankView>() != null)
        {
            Debug.Log("Collided with " + other.gameObject.name);
            //tankController.TakeDamage(tankController.TankModel.Damage);
        }
    }

    public void PlayerDie()
    {
        gameObject.SetActive(false);

    }

    public void ChangeHealthBarColor()
    {
        HealthText.text = "Health:"+tankController.currentHealth.ToString();
        if (tankController.currentHealth < tankController.TankModel.Health / 2)
        {
            HealthBar.color = Color.red;
        }
        else if (tankController.currentHealth<tankController.TankModel.Health)
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

    internal void Destroy()
    {
        Destroy(gameObject);
        tankController = null;
        tankRigidbody = null;
        HealthBar = null;
        bulletFirePos = null;
        bulletPrefab = null;
        NameText = null;
        HealthText = null;
        bulletSOL = null;
    }
}
