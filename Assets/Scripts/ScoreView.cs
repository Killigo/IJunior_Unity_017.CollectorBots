using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;

    private int _maxScore;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        transform.Rotate(0, 180, 0);
    }

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score.text = $"{score} / {_maxScore}".ToString();
    }

    internal void SetMaxScore(int value)
    {
        _maxScore = value;
    }
}

