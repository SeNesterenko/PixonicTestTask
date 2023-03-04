using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public int Rank { get; private set; }
        public Transform Transform => _transform;
    
        [SerializeField] private int _minRank;
        [SerializeField] private int _maxRank = 10000;
        [SerializeField] private Transform _transform;

        private void Start()
        {
            Rank = Random.Range(_minRank, _maxRank);
        }
    }
}