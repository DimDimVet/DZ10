using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;//создадим переменую результата запроса сущностей
    private float refFloat = 0.01f;
    private float2 distans;
    protected override void OnCreate()
    {
        //получим результат запроса всех сущностей 
        animQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>(),
                                   ComponentType.ReadOnly<HealtComponent>());
    }
    protected override void OnUpdate()
    {
        //при каждом кадре ищем в сущностях изменеия структуры InputData
        Entities.With(animQuery).ForEach
            (
            (Entity entity, ref InputData inputData, Animator animator, UserInputDataComponent userInput, HealtComponent healt) =>
            {

                //move
                //реакция на изменеия, запустим анимацию
                distans.x = Mathf.Abs(inputData.Move.x);
                distans.y = Mathf.Abs(inputData.Move.y);

                if (distans.x >= refFloat | distans.y >= refFloat)
                {
                    animator.SetFloat(userInput.AnimSpeed, userInput.Speed * math.distancesq(distans.x, -distans.y));
                }
                else
                {
                    animator.SetFloat(userInput.AnimSpeed, 0);
                }

                //pull
                //реакция на изменеия, запустим анимацию 
                if (inputData.Pull > 0f)
                {
                    animator.SetBool(userInput.AnimJamp, true);
                }
                else
                {
                    animator.SetBool(userInput.AnimJamp, false);
                }

                //dead
                if (healt.Dead)
                {
                    animator.SetBool(userInput.AnimDead, true);
                }

            }
            );
    }
}
