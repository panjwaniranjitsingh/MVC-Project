using System;
using UnityEngine;

public class TankController
{

    [SerializeField] float horizontal, vertical;
    public float currentHealth;
    const float TURNSPEED = 50f;
    

    public TankController(TankModel tankModel, TankView tankPrefab)
    {
        TankModel = tankModel;
        currentHealth = TankModel.Health;
        TankView = GameObject.Instantiate<TankView>(tankPrefab);
        TankView.tankController = this;

        //Debug.Log("tank view created", TankView);
    }

    public TankModel TankModel { get; }
    public TankView TankView { get; }

    public void PlayerMovement(float horizontal, float vertical)
    {
        //Debug.Log("PlayerMovement with speed=" + TankModel.Speed);
        Vector3 Movement = TankView.transform.forward * vertical * TankModel.Speed * Time.deltaTime;
        //TankView.tankRigidbody.MovePosition(TankView.transform.position + Movement);
        TankView.tankRigidbody.velocity = Movement;

        float turn = horizontal * TURNSPEED * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        TankView.tankRigidbody.MoveRotation(TankView.tankRigidbody.rotation * turnRotation);
    }

    public void TankHit()
    {
        currentHealth -= 25;
        TankView.ChangeHealthBarColor();
        if (currentHealth < 0)
        {
            //Enemy Dies
            TankView.PlayerDie();
        }
    }
}
