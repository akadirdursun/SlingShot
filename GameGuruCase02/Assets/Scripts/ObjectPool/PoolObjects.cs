using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    [CreateAssetMenu(fileName = "PoolObject", menuName = "Object Pooling/NewObjectPool")]
    public class PoolObjects : ScriptableObject
    {
        public GameObject objectPrefab;
        public Queue<GameObject> poolObjects = new Queue<GameObject>();

        public GameObject GetNextObject()
        {
            if (poolObjects.Count == 0 || poolObjects.Peek().activeInHierarchy)
                return InstantiateNewObjectToPool();

            GameObject obj = poolObjects.Dequeue();
            poolObjects.Enqueue(obj);
            return obj;
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