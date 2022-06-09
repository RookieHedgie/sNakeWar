using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Snake2 : MonoBehaviour
{
    private Vector2 _direction = Vector2.left;

    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    public int InıtıalSize = 3;
    public Text scoreDisplay;
    public int score = 0;
    public int endScore;
    public GameObject pointSound;
    public GameObject obstacleSound;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        scoreDisplay.text = score.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }

        if (score >= 5)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);

    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.InıtıalSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }


        this.transform.position = _direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Instantiate(pointSound, transform.position, Quaternion.identity);
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            Instantiate(obstacleSound, transform.position, Quaternion.identity);
            ResetState();
            score++;
        }else
        {
            ResetState();
        }
    }
}
