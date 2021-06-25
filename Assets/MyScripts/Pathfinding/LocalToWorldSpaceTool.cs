using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalToWorldSpaceTool : MonoBehaviour
{
    public GameObject obj;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(obj.GetComponent<RectTransform>().anchoredPosition);
        //Debug.Log(transform.TransformPoint(obj.transform.position));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
