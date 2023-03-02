using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private UnityEvent<List<Planet>> _chunkGenerated;
    
    [SerializeField] private Tilemap _spacePrefab;
    [SerializeField] private TileBase _tilePlanet;
    [SerializeField] private Grid _grid;
    [SerializeField] private Planet _planetPrefab;

    [SerializeField] private int _percentagePlanetsPerСhunk;
    [SerializeField] private int _chunkSize = 100;
    public int ChunkSize => _chunkSize;

    public Tilemap SpawnChunk(Vector2 currentPosition)
    {
        var spaceTilemap = Instantiate(_spacePrefab, _grid.transform);
        
        var xFinalPosition = (currentPosition.x + 1) * _chunkSize;
        var yFinalPosition = (currentPosition.y + 1) * _chunkSize;
        var planets = new List<Planet>();

        for (var i = 0; i < _percentagePlanetsPerСhunk; i++)
        {
            var x = (int) Random.Range(xFinalPosition - _chunkSize, xFinalPosition);
            var y = (int) Random.Range(yFinalPosition - _chunkSize, yFinalPosition);
            var tilePosition = new Vector3Int(x, y, 0);
            
            var planet = Instantiate(_planetPrefab, spaceTilemap.transform);
            planet.transform.position = tilePosition;
            planet.Initialize(tilePosition);
            planets.Add(planet);
            
            spaceTilemap.SetTile(tilePosition, _tilePlanet);
        }
        
        _chunkGenerated.Invoke(planets);
        return spaceTilemap;
    }
}