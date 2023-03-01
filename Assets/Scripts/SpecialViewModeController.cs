using Cinemachine;
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
    
    public void DisplayPlanets()
    {
        var planets = _planetSorter.GetNearestPlanets((int)_playerCamera.m_Lens.OrthographicSize, _player.position, _countNearestPlanets, 4639);

        Debug.Log("-----------------------------------");
        foreach (var planet in planets)
        {
            Debug.Log(planet.Rank);
        }
    }
}