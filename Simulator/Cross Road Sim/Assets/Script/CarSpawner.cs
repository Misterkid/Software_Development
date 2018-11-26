﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Point one to edit the bezier curve")]
    public Transform curveOne;

    [SerializeField]
    [Tooltip("point two to edit the bezier curve")]
    public Transform curveTwo;

    [SerializeField]
    [Tooltip("Object has to end here")]
    public Transform endTarget;


    [SerializeField]
    private Car carPrefab;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        Car car = Instantiate(carPrefab, transform);
        car.Init(this);
    }



    /// <summary>
    /// Draw Gizmos in unity for visual feed back
    /// </summary>
    void OnDrawGizmos()
    {
        if (curveOne == null || curveTwo == null)
            return;

        Gizmos.color = Color.green;
        //List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < 100; i += 2)
        {
            float t = i / 100.0f;
            float t2 = (i + 1) / 100.0f;
            Vector3 curveOne = CalculateBezierPoint(t, transform.position, this.curveOne.position, this.curveTwo.position, endTarget.position);
            Vector3 curveTwo = CalculateBezierPoint(t2, transform.position, this.curveOne.position, this.curveTwo.position, endTarget.position);
            Gizmos.DrawLine(curveOne, curveTwo);
        }
    }

    //from the internet
    //https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    //http://devmag.org.za/2011/04/05/bzier-curves-a-tutorial/
    /// <summary>
    /// Calculate a bezier curve using 3 points
    /// </summary>
    /// <param name="t">the position within the curve, (goes from 0-1)</param>
    /// <param name="point0">The start point/position</param>
    /// <param name="point1">Point 1 to edit the curve</param>
    /// <param name="point2">Point 2 to edit the curve</param>
    /// <param name="point3">The end point/position</param>
    /// <returns></returns>
    public Vector3 CalculateBezierPoint(float t,
        Vector3 point0,
        Vector3 point1,
        Vector3 point2,
        Vector3 point3)
    {
        //point = (1 – t)^3 p0 + 3(1 – t)^2 t p1 + 3(1 – t) t^2 p2 + t^3 p3
        //We want what is left too
        float u = 1 - t;//(1 – t)
        Vector3 point = Mathf.Pow(u, 3) * point0;//(1 – t)^3 p0
        point += 3 * Mathf.Pow(u, 2) * t * point1;//+ 3(1 – t)^2 t p1
        point += 3 * u * Mathf.Pow(t, 2) * point2; //+ 3(1 – t) t^2 p2
        point += Mathf.Pow(t, 3) * point3; //t^3 p3
        return point;
    }
}
