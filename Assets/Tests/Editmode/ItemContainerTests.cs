using System;
using NUnit.Framework;
using UnityEngine;
using Moq;
using WizardGame.ItemSystem;
using WizardGame.ItemSystem.Item_Containers;
using Object = UnityEngine.Object;

#pragma warning disable 0649
namespace WizardGame.Testing.Item_Containers
{
    // Arrange Act Assert
    // TODO: Create handmade InventoryItem stub to avoid the following error:
    // Castle.Proxies.InventoryItemProxy must be instantiated using the ScriptableObject.CreateInstance method instead of new InventoryItemProxy.
    // from moq's dependency 
    public class ItemContainerTests
    {
        public class GetSlotByIndex_Method
        {
            [Test]
            [TestCase(-4), TestCase(-200), TestCase(-20)]
            public void Calling_With_Negative_Index_Throws_IndexOutOfBounds_Exception(int index)
            {
                ItemContainer itemContainer = new ItemContainer(10);

                Assert.Throws<IndexOutOfRangeException>(() => itemContainer.GetSlotByIndex(index));
            }

            [Test]
            [TestCase(2), TestCase(4), TestCase(19), TestCase(0)]
            public void Calling_With_Index_Between_0_And_Size_20_Returns_ItemSlot(int index)
            {
                ItemContainer itemContainer = new ItemContainer(20);
                ItemSlot expectedEmptySlot = new ItemSlot();

                ItemSlot emptySlot = itemContainer.GetSlotByIndex(index);

                Assert.AreEqual(expectedEmptySlot, emptySlot);
            }
        }

        public class HasItem_Method
        {
            [Test]
            public void Calling_With_Null_Item_Returns_False()
            {
                ItemContainer itemContainer = new ItemContainer(20);

                bool hasItem = itemContainer.HasItem(null);

                Assert.False(hasItem);
            }

            [Test]
            public void Returns_False_If_Item_Isnt_In_Container()
            {
                ItemContainer itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock();

                bool hasItem = itemContainer.HasItem(invItemMock.Object);

                Assert.False(hasItem);
            }

            // integration test since it checks whether multiple dependencies integrated together
            // work properly
            [Test]
            public void Returns_True_After_Item_Is_Added()
            {
                ItemContainer itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock();
                var itemSlot = new ItemSlot(invItemMock.Object, 3);

                itemContainer.AddItem(itemSlot);
                bool hasItem = itemContainer.HasItem(itemSlot.invItem);

                Assert.True(hasItem);
            }
        }

        public class AddItem_Method
        {
            [Test]
            [TestCase(4), TestCase(10), TestCase(30), TestCase(25)]
            // this test needs to be renamed properly
            public void Item_Gets_Added_With_Positive_X_Quant_Slot(int xQuant)
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);
                var itemSlot = new ItemSlot(invItemMock.Object, xQuant);
                var expectedSlot = new ItemSlot(invItemMock.Object, 0);

                ItemSlot slot = itemContainer.AddItem(itemSlot);

                Debug.Log((expectedSlot == slot) + " | " + expectedSlot.Equals(slot) + " | " +
                          Object.Equals(expectedSlot, slot) + " | " + ReferenceEquals(expectedSlot, slot));

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Item_Gets_Added_To_1_Slot_Container_With_Over_MaxQuant_Slot()
            {
                var itemContainer = new ItemContainer(1);
                var invItemMock = InvItemMock(5);
                var slot = new ItemSlot(invItemMock.Object, 20);
                var expectedSlot = new ItemSlot(invItemMock.Object, 15);

                slot = itemContainer.AddItem(slot);

                Assert.AreEqual(expectedSlot, slot);
            }
        }

        public class SubtractItem_Method
        {
            [Test]
            public void Calling_With_Less_Than_MaxStack_Doesnt_Remove_Entire_Stack()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);
                var slot = new ItemSlot(invItemMock.Object, 5);
                var expectedSlot = new ItemSlot(invItemMock.Object, 3);

                itemContainer.AddItem(slot);
                slot = itemContainer.SubtractItem(new ItemSlot(invItemMock.Object, 8));

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Calling_With_MaxStack_Removes_MaxStack()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);
                var slot = new ItemSlot(invItemMock.Object, 5);
                var expectedSlot = new ItemSlot(invItemMock.Object, 0);

                itemContainer.AddItem(slot);
                itemContainer.SubtractItem(slot);
                slot = itemContainer.GetSlotByIndex(0);

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Returns_Same_Slot_If_Slot_Isnt_In_Container()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock();
                var expectedSlot = new ItemSlot(invItemMock.Object, 5);

                var slot = itemContainer.SubtractItem(expectedSlot);

                Assert.AreEqual(expectedSlot, slot);
            }
        }

        public class RemoveAt_Method
        {
            // would need to be an integration test...
            [Test]
            public void Calling_With_Index_4_Clears_Slot_At_Index_4()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(4);
                var expectedSlot = new ItemSlot(invItemMock.Object, 0);

                itemContainer.AddItem(new ItemSlot(invItemMock.Object, 5));
                itemContainer.RemoveAt(0);
                var slot = itemContainer.GetSlotByIndex(0);

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Calling_With_Index_10_Clears_Slot_At_Index_10()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(1);
                var expectedSlot = new ItemSlot(invItemMock.Object, 0);

                itemContainer.AddItem(new ItemSlot(invItemMock.Object, 12));
                itemContainer.RemoveAt(10);
                var slot = itemContainer.GetSlotByIndex(10);

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Throws_IndexOutOfRange_Exception_If_Index_Isnt_In_Bounds_Of_Container()
            {
                var itemContainer = new ItemContainer(20);

                Assert.Throws<IndexOutOfRangeException>(() => itemContainer.RemoveAt(25));
            }
        }

        public class GetTotalQuantity_Method
        {
            [Test]
            [TestCase(2), TestCase(4), TestCase(20), TestCase(50)]
            public void Adding_Positive_X_Quantity_Returns_X(int xQuant)
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);

                itemContainer.AddItem(new ItemSlot(invItemMock.Object, xQuant));
                int totalQuant = itemContainer.GetTotalQuantity(invItemMock.Object);

                // string bee = "";
                // for (int i = 0; i < 20; i++)
                // {
                //     bee += " | " + itemContainer.GetSlotByIndex(i).Quantity;
                // }
                // Debug.Log(bee);

                Assert.AreEqual(xQuant, totalQuant);
            }

            [Test]
            public void Returns_0_If_Item_Isnt_In_Container()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(4);

                int totalQuant = itemContainer.GetTotalQuantity(invItemMock.Object);

                Assert.AreEqual(0, totalQuant);
            }
        }

        public class Swap_Method
        {
            [Test]
            [TestCase(2), TestCase(9), TestCase(10)]
            public void Call_With_Same_Positive_InBounds_Index_Returns_With_No_Change_In_Container(int index)
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(4);
                var slotOne = new ItemSlot(invItemMock.Object, 4);
                var slotTwo = new ItemSlot(invItemMock.Object, 2);

                itemContainer.AddItem(slotOne);
                itemContainer.AddItem(slotTwo);
                itemContainer.Swap(index, index);

                var itemOneEquivalent = itemContainer.GetSlotByIndex(0) == slotOne;
                var itemTwoEquivalent = itemContainer.GetSlotByIndex(1) == slotTwo;

                Assert.True(itemOneEquivalent && itemTwoEquivalent);
            }

            [Test]
            public void Swapping_Quantity_4_With_2_Sets_Slots_Second_To_5_First_To_1_With_MaxStack_5()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);
                var slotOne = new ItemSlot(invItemMock.Object, 4);
                var slotTwo = new ItemSlot(invItemMock.Object, 2);
                var expectedSlotOne = new ItemSlot(invItemMock.Object, 2);
                var expectedSlotTwo = new ItemSlot(invItemMock.Object, 5);

                itemContainer.AddItem(slotOne);
                itemContainer.AddItem(slotTwo);
                itemContainer.Swap(0, 1);

                var indexOneItem = itemContainer.GetSlotByIndex(0);
                var indexTwoItem = itemContainer.GetSlotByIndex(1);
                var itemOneEquivalent = indexOneItem == expectedSlotOne;
                var itemTwoEquivalent = indexTwoItem == expectedSlotTwo;

                Assert.True(itemOneEquivalent && itemTwoEquivalent);
            }

            [Test]
            public void Swapping_Quantity_5_With_3_Sets_Slots_Second_To_5_First_To_3_With_MaxStack_5()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMock = InvItemMock(5);
                var slotOne = new ItemSlot(invItemMock.Object, 5);
                var slotTwo = new ItemSlot(invItemMock.Object, 3);
                var expectedSlotOne = new ItemSlot(invItemMock.Object, 3);
                var expectedSlotTwo = new ItemSlot(invItemMock.Object, 5);

                itemContainer.AddItem(slotOne);
                itemContainer.AddItem(slotTwo);
                itemContainer.Swap(0, 1);

                var indexOneSlot = itemContainer.GetSlotByIndex(0);
                var indexTwoSlot = itemContainer.GetSlotByIndex(1);
                var itemOneEquivalent = indexOneSlot == expectedSlotOne;
                var itemTwoEquivalent = indexTwoSlot == expectedSlotTwo;

                Assert.True(itemOneEquivalent && itemTwoEquivalent);
            }

            [Test]
            public void Swapping_Separate_Items_Swaps_Their_Positions()
            {
                var itemContainer = new ItemContainer(20);
                var invItemMockOne = InvItemMock(5, "Stringo mock");
                var invItemMockTwo = InvItemMock(5, "Mujahadeenoo");
                var slotOne = new ItemSlot(invItemMockOne.Object, 1);
                var slotTwo = new ItemSlot(invItemMockTwo.Object, 2);

                itemContainer.AddItem(slotOne);
                itemContainer.AddItem(slotTwo);
                itemContainer.Swap(0, 1);

                var indexOneSlot = itemContainer.GetSlotByIndex(0);
                var indexTwoSlot = itemContainer.GetSlotByIndex(1);
                var itemOneEquivalent = indexOneSlot == slotTwo;
                var itemTwoEquivalent = indexTwoSlot == slotOne;

                Debug.Log(string.Join(" | ", slotOne.ToString(), slotTwo.ToString(),
                    indexOneSlot.ToString(), indexTwoSlot.ToString()));

                Assert.True(itemOneEquivalent && itemTwoEquivalent);
            }
        }

        private static Mock<InventoryItem> InvItemMock(int maxStack = 1, string displayString = "Mock stringo")
        {
            var invItemMock = new Mock<InventoryItem>();

            invItemMock.Setup(x => x.GetInfoDisplayText()).Returns(displayString);
            invItemMock.SetupGet(x => x.MaxStack).Returns(() => maxStack);

            return invItemMock;
        }
    }
}