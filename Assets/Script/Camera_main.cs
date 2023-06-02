using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_main : MonoBehaviour
{
    public Transform target; // Vị trí của nhân vật đang đứng
    private float smoothing; // Làm cho camera trong quá trình quay trở nên mượt mà hơn
    Vector3 offset; // Vị trí từ nhân vật đến vị trí của Camera
    float lowY; // Trường hợp khi nhân vật rơi xuống quá vị trí quy định ==> Camera sẽ không đi theo nữa
    // Start is called before the first frame update
    void Start()
    {
        smoothing = 5;
        offset = transform.position - target.position;
        lowY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCamPost = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPost, smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}
