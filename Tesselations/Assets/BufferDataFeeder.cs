using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct pt
{   
    [SerializeField]
    public Vector3 position;
    [SerializeField]
    public Vector3 color;
    public pt(Vector3 position, Vector3 color)
    {
        this.position = position;
        this.color = color;
    }
}

[ExecuteAlways]
public class BufferDataFeeder : MonoBehaviour
{
    private ComputeBuffer computeBuffer;
    private int stride = sizeof(float) * 3 * 2 ;
    public List<pt> points;
    private pt[] arr;
    // Start is called before the first frame update
    void init()
    {
        points = new List<pt>();
        points.Add(new pt(new Vector3(.3f, .2f, 0), new Vector3(1,0,0)));
        points.Add(new pt(new Vector3(.7f, .3f, 0), new Vector3(0,1,0)));
        points.Add(new pt(new Vector3(.4f, .5f, 0), new Vector3(0,0,1)));
        points.Add(new pt(new Vector3(.5f,.5f,0), new Vector3(1,0,1)));
        computeBuffer = new ComputeBuffer(points.Count, stride, ComputeBufferType.Default);

      

        GetComponent<MeshRenderer>().material.SetBuffer("buffer", computeBuffer);
        GetComponent<MeshRenderer>().material.SetInt("bufferSize", points.Count);
        SetData();
    }

    void SetData()
    {
        if(computeBuffer == null)
        {
            init();
        }
        
        computeBuffer.SetData(points.ToArray());
    }

    //Adding point at runtime
    void AddPoint(Vector3 position, Vector3 color)
    {
        points.Add(new pt(position, color));
        computeBuffer = new ComputeBuffer(points.Count, stride, ComputeBufferType.Default);
        GetComponent<MeshRenderer>().material.SetBuffer("buffer", computeBuffer);
        GetComponent<MeshRenderer>().material.SetInt("bufferSize", points.Count);
        SetData();
    }

    public void AddPoint()
    {
        AddPoint(new Vector3(0.5f, 0.5f, 0), new Vector3(Random.value, Random.value, Random.value));

    }

    // Update is called once per frame
    void Update()
    {
        SetData();
    }
}
