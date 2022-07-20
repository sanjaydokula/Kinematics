using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    // Start is called before the first frame update
    Vector3 a;
    float angle;
    float local_angle;
    Vector3 b;
    float seg_length;
    public string name;
    Segment parent_seg = null;
    public Segment(Vector3 a, float angle, float seg_length, string name)
    {
        this.a = a;
        this.angle = angle;
        this.local_angle = this.angle;
        this.seg_length = seg_length;
        this.name = name;
        this.b = calculateB();
    }

    public Segment(Segment seg, float angle, float seg_length, string name)
    {
        this.a = seg.B;
        this.seg_length = seg_length;
        parent_seg = seg;
        this.angle = angle;
        this.local_angle = this.angle;
        this.name = name;
        this.b = calculateB();
        //Debug.Log(this.name + "parent_seg" + parent_seg);
    }

    public Vector3 A { get => a; set => a = value; }
    public Vector3 B { get => b; set => b = value; }
    public float Angle { get => angle; set => angle = value; }

    public Vector3 calculateB()
    {
        float r = this.seg_length;
        Vector3 b = new Vector3(0f, 0f, 0f);
        float dx = r * Mathf.Cos((this.angle * Mathf.PI) / 180);
        float dy = r * Mathf.Sin((this.angle * Mathf.PI) / 180);
        b.x = this.a.x + dx;
        b.y = this.a.y + dy;
        return b;

    }
    public void calc_updatePoints()
    {
        this.angle = local_angle;
        if (this.parent_seg != null)
        {

            this.a = this.parent_seg.B;
            this.angle += this.parent_seg.angle;

        }
        //Debug.Log(this.a);
        this.b = calculateB();
    }

    public void rotate(float angle)
    {
        this.local_angle += angle;
    }
}
