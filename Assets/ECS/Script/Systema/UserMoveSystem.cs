using UnityEngine;
using Unity.Entities;

public class UserMoveSystem : ComponentSystem
{
    private EntityQuery moveQuery;

    protected override void OnCreate()
    {
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                                   ComponentType.ReadOnly<MoveData>(),
                                   ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(moveQuery).ForEach
            (
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
            {
                if (transform==null)
                {
                    return;
                }
                Vector3 currentPosition = transform.position;
                currentPosition += new Vector3(inputData.Move.x*moveData.MoveSpeed,0,inputData.Move.y*moveData.MoveSpeed);
                transform.position = currentPosition;

                Vector3 dir = new Vector3(inputData.Move.x,0,inputData.Move.y);
                if (dir==Vector3.zero)
                {
                    return;
                }

                Quaternion currentRotation = transform.rotation;
                Quaternion newRotation = Quaternion.LookRotation(Vector3.Normalize(dir));

                if (newRotation==currentRotation)
                {
                    return;
                }
                transform.rotation = Quaternion.Lerp(currentRotation,newRotation,Time.DeltaTime*10);
            }
            );
    }

}
