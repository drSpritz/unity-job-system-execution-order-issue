using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;
using System;

[Serializable]
public struct ModifySpeed : IComponentData
{
    public float Value;
}

[UpdateBefore(typeof(MoveForwardSystem))]
[UpdateAfter(typeof(SpeedSystem))]
public class OverrideSpeedSystem : ComponentSystem
{
    public struct Data
    {
        [ReadOnly] public int Length;
        public ComponentDataArray<MoveSpeed> moveSpeed;
        [ReadOnly] public ComponentDataArray<ModifySpeed> overrideSpeed;
    }

    [Inject] Data m_data;

    protected override void OnUpdate()
    {
        for (int i = 0; i < m_data.Length; i++)
        {            
            m_data.moveSpeed[i] = new MoveSpeed() { speed = m_data.moveSpeed[i].speed * m_data.overrideSpeed[i].Value };
        }
    }
}