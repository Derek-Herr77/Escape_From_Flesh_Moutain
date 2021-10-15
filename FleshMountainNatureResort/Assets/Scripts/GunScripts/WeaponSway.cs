using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount;
    public float max_swayAmount;
    public float smooth;

    private Vector3 initialPos;
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
}
