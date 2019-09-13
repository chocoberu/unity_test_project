using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Transform transform;
    private Vector3 pos;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos.Set(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.SetPositionAndRotation(pos, transform.rotation);
        
    }
}
