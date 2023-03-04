using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class SpecialViewModeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<List<Planet>> _specialViewModeActivated;
        [SerializeField] private UnityEvent _specialViewModeDisabled;

        [SerializeField] private int _sizeRelativeCamera = 15;
        [SerializeField] private int _countNearestPlanets = 20;
        [SerializeField] private PlanetSorter _planetSorter;
        [SerializeField] private float _specialViewModeDistance = 30f;
        
        private Player _player;
        private CinemachineVirtualCamera _playerCamera;
        private bool _isSpecialViewMode;
        
        private Vector3 _previousPlayerPosition;
        private float _previousCameraOrthographicSize;

        public void Initialize(CinemachineVirtualCamera playerCamera, Player player)
        {
            _playerCamera = playerCamera;
            _player = player;
        }

        private void Update()
        {
            if (_playerCamera.m_Lens.OrthographicSize >= _specialViewModeDistance)
            {
                _isSpecialViewMode = true;
            }

            else if (_playerCamera.m_Lens.OrthographicSize <= _specialViewModeDistance)
            {
                _isSpecialViewMode = false;
            }
            
            if (_isSpecialViewMode)
            {
                if (_player.Transform.position != _previousPlayerPosition
                || _playerCamera.m_Lens.OrthographicSize != _previousCameraOrthographicSize)
                {
                    ProcessPlanets();
                    _previousPlayerPosition = _player.Transform.position;
                    _previousCameraOrthographicSize = _playerCamera.m_Lens.OrthographicSize;
                }
            }
            else
            {
                _specialViewModeDisabled.Invoke();
            }
        }

        private void ProcessPlanets()
        {
            var distanceValue = (int) _playerCamera.m_Lens.OrthographicSize;
            var playerPosition = _player.Transform.position;
            var newPlanetSize = new Vector3(distanceValue, distanceValue) / _sizeRelativeCamera;
            var planets = _planetSorter.GetNearestPlanets(distanceValue, playerPosition, _countNearestPlanets, _player.Rank);
            
            foreach (var specialViewPlanet in planets.Select(planet => planet.GetSpecialViewPlanet()))
            {
                specialViewPlanet.ResizePlanet(newPlanetSize);
            }
            
            _specialViewModeActivated.Invoke(planets);
        }
    }
}