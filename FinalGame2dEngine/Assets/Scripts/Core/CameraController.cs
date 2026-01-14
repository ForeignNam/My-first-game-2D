using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private float smoothTime = 0.2f; // Thời gian để camera đuổi kịp (càng cao càng chậm/mượt)
    [SerializeField] private float lookAheadDistance = 2f; // Khoảng cách nhìn trước mặt nhân vật
    [SerializeField] private float lookAheadSpeed = 3f; // Tốc độ thay đổi tầm nhìn khi quay đầu
    [SerializeField] private float yOffset = 1.5f;
    [Header("Boundaries (Map Limits)")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private float currentLookAheadX;
    private Vector3 velocity = Vector3.zero; // Biến phụ cho SmoothDamp
   
    private void LateUpdate() // Dùng LateUpdate để tránh rung lắc
    {
        if (playerTransform == null) return;

        // 1. Tính toán hướng nhìn (Look Ahead)
        // Nếu nhân vật quay mặt phải (scale.x > 0) thì nhìn sang phải và ngược lại
        float targetLookAhead = lookAheadDistance * Mathf.Sign(playerTransform.localScale.x);

        // Làm mượt việc thay đổi hướng nhìn (Lerp)
        currentLookAheadX = Mathf.Lerp(currentLookAheadX, targetLookAhead, Time.deltaTime * lookAheadSpeed);

        // 2. Tính vị trí mục tiêu
        Vector3 targetPosition = new Vector3(
            playerTransform.position.x + currentLookAheadX,
            playerTransform.position.y + yOffset,
            transform.position.z // Giữ nguyên Z của camera (thường là -10)
        );

        // 3. Làm mượt chuyển động (SmoothDamp thay vì gán trực tiếp)
        // Hàm này tự động tạo ra hiệu ứng camera đuổi theo nhân vật rất êm
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 4. Kẹp vị trí trong bản đồ (Clamp)
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // 5. Gán vị trí cuối cùng
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}