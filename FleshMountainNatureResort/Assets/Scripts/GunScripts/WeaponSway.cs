using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount;
    public float max_swayAmount;
    public float smooth;

    private Vector3 initialPos;


    public float swayMultiplier;
    /*
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * max_swayAmount;
        float moveY = Input.GetAxis("Mouse Y") * max_swayAmount;
        moveX = Mathf.Clamp(moveX, -max_swayAmount, max_swayAmount);
        moveY = Mathf.Clamp(moveY, -max_swayAmount, max_swayAmount);

        Vector3 finalPos = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initialPos, Time.deltaTime * smooth);

    }
    */


    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;


        Quaternion rotationX = Quaternion.AngleAxis(-mouseY , Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
