using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkViewController : MonoBehaviour
{
    private List<Tilemap> _activeChunks = new ();

    public void DisableChunks()
    {
        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.gameObject.SetActive(false);
        }
    }

    public void ActivateChunks(List<Tilemap> activeChunks)
    {
        _activeChunks = activeChunks;

        foreach (var activeChunk in _activeChunks)
        {
            activeChunk.gameObject.SetActive(true);
        }
    }
}