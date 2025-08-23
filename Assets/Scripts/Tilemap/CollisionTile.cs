using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileCollisionType
{
    Empty,
    Solid,
    HalfTop,
    HalfBottom,
    SlopeLeft,
    SlopeRight,
    OneWay,
}

[CreateAssetMenu(menuName = "Custom/CollisionTile")]
public class CollisionTile : Tile
{
    public TileCollisionType collisionType = TileCollisionType.Solid;
}