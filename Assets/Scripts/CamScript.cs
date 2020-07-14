using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {

    public Transform target;
    [SerializeField]
    private float smoothTime;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {

        if (target.position.x - transform.position.x > gameObject.GetComponent<Camera>().orthographicSize / 5 || target.position.x - transform.position.x < -gameObject.GetComponent<Camera>().orthographicSize / 5)
        {
            Vector3 targetPosition = new Vector3(Mathf.Clamp(target.position.x , -0.776f, 0.662f), 0.142f, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
