using System.Collections.Generic;
using UnityEngine;

namespace VyNS
{
    public class ObjectPool : MonoBehaviour
    {
        // The prefab to be pooled
        [SerializeField] private GameObject objectPrefab;
    
        // The initial size of the pool
        [SerializeField] private int poolSize = 3;
    
        // The list to store pooled objects
        private List<GameObject> pooledObjects;
    
        void Awake()
        {
            // Initialize the pool
            pooledObjects = new List<GameObject>();
    
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectPrefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    
        // Method to get an object from the pool
        public GameObject GetObject()
        {
            foreach (GameObject obj in pooledObjects)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
    
            // If no inactive objects are available, create a new one
            GameObject newObj = Instantiate(objectPrefab);
            newObj.SetActive(true);
            pooledObjects.Add(newObj);
            return newObj;
        }
    
        // Method to return an object back to the pool
        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}