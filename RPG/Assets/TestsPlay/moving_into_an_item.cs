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
        private Item _item;
        private Player _player;

        [UnitySetUp]
        public IEnumerator init()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>(); // need this and cannot inherit from base class player_input_test as [UnitySetUp] 
                                                                    // seems to run before [SetUp]
            yield return Helpers.LoadItemTestsScene();

            _player = Helpers.GetPlayer();
            _item = Object.FindObjectOfType<Item>();
        } 
        
        
        [UnityTest]
        public IEnumerator picks_up_and_equips_item()
        {
            PlayerInput.Instance.Vertical.Returns(1f);
            
            Assert.AreNotSame(_item, _player.GetComponent<Inventory>().ActiveItem);
            
            yield return new WaitForSeconds(2f);
            
            Assert.AreSame(_item, _player.GetComponent<Inventory>().ActiveItem);
        }
        
        [UnityTest]
        public IEnumerator changes_crosshair_to_item_crosshair()
        {
            Crosshair crosshair = Object.FindObjectOfType<Crosshair>();
            
            Assert.AreNotSame(_item.CrosshairDefinition.sprite, crosshair.GetComponent<Image>().sprite);

            // player.PlayerInput.Vertical.Returns(1f);
            // do not move player but do move item for testing purposes
            Vector3 playerPos = _player.transform.position;
            Vector3 playerPosPlus1 = playerPos + Vector3.up; // make item and player collide by moving item instead of player
            _item.transform.position = playerPosPlus1;
            yield return new WaitForFixedUpdate(); // just wait for physics for proper collision processing
            
            Assert.AreEqual(_item.CrosshairDefinition.sprite, crosshair.GetComponent<Image>().sprite);
        }
        
        [UnityTest]
        public IEnumerator changes_slot_1_icon_to_match_item_icon()
        {
            Hotbar hotbar = Object.FindObjectOfType<Hotbar>();
            Slot slot1 = hotbar.GetComponentInChildren<Slot>();
            
            Assert.AreNotSame(_item.Icon, slot1.IconImageImage.sprite); 
            
            // player.PlayerInput.Vertical.Returns(1f);
            // do not move player but do move item for testing purposes
            Vector3 playerPos = _player.transform.position;
            Vector3 playerPosPlus1 = playerPos + Vector3.up; // make item and player collide by moving item instead of player
            _item.transform.position = playerPosPlus1;
            yield return new WaitForFixedUpdate(); // just wait for physics for proper collision processing
            
            Assert.AreEqual(_item.Icon, slot1.IconImageImage.sprite);
        }
    }
}