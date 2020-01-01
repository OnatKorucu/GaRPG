using System.Collections;
using a_player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        [UnityTest]
        public IEnumerator start_in_idle_state()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            EntityStateMachine entityStateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Assert.AreEqual(typeof(Idle), entityStateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator switches_to_chase_player_when_in_chase_range()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();
            
            Player player = Helpers.GetPlayer();
            EntityStateMachine entityStateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            
            player.transform.position = entityStateMachine.transform.position + new Vector3(5.1f, 0f, 0f);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(typeof(Idle), entityStateMachine.CurrentStateType);
            
            player.transform.position = entityStateMachine.transform.position + new Vector3(4.9f, 0f, 0f);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(typeof(ChasePlayer), entityStateMachine.CurrentStateType);
        }
    }
}