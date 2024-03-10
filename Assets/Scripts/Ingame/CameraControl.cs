using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform limit_left;
    public Transform limit_right;
    public Transform trans_cam;
    private float pos_x;
    public float sensity = 0.03f;
    public float speed =15;
    private void Awake()
    {

    }
    void Start()
    {
        // trans = transform;
    }

    void Update()
    {
    }
    private void LateUpdate()
    {
        Vector3 delta_move = InputManager.delta_mouse;
        pos_x = trans_cam.localPosition.x;
        pos_x = Mathf.Lerp(pos_x, pos_x - delta_move.x * sensity, Time.deltaTime * speed);
        pos_x = Mathf.Clamp(pos_x, limit_left.localPosition.x, limit_right.localPosition.x);
        trans_cam.localPosition = new Vector3(pos_x, trans_cam.localPosition.y, trans_cam.localPosition.z);
    }
}
