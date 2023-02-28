using Cinemachine;
using UnityEngine;

public class PlayerScrollController : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 20f;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private float _minFieldView = 2.5f;
    [SerializeField] private float _maxFieldView = 500f;
    
    public void Scroll(float mouseScroll)
    {
        if (mouseScroll == 0
            || mouseScroll < 0 && _playerCamera.m_Lens.OrthographicSize >= _maxFieldView
            || mouseScroll > 0 && _playerCamera.m_Lens.OrthographicSize <= _minFieldView)
        {
            return;
        }
        
        _playerCamera.m_Lens.OrthographicSize += -mouseScroll * _scrollSpeed;
    }
}