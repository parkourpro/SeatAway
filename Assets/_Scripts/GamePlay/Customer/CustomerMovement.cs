using System.Collections;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    //private static float speed = 0.13f;
    private static float speed = 4f;


    public static IEnumerator Move(GameObject customerObject, Vector3 endPos)
    {
        Animator animator = customerObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on customer object.");
            yield break;
        }

        Vector3 direction = endPos - customerObject.transform.position;
        customerObject.transform.LookAt(endPos);
        animator.SetBool("Walk", true);

        while (Vector3.Distance(customerObject.transform.position, endPos) > 0.1f)
        {
            customerObject.transform.position += Vector3.Normalize(direction) * speed * Time.deltaTime;
            yield return null;
        }

        customerObject.transform.position = endPos;
        animator.SetBool("Walk", false);  // Stop walking for this customer
    }


    public static IEnumerator Sit(GameObject customerObject, Vector3 endPos, GridCell seatPosition)
    {
        Animator animator = customerObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on customer object.");
            yield break;
        }

        Vector3 startPos = customerObject.transform.position;

        // Set rotation for sitting position based on seat direction
        customerObject.transform.rotation = GetSeatQuaternion((Seat)seatPosition.objectOncell);

        animator.SetBool("Sit", true);

        float height = 0.8f; // Độ cao của vòng cung
        float duration = 0.3f; // Thời gian di chuyển
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration); // Tỉ lệ di chuyển từ 0 đến 1

            // Tính toán vị trí theo vòng cung parabol
            Vector3 arcPosition = Vector3.Lerp(startPos, endPos, t); // Lerp tuyến tính từ start đến end
            arcPosition.y += Mathf.Sin(t * Mathf.PI) * height; // Thêm vòng cung theo chiều cao

            customerObject.transform.position = arcPosition;
            yield return null;
        }

        // Đảm bảo vị trí cuối cùng là đích
        customerObject.transform.position = endPos;
        //Debug.Log(foundSeat.associatedObject.name);
    }





    private static Quaternion GetSeatQuaternion(Seat seat)
    {
        Quaternion seatRotation = Quaternion.identity;
        switch (seat.direction)
        {
            case Direction.Up:
                seatRotation = Quaternion.Euler(0, 0, 0); // Hướng lên
                break;
            case Direction.Down:
                seatRotation = Quaternion.Euler(0, 180, 0); // Hướng xuống
                break;
            case Direction.Left:
                seatRotation = Quaternion.Euler(0, 270, 0); // Hướng trái
                break;
            case Direction.Right:
                seatRotation = Quaternion.Euler(0, 90, 0); // Hướng phải
                break;
        }
        return seatRotation;
    }


}
