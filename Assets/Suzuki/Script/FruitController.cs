using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject[] fruitPrefabs;  // �����̃t���[�c�v���n�u���i�[�i�z��j
    public float spawnInterval;

    private float lastSpawnTime = 0f;
    public float FruitTime;  // �c�莞�Ԃ��J�E���g����ϐ�

    void Update()
    {
        // �c�莞�Ԃ��v�Z�i0�����ɂ͂��Ȃ��j
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

                // �^�O�̐ݒ�
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
                Debug.LogWarning("fruitIndex �� fruitPrefabs �͈̔͊O�ł��I");
            }
        }
        else
        {
            Debug.Log($"���̃t���[�c�����܂ł��� {FruitTime:F1} �b�ł�");
        }
    }
}
