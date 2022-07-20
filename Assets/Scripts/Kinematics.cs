using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematics : MonoBehaviour
{
    // Start is called before the first frame update
    List<Segment> segs = new List<Segment>();
    Vector3[] points;
    GameObject go;
    [SerializeField]
    float angleInc1 = 10.0f;
    [SerializeField]
    float angleInc2 = 10.0f;
    [SerializeField]
    float angleInc3 = 10.0f;
    [SerializeField]
    float angleInc4 = 10.0f;
    float phase = Mathf.PI;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    void Start()
    {
        Segment seg1 = new Segment(new Vector3(0f, 0f, 0f), 0.0f, 4.0f, "seg1");
        Segment segp = seg1;
        for(int i = 0; i < 40; i++)
        {
            Segment seg = new Segment(segp, 0, 2, "seg");
            segs.Add(seg);
            segp = seg;
        }



        //Segment seg2 = new Segment(seg1, 45.0f, 4.0f, "seg2");
        //Segment seg3 = new Segment(seg2, 20.0f, 4.0f, "seg3");
        //Segment seg4 = new Segment(seg3, 0.0f, 4.0f, "seg4");
        ////Segment2 seg3 = new Segment2(seg2,new Vector3(8f,0f,0f),0.0f,"seg3");
        //segs.Add(seg1);
        //segs.Add(seg2);
        //segs.Add(seg3);
        //segs.Add(seg4);



        go = new GameObject();
        go.transform.SetParent(this.gameObject.transform);
        go.AddComponent<LineRenderer>();
        go.GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        go.GetComponent<LineRenderer>().widthMultiplier = 0.2f;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        go.GetComponent<LineRenderer>().colorGradient = gradient;
        //go.GetComponent<LineRenderer>().col
        go.GetComponent<LineRenderer>().positionCount = (segs.Count) + 1;
        //setPoints(points, segs);
        go.GetComponent<LineRenderer>().SetPositions(setPoints(segs));
        //Debug.Log("calling updatePoints-main");
        //Debug.Log("exited updatePoints-main");
        //segs[1].rotate(0.0f);
        //updatePoints();
        //go.GetComponent<LineRenderer>().SetPositions(setPoints(segs));
    }

    Vector3[] setPoints(List<Segment> segs)
    {
        points = new Vector3[segs.Count + 1];

        for (int i = 0; i < segs.Count; i++)
        {

            points[i] = segs[i].A;
            points[i + 1] = segs[i].B;
        }
        return points;
    }
    void updatePoints()
    {
        //updating segment points.
        foreach (var item in segs)
        {
            //Debug.Log("calling updatePoints-main for seg. " + item.name);
            item.calc_updatePoints();
        }
        //Debug.Log("exiting updatePoints-main");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("update");
        for (int i = 0; i < segs.Count; i++)
        {
            segs[i].rotate(Mathf.Sin(i+phase*Time.time) * angleInc1);
        }
        updatePoints();
        go.GetComponent<LineRenderer>().SetPositions(setPoints(segs));


    }
}
