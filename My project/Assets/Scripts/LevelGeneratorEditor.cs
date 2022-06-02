﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : UnityEditor.Editor
{
    private LevelGenerator generator;
    private int[,] map;

    private void OnEnable()
    {
        generator = (LevelGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            Generate();
        }
    }

    private void Generate()
    {
        map = new int[generator.MapSize, generator.MapSize];
        GenerateMap();

        for(var i = 0; i < generator.SmoothingIterations; i++)
        {
            SmoothMap();
        }

        RemoveModules();
        CreateModules();

        CombineLightProbes();
    }

    private void GenerateMap()
    {
        for(var x = 0; x < generator.MapSize; x++)
        {
            for(var y = 0; y < generator.MapSize; y++)
            {
                if(x == 0 || x == generator.MapSize - 1 || y == 0 || y == generator.MapSize - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    var filled = Random.Range(0f, 1f) < generator.FillPercentage ? 1 : 0;
                    map[x, y] = filled;
                }
            }
        }
    }

    private void SmoothMap()
    {
        for(var x = 0; x < generator.MapSize; x++)
        {
            for (var y = 0; y < generator.MapSize; y++)
            {
                var neighbours = FindNeighbourCount(x, y);
                if(neighbours > 4)
                {
                    map[x, y] = 1;
                }
                else if(neighbours < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    private int FindNeighbourCount(int x, int y)
    {
        var count = 0;
        for(var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
        {
            for(var neighbourY = y -1; neighbourY <= y + 1; neighbourY++)
            {
                if(IsNeighbour(neighbourX, neighbourY)){
                    if(neighbourX != x || neighbourY != y)
                    {
                        count += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    count++;
                }
            }
        }

        return count;
    }

    private bool IsNeighbour(int x, int y)
    {
        return x >= 0 && x < generator.MapSize && y >= 0 && y < generator.MapSize;
    }

    private void RemoveModules()
    {
        var generatorTransform = generator.transform;
        for(var i = generatorTransform.childCount - 1; i >= 0; i--)
        {
                var child = generatorTransform.GetChild(i).gameObject;
                Undo.DestroyObjectImmediate(child);
        } 
    }

    private void CreateModules()
    {
        var offsetPosition = generator.transform.position;
        for(var x = 0; x < generator.MapSize; x++)
        {
            for(var y = 0; y < generator.MapSize; y++)
            {
                var targetModules = map[x, y] > 0
                    ? generator.WallModules
                    : generator.Modules;

                CreateModule(targetModules, offsetPosition, x, y);
            }
        }
    }

    private void CreateModule(IReadOnlyList<GameObject> targetModules,
        Vector3 offsetPosition,
        int x, int y)
    {
        if(targetModules.Count == 0)
        {
            return;
        }

        var targetModule = targetModules[Random.Range(0, targetModules.Count)];
        var instance = (GameObject)PrefabUtility.InstantiatePrefab(
            targetModule, generator.transform);

        var position = offsetPosition + new Vector3(
            x * generator.ModuleSize, 0, y * generator.ModuleSize);

        var rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);

        var instanceTransform = instance.transform;
        instanceTransform.position = position;
        instanceTransform.rotation = rotation;
    }

    private void CombineLightProbes()
    {
        var lightProbeGroups = generator.GetComponentsInChildren<LightProbeGroup>();
        var probePositions = new List<Vector3>();

        foreach(var lightProbeGroup in lightProbeGroups)
        {
            var groupTransform = lightProbeGroup.transform;
            var groupPosition = groupTransform.position;
            var groupRotation = groupTransform.rotation;

            foreach(var probePosition in lightProbeGroup.probePositions)
            {
                var adjustedProbePosition = groupRotation * probePosition + groupPosition;
                probePositions.Add(adjustedProbePosition);
            }
            lightProbeGroup.gameObject.SetActive(false);
        }
        CreateLightProbeGroup(probePositions);
    }

    private void CreateLightProbeGroup(List<Vector3> probePositions)
    {
        var gameObject = new GameObject
        {
            transform =
            {
                parent = generator.transform
            },
            name = "Main Light Probe Group"
        };

        var lightProbeGroup = gameObject.AddComponent<LightProbeGroup>();
        lightProbeGroup.probePositions = probePositions.ToArray();
    }
}
