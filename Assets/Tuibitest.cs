using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuibitest : MonoBehaviour
{
    public Transform target;
    public Transform planet;
    Vector3 preVec;
    // Start is called before the first frame update
    void Start()
    {
        preVec = target.transform.position - planet.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = (target.position - planet.position) * 3f;
        Vector3 next = target.transform.position - planet.transform.position;
        Vector3 perp= Vector3.Cross(preVec, next);
        float angle=Vector3.Angle(preVec,next);
        transform.RotateAround(planet.transform.position,perp,angle);
        preVec = target.transform.position - planet.transform.position;
        transform.position += -transform.up * 20f;

        
        
    }
}
