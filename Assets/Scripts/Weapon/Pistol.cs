using System.Collections;
using UnityEngine;
public class Pistol : Weapon
{
    public override void Shoot(Vector3 target)
    {
        var bullet = BulletPool.instance.CreateBullet();
        bullet.MyStart(bulletSpawn.position, target);
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        isReloaded = false;
        yield return new WaitForSeconds(reloadTime);
        isReloaded = true;
    }
}
