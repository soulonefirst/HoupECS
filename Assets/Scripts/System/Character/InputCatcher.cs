using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
[AlwaysSynchronizeSystem]
public class InputCatcher : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .ForEach((ref InputData inputData) =>
            {
                inputData.left = Input.GetKey(KeyCode.A);
                inputData.right = Input.GetKey(KeyCode.D); 
                inputData.mouseLeft = Input.GetKey(KeyCode.Mouse0);
                inputData.mouseRight = Input.GetKey(KeyCode.Mouse1);

            }).Run();
        return default;
    }
}
