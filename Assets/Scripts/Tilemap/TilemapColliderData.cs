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
    /// 地形の当たり判定を設定する
    /// </summary>
    public void SetSolidCollider()
    {
        solidTiles = new List<CollisionTileData>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            Vector3 worldPos = tilemap.CellToWorld(pos);

            // Tile に応じて衝突情報を作成
            var tile = tilemap.GetTile<TileBase>(pos) as CollisionTile;
            if (tile == null) continue;

            // そのまま 1x1 の矩形にする
            Rect rect = new Rect(worldPos, Vector2.one);

            // 衝突情報を保持
            var data = new CollisionTileData(rect, tile.collisionType);
            solidTiles.Add(data);
        }
    }

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("タイルマップ")]
    private Tilemap tilemap;
}
