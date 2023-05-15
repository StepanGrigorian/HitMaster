using System;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }

    private static Dictionary<Type, IPlayerState> statesDictionary;
    private IPlayerState currentState;

   [SerializeField] private Weapon weapon;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Input = GetComponent<PlayerInput>();

        InitStates();
        SetStateIdle();

        Animator.applyRootMotion = true;
        Agent.updatePosition = false;
        Agent.updateRotation = true;

        Input.PositionCallback += ShootToPosition;
    }
    public void SetStateIdle()
    {
        SetState(GetState<PlayerStateIdle>());
    }
    public void GoToPosition(Vector3 position)
    {
        SetState(new PlayerStateMoving(this, position));
    }
    private void ShootToPosition(Vector3 position)
    {
        if (weapon.IsReloaded)
        {
            currentState.LookAt(position);
            weapon.Shoot(position);
        }
    }
    
    #region State
    private void InitStates()
    {
        statesDictionary = new();
        statesDictionary[typeof(PlayerStateIdle)] = new PlayerStateIdle(this);
    }
    public void SetState(IPlayerState state)
    {
        if (currentState != null)
            currentState.End();
        currentState = state;
        currentState.Start();
    }
    public static IPlayerState GetState<T>() where T : IPlayerState
    {
        Type type = typeof(T);
        return statesDictionary[type];
    }
    private void Update()
    {
        currentState?.Update();
    }
    private void OnAnimatorMove()
    {
        currentState?.OnAnimatorMove();
    }
    #endregion
}