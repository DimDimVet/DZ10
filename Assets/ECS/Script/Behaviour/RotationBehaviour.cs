using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour,IBehaviour
{
    public HealtComponent HealtComponent;
    private void Start()
    {
        HealtComponent = FindObjectOfType<HealtComponent>();//найдем объект с данным компонентом
    }
    public void Behaver()
    {
        //transform.Rotate(Vector3.up,10);
        Debug.Log("rotation");
    }

    public float Evaluete()
    {
        if (HealtComponent==null)
        {
            return 0;
        }
        return 1/(this.gameObject.transform.position-HealtComponent.transform.position).magnitude; //чем ближе значение выше
    }
}
