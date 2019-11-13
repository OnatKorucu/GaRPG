using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestsPlay
{
    public class a_moving_cube
    {
        [UnityTest]
        public IEnumerator cube_moving_forward_changes_position()
        {
            // Arrange
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            
            
            for (int i = 0; i < 10; i++)
            {
                // Act
                cube.transform.position += Vector3.forward;
                yield return new WaitForSeconds(0.1f);
                
                // Assert
                Assert.AreEqual(i+1, cube.transform.position.z);
            }
            
        }
    }
}
