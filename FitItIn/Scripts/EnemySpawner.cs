using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Sprite topRightPivot;
    [SerializeField] Sprite topLeftPivot;
    [SerializeField] Sprite bottomRightPivot;
    [SerializeField] Sprite bottomLeftPivot;
    [SerializeField] float velocity = 1;
    [SerializeField] float spawnRate = 1;
    [SerializeField] int nextDifficultyLevel = 20;
    [SerializeField] float velocityAcceleration = 0.5f;
    [SerializeField] float spawnAcceleration = 0.25f;
    [SerializeField] float cubeSize = 1;
    [SerializeField] AudioClip[] audioClips;

    float width;
    float height;
    int numberOfSpawnPoints = 1;
    float nextSpawnTime;
    SpriteRenderer spriteRenderer;
    float minXScale = 1;
    float maxXScale;
    float minYScale = 1;
    float maxYScale;
    float randomX;
    float randomY;
    AudioSource audioSource;
    int currentAudio;

    void Start()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        width = screenToWorld.x;
        height = screenToWorld.y;

        maxXScale = width * 2 - cubeSize;
        maxYScale = height * 2 - cubeSize;

        ScoreSystem.OnScoreChange += UpdateDifficulty;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + 1/spawnRate;
        }

        if (Time.timeScale == 0)
            audioSource.Stop();
    }

    [ContextMenu("Spawn")]
    void Spawn()
    {
        var index = Random.Range(0, numberOfSpawnPoints);

        Vector3 position;
        Sprite sprite;
        Vector2 offset;
        Vector3 newVelocity;

        var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        
        switch (index)
        {
            case 0: //From right side to left side
                newVelocity = Vector3.left;

                //Enemy 1
                NewMethod(out position, out sprite, out offset);
                RandomizeScale(minXScale, maxYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                //Enemy 2
                position = new Vector3(width, -height, 0);
                sprite = bottomLeftPivot;
                offset = new Vector2(0.5f, 0.5f);
                RandomizeScale(minXScale, maxYScale - randomY - cubeSize);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 1: //From left side to right side
                newVelocity = Vector3.right;

                //Enemy 1
                position = new Vector3(-width, height, 0);
                sprite = topRightPivot;
                offset = new Vector2(-0.5f, -0.5f);
                RandomizeScale(minXScale, maxYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                //Enemy 2
                position = new Vector3(-width, -height, 0);
                sprite = bottomRightPivot;
                offset = new Vector2(-0.5f, 0.5f);
                RandomizeScale(minXScale, maxYScale - randomY - cubeSize);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 2: //From top to bottom
                newVelocity = Vector3.down;

                //Enemy 1
                position = new Vector3(width, height, 0);
                sprite = bottomRightPivot;
                offset = new Vector2(-0.5f, 0.5f);
                RandomizeScale(maxXScale, minYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                //Enemy 2
                position = new Vector3(-width, height, 0);
                sprite = bottomLeftPivot;
                offset = new Vector2(0.5f, 0.5f);
                RandomizeScale(maxXScale - randomX - cubeSize, minYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 3: //From bottom to the top
                newVelocity = Vector3.up;

                //Enemy 1
                position = new Vector3(width, -height, 0);
                sprite = topRightPivot;
                offset = new Vector2(-0.5f, -0.5f);
                RandomizeScale(maxXScale, minYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                //Enemy 2
                position = new Vector3(-width, -height, 0);
                sprite = topLeftPivot;
                offset = new Vector2(0.5f, -0.5f);
                RandomizeScale(maxXScale - randomX - cubeSize, minYScale);
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;
        }
        /*
        switch (index)
        {
            case 0: //From right side to left side
                newVelocity = Vector3.left;

                //Enemy 1
                position = new Vector3(width, height, 0);
                sprite = topLeftPivot;
                offset = new Vector2(0.5f, -0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 1:
                newVelocity = Vector3.left;
                //Enemy 2
                position = new Vector3(width, -height, 0);
                sprite = bottomLeftPivot;
                offset = new Vector2(0.5f, 0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 2: //From left side to right side
                newVelocity = Vector3.right;

                //Enemy 1
                position = new Vector3(-width, height, 0);
                sprite = topRightPivot;
                offset = new Vector2(-0.5f, -0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 3:
                newVelocity = Vector3.right;
                //Enemy 2
                position = new Vector3(-width, -height, 0);
                sprite = bottomRightPivot;
                offset = new Vector2(-0.5f, 0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 4: //From top to bottom
                newVelocity = Vector3.down;

                //Enemy 1
                position = new Vector3(width, height, 0);
                sprite = bottomRightPivot;
                offset = new Vector2(-0.5f, 0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 5:
                newVelocity = Vector3.down;
                //Enemy 2
                position = new Vector3(-width, height, 0);
                sprite = bottomLeftPivot;
                offset = new Vector2(0.5f, 0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 6: //From bottom to the top
                newVelocity = Vector3.up;

                //Enemy 1
                position = new Vector3(width, -height, 0);
                sprite = topRightPivot;
                offset = new Vector2(-0.5f, -0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;

            case 7:
                newVelocity = Vector3.up;
                //Enemy 2
                position = new Vector3(-width, -height, 0);
                sprite = topLeftPivot;
                offset = new Vector2(0.5f, -0.5f);
                RandomizeScale();
                SpawnEnemy(position, sprite, offset, color, randomX, randomY, newVelocity);

                break;
        }*/
    }

    private void NewMethod(out Vector3 position, out Sprite sprite, out Vector2 offset)
    {
        position = new Vector3(width, height, 0);
        sprite = topLeftPivot;
        offset = new Vector2(0.5f, -0.5f);
    }

    void RandomizeScale(float maxX, float maxY)
    {
        randomX = Random.Range(minXScale, maxX);
        randomY = Random.Range(minYScale, maxY);
    }

    void SpawnEnemy(Vector3 position, Sprite sprite, Vector2 offset, Color color, float randomX, float randomY, Vector3 newVelocity)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        enemy.GetComponent<Collider2D>().offset = offset;
        enemy.transform.localScale = new Vector3(randomX, randomY, 0);
        enemy.GetComponent<Rigidbody2D>().velocity = newVelocity * velocity;
    }

    void UpdateDifficulty(int score)
    {
        if(score >= nextDifficultyLevel)
        {
            nextDifficultyLevel += nextDifficultyLevel;
            if(velocity < 5)
                velocity += velocityAcceleration;
            if(spawnRate < 1)
                spawnRate += spawnAcceleration;
            if (numberOfSpawnPoints < 4)
                numberOfSpawnPoints++;
            if (currentAudio < audioClips.Length - 1)
            {
                currentAudio++;
                float time = audioSource.time;
                audioSource.clip = audioClips[currentAudio];
                audioSource.Play();
            }
        }
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChange -= UpdateDifficulty; 
    }
}
