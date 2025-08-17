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
    /// 地形の当たり判定を設定する
    /// </summary>
    public void SetSolidCollider()
    {
        solidTiles = new List<CollisionTileData>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile<CollisionTile>(pos);
            if (tile != null)
            {
                // セルの左下座標
                Vector3 worldPos = tilemap.CellToWorld(pos);

                // そのまま 1x1 の矩形にする
                Rect rect = new Rect(worldPos, Vector2.one);

                // 衝突情報を保持
                var data = new CollisionTileData(rect, tile.collisionType);
                solidTiles.Add(data);
            }
        }
    }

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("タイルマップ")]
    private Tilemap tilemap;
}
