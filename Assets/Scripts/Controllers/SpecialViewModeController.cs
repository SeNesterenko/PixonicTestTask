using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class SpecialViewModeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<List<Planet>> _viewChanged;

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
            var planets = _planetSorter.GetNearestPlanets((int)_playerCamera.m_Lens.OrthographicSize, _player.Transform.position, _countNearestPlanets, _player.Rank);
            _viewChanged.Invoke(planets);
            _isSpecialViewMode = true;
        }

        private void Update()
        {
            if (_isSpecialViewMode)
            {
                if (_player.Transform.position != _previousPlayerPosition)
                {
                    var planets = _planetSorter.GetNearestPlanets((int)_playerCamera.m_Lens.OrthographicSize, _player.Transform.position, _countNearestPlanets, _player.Rank);
                    _viewChanged.Invoke(planets);
                    _previousPlayerPosition = _player.Transform.position;
                }
            }
        }
    }
}