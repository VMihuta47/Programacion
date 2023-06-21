using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolItem
{
    //Prefabricado de este Item concreto
    public GameObject prefab;
    //Cantidad de este Item
    public int amount;
    //Variable para hacer expandible este pool
    public bool expandable;
}

public class PoolObjects : MonoBehaviour
{
    //Singletón del pool
    public static PoolObjects singleton;
    public List<PoolItem> items;
    public List<GameObject> pooledObjects;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (PoolItem item in items)
        {
            //Se guarda la cantidad de objetos que hay de cada tipo
            for (int i = 0; i < item.amount; i++)
            {
                //Referencia para guardar GameObjects en la lista y también los instanciamos en la escena
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                //Los añadimos a la lista
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject Get(string tag)
    {
        //Se realiza una pasada por toda la lista de objetos creados mediante el Pool
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //Si el objeto que se ve no está activo en la jerarquía y su tag coincide con la que le hemos pasado
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                //Nos devuelve ese objeto en concreto de la lista
                return pooledObjects[i];
            }
        }
        //Pasada por la lista de PoolItems, y se guarda en esta nueva lista, el nuevo objeto que ha sido creado de un tipo elegido
        foreach (PoolItem item in items)
        {
            //Si el objeto que se encuentra en esa lista tiene el mismo tag y además es expandible
            if (item.prefab.tag == tag && item.expandable)
            {
                //SE genera una referencia al gameObject
                GameObject obj = Instantiate(item.prefab);
                //Se desactiva
                obj.SetActive(false);
                //Se añade a la lista de objetos del pool
                pooledObjects.Add(obj);
                //Devuelve ese objeto en concreto de la lista
                return obj;
            }
        }
        //Devuelve un objeto vacío
        return null;
    }
}
