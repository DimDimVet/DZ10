using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BehaviourManagerEnemy : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> behaviours=new List<MonoBehaviour>(); //создадим список поведений
    public IBehaviour activeBehaviout;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
}

//structur
public struct AIAgent: IComponentData
{
    
}
