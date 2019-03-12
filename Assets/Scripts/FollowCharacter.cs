using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    Transform playerCharacter;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerCharacter.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCharacter.transform.position + offset;
    }
}
