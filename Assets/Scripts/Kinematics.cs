using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematics : MonoBehaviour
{
    // Start is called before the first frame update
    List<Segment> leg0 = new List<Segment>();

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

    float t = 0;

    int k;

    int numSegments = 4;//number of segemets for the arm.

    float segLength = 2;// the length of each segment.

    float armRadius;

    //the function that is called at the begin of the game.
    /* In this function segements data of class Segment the makes the arm are created and added to a list.
     * 
     * A game object is created dynamically and is attached a LineRenderer component.
     * The LineRenderer properties are set with the segment data and the predefined values.
     */
    void Start()
    {
        //the base segement
        Segment seg1 = new Segment(new Vector3(0f, 0f, 0f), 90.0f, segLength, t, "seg1");
        Segment segp = seg1;
        leg0.Add(segp);

        //loop to create and add segments data to a list
        for (int i = 1; i < numSegments; i++)
        {
            t += .1f;
            Segment seg = new Segment(segp, 0, segLength, t, "seg");
            leg0.Add(seg);
            segp = seg;
        }

        //variables required to spwan random points with the radius of the arm  <====!!!! should be implemented. !!!!====>
        armRadius = numSegments * segLength;


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
        go.GetComponent<LineRenderer>().positionCount = (leg0.Count) + 1;

        //setting the coordinates of the segements. setPoints(Segment); is called to get the (x,y) data of each segment from the segments list.
        go.GetComponent<LineRenderer>().SetPositions(setPoints(leg0));

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


    //this function calls a function that calculates the updated position of point b of the segment for each segment of the arm
    void updatePoints()
    {
        //updating segment points.
        foreach (var item in leg0)
        {

            item.calc_updatePoints();
        }

    }



    //void spwan()
    //{
    //    float x = Random.Range(0,)
    //}
    float tt = 0.2f;

    //Is called several times per frame to handel gui renderering or keyboard(in my case) events to be handeled
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

    //called several times per frame.
    //instructions for the arm movemet is handled in this function.
    void FixedUpdate()
    {
        //Debug.Log("update");
        float hor = Input.GetAxisRaw("Horizontal");
       
        Debug.Log("k-->" + k);
        leg0[k].rotate(hor);
        tt += 1f;
        updatePoints();
        go.GetComponent<LineRenderer>().SetPositions(setPoints(leg0));
        Debug.Log(leg0[k].Angle);
        Debug.Log(leg0[k].Langle);


    }
}
