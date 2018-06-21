using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;
using System;

[Serializable]
public struct Speed : IComponentData
{
    public float Value;
}

[UpdateBefore(typeof(MoveForwardSystem))]
public class SpeedSystem : ComponentSystem
{
    public struct Data
    {
        [ReadOnly] public int Length;
        public ComponentDataArray<MoveSpeed> moveSpeed;
        [ReadOnly] public ComponentDataArray<Speed> speed;
    }

    [Inject] Data m_data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < m_data.Length; i++)
        {
            m_data.moveSpeed[i] = new MoveSpeed() { speed = m_data.speed[i].Value };
        }
    }
}