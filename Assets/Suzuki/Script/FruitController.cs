using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject[] fruitPrefabs;  // 複数のフルーツプレハブを格納（配列）
    public float spawnInterval;

    private float lastSpawnTime = 0f;
    public float FruitTime;  // 残り時間をカウントする変数

    void Update()
    {
        // 残り時間を計算（0未満にはしない）
        float timeSinceLastSpawn = Time.time - lastSpawnTime;
        FruitTime = Mathf.Max(0, spawnInterval - timeSinceLastSpawn);
    }

    public void SpawnFruit(Vector3 spawnPosition, int fruitIndex)
    {
        float currentTime = Time.time;

        if (currentTime - lastSpawnTime >= spawnInterval)
        {
            if (fruitIndex >= 0 && fruitIndex < fruitPrefabs.Length)
            {
                GameObject fruit = Instantiate(fruitPrefabs[fruitIndex], spawnPosition, Quaternion.identity);

                // タグの設定
                if (fruitIndex == 0)
                {
                    fruit.tag = "Fruit01";
                }
                else if (fruitIndex == 1)
                {
                    fruit.tag = "Fruit02";
                }

                lastSpawnTime = currentTime;
            }
            else
            {
                Debug.LogWarning("fruitIndex が fruitPrefabs の範囲外です！");
            }
        }
        else
        {
            Debug.Log($"次のフルーツ生成まであと {FruitTime:F1} 秒です");
        }
    }
}
