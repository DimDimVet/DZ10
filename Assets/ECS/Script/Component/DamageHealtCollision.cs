using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DamageHealtCollision : MonoBehaviour, ICollisionsComponent
{
    [SerializeField] private int damage = 10;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private bool isOnDamage=true;
    private float count;
    private bool isDestroy=false;
    private bool isStop = false;

    public void Execute(List<Collider> colliders)
    {
        if (isStop==false)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                HealtComponent healt = colliders[i].GetComponent<HealtComponent>();
                if (healt != null)
                {
                    if (isOnDamage)
                    {
                        healt.HealtContoll(damage);
                    }
                    else
                    {
                        healt.HealtContoll(-damage);
                    }

                }
            }

            animator.SetBool("ContactPlayer", true);
            isDestroy = true;
            isStop = true;
        }
    }

    private void Update()
    {
        if (isDestroy)
        {
            if (count > 2)
            {
                count = 0;
                Destroy(parentObject);
            }
            else
            {
                count += 1 * Time.deltaTime;
            }
        }
        
    }
}
