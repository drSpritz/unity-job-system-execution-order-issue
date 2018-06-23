using Unity.Entities;
using Unity.Mathematics;
using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct PhysicBodyLocalVelocity : IComponentData
{
    public float3 Value;
}

public class PhysicBodyLocalVelocityComponent : ComponentDataWrapper<PhysicBodyLocalVelocity> { }