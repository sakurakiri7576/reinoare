using UnityEngine;

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