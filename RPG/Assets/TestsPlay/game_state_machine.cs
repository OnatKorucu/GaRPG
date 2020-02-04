using System.Collections;
using a_player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine
    {
        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadMenuScene();
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), gameStateMachine.CurrentStateType);
            yield return new WaitForEndOfFrame();

            PlayButton.LevelToLoad = "Level01";
            yield return new WaitForEndOfFrame();
            
            Assert.AreEqual(typeof(Load), gameStateMachine.CurrentStateType);
        }
    }
}