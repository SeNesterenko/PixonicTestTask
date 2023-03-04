using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    public int ChunkSize => _chunkSize;
    public List<Planet> Planets { get; private set; }
    
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase _tilePlanet;
    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private int _chunkSize = 100;

    public void GeneratePlanets(int countPlants, int xPosition, int yPosition)
    {
        Planets = new List<Planet>();
        
        for (var i = 0; i < countPlants; i++)
        {
            var x = Random.Range(xPosition - _chunkSize, xPosition);
            var y = Random.Range(yPosition - _chunkSize, yPosition);
            var planetPosition = new Vector3Int(x, y, 0);
            
            var planet = Instantiate(_planetPrefab, transform.transform);
            planet.transform.position = planetPosition;
            planet.Initialize(planetPosition, true);
            Planets.Add(planet);
            
            _tileMap.SetTile(planetPosition, _tilePlanet);
        }
    }
}