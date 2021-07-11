
using System;
using System.Collections;
using UnityEngine;

public class EnemyTankController
{
    public float currentHealth;

    public EnemyTankController(EnemyTankModel tankModel, EnemyTankView tankPrefab,Transform spawnPos)
    {
        TankModel = tankModel;
        currentHealth = TankModel.Health;
        TankView = GameObject.Instantiate<EnemyTankView>(tankPrefab);

        TankView.tankController = this;
        TankView.transform.position = spawnPos.position;
        //Debug.Log("tank view created", TankView);
    }

    

    public EnemyTankModel TankModel { get; }
    public EnemyTankView TankView { get; }

    public void EnemyMove(Rigidbody tankRigidbody,Vector3 nextPos)
    {
        var localTarget = tankRigidbody.transform.InverseTransformPoint(nextPos);
        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        Vector3 dir = (nextPos - tankRigidbody.transform.position).normalized * TankModel.Speed * Time.deltaTime;
        if (deltaRotation != Quaternion.identity)
        {
            //Rotate Enemy to NextPos
            tankRigidbody.MoveRotation(tankRigidbody.rotation * deltaRotation);
        }
        else if (deltaRotation == Quaternion.identity)
        {
            //transform.position = Vector3.Lerp(transform.position,EnemytargetPos[enemyTarget],Time.deltaTime);
            tankRigidbody.velocity = dir / 2;
        }
    }

    public void TankHit()
    {
        currentHealth -= 25;
        TankView.ChangeHealthBarColor();
        if (currentHealth < 0)
        {
            //Enemy Dies
            TankView.EnemyDie();
        }
    }

    public IEnumerator EnableTankView()
    {
        yield return new WaitForSeconds(5f);
        TankView.gameObject.SetActive(true);
        currentHealth = TankModel.Health;
        TankView.ChangeHealthBarColor();
    }
}
