using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public bool leverStat = false;
    [SerializeField] GameObject charInRange;
    [SerializeField] Gate gate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(charInRange != null)
        {
            if(Mathf.Abs(charInRange.transform.position.x - this.transform.position.x) < 2 
                && Mathf.Abs(charInRange.transform.position.y - this.transform.position.y) < 2
                && Mathf.Abs(charInRange.transform.position.z - this.transform.position.z) < 2)
            {
                leverStat = true;
                gate.leverPulled = leverStat;
            }
      
        }
    }
}
