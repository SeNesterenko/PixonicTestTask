using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGeneratorController : MonoBehaviour
{
    [SerializeField] private ChunkGenerator _chunkGenerator;
    [SerializeField] private Transform _player;
    [SerializeField] private int _startQuantityChunks;

    private readonly Dictionary<Vector2, Tilemap> _chunks = new ();
    private readonly List<Tilemap> _activeChunks = new ();
    private Vector2 _currentPosition;

    private int _currentXIndex;
    private int _currentYIndex;
    private void Start()
    {
        SetCurrentPositionIndexes();
        var currentPosition = new Vector2(_currentXIndex, _currentYIndex);
        var chunkStartIndex = Mathf.Sqrt(_startQuantityChunks) / 2;

        for (var i = -chunkStartIndex; i < chunkStartIndex; i++)
        {
            for (var j = -chunkStartIndex; j < chunkStartIndex; j++)
            {
                SetChunk(new Vector2(currentPosition.x + i, currentPosition.y + j));
            }
        }
        
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                _chunks[new Vector2(currentPosition.x + x, currentPosition.y + y)].gameObject.SetActive(true);
                _activeChunks.Add(_chunks[new Vector2(currentPosition.x + x, currentPosition.y + y)]);
            }
        }
        
        _currentPosition = currentPosition;
    }

    private void Update()
    {
        SetCurrentPositionIndexes();
        var currentPosition = new Vector2(_currentXIndex, _currentYIndex);
    
        if (!_chunks.ContainsKey(currentPosition) || _chunks.ContainsKey(currentPosition) && _currentPosition != currentPosition)
        {
            DisableChunks();
            
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (!_chunks.ContainsKey(new Vector2(currentPosition.x + x, currentPosition.y + y)))
                    {
                        var currentVector2Index = new Vector2(currentPosition.x + x, currentPosition.y + y);
                        SetChunk(currentVector2Index);
                        _chunks[currentVector2Index].gameObject.SetActive(true);
                        _activeChunks.Add(_chunks[currentVector2Index]);
                    }
                    else if (_chunks.ContainsKey(new Vector2(currentPosition.x + x, currentPosition.y + y)))
                    {
                        _chunks[new Vector2(currentPosition.x + x, currentPosition.y + y)].gameObject.SetActive(true);
                        _activeChunks.Add(_chunks[new Vector2(currentPosition.x + x, currentPosition.y + y)]);
                    }
                }
            }
            
            _currentPosition = currentPosition;
        }
    }

    private void DisableChunks()
    {
        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.gameObject.SetActive(false);
        }
    }

    private void SetCurrentPositionIndexes()
    {
        _currentXIndex = (int)_player.position.x / _chunkGenerator.ChunkSize;
        _currentYIndex = (int)_player.position.y / _chunkGenerator.ChunkSize;
    }
    
    private void SetChunk(Vector2 currentPosition)
    {
        if (!_chunks.ContainsKey(currentPosition))
        {
            var spaceTilemap = _chunkGenerator.GenerateChunk((int)currentPosition.x + 1, (int)currentPosition.y + 1);
            _chunks.Add(currentPosition, spaceTilemap);

            spaceTilemap.gameObject.SetActive(false);
        }
    }
}