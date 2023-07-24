using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GunCollision : MonoBehaviour, ICollisionsComponent
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject parentObject;
    public bool IsMode;
    private float count;
    private bool isDestroy = false;
    private bool isStop = false;
    
    public void Execute(List<Collider> colliders)
    {
        if (isStop == false)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                ShootComponent shootMode = colliders[i].GetComponent<ShootComponent>();
                if (shootMode != null)
                {
                    shootMode.IsModeBull =true;
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
