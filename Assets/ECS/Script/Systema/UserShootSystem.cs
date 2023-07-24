using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class UserShootSystem : ComponentSystem
{
    private EntityQuery shootQuery;
    protected override void OnCreate()
    {
        shootQuery = GetEntityQuery(ComponentType.ReadOnly<ShootData>(),
                                   ComponentType.ReadOnly<UserInputDataComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(shootQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
            {

                if (inputData.Shoot > 0f && userInput.ShootAction != null && userInput.ShootAction is IShootComponent ability)
                {
                    ability.Shoot();
                }
            }
            );
    }
}
