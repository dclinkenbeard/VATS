using UnityEngine;

public class ModelCollider : MonoBehaviour
{
    public GameObject model;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == model)
        {
            Debug.Log("gameobject collided");
        }

        if (collision.collider.gameObject == model)
        {
            Debug.Log("gameobject collided");

        }
        if (collision.collider.gameObject.tag == "model")
        {
            Debug.Log("gameobject collided");

        }
    }
}
