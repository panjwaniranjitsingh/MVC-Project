
using System;

public class BulletModel 
{

    public BulletModel(BulletScriptableObject bulletSO)
    {
        BulletName = bulletSO.BulletName;
        BulletForce = bulletSO.BulletForce;
        Damage = bulletSO.Damage;
    }

    public string BulletName { get; set; }
    public float BulletForce { get; set; }
    public float Damage { get; set; }

    internal void Destroy()
    {
        BulletName = "";
        BulletForce = 0;
        Damage = 0;
    }
}
