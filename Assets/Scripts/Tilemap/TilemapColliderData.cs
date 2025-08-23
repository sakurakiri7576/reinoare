using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionTileData
{
    public Rect rect;
    public TileCollisionType collisionType;

    public CollisionTileData(Rect rect, TileCollisionType type)
    {
        this.rect = rect;
        this.collisionType = type;
    }
}

public class TilemapColliderData : MonoBehaviour
{
    //==============================
    // property
    //==============================

    public List<CollisionTileData> solidTiles { get; private set; }

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
            if (!tilemap.HasTile(pos)) continue;

            Vector3 worldPos = tilemap.CellToWorld(pos);

            // Tile �ɉ����ďՓˏ����쐬
            var tile = tilemap.GetTile<TileBase>(pos) as CollisionTile;
            if (tile == null) continue;

            // ���̂܂� 1x1 �̋�`�ɂ���
            Rect rect = new Rect(worldPos, Vector2.one);

            // �Փˏ���ێ�
            var data = new CollisionTileData(rect, tile.collisionType);
            solidTiles.Add(data);
        }
    }

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("�^�C���}�b�v")]
    private Tilemap tilemap;
}
