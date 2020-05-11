using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct InputData : IComponentData
{
    public bool left;
    public bool right;
    public bool mouseLeft;
    public bool mouseRight;
}
