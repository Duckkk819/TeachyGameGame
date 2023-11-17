using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScriptController : MonoBehaviour
{
    public GameObject Patrol;

    void Start()
    {
        Patrol.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScript (string scriptName)
    {
        
        if (scriptName == "Patrol.cs")
        {
            this.GetComponent<inspectorController>().HideInspector();
            Patrol.SetActive(true);
        }
    }
}
