using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections;
using System.Collections.Generic;

namespace Dragablz.Core.Tests
{
    [TestFixture]
    public class CollectionTeaserFixture
    {
        [Test]
        public void WillCreateForList()
        {
            ArrayList myList = [];

            bool result = CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser);

            Assert.That(result, Is.True);
            Assert.That(collectionTeaser, Is.Not.Null);
        }

        [Test]
        public void WillCreateForGenericCollection()
        {
            ICollection<string> myList = A.Fake<ICollection<string>>();

            bool result = CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser);

            Assert.That(result);
            Assert.That(collectionTeaser is not null);
        }

        [Test]
        public void WillCreateForCollection()
        {
            ICollection myList = A.Fake<ICollection>();

            bool result = CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser);

            Assert.That(result, Is.False);
            Assert.That(collectionTeaser, Is.Null);
        }

        [Test]
        public void WillAddForList()
        {
            ArrayList myList = [];
            Assert.That(CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser), Is.True);

            collectionTeaser.Add("i am going to type this in, manually, twice.");

            CollectionAssert.AreEquivalent(new[] { "i am going to type this in, manually, twice." }, myList);
            //i didnt really.  i copied and pasted it.
        }

        [Test]
        public void WillRemoveForList()
        {
            ArrayList myList =
            [
                1,
                2,
                3,
                4,
                5
            ];
            Assert.That(CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser), Is.True);

            collectionTeaser.Remove(3);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 4, 5 }, myList);
        }

        [Test]
        public void WillAddForGenericCollection()
        {
            ICollection<string> myList = A.Fake<ICollection<string>>();
            Assert.That(CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser), Is.True);

            collectionTeaser.Add("hello");

            A.CallTo(() => myList.Add("hello")).MustHaveHappened();
            A.CallTo(() => myList.Remove("hello")).MustNotHaveHappened();
        }

        [Test]
        public void WillRemoveForGenericCollection()
        {
            ICollection<string> myList = A.Fake<ICollection<string>>();
            Assert.That(CollectionTeaser.TryCreate(myList, out CollectionTeaser collectionTeaser), Is.True);

            collectionTeaser.Remove("bye");

            A.CallTo(() => myList.Remove("bye")).MustHaveHappened();
            A.CallTo(() => myList.Add("bye")).MustNotHaveHappened();
        }
    }
}