using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class SomeTestyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ShouldPass()
        {
            Assert.Pass();
        }
        
        [Test]
        public void Test_WithFluentAssertions_ShouldPass()
        {
            var number = 4;
            (2*2).Should().Be(number);
        }
    }
}