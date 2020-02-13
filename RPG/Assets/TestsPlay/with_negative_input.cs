using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_negative_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_backward_on_vertical()
        {
            yield return Helpers.LoadMovementTestsScene();
            
            Player player = Helpers.GetPlayer();
            PlayerInput.Instance.Vertical.Returns(-1f);
            
            float startingZ = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZ = player.transform.position.z;
            Assert.Less(endingZ, startingZ);
        }
    }
}