using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseView : MonoBehaviour
{
    public enum RotationAxies
    {
        MouseX, MouseY
    }

    public RotationAxies axes = RotationAxies.MouseX;
    private float rotation_X = 0f;
    private float snsitive_X = 1.5f;//x方向鼠标敏感度
    private Quaternion originalRotation;
    private float rotation_Y = 0f;
    private float snsitive_Y = 1.5f;//y 方向鼠标敏感度

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        //鼠标左右移动，玩家左右转向
        if (axes == RotationAxies.MouseX)
        {
            rotation_X += Input.GetAxis("Mouse X") * snsitive_X;

            rotation_X = ClampAngle(rotation_X, -360f, 360f);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotation_X, Vector3.up);

            transform.localRotation = originalRotation * xQuaternion;
        }

        if (axes == RotationAxies.MouseY)
        {
            rotation_Y += Input.GetAxis("Mouse Y") * snsitive_Y;

            rotation_Y = ClampAngle(rotation_Y, -60f, 60f);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotation_Y, Vector3.right);

            transform.localRotation = originalRotation * yQuaternion;
        }


    }

    private float ClampAngle(float angle, float v1, float v2)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, v1, v2);
    }
}
