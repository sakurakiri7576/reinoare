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

    [SerializeField, Tooltip("�^�C���}�b�v�R���C�_�[�f�[�^")]
    private TilemapColliderData tilemapColliderData;

    [SerializeField, Tooltip("�v���C���[")]
    private PlayerController playerController;
}
