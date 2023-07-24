using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class AIEvaluateSystem : ComponentSystem
{
    private EntityQuery evaluateQuery;
    protected override void OnCreate()
    {
        evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());//получим все ентити у которых есть структура AI
    }

    protected override void OnUpdate()
    {
        Entities.With(evaluateQuery).ForEach
            (
            (Entity entity, BehaviourManagerEnemy manager) =>
            {
                float highScore = float.MinValue;
                manager.activeBehaviout = null;

                foreach (var behaviour in manager.behaviours)
                {
                    if (behaviour is IBehaviour ai)
                    {
                        var currScore = ai.Evaluete();
                        if (currScore> highScore)
                        {
                            highScore = currScore;
                            manager.activeBehaviout = ai;
                        }
                    }
                }
                //Debug.Log(manager.activeBehaviout);
            }
            );
    }
}
