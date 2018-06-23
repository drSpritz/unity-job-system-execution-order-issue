using Unity.Entities;
using Unity.Mathematics;
using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct PhysicBodyVelocity : IComponentData
{
    public float3 Value;
}

public class PhysicBodyVelocityComponent : ComponentDataWrapper<PhysicBodyVelocity> { }