using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using CortexDeveloper.ECSMessages.Service;
using Samples.UserInterfaceExample;

public class ECSMessageInit : MonoBehaviour
{
    private static World _world;
    private SimulationSystemGroup _simulationSystemGroup;
    private LateSimulationSystemGroup _lateSimulationSystemGroup;
    private void Awake()
    {
        InitializeMessageBroadCaster();
        CreateExampleSystem();
    }
    private void OnDestroy()
    {
        if (!_world.IsCreated) return;
        DisposeMessageBroadcaster();
        RemoveExampleSystem();
    }
    private void InitializeMessageBroadCaster()
    {
        _world = World.DefaultGameObjectInjectionWorld;
        _simulationSystemGroup = _world.GetOrCreateSystemManaged<SimulationSystemGroup>();
        _lateSimulationSystemGroup = _world.GetOrCreateSystemManaged<LateSimulationSystemGroup>();
        MessageBroadcaster.InitializeInWorld(_world, _lateSimulationSystemGroup);
    }
    private void DisposeMessageBroadcaster()
    {
        MessageBroadcaster.DisposeFromWorld(_world);
    }

    private void CreateExampleSystem()
    {
        _simulationSystemGroup.AddSystemToUpdateList(_world.CreateSystem<StartGameSystem>());
        _simulationSystemGroup.AddSystemToUpdateList(_world.CreateSystem<PauseGameSystem>());
    }

    private void RemoveExampleSystem()
    {
        _simulationSystemGroup.RemoveSystemFromUpdateList(_world.GetExistingSystem<StartGameSystem>());
        _simulationSystemGroup.RemoveSystemFromUpdateList(_world.GetExistingSystem<PauseGameSystem>());
    }
}
