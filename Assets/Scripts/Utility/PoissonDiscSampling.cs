using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{
    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int rejectionThreshold = 10)
    {
        float cellSize = radius / Mathf.Sqrt(2);
        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];
        List<Vector2> points = new List<Vector2>();

        List<Vector2> samplePoints = new List<Vector2>();
        samplePoints.Add(sampleRegionSize / 2);
        while (samplePoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, samplePoints.Count);
            Vector2 spawnPoint = samplePoints[spawnIndex];
            bool validCandidate = false;
            for (int i = 0; i < rejectionThreshold; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnPoint + dir * Random.Range(radius, radius * 2);
                if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    samplePoints.Add(candidate);
                    grid[Mathf.FloorToInt(candidate.x / cellSize), Mathf.FloorToInt(candidate.y / cellSize)] = points.Count;
                    validCandidate = true;
                    //Debug.Log("valid candidate");
                    break;
                }
            }
            if (!validCandidate)
            {
                samplePoints.RemoveAt(spawnIndex);
            }
        }
        Debug.Log(points.Count);
        return points;
    }

    static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);
            for (int x = searchStartX; x < searchEndX; x++)
            {
                for (int y = searchStartY; y < searchEndY; y++)
                {
                    int pointIndex = grid[x, y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDist = (candidate - points[pointIndex]).sqrMagnitude;
                        if (sqrDist < radius * radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}
