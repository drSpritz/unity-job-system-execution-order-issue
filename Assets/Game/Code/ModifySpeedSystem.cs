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
public class OverrideSpeedSystem : JobComponentSystem
{
    const int BATCH = 4;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new Job().Schedule(this, BATCH, inputDeps);
    }

    [BurstCompile]
    struct Job : IJobProcessComponentData<MoveSpeed, ModifySpeed>
    {
        public void Execute([ReadOnly] ref MoveSpeed moveSpeed, [ReadOnly] ref ModifySpeed overrideSpeed)
        {
            moveSpeed.speed *= overrideSpeed.Value;
        }
    }
}