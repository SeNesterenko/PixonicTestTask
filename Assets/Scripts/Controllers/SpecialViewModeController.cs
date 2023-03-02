using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class SpecialViewModeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<List<Planet>> _viewChanged;

        [SerializeField] private int _sizeRelativeCamera = 15;
        [SerializeField] private int _countNearestPlanets = 20;
        [SerializeField] private PlanetSorter _planetSorter;

        private Player _player;
        private CinemachineVirtualCamera _playerCamera;
        private bool _isSpecialViewMode;
        private Vector3 _previousPlayerPosition;

        public void Initialize(CinemachineVirtualCamera playerCamera, Player player)
        {
            _playerCamera = playerCamera;
            _player = player;
        }

        //Call it when special view mode off
        [UsedImplicitly]
        public void OffSpecialViwMode()
        {
            _isSpecialViewMode = false;
        }
    
        //Call it when special view mode on
        [UsedImplicitly]
        public void OnSpecialViewMode()
        {
            ProcessPlanets();
            _isSpecialViewMode = true;
        }

        private void Update()
        {
            if (_isSpecialViewMode)
            {
                if (_player.Transform.position != _previousPlayerPosition)
                {
                    ProcessPlanets();
                    _previousPlayerPosition = _player.Transform.position;
                }
            }
        }

        private void ProcessPlanets()
        {
            var planets = _planetSorter.GetNearestPlanets((int)_playerCamera.m_Lens.OrthographicSize, _player.Transform.position, _countNearestPlanets, 5674);
            foreach (var specialViewPlanet in planets.Select(planet => planet.GetSpecialViewPlanet()))
            {
                specialViewPlanet.ResizePlanet(new Vector3((int) _playerCamera.m_Lens.OrthographicSize,
                    (int) _playerCamera.m_Lens.OrthographicSize) / _sizeRelativeCamera);
            }
            _viewChanged.Invoke(planets);
        }
    }
}