using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    private TankController tankController;

    public TankController CreateNewTank(TankView tankView, TankScriptableObject playerTSO)
    {
        TankModel tankModel = new TankModel(playerTSO);
        tankController = new TankController(tankModel, tankView);
        return tankController;
    }

    public Vector3 GetPlayerPos()
    {
        return tankController.TankView.transform.position;
    }

    internal void DestroyTankMVC(TankController tankController)
    {
        tankController.Destroy();
        tankController = null;
    }
}
