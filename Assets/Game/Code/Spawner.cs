﻿using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int instancesCount;
    public Mesh mesh;
    public Material mat;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

        EntityArchetype archetype = manager.CreateArchetype(
            ComponentType.Create<TransformMatrix>(),
            ComponentType.Create<TransformLocalMatrix>(),
            ComponentType.Create<PhysicBodyLocalVelocity>(),
            ComponentType.Create<PhysicBodyVelocity>(),
            ComponentType.Create<Position>(),
            ComponentType.Create<Rotation>()
            );

        Entity entity = manager.CreateEntity(archetype);

        var instances = new NativeArray<Entity>(instancesCount, Allocator.Temp);
        manager.Instantiate(entity, instances);
        manager.DestroyEntity(entity);

        Vector3 initialPosition = transform.position;

        foreach (Entity e in instances)
        {
            manager.SetComponentData(e, new Position() { Value = initialPosition + Random.onUnitSphere * Random.Range(0f, 100f) });
            manager.SetComponentData(e, new Rotation() { Value = Random.rotation });
            manager.SetComponentData(e, new PhysicBodyVelocity() { Value = Random.onUnitSphere * Random.Range(0f, 100f) });
        }

        instances.Dispose();
    }
}