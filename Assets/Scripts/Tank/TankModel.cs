
using System;
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

    public string Name { get; set; }
    public float Speed { get; set; }
    public float Health { get; set; }
    public float Damage { get; set; }
    public Color TankColor { get; set; }

    internal void Destroy()
    {
        Name = null;
        Speed = 0;
        Health = 0;
        Damage = 0;
        TankColor = Color.black;
    }
}
