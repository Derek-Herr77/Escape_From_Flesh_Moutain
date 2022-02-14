using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth;
    public float swayMultiplier;


    public float framerate;
    private void Update()
    {
        framerate = 1.0f / Time.deltaTime;

        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        if (mouseX > 20 || mouseX < -20)
        {
            if (mouseX > 20) { mouseX = 20; }
            else { mouseX = -20; }
        }

        if (mouseY > 20 || mouseY < -20)
        {
            if (mouseY > 20) { mouseY = 20; }
            else { mouseY = -20; }
        }


        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.fixedDeltaTime);
        
    }
}
