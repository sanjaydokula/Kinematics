using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{
    List<Segment> segs = new List<Segment>();


    [SerializeField] private Camera mainCamera;


    GameObject go;

    Vector3[] points;

    public Color c1 = Color.yellow;

    public Color c2 = Color.red;

    [SerializeField] int numSegments = 1;

    [SerializeField] float segLength = 2f;

    float t = 0;

    int k;

    // Start is called before the first frame update
    void Start()
    {
        Segment seg1 = new Segment(new Vector3(0f, 0f, 0f), 90.0f, segLength, t, "seg1");
        Segment segp = seg1;
        segs.Add(segp);

        //loop to create and add segments data to a list
        for (int i = 1; i < numSegments; i++)
        {
            t += .1f;
            Segment seg = new Segment(segp, 0, segLength, t, "seg");
            segs.Add(seg);
            segp = seg;
        }


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

        //this lines tells the LineRenderer component how many segments we need.
        go.GetComponent<LineRenderer>().positionCount = (segs.Count) + 1;

        //setting the coordinates of the segements. setPoints(Segment); is called to get the (x,y) data of each segment from the segments list.
        go.GetComponent<LineRenderer>().SetPositions(setPoints(segs));

    }


    //Takes the segments list and returs the (x,y) data of the segments.
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

            item.calc_updatePoints();
        }

    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode == KeyCode.UpArrow || e.keyCode == KeyCode.RightArrow || e.keyCode == KeyCode.LeftArrow || e.keyCode == KeyCode.DownArrow)
            {

            }
            else
            {
                k = ((int)e.keyCode) % 48;

            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 m = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(m);
        segs[numSegments - 1].drag(m);
        Debug.Log(segs[0].Angle);
        go.GetComponent<LineRenderer>().SetPositions(setPoints(segs));

    }
}
