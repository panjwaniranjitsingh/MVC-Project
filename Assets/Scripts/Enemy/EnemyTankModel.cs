
public class EnemyTankModel
{
    public EnemyTankModel(int speed,float health)
    {
        Speed = speed;
        Health = health;
    }

    public int Speed { get; set; }
    public float Health { get; set; }

    public void DestroyModel()
    {
        Speed = 0;
        Health = 0;
    }
}
