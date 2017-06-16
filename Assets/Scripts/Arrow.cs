using UnityEngine;

public class Arrow : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
