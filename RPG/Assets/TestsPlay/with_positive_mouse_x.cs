using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_mouse_x : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestsScene();

            Player player = Helpers.GetPlayer();
            PlayerInput.Instance.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            
            yield return new WaitForSeconds(0.5f);

            float turnAmout = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmout, 0f);
        }
    }
}