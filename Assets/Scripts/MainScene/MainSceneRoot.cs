using UnityEngine;

public class MainSceneRoot : MonoBehaviour
{
    //==============================
    // function
    //==============================

    void Start()
    {
        tilemapColliderData.SetSolidCollider();
        playerController.groundTiles = tilemapColliderData.solidTiles;
    }

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("タイルマップコライダーデータ")]
    private TilemapColliderData tilemapColliderData;

    [SerializeField, Tooltip("プレイヤー")]
    private PlayerController playerController;
}
