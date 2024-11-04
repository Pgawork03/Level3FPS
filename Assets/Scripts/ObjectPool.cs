using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private int objectsNumberOnStart;

    private List<GameObject> objectsPool = new List<GameObject>();

    private void Start()
    {
        CreateObjects(objectsNumberOnStart);

    }

    void CreateObjects (int numbersOfObejectes)
    {
        //Create the objects needed at the begining of the game
        for (int i = 0; i < numbersOfObejectes; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Instantiate new object and add to the list
    /// </summary>
    /// <returns></returns>
    private GameObject CreateNewObject()
    {

        //Instantiate anywhere
        GameObject newObject = Instantiate(prefabObject);
        //Desactive
        newObject.SetActive(false);
        //Ass to the list
        objectsPool.Add( newObject );

        return newObject;
    }
    /// <summary>
    /// Take from the list an available object
    /// id not exist create a new one and 
    /// Active the object
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject() 
    {
        //Find in the objectsPool an object that is inactive in the game hierachy
        GameObject theObject = objectsPool.Find(x => x.activeInHierarchy == false);

        //if not exist, create one
        if (gameObject == null)
        {
            theObject = CreateNewObject();
        }

        //Active gameObjects
        theObject.SetActive(true);

        return theObject;
    }
}
