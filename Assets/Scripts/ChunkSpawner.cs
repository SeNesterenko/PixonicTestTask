using UnityEngine;
using UnityEngine.Events;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private UnityEvent<Chunk> _chunkGenerated;
    [SerializeField] private Grid _grid;
    [SerializeField] private Chunk _chunkPrefab;
    [SerializeField] private int _countPlantsPerChunk;

    public Chunk SpawnChunk(Vector2 currentPosition)
    {
        var chunk = Instantiate(_chunkPrefab, _grid.transform);
        
        var xFinalPosition = (int)(currentPosition.x + 1) * chunk.ChunkSize;
        var yFinalPosition = (int)(currentPosition.y + 1) * chunk.ChunkSize;
        
        chunk.GeneratePlanets(_countPlantsPerChunk, xFinalPosition, yFinalPosition);

        _chunkGenerated.Invoke(chunk);
        chunk.gameObject.SetActive(false);
        return chunk;
    }
}