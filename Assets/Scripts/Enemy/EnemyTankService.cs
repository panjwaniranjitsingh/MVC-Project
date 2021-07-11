
using UnityEngine;

public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
{
    EnemyTankModel model;
    EnemyTankController tank;

    public EnemyTankController CreateNewTank(EnemyTankView tankView,Transform spawnPos)
    {
        model = new EnemyTankModel(1000, 100f);
        tank = new EnemyTankController(model, tankView,spawnPos);
        return tank;
    }

    void Update()
    {
        if (!tank.TankView.gameObject.activeSelf)
            StartCoroutine(tank.EnableTankView());
    }

}
