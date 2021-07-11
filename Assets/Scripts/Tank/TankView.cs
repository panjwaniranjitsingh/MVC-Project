
using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    public TankController tankController;
    public Rigidbody tankRigidbody;
    [SerializeField] float horizontal, vertical;
    const string HORIZONTAL = "Horizontal1";
    const string VERTICAL = "Vertical1";
    private float BulletForce = 1000f;
    [SerializeField] Image HealthBar;

    public Transform bulletFirePos;
    public GameObject bulletPrefab;
    [SerializeField] Text NameText;
    [SerializeField] Text HealthText;

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
        Debug.Log("Tank View created");
        DisplayTank();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw(HORIZONTAL); 

        vertical = Input.GetAxisRaw(VERTICAL);

        if(Input.GetKeyDown(KeyCode.Space))
            FireBullet(bulletFirePos,bulletPrefab);

        tankController.PlayerMovement(horizontal, vertical);
    }

    public void FireBullet(Transform bulletFirePos, GameObject bulletPrefab)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFirePos.position, bulletFirePos.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Rigidbody>().AddForce(bulletFirePos.forward * BulletForce);
        Destroy(bullet, 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.parent.GetComponent<EnemyTankView>() != null)
        {
            Debug.Log("Bullet Fired by " + other.gameObject.transform.parent.name);
            Destroy(other.gameObject);
            tankController.TankHit();
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
}
