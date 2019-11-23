using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_positive_input
    {
        [UnityTest]
        public IEnumerator moves_forward_on_vertical()
        {
            Helpers.CreateFloor();
            
            Player player = Helpers.CreatePlayer();
            player.PlayerInput.Vertical.Returns(1f);
            
            float startingZ = player.transform.position.z;
            
            
            yield return new WaitForSeconds(5f);

            float endingZ = player.transform.position.z;
            Assert.Greater(endingZ, startingZ);
        }
        
    }
}