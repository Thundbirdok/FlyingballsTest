using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BallPath
{

    struct PositionsArrays
    {

        public float[] x;
        public float[] y;
        public float[] z;

        public PositionsArrays(float[] x, float[] y, float[] z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }

    private Vector3[] positions;    
    public int Length { get => positions.Length; }

    public void ReadJSON(string name)
    {

        string json = File.ReadAllText(Application.dataPath + "/Resources/" + name);                

        PositionsArrays tmp = JsonUtility.FromJson<PositionsArrays>(json);

        List<Vector3> list = new List<Vector3>();

        for (int i = 0; i < tmp.x.Length; ++i)
        {

            list.Add(new Vector3(tmp.x[i], tmp.y[i], tmp.z[i]));

        }

        positions = list.ToArray();        

    }

    public Vector3 this[int index]
    {

        get
        {

            return positions[index];

        }

    }

}
