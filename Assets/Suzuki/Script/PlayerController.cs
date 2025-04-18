using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    public FruitController fruitController;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 worldPos = GetWorldPositionFromInput();

        // ���N���b�N�iFruit2 �𐶐��j
        if (Input.GetMouseButtonDown(0))
        {
            fruitController.SpawnFruit(worldPos, 1);
        }
        // �E�N���b�N�iFruit1 �𐶐��j
        else if (Input.GetMouseButtonDown(1))
        {
            fruitController.SpawnFruit(worldPos, 0);
        }
    }

    Vector3 GetWorldPositionFromInput()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 10f;
        return mainCamera.ScreenToWorldPoint(screenPos);
    }
}
