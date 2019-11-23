using NSubstitute;
using UnityEngine;
using UnityEngine.AI;

namespace a_player
{
    public static class Helpers
    {
        public static void CreateFloor()
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localPosition = Vector3.zero;
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
        }

        public static Player CreatePlayer()
        {
            GameObject playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.AddComponent<NavMeshAgent>();
            playerGameObject.transform.position = new Vector3(0, 1.5f, 0);
            
            Player player = playerGameObject.AddComponent<Player>();
            
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;

            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion transformRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, transformRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
    }
}