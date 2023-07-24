using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionsComponent : MonoBehaviour, IConvertGameObjectToEntity, ICollisionsComponent
{
    public Collider Collider;

    //соберем в лист стороние скрипты
    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();
    //создадим лист для хранения объектов-скриптов реализующие ICollisionsComponent 
    [HideInInspector] public List<ICollisionsComponent> ComponentCollisions = new List<ICollisionsComponent>();

    private void Start()
    {
        if (CollisionAction!=null)//проверим на null лист скриптов
        {
            for (int i = 0; i < CollisionAction.Count; i++)
            {
                if (CollisionAction[i] is ICollisionsComponent component)//проверим на наличие ICollisionsComponent лист скриптов
                {
                    ComponentCollisions.Add(component);//добавим в лист ICollisionsComponent найденое
                }
                else
                {
                    Debug.Log("Нет компонентов с ICollisionsComponent");
                }
            }
        }
        
    }

    //реализуем ICollisionsComponent, лист получаем из CollisionSystem
    public void Execute(List<Collider> colliders)
    {
        //проверим на null лист сICollisionsComponent
        if (ComponentCollisions!=null)
        {

            for (int i = 0; i < ComponentCollisions.Count; i++)
            {
                //вызовем метод во всех объектах из справочника и передадим лист с получеными коллайдерами
                ComponentCollisions[i].Execute(colliders);
            }
        }
       
    }

    //конвертируем в сущность текущий объект по типу коллайдера
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                //получим параметры коллайдера данного объекта
                sphere.ToWorldSpaceSphere(out float3 sphereCenter, out float sphereRadius);
                //добавим в сущности коллайдер объекта (через структуру ColliderData)
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    isInitalTakeOff = true
                });
                break;

            case CapsuleCollider capsule:
                //получим параметры коллайдера данного объекта
                capsule.ToWorldSpaceCapsule(out float3 capsuleStart, out float3 capsuleEnd, out float capsuleRadius);
                //добавим в сущности коллайдер объекта (через структуру ColliderData)
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    isInitalTakeOff = true
                });
                break;

            case BoxCollider box:
                //получим параметры коллайдера данного объекта
                box.ToWorldSpaceBox(out float3 boxCenter, out float3 boxHalfExtents, out quaternion boxOrientation);
                //добавим в сущности коллайдер объекта (через структуру ColliderData)
                entityManager.AddComponentData(entity, new ColliderData
                {
                    TypeCollider = TypeCollider.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    isInitalTakeOff = true
                });
                break;

            default:
                break;
        }
        //отключим коллайдер(аналог режима тригера)
        Collider.enabled = false;
    }

}

//structure
public struct ColliderData : IComponentData
{
    public TypeCollider TypeCollider;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool isInitalTakeOff;
}

public enum TypeCollider
{
    Sphere=0,
    Capsule=1,
    Box=2
}
