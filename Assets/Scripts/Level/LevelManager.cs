using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private LevelSegment[] segments;

    [SerializeField] private CinemachineTargetGroup targetGroup;

    private int currentSegment = 0;
    private void Awake()
    {
        foreach (var segment in segments)
        {
            segment.OnEnemyKilled += CheckEnemyCount;
        }
    }
    private void Start()
    {
        GoToNext();
    }
    public void CheckEnemyCount()
    {
        UpdateCamera();
        if (segments[currentSegment].GetEnemyCount() == 0)
            GoToNext();
    }
    public void GoToNext()
    {
        if(++currentSegment >= segments.Length)
        {
            Invoke("ReloadScene", 2f);
            return;
        }
        player.GoToPosition(segments[currentSegment].GetPlayerWaypoint());
        UpdateCamera();
        CheckEnemyCount();
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    private void UpdateCamera()
    {
        var a = segments[currentSegment].GetEnemies();
        CinemachineTargetGroup.Target[] enemies = new CinemachineTargetGroup.Target[segments[currentSegment].GetEnemyCount() + 1];

        enemies[0] = GetTarget(player.transform, 2, 1);
        for (int i = 0; i < a.Count; i++)
        {
            enemies[i + 1] = GetTarget(a[i].transform, 1, 1);
        }
        targetGroup.m_Targets = enemies;
    }
    private CinemachineTargetGroup.Target GetTarget(Transform transform, int weight, int radius)
    {
        var target = new CinemachineTargetGroup.Target();
        target.target = transform;
        target.weight = weight;
        target.radius = radius;
        return target;
    }
}