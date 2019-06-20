using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        private GameManager game;
        private Player player;

        [SetUp]
        public void Setup()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
            GameObject clone = MonoBehaviour.Instantiate(prefab);
            game = clone.GetComponent<GameManager>();
            player = game.GetComponentInChildren<Player>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(game.gameObject);
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LoadGameResource()
        {

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(game != null);
        }

        [UnityTest]
        public IEnumerator PlayerExistsInGame()
        { 
            player = game.GetComponentInChildren<Player>();

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(player != null);
        }

        [UnityTest]
        public IEnumerator ItemCollidesWithPlayer()
        {
            Item item = game.itemManager.GetItem(0);

            player.transform.position = new Vector3(0, 1, 0);
            item.transform.position = new Vector3(0, 1, 0);

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(item == null);
        }

        [UnityTest]
        public IEnumerator ItemCollectAddsScoreToGameManager()
        {
            Item item = game.itemManager.GetItem(0);

            int oldScore = game.score;

            player.transform.position = new Vector3(0, 2, 0);
            item.transform.position = new Vector3(0, 2, 0);

            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(game.score > oldScore);
        }


    }
}
