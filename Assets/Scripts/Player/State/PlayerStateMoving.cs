using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMoving : IPlayerState
{
    private readonly Player player;
    private readonly NavMeshAgent agent;
    private readonly Transform transform;

    private Coroutine LookCoroutine;

    private Vector3 target;
    private Vector2 Velocity;
    private Vector2 SmoothDeltaPosition;

    public PlayerStateMoving(Player player, Vector3 target)
    {
        this.player = player;
        player.Agent.SetDestination(target);
        this.target = target;
        agent = player.Agent;
        transform = player.transform;
    }
    public void Start()
    {
        player.Animator.SetBool("isMoving", true);
    }
    public void Update()
    {
        SynchronizeAnimatorAndAgent();
    }
    public void OnAnimatorMove()
    {
        Vector3 rootPosition = player.Animator.rootPosition;
        rootPosition.y = Mathf.Lerp(rootPosition.y, agent.nextPosition.y, 10f * Time.deltaTime);
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }
    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        Velocity = SmoothDeltaPosition / Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Velocity = Vector2.Lerp(
                Vector2.zero,
                Velocity,
                agent.remainingDistance / agent.stoppingDistance
            );
        }

        player.Animator.SetFloat("Velocity", Velocity.magnitude);

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if (deltaMagnitude > agent.radius / 2f)
        {
            transform.position = Vector3.Lerp(
                player.Animator.rootPosition,
                agent.nextPosition,
                smooth
            );
        }
        if (Vector3.Distance(transform.position, target) < agent.stoppingDistance)
        {
            player.SetStateIdle();
        }
    }
    public void LookAt(Vector3 target)
    {
        if (LookCoroutine != null)
        {
            player.StopCoroutine(LookCoroutine);
        }
        LookCoroutine = player.StartCoroutine(LookAtCoutine(target));
    }

    private IEnumerator LookAtCoutine(Vector3 target)
    {
        agent.updateRotation = false;
        target.y = transform.position.y;
        Quaternion lookRotation = Quaternion.LookRotation(target - transform.position);

        while (Quaternion.Angle(transform.rotation, lookRotation) > 5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
            yield return null;
        }
        agent.updateRotation = true;
    }
}