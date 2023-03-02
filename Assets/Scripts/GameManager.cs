using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private Transform _player;
    
    [SerializeField] private SpecialViewModeController _specialViewModeController;
    [SerializeField] private ChunkController _chunkController;

    private void Start()
    {
        _specialViewModeController.Initialize(_playerCamera, _player);
        _chunkController.Initialize(_player);
    }
}