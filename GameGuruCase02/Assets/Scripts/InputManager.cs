using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SlingShotProject
{
    [CreateAssetMenu(fileName = "InputDataManager", menuName = "Inputs/New Input Manager")]
    public class InputManager : ScriptableObject
    {
        [SerializeField] private float sensetivity = 0.005f;

        private PlayerControls controls;
        private Vector2 stretch;

        #region Events
        public delegate void TouchPressDelegate();
        public event TouchPressDelegate onTouchPressStarted;

        public delegate void TouchPositionDelegate(Vector2 position);
        public event TouchPositionDelegate onTouchDrag;
        public event TouchPositionDelegate onTouchPressCanceled;
        #endregion

        public void EnableControls()
        {
            controls = new PlayerControls();
            controls.Enable();
            controls.Touch.TouchPress.started += TouchPressStarted;
            controls.Touch.TouchPress.canceled += TouchPressCanceled;
            controls.Touch.TouchDelta.performed += TouchDragPerformed;
        }

        public void DisableControls()
        {
            controls.Touch.TouchPress.started -= TouchPressStarted;
            controls.Touch.TouchPress.canceled -= TouchPressCanceled;
            controls.Touch.TouchDelta.performed -= TouchDragPerformed;
            controls.Disable();
        }

        private void TouchPressStarted(InputAction.CallbackContext context)
        {
            stretch = Vector2.zero;
            onTouchPressStarted?.Invoke();
        }
        private void TouchDragPerformed(InputAction.CallbackContext context)
        {
            Vector2 pos = context.ReadValue<Vector2>() * sensetivity;
            stretch -= pos;
            onTouchDrag?.Invoke(pos);
        }

        private void TouchPressCanceled(InputAction.CallbackContext context)
        {
            onTouchPressCanceled?.Invoke(stretch);
        }

    }
}