using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Burst;

[UpdateAfter(typeof(TransformSystem))]
public class TransformLocalMatrixSystem : JobComponentSystem
{
    const int BATCH = 64;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new Job().Schedule(this, BATCH, inputDeps);
    }

    [BurstCompile]
    struct Job : IJobProcessComponentData<TransformLocalMatrix, TransformMatrix>
    {
        public void Execute([WriteOnly] ref TransformLocalMatrix transformLocalMatrix, [ReadOnly] ref TransformMatrix transformMatrix)
        {
            float4x4 matrix4x4 = transformMatrix.Value;

            float3x3 matrix3x3 = new float3x3()
            {
                c0 = new float3(matrix4x4.c0.x, matrix4x4.c1.x, matrix4x4.c2.x),
                c1 = new float3(matrix4x4.c0.y, matrix4x4.c1.y, matrix4x4.c2.y),
                c2 = new float3(matrix4x4.c0.z, matrix4x4.c1.z, matrix4x4.c2.z),
            };

            transformLocalMatrix.Value = matrix3x3;
        }
    }
}