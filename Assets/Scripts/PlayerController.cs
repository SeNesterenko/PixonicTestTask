using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _minRank;
    [SerializeField] private int _maxRank = 10000;
    
    private int _rank;

    private void Start()
    {
        _rank = Random.Range(_minRank, _maxRank);
    }
}