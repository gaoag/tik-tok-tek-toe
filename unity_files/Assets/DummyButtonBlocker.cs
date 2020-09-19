using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyButtonBlocker : MonoBehaviour, OnTouch3D
{
    // Start is called before the first frame update
    private int x;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouch(GameStateManager gsm) { 
        x=1+1;
    }

    
}
