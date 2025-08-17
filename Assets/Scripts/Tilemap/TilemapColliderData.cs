using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderData : MonoBehaviour
{
    //==============================
    // property
    //==============================

    public List<CollisionTileData> solidTiles { get; set; }

    //==============================
    // function
    //==============================

    /// <summary>
    /// �n�`�̓����蔻���ݒ肷��
    /// </summary>
    public void SetSolidCollider()
    {
        solidTiles = new List<CollisionTileData>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile<CollisionTile>(pos);
            if (tile != null)
            {
                // �Z���̍������W
                Vector3 worldPos = tilemap.CellToWorld(pos);

                // ���̂܂� 1x1 �̋�`�ɂ���
                Rect rect = new Rect(worldPos, Vector2.one);

                // �Փˏ���ێ�
                var data = new CollisionTileData(rect, tile.collisionType);
                solidTiles.Add(data);
            }
        }
    }

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("�^�C���}�b�v")]
    private Tilemap tilemap;
}
