using System.Collections;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    private float busSpeed = 1.3f;
    private float duration = 4f;
    public IEnumerator MoveToTheBusStop(GameObject bus, Vector3 endPos)
    {
        while (Vector3.Distance(bus.transform.position, endPos) > 0.01f)
        {
            bus.transform.position = Vector3.Lerp(bus.transform.position, endPos, Time.deltaTime * busSpeed);
            if (Vector3.Distance(bus.transform.position, endPos) < 2f)
            {
                busSpeed += Time.deltaTime * 1.4f;
            }
            yield return null;
        }
        bus.transform.position = endPos;
    }

    public IEnumerator MoveOutOfTheBusStop(GameObject bus, Vector3 endPos)
    {
        Vector3 startPosition = bus.transform.position;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            bus.transform.position = Vector3.Lerp(startPosition, endPos, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        bus.transform.position = endPos;
        //yield return null;
    }

    public IEnumerator RotateDoorSmoothly(Transform door, float targetAngle, float duration)
    {
        Quaternion initialRotation = door.localRotation; // Góc quay ban đầu
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0); // Góc quay mục tiêu (-90 độ Y)

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Nội suy từ góc quay ban đầu đến góc quay mục tiêu
            door.localRotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo rằng cửa dừng chính xác ở góc mục tiêu
        door.localRotation = targetRotation;
    }

}
