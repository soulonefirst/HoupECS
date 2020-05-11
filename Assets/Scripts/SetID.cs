using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SetID : MonoBehaviour, IConvertGameObjectToEntity
{
    public GameObject doll;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.SetComponentData(entity, new Id { Value = doll.GetHashCode() });
    }


}
