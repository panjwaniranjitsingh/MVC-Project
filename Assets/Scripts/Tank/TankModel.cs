
using UnityEngine;

public class TankModel
{
    public TankModel(TankScriptableObject playerTSO)
    {
        Name = playerTSO.TankName;
        Speed = playerTSO.Speed;
        Health = playerTSO.Health;
        Damage = playerTSO.Damage;
        TankColor = playerTSO.TankColor;
    }

    public string Name { get; }
    public float Speed { get; }
    public float Health { get; }
    public float Damage { get; }
    public Color TankColor { get; }
}
