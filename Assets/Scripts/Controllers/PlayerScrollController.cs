using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class PlayerScrollController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _specialViewModeOn;
        [SerializeField] private UnityEvent _specialViewModeOff;
    
        [SerializeField] private float _scrollSpeed = 20f;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private float _minFieldView = 5f;
        [SerializeField] private float _maxFieldView = 10000f;
        [SerializeField] private float _specialViewModeDistance = 10f;

        private bool _isSpecialViewMode;
        private void Start()
        {
            _minFieldView /= 2;
            _maxFieldView /= 2;
        }

        public void Scroll(float mouseScroll)
        {
            if (mouseScroll == 0
                || mouseScroll < 0 && _playerCamera.m_Lens.OrthographicSize >= _maxFieldView
                || mouseScroll > 0 && _playerCamera.m_Lens.OrthographicSize <= _minFieldView)
            {
                return;
            }

            _playerCamera.m_Lens.OrthographicSize += -mouseScroll * _scrollSpeed;
            
            if (_playerCamera.m_Lens.OrthographicSize >= _specialViewModeDistance && !_isSpecialViewMode)
            {
                _isSpecialViewMode = true;
            }
            else if (_isSpecialViewMode && _playerCamera.m_Lens.OrthographicSize <= _specialViewModeDistance)
            {
                _specialViewModeOff.Invoke();
                _isSpecialViewMode = false;
            }

            if (_isSpecialViewMode)
            {
                _specialViewModeOn.Invoke();
            }
        }
    }
}