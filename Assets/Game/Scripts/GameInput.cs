using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CSC2026
{
    public class GameInput : MonoBehaviour
    {
        public static Vector2 Position => Camera.main.ScreenToWorldPoint(_actions.PC.Position.ReadValue<Vector2>());
        public static event UnityAction OnMouseUp;

        private static Actions _actions;

        private void Awake()
        {
            _actions = new Actions();
            _actions.Enable();

            _actions.PC.Click.canceled += (InputAction.CallbackContext context) => { OnMouseUp?.Invoke(); };
        }
    }
}
