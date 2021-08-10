using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    [CreateAssetMenu(fileName = "PoolObject", menuName = "Object Pooling/NewObjectPool")]
    public class PoolObjects : ScriptableObject
    {
        [SerializeField] private GameObject objectPrefab;
        private GameObject currentObject;
        private Queue<GameObject> poolObjects = new Queue<GameObject>();

        public GameObject CurrentObject { get => currentObject; }

        public GameObject GetNextObject()
        {
            if (poolObjects.Count == 0 || poolObjects.Peek().activeInHierarchy)
            {
                currentObject = InstantiateNewObjectToPool();
                return currentObject;
            }

            currentObject = poolObjects.Dequeue();
            poolObjects.Enqueue(currentObject);
            return currentObject;
        }

        public GameObject InstantiateNewObjectToPool()
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            poolObjects.Enqueue(obj);
            return obj;
        }

        private void OnDisable()
        {
            poolObjects.Clear();
        }
    }
}