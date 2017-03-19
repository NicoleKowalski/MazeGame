using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public GameObject instObj;
    public float minx = 1;
    public float maxx = 10;
    public float miny = 0;
    public float maxy = 10;
    public float minz = 0;
    public float maxz = 10;

    void Start(){
        float x = Random.Range(minx, maxx);
        float y = Random.Range(miny, maxy);
        float z = Random.Range(minz, maxz);
        //GameObject spawnedObject = (GameObject) 
        Instantiate(instObj, new Vector3(x, y, z), Quaternion.identity);
        //spawnedObject.transform.parent = gameObject.transform;
    }
}
