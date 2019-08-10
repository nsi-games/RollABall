using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Scores : MonoBehaviour
{
    public class ScoreMessage : MessageBase
    {
        public int score;
        public Vector3 scorePos;
        public int lives;
    }

    private void Start()
    {
        SetupClient();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendScore(10, transform.position, 12);
        }
    }

    public void SendScore(int score, Vector3 scorePos, int lives)
    {
        ScoreMessage msg = new ScoreMessage()
        {
            score = score,
            scorePos = scorePos,
            lives = lives
        };

        NetworkServer.SendToAll(msg);
    }

    public void SetupClient()
    {
        NetworkClient.RegisterHandler<ScoreMessage>(OnScore);
    }

    public void OnScore(NetworkConnection conn, ScoreMessage msg)
    {
        Debug.Log("OnScoreMessage " + msg.score);
    }
}