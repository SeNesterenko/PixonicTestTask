using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _spacePrefab;
    [SerializeField] private TileBase _tilePlanet;
    [SerializeField] private Grid _grid;
    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private PlanetSorter _planetSorter;

    [SerializeField] private int _percentagePlanetsPerСhunk;
    [SerializeField] private int _chunkSize = 100;

    public int ChunkSize => _chunkSize;
    
    public Tilemap GenerateChunk(int xPosition, int yPosition)
    {
        var spaceTilemap = Instantiate(_spacePrefab, _grid.transform);
        GeneratePlanets(xPosition, yPosition, spaceTilemap);

        return spaceTilemap;
    }

    private void GeneratePlanets(int xPosition, int yPosition, Tilemap spaceTilemap)
    {
        var xFinalPosition = xPosition * _chunkSize;
        var yFinalPosition = yPosition * _chunkSize;
        var planets = new List<Planet>();

        for (var i = 0; i < _percentagePlanetsPerСhunk; i++)
        {
            var x = Random.Range(xFinalPosition - _chunkSize, xFinalPosition);
            var y = Random.Range(yFinalPosition - _chunkSize, yFinalPosition);
            var tilePosition = new Vector3Int(x, y, 0);
            
            var planet = Instantiate(_planetPrefab, spaceTilemap.transform);
            planet.transform.position = tilePosition;
            planet.Initialize(tilePosition);
            planets.Add(planet);
            
            spaceTilemap.SetTile(tilePosition, _tilePlanet);
        }
        
        _planetSorter.ResortPlanets(planets);
    }
}