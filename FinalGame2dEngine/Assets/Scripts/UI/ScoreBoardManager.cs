using UnityEngine;
using System.Collections.Generic;
public class ScoreBoardManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform contentContainer;
    private int totalLevels = 5;
    void Start()
    {
        LoadScoreBoard();
    }
    void LoadScoreBoard()
    {
        // Vòng lặp chạy từ Level 1 đến Level 5
        for (int i = 1; i <= totalLevels; i++)
        {
         
            GameObject newRow = Instantiate(rowPrefab, contentContainer);

            // 2. Lấy Script (hoặc các thành phần con) để sửa chữ
            // Ở đây mình dùng cách tìm nhanh bằng tên (hoặc bạn có thể tạo script riêng cho Row)
            TMPro.TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

            // Giả sử thứ tự text trong Prefab là: 0:Tên, 1:Thời gian, 2:Sao
            // (Bạn cần sắp xếp thứ tự trong Hierarchy của Prefab cho đúng nhé)

            // --- TÊN LEVEL ---
            texts[0].text = "Level " + i;

            // --- LẤY DỮ LIỆU TỪ PLAYER PREFS ---
            string key = "Level" + i + "_BestTime";

            if (PlayerPrefs.HasKey(key))
            {
                float time = PlayerPrefs.GetFloat(key);
                texts[1].text = FormatTime(time);
                texts[2].text = CalculateStars(time); // Hàm tính sao
            }
            else
            {
                texts[1].text = "--:--";
                texts[2].text = "Chưa mở";
                // Có thể làm mờ dòng này đi nếu chưa chơi
            }
        }
        string FormatTime(float time)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Hàm tính sao dựa trên thời gian (Ví dụ: <30s là 3 sao)
        string CalculateStars(float time)
        {
            if (time < 30) return "⭐⭐⭐";
            if (time < 60) return "⭐⭐";
            return "⭐";
        }
    }
}