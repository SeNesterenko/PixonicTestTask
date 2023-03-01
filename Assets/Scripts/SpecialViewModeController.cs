using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class SpecialViewModeController : MonoBehaviour
{
    [SerializeField] private int _countNearestPlanets = 20;
    [SerializeField] private PlanetSorter _planetSorter;
    
    private CinemachineVirtualCamera _playerCamera;
    private Transform _player;

    public void Initialize(CinemachineVirtualCamera playerCamera, Transform player)
    {
        _playerCamera = playerCamera;
        _player = player;
    }

    //Call it when special view mode off
    [UsedImplicitly]
    public void OffSpecialViwMode()
    {
        
    }
    
    //Call it when special view mode on
    [UsedImplicitly]
    public void OnSpecialViewMode()
    {
        var planets = _planetSorter.GetNearestPlanets((int)_playerCamera.m_Lens.OrthographicSize, _player.position, _countNearestPlanets, 4639);

        Debug.Log("-----------------------------------");
        foreach (var planet in planets)
        {
            Debug.Log(planet.Rank);
        }
    }
}