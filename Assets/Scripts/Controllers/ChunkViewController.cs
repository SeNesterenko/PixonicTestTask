using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
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