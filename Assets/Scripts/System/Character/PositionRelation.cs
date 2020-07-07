
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
[UpdateAfter(typeof(Move))]
public class PositionRelation : SystemBase
{
        Dictionary<int , Transform> transforms = new Dictionary<int, Transform>();
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((Entity entity, ref Translation translation, in Id id) =>
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
                    
                    if (HasComponent<PlayableDirectorData>(entity) && GetComponent<PlayableDirectorData>(entity).Value)
                    {                           
                        SetComponent(entity, new Translation { Value = transforms[id.Value].position });
                    } else
                        transforms[id.Value].position = translation.Value;

                }

            }).Run();
    }
}