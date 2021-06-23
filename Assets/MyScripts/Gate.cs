using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    [SerializeField]
    bool Gotop = false;
    [SerializeField] Lever attachedLever;
    public bool leverPulled = false;
    //[SerializeField]
    //bool setTime = false;
    [SerializeField]
    float currTime;
    [SerializeField]
    bool changeStatus;

    // Update is called once per frame
    void Update()
    {
        // If at the bottom then move up
        if (Gotop && this.transform.position.y < startPos.y)
        {
            this.transform.position = (new Vector3(this.transform.position.x, this.transform.position.y + 10 * Time.deltaTime, this.transform.position.z));
        }
        if(attachedLever.leverStat)
        {
            Gotop = true;
        }
    }
}
