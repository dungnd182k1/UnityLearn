using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3[] checkPoints;
    public float speed = 50.0f;
    private int cur = 0;

    private void Update()
    {
        // Quay đầu xe về điểm cần di chuyển đến
        Vector3 direction = checkPoints[cur] - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        // Di chuyển xe
        transform.position = Vector3.MoveTowards(transform.position, checkPoints[cur], speed * Time.deltaTime);

        // Kiểm tra xe đã đến đích chưa
        if (Vector3.Distance(transform.position, checkPoints[cur]) == 0)
        {
            cur = (cur + 1) % checkPoints.Length;
        }
    }
}
