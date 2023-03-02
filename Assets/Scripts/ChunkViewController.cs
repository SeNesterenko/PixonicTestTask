using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkViewController : MonoBehaviour
{
    private List<Tilemap> _activeChunks = new ();
    private List<Planet> _specialViewModePlanets = new();
    
    public void DisableChunks()
    {
        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.gameObject.SetActive(false);
        }
    }

    public void ActivateChunks(List<Tilemap> activeChunks)
    {
        _activeChunks = activeChunks;

        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.gameObject.SetActive(true);
        }
    }

    public void ShowPlanets(List<Planet> planets)
    {
        DisableChunks();
        DisablePlanets();

        Debug.Log("-----------------------------------");
        foreach (var planet in planets)
        {
            Debug.Log(planet.Rank);
            var specialView = planet.GetSpecialViewPlanet();
            specialView.gameObject.SetActive(true);
            _specialViewModePlanets.Add(specialView);
        }
    }

    private void DisablePlanets()
    {
        foreach (var planet in _specialViewModePlanets)
        {
            planet.gameObject.SetActive(false);
        }
        
        _specialViewModePlanets.Clear();
    }
}