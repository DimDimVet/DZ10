using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class UserPullSystem : ComponentSystem
{
    private EntityQuery pullQuery;
    protected override void OnCreate()
    {
        pullQuery = GetEntityQuery(ComponentType.ReadOnly<PullData>(),
                                   ComponentType.ReadOnly<UserInputDataComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(pullQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
            {
                if (inputData.Pull > 0 && userInput.PullAction != null && userInput.PullAction is IPullComponent pull)
                {
                    pull.Jamp();
                }
            }
            );
    }
}
