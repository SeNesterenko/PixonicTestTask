using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PlanetSorter : MonoBehaviour
{
    private bool _isActive;
    
    private List<Planet> _sortedPlanetsByX = new ();

    //Call it when new chunk generated in ChunkSpawner
    [UsedImplicitly]
    public void ResortPlanets(Chunk chunk)
    {
        var newPlanets = chunk.Planets;
        
        foreach (var planet in newPlanets)
        {
            _sortedPlanetsByX.Add(planet);
        }

        _sortedPlanetsByX = _sortedPlanetsByX.OrderBy(planet => (int) planet.Coordinates.x).ToList();
    }

    public List<Planet> GetNearestPlanets(int fieldView, Vector3 currentPlayerPosition, int countPlanets, int playerRank)
    {
        var closestPlanets = new List<Planet>();
        var planetsInSight = SortPlanetsInFieldOfViewByAscendingRank(fieldView, currentPlayerPosition);

        var index = -1;
        
        if (planetsInSight.Count != 0)
        {
            index = SearchNearestIndexByValue(playerRank, planetsInSight, index);
            closestPlanets = GetNearestPlanetsByRank(countPlanets, playerRank, index, planetsInSight);
        }

        return closestPlanets;
    }

    private List<Planet> GetNearestPlanetsByRank(int countPlanets, int playerRank, int index, List<Planet> planetsInSight)
    {
        var closestPlanets = new List<Planet>();
        
        var leftBorderIndex = index - 1;
        var rightBorderIndex = index;

        while (closestPlanets.Count < countPlanets && CheckIndexNotOutOfRange(planetsInSight, leftBorderIndex, rightBorderIndex))
        {
            if (leftBorderIndex >= 0 && (rightBorderIndex >= planetsInSight.Count || CheckLeftBorderRankLessThanRightBorderRank(playerRank, planetsInSight, leftBorderIndex, rightBorderIndex)))
            {
                closestPlanets.Add(planetsInSight[leftBorderIndex]);
                leftBorderIndex--;
            }
            else
            {
                closestPlanets.Add(planetsInSight[rightBorderIndex]);
                rightBorderIndex++;
            }
        }
        
        return closestPlanets;
    }

    private static bool CheckIndexNotOutOfRange(List<Planet> planetsInSight, int leftBorder, int rightBorder)
    {
        return leftBorder >= 0 || rightBorder < planetsInSight.Count;
    }

    private bool CheckLeftBorderRankLessThanRightBorderRank(int playerRank, List<Planet> planetsInSight, int leftBorder, int rightBorder)
    {
        return Math.Abs(playerRank - planetsInSight[leftBorder].Rank) <=
               Math.Abs(planetsInSight[rightBorder].Rank - playerRank);
    }

    private List<Planet> SortPlanetsInFieldOfViewByAscendingRank(int fieldView, Vector3 currentPlayerPosition)
    {
        var xPosition = (int) currentPlayerPosition.x;
        var yPosition = (int) currentPlayerPosition.y;

        var planetsInSight = _sortedPlanetsByX
            .Where(t => t.Coordinates.x >= xPosition - fieldView && t.Coordinates.x <= xPosition + fieldView)
            .Where(t => t.Coordinates.y >= yPosition - fieldView && t.Coordinates.y <= yPosition + fieldView)
            .ToList();

        planetsInSight = planetsInSight.OrderBy(planet => planet.Rank).ToList();
        
        return planetsInSight;
    }

    private int SearchNearestIndexByValue(int playerRank, List<Planet> planetsInSight, int index)
    {
        if (playerRank <= planetsInSight[0].Rank)
        {
            index = 0;
        }

        if (playerRank >= planetsInSight[^1].Rank)
        {
            index = planetsInSight.Count - 1;
        }

        if (index == -1)
        {
            index = Utils.BinarySearchByKey(planetsInSight, playerRank, i => planetsInSight[i].Rank);
        }
        
        return index;
    }
}