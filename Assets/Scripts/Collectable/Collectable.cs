using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RollABall
{
    public class Collectable : MonoBehaviour
    {
        public int value = 1;

        public void Collect()
        {
            GameManager.Instance.AddScore(value);
            // Temp
            Destroy(gameObject);
        }
    }
}
