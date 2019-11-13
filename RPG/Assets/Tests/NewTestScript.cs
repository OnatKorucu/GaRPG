using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void should_return_one_as_list_count()
        {
            // Arrange
            List<string> myStrings = new List<string>();
            
            // Act
            myStrings.Add("Aloys' string");
            myStrings.Add("Aloys' other string");
            myStrings.RemoveAt(0);
            
            // Assert
            Assert.AreEqual(1, myStrings.Count);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
