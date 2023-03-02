using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Planet : MonoBehaviour
{
    [SerializeField] private RectTransform _planetRectTransform;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _minRank;
    [SerializeField] private int _maxRank = 10000;
    [SerializeField] private Planet _specialViewPrefab;

    private Planet _specialViewPlanet;
    public int Rank { get; private set; }
    public Vector2 Coordinates { get; private set; }

    public void Initialize(Vector3 coordinates, bool isSpecialView, int rank = -1)
    {
        Rank = rank == -1 ? Random.Range(_minRank, _maxRank) : rank;
        
        Coordinates = coordinates;
        _text.text = Rank.ToString();

        if (isSpecialView)
        {
            var specialViewPlanet = Instantiate(_specialViewPrefab);
            specialViewPlanet.Initialize(transform.position, false, Rank);
            specialViewPlanet.transform.position = transform.position;
            specialViewPlanet.gameObject.SetActive(false);
            _specialViewPlanet = specialViewPlanet;
        }
    }

    public void ResizePlanet(Vector3 newSize)
    {
        _planetRectTransform.localScale = newSize;
    }

    public Planet GetSpecialViewPlanet()
    {
        return _specialViewPlanet;
    }
}