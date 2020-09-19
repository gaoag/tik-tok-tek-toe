using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale += new Vector3(-0.55f, -0.55f, -0.55f);
        this.gameObject.name = "ParentObject";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
