using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using System;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;

[UpdateBefore(typeof(TransformSystem))]
public class PhysicBodyLocalVelocitySystem : JobComponentSystem
{
    const int BATCH = 64;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new Job().Schedule(this, BATCH, inputDeps);
    }

    [BurstCompile]
    struct Job : IJobProcessComponentData<PhysicBodyLocalVelocity, PhysicBodyVelocity, TransformLocalMatrix>
    {
        public void Execute([WriteOnly] ref PhysicBodyLocalVelocity physicBodyLocalVelocity, [ReadOnly] ref PhysicBodyVelocity physicBodyVelocity, [ReadOnly] ref TransformLocalMatrix transformLocalMatrix)
        {
            // 1st case - this is what i'm realy want to do in this system
            // The problem is - broken execution order + burst mode increase CPU cost by 2-3 times
            physicBodyLocalVelocity.Value = math.mul(transformLocalMatrix.Value, physicBodyVelocity.Value);

            // 2nd case
            // Works prefectly (burst + execution order), difference - i don't write calculated result to physicBodyLocalVelocity
            // float3 testVar = math.mul(transformLocalMatrix.Value, physicBodyVelocity.Value);
            // physicBodyLocalVelocity.Value = physicBodyVelocity.Value;

            // 3rd case
            // Also works (burst + execution order), but CPU cost in burst mode is mutch more than in 2nd case burst mode
            // physicBodyLocalVelocity.Value = math.mul(transformLocalMatrix.Value, new float3());

            // 4th case
            // Also works (burst + execution order), but CPU cost in burst mode is mutch more than in 2nd case burst mode
            // physicBodyLocalVelocity.Value = math.mul(new float3x3(), physicBodyVelocity.Value);
        }
    }
}