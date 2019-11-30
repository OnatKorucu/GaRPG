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
            yield return Helpers.LoadTestScene();
            
            Player player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(1f);
            
            float startingZ = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZ = player.transform.position.z;
            Assert.Greater(endingZ, startingZ);
        }
    }
    
    public class with_negative_input
    {
        [UnityTest]
        public IEnumerator moves_backward_on_vertical()
        {
            yield return Helpers.LoadTestScene();
            
            Player player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(-1f);
            
            float startingZ = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);

            float endingZ = player.transform.position.z;
            Assert.Less(endingZ, startingZ);
        }
    }

    public class moving_into_an_item
    {
        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            yield return Helpers.LoadTestScene();

            Player player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(1f);
            
            Item item = Object.FindObjectOfType<Item>();
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(2f);
            
            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
    }
}