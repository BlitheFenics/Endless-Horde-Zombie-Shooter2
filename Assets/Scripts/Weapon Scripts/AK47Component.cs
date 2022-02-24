using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    protected override void FireWeapon()
    {
        Vector3 hitLocation;

        if(weaponStats.bulletsInClip > 0 && !isReloading)
        {
            base.FireWeapon();
            if(firingEffect)
            {
                firingEffect.Play();
            }
            
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if(Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);

                if(hit.collider.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    //Debug.Log("Hit Pillar");
                    rigidbody.AddExplosionForce(1000f, gameObject.transform.position, 5f);
                }

                if (hit.collider.gameObject.TryGetComponent<Damageable>(out Damageable damageable))
                {
                    //Debug.Log("Damage");
                    damageable.Damage(1, hit.point);
                }
            }
        }
        else if(weaponStats.bulletsInClip <= 0)
        {
            //Trigger reload when no bullets left
            weaponHolder.StartReloading();
        }
    }
}