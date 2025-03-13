using UnityEngine;

public class camera_control : MonoBehaviour
{
    public Transform target; // วัตถุที่กล้องจะโคจรรอบ
    public float distance = 5.0f; // ระยะห่างจากเป้าหมาย
    public float minDistance = 2.0f; // ระยะซูมเข้าใกล้สุด
    public float maxDistance = 10.0f; // ระยะซูมออกไกลสุด
    public float sensitivity = 3.0f; // ความไวของเมาส์

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xRotation = angles.x;
        yRotation = angles.y;
    }

    void Update()
    {
        // หมุนกล้องด้วยเมาส์ (คลิกขวา)
        if (Input.GetMouseButton(1))
        {
            xRotation += Input.GetAxis("Mouse X") * sensitivity;
            yRotation -= Input.GetAxis("Mouse Y") * sensitivity;
            yRotation = Mathf.Clamp(yRotation, -20f, 80f); // จำกัดมุมก้มเงย
        }

        // คำนวณตำแหน่งใหม่ของกล้อง
        Quaternion rotation = Quaternion.Euler(yRotation, xRotation, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
