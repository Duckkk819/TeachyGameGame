using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolScriptUI : MonoBehaviour
{
    public inspectorController inspectorController;
    VisualElement root;
    Button closeButton;
    //Label 

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        closeButton = root.Q<Button>("x_button");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        closeButton.clickable.clicked += () =>
        {
            Debug.Log("close");
            this.GetComponent<GameObject>().SetActive(false);
            inspectorController.ShowInspector();
        };
    }

    void LightUp(Label line)
    {
        //line.
    }
}
