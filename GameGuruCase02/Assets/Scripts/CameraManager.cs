using UnityEngine;
using Cinemachine;

namespace SlingShotProject
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PoolObjects objectPool;        
        [Header("Cameras")]
        [SerializeField] private CinemachineVirtualCamera mainCM;
        [SerializeField] private CinemachineVirtualCamera aimCM;

        private Character currentCharacter;
        private void OnEnable()
        {
            inputManager.onTouchPressStarted += Aim;
            inputManager.onTouchPressCanceled += OnTouchCanceled;
        }
        private void OnDisable()
        {
            inputManager.onTouchPressStarted -= Aim;
            inputManager.onTouchPressCanceled -= OnTouchCanceled;
        }
        private void OnTouchCanceled(Vector2 stretch)
        {
            if (stretch.y < 0.5f)
            {
                ReleaseAim();
                return;
            }
        }
        private void Aim()
        {
            currentCharacter = objectPool.CurrentObject.GetComponent<Character>();
            aimCM.Follow = currentCharacter.ShoulderTarget;

            mainCM.gameObject.SetActive(false);
            aimCM.gameObject.SetActive(true);
        }
        private void ReleaseAim()
        {
            aimCM.gameObject.SetActive(false);
            mainCM.gameObject.SetActive(true);
        }
    }
}