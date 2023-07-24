using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class AIBehavSystem : ComponentSystem
{
    private EntityQuery behaviourQuery;
    protected override void OnCreate()
    {
        behaviourQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());//получим все ентити у которых есть структура AI
    }

    protected override void OnUpdate()
    {
        Entities.With(behaviourQuery).ForEach
            (
            (Entity entity, BehaviourManagerEnemy manager) =>
            {
                if (manager.activeBehaviout!=null)
                {
                    manager.activeBehaviout.Behaver();
                }
            }
            );
    }
}
