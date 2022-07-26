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
    float t;
    public Segment(Vector3 a, float angle, float seg_length, float t, string name)
    {
        this.a = a;
        this.angle = angle;
        this.local_angle = this.angle;
        this.seg_length = seg_length;
        this.name = name;
        this.t = t;
        this.b = calculateB();
    }

    public Segment(Segment seg, float angle, float seg_length, float t, string name)
    {
        this.a = seg.B;
        this.seg_length = seg_length;
        parent_seg = seg;
        this.angle = angle;
        this.local_angle = this.angle;
        this.name = name;
        this.t = t;
        this.b = calculateB();
        //Debug.Log(this.name + "parent_seg" + parent_seg);
    }

    public Vector3 A { get => a; set => a = value; }
    public Vector3 B { get => b; set => b = value; }
    public float Angle { get => angle; set => angle = value; }
    public float Langle { get => local_angle; set => local_angle = value; }
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

    public void calc_updatePointsIk()
    {
        this.angle = local_angle;
        //Debug.Log(this.a);
        this.b = calculateB();
    }

    public void pointAt(Vector2 a)
    {
        float dx = a.x - this.a.x;
        float dy = a.y - this.a.y;
        float ag = (Mathf.Atan2(dy, dx) * 180) / Mathf.PI;
        Debug.Log("ag " + ag);
        this.local_angle = ag;
        //this.b = calculateB();
        calc_updatePointsIk();
     }
    public void rotate(float angle)
    {
        //Debug.Log(Mathf.Sin(angle));
        this.local_angle += angle;
        //this.local_angle += Mathf.Sin(this.t + angle);
        //this.local_angle = Mathf.InverseLerp(-8 * Mathf.PI, 8 * Mathf.PI, this.local_angle);
        //this.local_angle = Mathf.Lerp(-6 * Mathf.PI, 6 * Mathf.PI, this.local_angle);

    }

    public void drag(Vector2 a)
    {
        pointAt(a);
        this.a.x = a.x - Mathf.Cos((this.angle * Mathf.PI) / 180) * this.seg_length;
        this.a.y = a.y - Mathf.Sin((this.angle * Mathf.PI) / 180) * this.seg_length;
        if (this.parent_seg!=null)
        {
            this.parent_seg.drag(this.a);
            calc_updatePointsIk();
        }
    }
}
