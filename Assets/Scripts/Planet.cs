using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _minRank;
    [SerializeField] private int _maxRank = 10000;
    
    public Guid Id { get; private set; }
    public int Rank { get; private set; }
    public Vector2 Coordinates { get; private set; }

    public void Initialize(Vector3 coordinates)
    {
        Id = Guid.NewGuid();
        Rank = Random.Range(_minRank, _maxRank);
        Coordinates = coordinates;
        _text.text = Rank.ToString();
    }
}