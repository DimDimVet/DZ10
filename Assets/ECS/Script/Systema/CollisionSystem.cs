using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery entityQuery;//создадим переменую результата запроса сущностей
    private Collider[] result = new Collider[50];//кэш условного массива для получения результатов коллизии
    protected override void OnCreate()
    {
        //получим результат запроса всех сущностей имеющие ColliderData и Transform
        entityQuery = GetEntityQuery(ComponentType.ReadOnly<ColliderData>(),
                                     ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //получим из запроса согласно условий поиска определные сущности
        Entities.With(entityQuery).ForEach
            (
            (Entity entity, CollisionsComponent collisions, ref ColliderData colliderData) =>
            {
                if (collisions!=null)
                {
                    GameObject gameObject = collisions.gameObject;//кэшируем gameObject сущности имеющая CollisionsComponent
                    float3 position = gameObject.transform.position;//кэшируем float3 позиции сущности имеющая CollisionsComponent
                    Quaternion rotation = gameObject.transform.rotation;//кэшируем Quaternion позиции сущности имеющая CollisionsComponent


                    int size = 0;

                    switch (colliderData.TypeCollider)//опросим структуру ColliderData самой сущности 
                    {
                        case TypeCollider.Capsule:
                            float3 center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                            float3 point1 = colliderData.CapsuleStart + position;
                            float3 point2 = colliderData.CapsuleEnd + position;
                            point1 = (float3)(rotation * (point1 - center)) + center;
                            point2 = (float3)(rotation * (point2 - center)) + center;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, result);
                            break;

                        case TypeCollider.Sphere:
                            size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, result);
                            break;

                        case TypeCollider.Box:
                            size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtents,
                                                              result, colliderData.BoxOrientation * rotation);
                            break;

                        default:
                            break;
                    }

                    if (size > 0)//если результат Physics.Overlap более 0
                    {
                        List<Collider> colliders = new List<Collider>();//при каждом событие инициализируем лист
                        for (int i = 0; i < result.Length; i++)
                        {
                            if (result[i] != null)//откинем все пустышки массива
                            {
                                colliders.Add(result[i]);//то что осталось, запишем в лист
                            }

                        }
                        //вызовем метод у сущности(компонента) имеющий CollisionsComponent и с реализованным ICollisionsComponent
                        //и передадим туда полученый лист колайдеров сущностей
                        collisions.Execute(colliders);
                    }
                }
                
            }
            );
    }
}
