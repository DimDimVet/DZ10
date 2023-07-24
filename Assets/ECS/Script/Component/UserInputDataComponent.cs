using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserInputDataComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed=1f;
    public MonoBehaviour ShootAction;
    public MonoBehaviour PullAction;
    public MonoBehaviour ModeAction;

    //anim
    public string AnimSpeed;
    public string AnimJamp;
    public string AnimDead;


    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new InputData());//добавим в сущность стурктуру ввода

        entityManager.AddComponentData(entity, new MoveData()//добавим в сущность стурктуру режима движения(скорость)
        {
            MoveSpeed = Speed / 100,
        });

        if (ShootAction != null & ShootAction is IShootComponent)
        {
            entityManager.AddComponentData(entity, new ShootData());//добавим в сущность стурктуру ввода стрельбы
        }

        if (PullAction != null & PullAction is IPullComponent)
        {
            entityManager.AddComponentData(entity, new PullData());//добавим в сущность стурктуру ввода прыжка
        }

        if (ModeAction != null & ModeAction is IModeComponent)
        {
            entityManager.AddComponentData(entity, new ModeData());//добавим в сущность стурктуру ввода режима
        }

        if (AnimSpeed!=string.Empty)
        {
            entityManager.AddComponentData(entity, new AnimData());//добавим в сущность стурктуру ввода анимации
        }
    }
}

//srukture Entity
public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Pull;
    public float Mode;
}
public struct MoveData : IComponentData
{
    public float MoveSpeed;

}

public struct ShootData : IComponentData
{
    //
}
public struct PullData : IComponentData
{
    //
}
public struct ModeData : IComponentData
{
    //
}
public struct AnimData : IComponentData
{
    //
}
