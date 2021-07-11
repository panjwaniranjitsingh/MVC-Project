using UnityEngine;
using Cinemachine;

public class GameService : MonoSingletonGeneric<GameService>
{
    [SerializeField] TankView tankView;
    [SerializeField] EnemyTankView enemyTankView;
    [SerializeField] Transform enemySpawnPos;
    [SerializeField] CinemachineVirtualCamera cinemachineVcam;
    [SerializeField] TankScriptableObjectList tankSOL;

    // Start is called before the first frame update
    void Start()
    {
        int selectPlayerType = UnityEngine.Random.Range(0, tankSOL.Tanks.Length);
        TankScriptableObject playerTSO = tankSOL.Tanks[selectPlayerType];
        TankController playerTank = TankService.GetInstance().CreateNewTank(tankView,playerTSO);
        cinemachineVcam.Follow=playerTank.TankView.transform;
        EnemyTankController enemyTank = EnemyTankService.GetInstance().CreateNewTank(enemyTankView,enemySpawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
