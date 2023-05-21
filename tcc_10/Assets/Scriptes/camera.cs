using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Transform LimiteCamera;
    public Transform posicaoPlayer;
    private Vector3 offset;
    void Start() {
        offset = transform.position - target.position;
    }

    void Update() {

        if (posicaoPlayer.transform.position.x < LimiteCamera.position.x) {

            smoothSpeed = 0;

        }
        else if (posicaoPlayer.position.x > LimiteCamera.position.x) {

            smoothSpeed = 0.125f;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
