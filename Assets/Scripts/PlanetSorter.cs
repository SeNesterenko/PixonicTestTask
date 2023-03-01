using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PlanetSorter : MonoBehaviour
{
    private bool _isActive;
    
    private List<Planet> _sortedPlanetsByX = new ();
    private List<Planet> _sortedPlanetsByY = new ();

    //Call it when new chunk generated
    [UsedImplicitly]
    public void ResortPlanets(List<Planet> newPlanets)
    {
        foreach (var planet in newPlanets)
        {
            _sortedPlanetsByX.Add(planet);
            _sortedPlanetsByY.Add(planet);
        }

        _sortedPlanetsByX = _sortedPlanetsByX.OrderBy(planet => (int) planet.Coordinates.x).ToList();
        _sortedPlanetsByY = _sortedPlanetsByY.OrderBy(planet => (int) planet.Coordinates.y).ToList();
    }

    public List<Planet> GetNearestPlanets(int fieldView, Vector3 currentPlayerPosition, int countPlanets, int playerRank)
    {
        var xPosition = (int) currentPlayerPosition.x;
        var yPosition = (int) currentPlayerPosition.y;
        var closestPlanets = new List<Planet>();

        var planetsInSight = _sortedPlanetsByX.
            Where(t => t.Coordinates.x >= xPosition - fieldView && t.Coordinates.x <= xPosition + fieldView).
            Where(t => t.Coordinates.y >= yPosition - fieldView && t.Coordinates.y <= yPosition + fieldView).
            ToList();

        planetsInSight = planetsInSight.OrderBy(planet => planet.Rank).ToList();

        var index = -1;
        if (planetsInSight.Count != 0)
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
                for (var i = 0; i < planetsInSight.Count; i++)
                {
                    if (playerRank <= planetsInSight[i].Rank)
                    {
                        index = i;
                        break;
                    }
                }
            }
            
            var leftBorder = index - 1;
            var rightBorder = index;
        
            while (closestPlanets.Count < countPlanets && (leftBorder >= 0 || rightBorder < planetsInSight.Count))
            {
                if (leftBorder >= 0 && (rightBorder >= planetsInSight.Count ||
                                        Math.Abs(playerRank - planetsInSight[leftBorder].Rank) <= Math.Abs(planetsInSight[rightBorder].Rank - playerRank)))
                {
                    closestPlanets.Add(planetsInSight[leftBorder]);
                    leftBorder--;
                }
                else
                {
                    closestPlanets.Add(planetsInSight[rightBorder]);
                    rightBorder++;
                }
            }
        }
        
        
        return closestPlanets;
    }
}