using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class UserModeSystem : ComponentSystem
{
    private EntityQuery modeQuery;
    protected override void OnCreate()
    {
        modeQuery = GetEntityQuery(ComponentType.ReadOnly<ModeData>(),
                                   ComponentType.ReadOnly<UserInputDataComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(modeQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
            {
                if (inputData.Mode > 0 && userInput.ModeAction != null && userInput.ModeAction is IModeComponent mode)
                {
                    mode.Mode();

                }
            }
            );
    }
}
