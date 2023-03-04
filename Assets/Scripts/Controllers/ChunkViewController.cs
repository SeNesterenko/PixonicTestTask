using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class ChunkViewController : MonoBehaviour
    {
        private readonly List<Tilemap> _activeChunks = new ();
        private readonly List<Planet> _specialViewModePlanets = new();
    
        public void DisableChunks()
        {
            foreach (var activeChunk in _activeChunks)
            {
                activeChunk.gameObject.SetActive(false);
            }
        }

        public void ActivateChunks(List<Tilemap> activeChunks)
        {
            _activeChunks.Clear();

            foreach (var chunk in activeChunks)
            {
                chunk.gameObject.SetActive(true);
                _activeChunks.Add(chunk);
            }
        }

        //Call it when ViewMode changed
        [UsedImplicitly]
        public void ShowPlanets(List<Planet> planets)
        {
            DisableChunks();
            DisablePlanets();

            foreach (var specialView in planets.Select(planet => planet.GetSpecialViewPlanet()))
            {
                specialView.gameObject.SetActive(true);
                _specialViewModePlanets.Add(specialView);
            }
        }

        public void DisablePlanets()
        {
            foreach (var planet in _specialViewModePlanets)
            {
                planet.gameObject.SetActive(false);
            }
            
            _specialViewModePlanets.Clear();
        }
    }
}