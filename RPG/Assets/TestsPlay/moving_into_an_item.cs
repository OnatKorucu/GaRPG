using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace a_player
{
    public class moving_into_an_item
    {
        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            yield return Helpers.LoadMovementTestScene();

            Player player = Helpers.GetPlayer();
            player.PlayerInput.Vertical.Returns(1f);
            
            Item item = Object.FindObjectOfType<Item>();
            Assert.AreNotSame(item, player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(2f);
            
            Assert.AreSame(item, player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator changes_crosshair_to_item_crosshair()
        {
            yield return Helpers.LoadItemTestScene();

            Player player = Helpers.GetPlayer();
            Crosshair crosshair = Object.FindObjectOfType<Crosshair>();
            
            // player.PlayerInput.Vertical.Returns(1f);
            // do not move player but do move item for testing purposes
            
            Item item = Object.FindObjectOfType<Item>();
            Assert.AreNotSame(item.CrosshairDefinition.sprite, crosshair.GetComponent<Image>().sprite);

            Vector3 playerPos = player.transform.position;
            Vector3 playerPosPlus1 = playerPos + Vector3.up; // make item and player collide
            item.transform.position = playerPosPlus1;
            yield return null; // just wait for end of frame for proper processing, as player does not need to move there is no need to really wait otherwise
            
            Assert.AreEqual(item.CrosshairDefinition.sprite, crosshair.GetComponent<Image>().sprite);
        }
    }
}