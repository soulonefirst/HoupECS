using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
[AlwaysSynchronizeSystem]
[UpdateAfter(typeof(Move))]
public class PositionRelation : JobComponentSystem
{
        Dictionary<int , Transform> transforms = new Dictionary<int, Transform>();
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Translation translation, in Id id) =>
            {
                if(!transforms.ContainsKey(id.Value))
                {
                    foreach (GameObject gObj in DollsBuffer.dolls)
                    {
                        if (gObj.GetHashCode() == id.Value)
                        {
                            transforms.Add(id.Value, gObj.transform);
                            translation.Value = gObj.transform.position;
                        }
                    }

                }else
                {
                    transforms[id.Value].position = translation.Value;
                }

            }).Run();
        return default;
    }
}