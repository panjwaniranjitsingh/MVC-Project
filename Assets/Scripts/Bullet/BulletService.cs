
using System;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    
    private BulletController bulletController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BulletController CreateBullet(BulletView bulletView,BulletScriptableObject bulletSO,GameObject parent)
    {
        BulletModel bulletModel = new BulletModel(bulletSO);
        bulletController = new BulletController(bulletModel,bulletView,parent);
        return bulletController;
    }

    public void FireBullet(Transform bulletFirePos)
    {
        bulletController.FireBullet(bulletFirePos);
    }

    internal void DestroyBulletMVC(BulletController bulletController)
    {
        bulletController.Destroy();
        bulletController = null;
    }
}
