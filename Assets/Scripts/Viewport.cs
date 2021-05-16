using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    float minX;
    public float MinX { get { return minX; } }
    float maxX;
    public float MaxX { get { return maxX; } }
    float minY;
    public float MinY { get { return minY; } }
    float maxY;
    public float MaxY { get { return maxY; } }
    float middleX;

    void Start()
    {
        Camera mainCamera = Camera.main;

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));

        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).z;

        maxX = bottomLeft.z;
        minY = bottomLeft.y;
        minX = topRight.z;
        maxY = topRight.y;
    }

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.z = Mathf.Clamp(playerPosition.x, minX + paddingX, maxX - paddingX);
        position.y = Mathf.Clamp(playerPosition.y, minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomEnemySpawnPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.z = maxX + paddingX;
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomRightHalfPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.z = Random.Range(minX + paddingX, middleX);
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }

    public Vector3 RandomEnemyMovePosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;

        position.z = Random.Range(minX + paddingX, maxX - paddingX);
        position.y = Random.Range(minY + paddingY, maxY - paddingY);

        return position;
    }
}