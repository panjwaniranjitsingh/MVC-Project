using System;
using UnityEngine;

public class BulletController 
{

    public BulletController(BulletModel bulletModel,BulletView bulletPrefab,GameObject parent)
    {
        BulletModel = bulletModel;
        BulletView = GameObject.Instantiate<BulletView>(bulletPrefab);
        BulletView.gameObject.transform.parent = parent.transform;
        BulletView.bulletController = this;
    }

    public BulletModel BulletModel { get; set; }
    public BulletView BulletView { get; set; }

    public void FireBullet(Transform bulletFirePos)
    {
        BulletView.gameObject.SetActive(true);
        BulletView.transform.position = bulletFirePos.position;
        BulletView.transform.rotation = bulletFirePos.rotation;
        BulletView.GetComponent<Rigidbody>().AddForce(bulletFirePos.forward * BulletModel.BulletForce);
        //Destroy(bullet, 2f);
    }

    internal void DestroyBullet()
    {
        BulletService.GetInstance().DestroyBulletMVC(this);
    }
    internal void Destroy()
    {
        BulletModel.Destroy();
        BulletView.Destroy();
        BulletModel = null;
        BulletView = null;
    }

    public float GetDamage()
    {
        return BulletModel.Damage;
    }

}
