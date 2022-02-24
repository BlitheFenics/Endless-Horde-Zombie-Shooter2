using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePillar : MonoBehaviour, Damageable
{
    public int health;
    private Vector3 lastDamagePosition;

    [SerializeField] private Transform pfPillarBroken;
    //[SerializeField] private Transform vfxSmoke;

    private void Awake()
    {
        health = 10;
    }

    public void Damage(int damageAmount, Vector3 damagePosition)
    {
        lastDamagePosition = damagePosition;
        health -= damageAmount;
        
        if (health <= 0)
        {
            //Instantiate(vfxSmoke, lastDamagePosition, Quaternion.identity);
            Transform pillarBrokenTransform = Instantiate(pfPillarBroken, new Vector3(transform.position.x,transform.position.y*3,transform.position.z), transform.rotation);
            Debug.Log(pfPillarBroken.transform.position);
            foreach (Transform child in pillarBrokenTransform)
            {
                if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(100f, lastDamagePosition, 5f);
                }
            }
            Destroy(gameObject);
        }
    }
}
