using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeNode : MonoBehaviour
{
    [SerializeField] private SnakeNode nextNode = null;

    [SerializeField] private bool isHead = false;

    [SerializeField] private float tickRate = 0.5f;

    [SerializeField] private float timeToNextTick;

    [SerializeField] private GameObject snakePrefab;

    [SerializeField] private Vector3 lookingDirection;

    [SerializeField] private float moveValue = 0.1f;

    [SerializeField] private bool shouldSpawn;

    [SerializeField] private GameObject fruitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        timeToNextTick = tickRate;
        lookingDirection = Vector3.right;

        if (isHead)
        {
            SpawnFruit();
        }
    }

    void SpawnFruit()
    {
        float randomX = UnityEngine.Random.Range(-5f, 5f);
        float randomZ = UnityEngine.Random.Range(-5f, 5f);
        Vector3 fruitPos = new Vector3(randomX, 0, randomZ);
        Instantiate(fruitPrefab, fruitPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHead)
        {
            timeToNextTick = timeToNextTick - Time.deltaTime;
            if (timeToNextTick < 0)
            {
                Debug.Log("Going to move");
                if (Input.GetKey(KeyCode.W) && !lookingDirection.Equals(Vector3.back))
                {
                    lookingDirection = Vector3.forward;
                } else if (Input.GetKey(KeyCode.A) && !lookingDirection.Equals(Vector3.right))
                {
                    lookingDirection = Vector3.left;
                } else if (Input.GetKey(KeyCode.S) && !lookingDirection.Equals(Vector3.forward))
                {
                    lookingDirection = Vector3.back;
                } else if (Input.GetKey(KeyCode.D) && !lookingDirection.Equals(Vector3.left))
                {
                    lookingDirection = Vector3.right;
                }

                Vector3 newPosition = this.transform.position + lookingDirection * moveValue;

                Move(newPosition);

                if (Input.GetKey(KeyCode.Space))
                {
                    Spawn();
                }
                timeToNextTick = tickRate;
            }
        }
    }

    private void Move(Vector3 newPosition)
    {
        Vector3 prevPosition = this.transform.position;
        this.transform.position = newPosition;

        if (this.nextNode != null)
        {
            nextNode.Move(prevPosition);
        } else if (shouldSpawn)
        {
            shouldSpawn = false;
            nextNode = Instantiate(snakePrefab, prevPosition, Quaternion.identity).GetComponent<SnakeNode>();
        }
    }

    private void Spawn()
    {
        if (nextNode != null)
        {
            nextNode.Spawn();
        }
        else
        {
            shouldSpawn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            Destroy(other.gameObject);
            SpawnFruit();
            Spawn();
        }
    }
}
