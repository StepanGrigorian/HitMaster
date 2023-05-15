using System.Collections;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    private readonly Player player;
    private readonly Transform transform;

    private Coroutine LookCoroutine;

    public PlayerStateIdle(Player player)
    {
        this.player = player;
        transform = player.transform;
    }
    public void Start()
    {
        player.Animator.SetBool("isMoving", false);
    }
    public void LookAt(Vector3 target)
    {
        if (LookCoroutine != null)
        {
            player.StopCoroutine(LookCoroutine);
        }
        LookCoroutine = player.StartCoroutine(LookAtIE(target));
    }

    private IEnumerator LookAtIE(Vector3 target)
    {
        target.y = transform.position.y;
        Quaternion lookRotation = Quaternion.LookRotation(target - transform.position);

        while (Quaternion.Angle(transform.rotation, lookRotation) > 5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 120 * Time.deltaTime);
            yield return null;
        }
    }
}