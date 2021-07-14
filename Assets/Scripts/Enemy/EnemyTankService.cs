
using System;
using System.Collections;
using UnityEngine;

public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
{
    EnemyTankModel model;
    EnemyTankController tank;
    EnemyTankView refTankPrefab;
    Transform refSpawnPos;

    public EnemyTankController CreateNewTank(EnemyTankView tankView,Transform spawnPos)
    {
        model = new EnemyTankModel(1000, 100f);
        tank = new EnemyTankController(model, tankView,spawnPos);
        refTankPrefab = tankView;
        refSpawnPos = spawnPos;
        return tank;
    }

    void Update()
    {
        
    }

    public void DestroyTank(EnemyTankController enemyTankController)
    {
        Debug.Log("DestroyTank");
        enemyTankController.DestroyController();
        model = null;
        tank = null;
        StartCoroutine(CreateNewTankAfterSomeTime());
    }

    IEnumerator CreateNewTankAfterSomeTime()
    {
        yield return new WaitForSeconds(5f);
        EnemyTankController enemyTank = CreateNewTank(refTankPrefab,refSpawnPos);
    }
}
