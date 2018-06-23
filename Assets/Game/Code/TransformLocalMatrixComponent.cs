using Unity.Entities;
using Unity.Mathematics;
using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct TransformLocalMatrix : IComponentData
{
    public float3x3 Value;
}

public class TransformLocalMatrixComponent : ComponentDataWrapper<TransformLocalMatrix> { }