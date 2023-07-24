using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserInputSystem : ComponentSystem
{
    //создадим переменую результата запроса сущностей
    private EntityQuery inputQuery;
    //создадим переменую MapCurrent(new input)
    private MapCurrent inputAction;

    private float2 moveInput;
    private float shootInput;
    private float pullInput;
    private float modeInput;

    protected override void OnCreate()
    {
        //получим результат запроса сущностей имеющие структуру InputData
        inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        inputAction = new MapCurrent();//инициализируем карту input

        //подпишем на event события нажатий и значения присвоим локальным переменым
        inputAction.UIMap.WASD.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.UIMap.WASD.started += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.UIMap.WASD.canceled += context => { moveInput = context.ReadValue<Vector2>(); };

        inputAction.Map.WASD.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.Map.WASD.started += context => { moveInput = context.ReadValue<Vector2>(); };
        inputAction.Map.WASD.canceled += context => { moveInput = context.ReadValue<Vector2>(); };

        inputAction.Map.Shoot.performed += context => { shootInput = context.ReadValue<float>(); };
        inputAction.Map.Shoot.started += context => { shootInput = context.ReadValue<float>(); };
        inputAction.Map.Shoot.canceled += context => { shootInput = context.ReadValue<float>(); };

        inputAction.Map.Pull.performed += context => { pullInput = context.ReadValue<float>(); };
        inputAction.Map.Pull.started += context => { pullInput = context.ReadValue<float>(); };
        inputAction.Map.Pull.canceled += context => { pullInput = context.ReadValue<float>(); };

        inputAction.Map.ModePlayer.performed += context => { modeInput = context.ReadValue<float>(); };
        inputAction.Map.ModePlayer.started += context => { modeInput = context.ReadValue<float>(); };
        inputAction.Map.ModePlayer.canceled += context => { modeInput = context.ReadValue<float>(); };
        //запустим 
        inputAction.Enable();

    }

    protected override void OnStopRunning()
    {
        inputAction.Disable();
    }

    protected override void OnUpdate()
    {
        //при каждом кадре отслеживать значение из локальных переменых и присваивать в структуру InputData
        Entities.With(inputQuery).ForEach
            (
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = moveInput;
                inputData.Shoot = shootInput;
                inputData.Pull = pullInput;
                inputData.Mode = modeInput;
            }
            );
    }
}

