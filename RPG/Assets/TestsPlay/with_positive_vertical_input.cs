using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_vertical_input
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localPosition = Vector3.zero;
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
            
            GameObject playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.transform.position = new Vector3(0, 1.5f, 0);
            
            Player player = playerGameObject.AddComponent<Player>();
            float startingZ = player.transform.position.z;
            
            TestPlayerInput testPlayerInput = new TestPlayerInput();
            testPlayerInput.Vertical = 1f;
            player.PlayerInput = testPlayerInput;
            
            yield return new WaitForSeconds(5f);

            float endingZ = player.transform.position.z;
            Assert.Greater(endingZ, startingZ);
        }
    }

    public class TestPlayerInput : IPlayerInput
    {
        public float Vertical { get; set; }
    }
}