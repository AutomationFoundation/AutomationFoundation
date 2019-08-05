using System;
using AutomationFoundation.Runtime.Abstractions;
using AutomationFoundation.Runtime.Tests.Stubs;
using Moq;
using NUnit.Framework;

namespace AutomationFoundation.Runtime.Tests
{
    [TestFixture]
    public class ProcessorCollectionTests
    {
        [Test]
        public void AddTheProcessorsToTheCollection()
        {
            var parent = new Mock<IRuntime>();
            parent.Setup(o => o.IsActive).Returns(false);

            var target = new ProcessorCollection(parent.Object);

            var p1 = new StubProcessor();
            var p2 = new StubProcessor();

            target.Add(p1);
            target.Add(p2);

            Assert.AreEqual(2, target.Count);
            Assert.Contains(p1, target);
            Assert.Contains(p2, target);
        }

        [Test]
        public void RemoveTheProcessorsFromToTheCollection()
        {
            var parent = new Mock<IRuntime>();
            parent.Setup(o => o.IsActive).Returns(false);

            var target = new ProcessorCollection(parent.Object);

            var p1 = new StubProcessor();
            var p2 = new StubProcessor();

            target.Add(p1);
            target.Add(p2);

            Assert.AreEqual(2, target.Count);

            target.Remove(p1);
            Assert.That(() => !target.Contains(p1));
            target.Remove(p2);
            Assert.That(() => !target.Contains(p2));

            Assert.IsEmpty(target);
        }

        [Test]
        public void NotAllowTheSameProcessorToBeAddedTwice()
        {
            var parent = new Mock<IRuntime>();
            var target = new ProcessorCollection(parent.Object);

            var processor = new StubProcessor();
            target.Add(processor);

            Assert.AreEqual(1, target.Count);
            Assert.Throws<ArgumentException>(() => target.Add(processor));
        }

        [Test]
        public void NotAllowProcessorsToAddWhenTheParentIsActive()
        {
            var parent = new Mock<IRuntime>();
            parent.Setup(o => o.IsActive).Returns(true);

            var target = new ProcessorCollection(parent.Object);

            var processor = new StubProcessor();

            Assert.Throws<NotSupportedException>(() => target.Add(processor));
            Assert.AreEqual(0, target.Count);
        }

        [Test]
        public void NotAllowProcessorsToClearWhenTheParentIsActive()
        {
            var called = false;

            var parent = new Mock<IRuntime>();
            parent.Setup(o => o.IsActive).Returns(() =>
            {
                if (called)
                {
                    return true;
                }

                called = true;
                return false;
            });

            var target = new ProcessorCollection(parent.Object)
            {
                new StubProcessor()
            };

            Assert.Throws<NotSupportedException>(() => target.Clear());
            Assert.AreEqual(1, target.Count);
        }

        [Test]
        public void NotAllowProcessorsToRemoveWhenTheParentIsActive()
        {
            var parent = new Mock<IRuntime>();

            var target = new ProcessorCollection(parent.Object);

            var processor = new StubProcessor();
            target.Add(processor);

            Assert.AreEqual(1, target.Count);

            parent.Setup(o => o.IsActive).Returns(true);

            Assert.Throws<NotSupportedException>(() => target.Remove(processor));
        }
    }
}