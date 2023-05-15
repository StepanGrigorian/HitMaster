using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    [SerializeField] private Transform playerWaypoint;

    [SerializeField] private List<Enemy> Enemies = new();

    public delegate void EnemyKilled();
    public event EnemyKilled OnEnemyKilled;
    public Vector3 GetPlayerWaypoint() => playerWaypoint.position;
    public List<Enemy> GetEnemies() => Enemies;
    public int GetEnemyCount() => Enemies.Count;

    private void Awake()
    {
        foreach (Enemy enemy in Enemies)
        {
            enemy.SetSegment(this);
        }
    }
    public void RemoveEnemyFromList(Enemy enemy)
    {
        Enemies.Remove(enemy);
        OnEnemyKilled?.Invoke();
    }
}