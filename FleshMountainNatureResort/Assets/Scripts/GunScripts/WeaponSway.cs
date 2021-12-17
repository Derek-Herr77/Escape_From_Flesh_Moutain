using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth;
    public float swayMultiplier;
    
 

    private void FixedUpdate()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        if(mouseX > 50 || mouseX < -50)
        {
            if (mouseX > 50) { mouseX = 50; }
            else { mouseX = -50; }
        }

        if (mouseY > 50 || mouseY < -50)
        {
            if (mouseY > 50) { mouseY = 50; }
            else { mouseY = -50; }
        }


        Quaternion rotationX = Quaternion.AngleAxis(-mouseY , Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
