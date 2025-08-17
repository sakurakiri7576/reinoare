using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //==============================
    // property
    //==============================

    public List<CollisionTileData> groundTiles { get; set; }

    //==============================
    // function
    //==============================

    void Update()
    {
        // 水平方向移動
        velocity.x = inputMove.x * speed;

        // 重力適用
        velocity.y += gravity * Time.deltaTime;

        // 現在位置を元にプレイヤーのRectを作る
        Vector2 pos = (Vector2)transform.position;
        Rect playerRect = new Rect(pos - size * 0.5f, size);

        // Y方向移動先を試す
        Vector2 newPosY = pos + new Vector2(0, velocity.y * Time.deltaTime);
        Rect newRectY = new Rect(newPosY - size * 0.5f, size);
        isGrounded = false;
        foreach (var tile in groundTiles)
        {
            if (tile.collisionType == TileCollisionType.Solid)
            {
                if (IsColliding(newRectY, tile.rect))
                {
                    if (velocity.y < 0)
                    {
                        newPosY.y = tile.rect.yMax + size.y * 0.5f;
                        isGrounded = true;
                    }
                    velocity.y = 0;
                }
            }
            else if (tile.collisionType == TileCollisionType.HalfBottom)
            {
                Rect rect = new Rect(tile.rect.position, new Vector2(1, 0.5f));
                if (IsColliding(newRectY, rect))
                {
                    if (velocity.y < 0)
                    {
                        newPosY.y = rect.yMax + size.y * 0.5f;
                        isGrounded = true;
                    }
                    velocity.y = 0;
                }
            }
            else if (tile.collisionType == TileCollisionType.HalfTop)
            {
                Rect rect = new Rect(tile.rect.position + Vector2.up * 0.5f, new Vector2(1, 0.5f));
                if (IsColliding(newRectY, rect))
                {
                    velocity.y = 0;
                }
            }
            else if (tile.collisionType == TileCollisionType.SlopeRight)
            {
                if (IsCollidingSlopeRight(newRectY, tile.rect.position))
                {
                    newPosY.y = tile.rect.y + (newRectY.center.x - tile.rect.xMin) + size.y * 0.5f;
                    isGrounded = true;
                    velocity.y = 0;
                }
            }
            else if (tile.collisionType == TileCollisionType.SlopeLeft)
            {
                if (IsCollidingSlopeLeft(newRectY, tile.rect.position))
                {
                    float localX = newRectY.center.x - tile.rect.xMin;
                    float slopeY = 1f - localX; // ／の形
                    newPosY.y = tile.rect.y + slopeY + size.y * 0.5f;
                    isGrounded = true;
                    velocity.y = 0;
                }
            }
        }

        // X方向移動先を試す
        Vector2 newPosX = new Vector2(pos.x + velocity.x * Time.deltaTime, newPosY.y);
        Rect newRectX = new Rect(newPosX - size * 0.5f, size);
        foreach (var tile in groundTiles)
        {
            if (tile.collisionType == TileCollisionType.Solid && IsColliding(newRectX, tile.rect))
            {
                newPosX.x = pos.x;
                velocity.x = 0;
            }
        }

        // 最終位置反映
        transform.position = newPosX;

        // めっちゃ下に行ったら、初期位置に戻す
        if(transform.position.y <= -50.0f)
        {
            transform.position = Vector2.zero;
        }
    }

    bool IsColliding(Rect a, Rect b)
    {
        return a.xMin < b.xMax && a.xMax > b.xMin &&
               a.yMin < b.yMax && a.yMax > b.yMin;
    }

    bool IsCollidingSlopeRight(Rect playerRect, Vector2 tilePos)
    {
        Vector2 foot = new Vector2(playerRect.center.x, playerRect.yMin);
        float localX = foot.x - tilePos.x;
        float localY = foot.y - tilePos.y;

        if (localX >= 0 && localX <= 1)
        {
            float slopeY = localX;
            return localY <= slopeY && localY >= 0;
        }
        return false;
    }

    bool IsCollidingSlopeLeft(Rect playerRect, Vector2 tilePos)
    {
        Vector2 foot = new Vector2(playerRect.center.x, playerRect.yMin);
        float localX = foot.x - tilePos.x;
        float localY = foot.y - tilePos.y;

        if (localX >= 0 && localX <= 1)
        {
            float slopeY = 1 - localX;
            return localY <= slopeY && localY >= 0;
        }
        return false;
    }

    #region InputAction
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(isGrounded)
            {
                velocity.y = jumpSpeed;
                isGrounded = false;
            }
        }
    }
    #endregion

    //==============================
    // variable
    //==============================

    [SerializeField, Tooltip("移動速度")]
    private float speed = 5.0f;

    [SerializeField, Tooltip("重力")]
    private float gravity = -20f;

    [SerializeField, Tooltip("ジャンプ速度")]
    private float jumpSpeed = 10.0f;

    [SerializeField, Tooltip("当たり判定")]
    private Vector2 size = new Vector2(1f, 1f);

    private Vector2 velocity;
    private Vector2 inputMove;
    private bool isGrounded;
}
