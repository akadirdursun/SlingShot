using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PoolObjects poolObjects;

        private void Awake()
        {
            for (int i = 0; i < 2; i++)
            {
                poolObjects.InstantiateNewObjectToPool();
            }
        }

        private void OnEnable()
        {
            inputManager.EnableControls();
        }

        private void OnDisable()
        {
            inputManager.DisableControls();
        }
    }
}