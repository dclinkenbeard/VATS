/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class ShowModelButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform objectToShow;
    private Action<Transform> clickAction;

    public void Initialize(Transform objectToShow, Action<Transform> clickAction)
    {
        this.objectToShow = objectToShow;
        this.clickAction = clickAction;
        GetComponentInChildren<Text>().text = objectToShow.gameObject.name;

    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        //clickAction(objectToShow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/