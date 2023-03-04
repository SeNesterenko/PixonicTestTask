using UnityEngine;

namespace PlayerManagement
{
    [RequireComponent(typeof(PlayerMoveController))]
    [RequireComponent(typeof(PlayerScrollController))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerMoveController _playerMoveController;
        [SerializeField] private PlayerScrollController _playerScrollController;
    
        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        
            _playerMoveController.Move(x, y);
            _playerScrollController.Scroll(mouseScroll);
        }
    }
}