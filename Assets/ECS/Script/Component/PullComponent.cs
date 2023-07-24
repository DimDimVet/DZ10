using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullComponent : MonoBehaviour, IPullComponent
{
    [SerializeField] private Rigidbody rigidbodyObj;
    [SerializeField] private float axisX = 0f, axisY = 0f, axisZ = 0f;
    [Header("”казать точки-датчики контакта с GND")]
    [SerializeField] private Transform[] pointGnd;
    [Header("”казать слой GND")]
    [SerializeField] LayerMask groundLayer;
    public float ShootDelay;
    private float shootTime = float.MinValue;

    public void Jamp()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        if (GndControl())
        {
            Vector3 tempVector = new Vector3(axisX, axisY, axisZ);
            rigidbodyObj.AddForce(tempVector, ForceMode.Impulse);
        }

    }
    private bool GndControl()
    {
        for (int i = 0; i < pointGnd.Length; i++)//переберем все точки-датчики контакта с GND
        {
            if (Physics.CheckSphere(pointGnd[i].position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore))
            {
                return true;
            }
        }
        return true;
    }
}
