using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    GameObject obj;
 

    [System.Serializable]
    public class ObjectPool
    {
        public string tag;
        public GameObject Prefab;
        public int Size;   
    }

    public static ObjectPoolingManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(this.gameObject);
    }

    public List<ObjectPool> ObjectPoolList;
    public Dictionary<string, Queue<GameObject>> ObjectPoolDictionary;
  
    private void Start()
    {
        ObjectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(ObjectPool pool in ObjectPoolList)
        {   
            Queue<GameObject> objectPool = new Queue<GameObject>();
              
            for (int i=0;i< pool.Size; i++)
            {
                obj=  Instantiate(pool.Prefab) as GameObject;
                obj.name = pool.Prefab.name;
                obj.transform.SetParent(gameObject.transform);  
                obj.SetActive(false);                      
                objectPool.Enqueue(obj);                                
            }         
            ObjectPoolDictionary.Add(pool.tag, objectPool);          
        }
    }
  
    public GameObject GetObject(string tag,GameObject Parent)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();  
        SpawnObject.transform.SetParent(Parent.transform);
        SpawnObject.transform.position = Parent.transform.position;  
        SpawnObject.transform.rotation = Parent.transform.rotation;
        SpawnObject.SetActive(true);
        return SpawnObject;
    }
    public GameObject GetObject(string tag, GameObject Parent,Vector3 position,Quaternion rotation)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue(); 
        SpawnObject.transform.SetParent(gameObject.transform);
        SpawnObject.transform.position = position;
        SpawnObject.transform.rotation = rotation;
        SpawnObject.SetActive(true);
        return SpawnObject;
    }
    public GameObject GetObject_Noparent(string tag, GameObject Parent)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();
        SpawnObject.SetActive(true);
        SpawnObject.transform.position = Parent.transform.position;
        SpawnObject.transform.rotation = Parent.transform.rotation;

        return SpawnObject;
    }
    public GameObject GetObject_NoRotate(string tag, GameObject Parent)
    {
        if (!ObjectPoolDictionary.ContainsKey(tag))
            return null;
        GameObject SpawnObject;
        SpawnObject = ObjectPoolDictionary[tag].Dequeue();
        SpawnObject.transform.position = Parent.transform.position;
        SpawnObject.transform.rotation = Parent.transform.rotation;
        SpawnObject.SetActive(true);
        return SpawnObject;
    }
    public GameObject ReturnObject(string tag, GameObject Object)
    {
        ObjectPoolDictionary[tag].Enqueue(Object);
        Object.transform.SetParent(gameObject.transform);
        Object.SetActive(false);
        return Object;
    }
}
