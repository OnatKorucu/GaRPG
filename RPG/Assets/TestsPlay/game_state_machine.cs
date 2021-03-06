using System.Collections;
using a_player;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine 
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>(); 
        }
        
        [TearDown]
        public void tear_down()
        {
            GameObject.Destroy(GameObject.FindObjectOfType<GameStateMachine>());
        }

        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadMenuScene();
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            // For an explanation of the differences between yield return null vs yield return new WaitForEndOfFrame(),
            // see: https://answers.unity.com/questions/755196/yield-return-null-vs-yield-return-waitforendoffram.html
            Assert.AreEqual(typeof(Menu), gameStateMachine.CurrentStateType);
            yield return null;

            PlayButton.LevelToLoad = "Level01";
            yield return null;

            Assert.AreEqual(typeof(Load), gameStateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator switches_to_play_when_level_to_load_completed()
        {
            yield return Helpers.LoadMenuScene();
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            Assert.AreEqual(typeof(Menu), gameStateMachine.CurrentStateType);
            yield return null;

            PlayButton.LevelToLoad = "Level01";
            yield return null;

            Assert.AreEqual(typeof(Load), gameStateMachine.CurrentStateType);

            yield return new WaitUntil(() => gameStateMachine.CurrentStateType == typeof(Play));
            Assert.AreEqual(typeof(Play), gameStateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_from_play_to_paise_when_pause_button_pressed()
        {
            yield return Helpers.LoadMenuScene();
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();

            PlayButton.LevelToLoad = "Level01";
            yield return new WaitUntil(() => gameStateMachine.CurrentStateType == typeof(Play));

            PlayerInput.Instance.PausePressed.Returns(true);
            yield return null;
            
            Assert.AreEqual(typeof(Pause), gameStateMachine.CurrentStateType);
        }


        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exist()
        {
            var stateMachineOne = new GameObject("State Machine One").AddComponent<GameStateMachine>();
            var stateMachineTwo = new GameObject("State Machine Two").AddComponent<GameStateMachine>();
            yield return null;

            // Cannot use Assert.IsNull on second game state machine as we have no control over when the GameObject is removed,
            // but using == will use Unity's overload of the operator which will return also true for <null>.
            // See for example: https://answers.unity.com/questions/865405/nunit-notnull-assert-strangeness.html
            Assert.IsNotNull(stateMachineOne);
            Assert.IsTrue(stateMachineTwo == null);
        }
    }
}