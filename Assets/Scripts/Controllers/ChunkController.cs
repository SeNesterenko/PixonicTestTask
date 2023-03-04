using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public class ChunkController : MonoBehaviour
    {
        [SerializeField] private ChunkViewController _chunkViewController;
        [SerializeField] private ChunkSpawner _chunkSpawner;

        [SerializeField] private int _startQuantityChunks;
        [SerializeField] private int _chunkSize = 100;
        
        private readonly Dictionary<Vector2, Chunk> _chunks = new ();
        private Transform _player;
    
        private int _currentXIndex;
        private int _currentYIndex;
    
        private Vector2 _currentPosition;
    
        public void Initialize(Transform player)
        {
            _player = player;
        }
        
        //Call it when SpecialViewMode disabled from SpecialViewController
        [UsedImplicitly]
        public void InitializeSpawnChunks()
        {
            var currentPosition = SetCurrentPositionIndexes();
        
            var activeChunks = new List<Chunk>();
            
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var rangeOfCurrentPosition = new Vector2(currentPosition.x + x, currentPosition.y + y);
                    
                    if (!_chunks.ContainsKey(rangeOfCurrentPosition))
                    {
                        activeChunks.Add(_chunkSpawner.SpawnChunk(rangeOfCurrentPosition));
                        _chunks.Add(rangeOfCurrentPosition, activeChunks[^1]);
                    }
                    else
                    {
                        activeChunks.Add(_chunks[rangeOfCurrentPosition]);
                    }
                }
            }

            _chunkViewController.ActivateChunks(activeChunks);
            _currentPosition = currentPosition;
        }

        private void Start()
        {
            var currentPosition = SetCurrentPositionIndexes(); 
            var chunkStartIndex = Mathf.Sqrt(_startQuantityChunks) / 2;

            for (var x = -chunkStartIndex; x < chunkStartIndex; x++)
            {
                for (var y = -chunkStartIndex; y < chunkStartIndex; y++)
                {
                    var rangeOfCurrentPosition = new Vector2(currentPosition.x + x, currentPosition.y + y);
                    _chunks.Add(rangeOfCurrentPosition, _chunkSpawner.SpawnChunk(rangeOfCurrentPosition));
                }
            }
        
            var activeChunks = new List<Chunk>();
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    activeChunks.Add(_chunks[new Vector2(currentPosition.x + x, currentPosition.y + y)]);
                }
            }
        
            _chunkViewController.ActivateChunks(activeChunks);
            _currentPosition = currentPosition;
        }

        private void Update()
        {
            var currentPosition = SetCurrentPositionIndexes();

            if (!_chunks.ContainsKey(currentPosition) ||
                _chunks.ContainsKey(currentPosition) && _currentPosition != currentPosition)
            {
                _chunkViewController.DisableChunks();
                var activeChunks = new List<Chunk>();
            
                for (var x = -1; x <= 1; x++)
                {
                    for (var y = -1; y <= 1; y++)
                    {
                        var rangeOfCurrentPosition = new Vector2(currentPosition.x + x, currentPosition.y + y);
                    
                        if (!_chunks.ContainsKey(rangeOfCurrentPosition))
                        {
                            activeChunks.Add(_chunkSpawner.SpawnChunk(rangeOfCurrentPosition));
                            _chunks.Add(rangeOfCurrentPosition, activeChunks[^1]);
                        }
                        else
                        {
                            activeChunks.Add(_chunks[rangeOfCurrentPosition]);
                        }
                    }
                }

                _chunkViewController.ActivateChunks(activeChunks);
                _currentPosition = currentPosition;
            }
        }

        private Vector2 SetCurrentPositionIndexes()
        {
            if (_player != null)
            {
                _currentXIndex = (int)_player.position.x / _chunkSize;
                _currentYIndex = (int)_player.position.y / _chunkSize;
    
                return new Vector2(_currentXIndex, _currentYIndex);
            }

            return default;
        }
    }
}