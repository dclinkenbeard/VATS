/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowModelController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Transform> models;

    private void Awake()
    {
        models = new List<Transform>();
        for (int ii = 0; ii < transform.childCount; ii++)
        {
            var model = transform.GetChild(ii);
            models.Add(model);

            model.gameObject.SetActive(ii == 0);
        }
    }

    public void EnableModel(Transform modelTransform)
    {
        for (int ii = 0; ii < transform.childCount; ii++)
        {
            var transformToToggle = transform.GetChild(ii);
            bool shouldBeActive = transformToToggle == modelTransform;

            transformToToggle.gameObject.SetActive(shouldBeActive);
        }
    }

    public List<Transform> GetModels()
    {
        return new List<Transform>(models);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/