using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    TankController tankController;
    

    public TankController CreateNewTank(TankView tankView, TankScriptableObject playerTSO)
    {
        TankModel model = new TankModel(playerTSO);
        tankController = new TankController(model, tankView);
        return tankController;
    }

    public Vector3 GetPlayerPos()
    {
        return tankController.TankView.transform.position;
    }

}
