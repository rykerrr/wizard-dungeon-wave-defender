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
                var invItemStub = InventoryItemFake();

                bool hasItem = itemContainer.HasItem(invItemStub.Object);

                Assert.False(hasItem);
            }

            // integration test since it checks whether multiple dependencies integrated together
            // work properly
            [Test]
            public void Returns_True_After_Item_Is_Added()
            {
                ItemContainer itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake();
                var itemSlot = new ItemSlot(invItemStub.Object, 3);

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
            public void ItemSlot_Returned_With_0_Quantity_When_Added_With_Positive_X_Quant_Slot(int xQuant)
            {
                var itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, xQuant);
                var expectedSlot = new ItemSlot(invItemStub.Object, 0);

                ItemSlot slot = itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void ItemSlot_Returned_With_6_Quantity_MaxStack_5_When_Added_To_2_With_9_And_Container_Full()
            {
                var itemContainer = new ItemContainer(1);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, 9);
                var expectedSlot = new ItemSlot(invItemStub.Object, 6);

                itemContainer.AddItem(new ItemSlot(invItemStub.Object, 2));
                ItemSlot slot = itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void ItemSlot_With_6_Quantity_MaxStack_5_Fills_Container_With_1_Space_Up()
            {
                var itemContainer = new ItemContainer(1);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, 6);
                var expectedSlot = new ItemSlot(invItemStub.Object, invItemStub.Object.MaxStack);
                
                itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(expectedSlot, itemContainer.GetSlotByIndex(0));
            }

            [Test]
            public void ItemSlot_With_3_Quant_Gets_Added_To_Next_Slot_If_First_Full()
            {
                var itemContainer = new ItemContainer(2);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, 5);
                var expectedSlot = new ItemSlot(invItemStub.Object, 3);
                
                itemContainer.AddItem(itemSlot);
                itemContainer.AddItem(expectedSlot);
                
                Assert.AreEqual(expectedSlot, itemContainer.GetSlotByIndex(1));
            }

            [Test]
            public void ItemSlot_With_3_Quant_MaxStack_5_Gets_Added_To_Empty_Slot()
            {
                var itemContainer = new ItemContainer(2);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, 3);

                itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(itemSlot, itemContainer.GetSlotByIndex(0));
            }

            [Test]
            [TestCase(1), TestCase(4), TestCase(3)]
            public void ItemSlot_With_Max_Quant_Gets_Added_To_Empty_Slot(int xQuant)
            {
                var itemContainer = new ItemContainer(2);
                var invItemStub = InventoryItemFake(xQuant);
                var itemSlot = new ItemSlot(invItemStub.Object, xQuant);

                itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(itemSlot, itemContainer.GetSlotByIndex(0));
            }

            [Test]
            [TestCase(1), TestCase(4), TestCase(3)]
            public void Returns_Same_Slot_If_No_Space_In_Container(int xQuant)
            {
                var itemContainer = new ItemContainer(0);
                var invItemStub = InventoryItemFake(5);
                var itemSlot = new ItemSlot(invItemStub.Object, xQuant);

                var newSlot = itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(itemSlot, newSlot);
            }

            [Test]
            public void Adds_To_Next_Slot_If_ItemSlots_MaxStack_3_Quant_2_MaxStack_4_Quant_3_With_Different_Items()
            {
                var itemContainer = new ItemContainer(3);
                var invItemStub1 = InventoryItemFake(3);
                var invItemStub2 = InventoryItemFake(4);
                var itemSlot1 = new ItemSlot(invItemStub1.Object, 2);
                var itemSlot2 = new ItemSlot(invItemStub2.Object, 3);

                itemContainer.AddItem(itemSlot1);
                itemContainer.AddItem(itemSlot2);
                
                Assert.AreEqual(itemSlot2, itemContainer.GetSlotByIndex(1));
            }

            [Test]
            [TestCase(-3), TestCase(-4), TestCase(-244)]
            public void Does_Nothing_If_Called_With_Negative_Quant(int xQuant)
            {
                var itemContainer = new ItemContainer(3);
                var invItemStub = InventoryItemFake(3);
                var itemSlot = new ItemSlot(invItemStub.Object, xQuant);

                itemContainer.AddItem(itemSlot);
                
                Assert.AreEqual(ItemSlot.Empty, itemContainer.GetSlotByIndex(0));
            }
        }

        public class SubtractItem_Method
        {
            [Test]
            public void Calling_With_Less_Than_MaxStack_Doesnt_Remove_Entire_Stack()
            {
                var itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake(5);
                var slot = new ItemSlot(invItemStub.Object, 5);
                var expectedSlot = new ItemSlot(invItemStub.Object, 3);

                itemContainer.AddItem(slot);
                slot = itemContainer.SubtractItem(new ItemSlot(invItemStub.Object, 8));

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Calling_With_MaxStack_Removes_MaxStack()
            {
                var itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake(5);
                var slot = new ItemSlot(invItemStub.Object, 5);
                var expectedSlot = new ItemSlot(invItemStub.Object, 0);

                itemContainer.AddItem(slot);
                itemContainer.SubtractItem(slot);
                slot = itemContainer.GetSlotByIndex(0);

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Returns_Same_Slot_If_Slot_Isnt_In_Container()
            {
                var itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake();
                var expectedSlot = new ItemSlot(invItemStub.Object, 5);

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
                var invItemStub = InventoryItemFake(4);
                var expectedSlot = new ItemSlot(invItemStub.Object, 0);

                itemContainer.AddItem(new ItemSlot(invItemStub.Object, 5));
                itemContainer.RemoveAt(0);
                var slot = itemContainer.GetSlotByIndex(0);

                Assert.AreEqual(expectedSlot, slot);
            }

            [Test]
            public void Calling_With_Index_10_Clears_Slot_At_Index_10()
            {
                var itemContainer = new ItemContainer(20);
                var invItemStub = InventoryItemFake(1);
                var expectedSlot = new ItemSlot(invItemStub.Object, 0);

                itemContainer.AddItem(new ItemSlot(invItemStub.Object, 12));
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
                var invItemStub = InventoryItemFake(5);

                itemContainer.AddItem(new ItemSlot(invItemStub.Object, xQuant));
                int totalQuant = itemContainer.GetTotalQuantity(invItemStub.Object);

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
                var invItemStub = InventoryItemFake(4);

                int totalQuant = itemContainer.GetTotalQuantity(invItemStub.Object);

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
                var invItemStub = InventoryItemFake(4);
                var slotOne = new ItemSlot(invItemStub.Object, 4);
                var slotTwo = new ItemSlot(invItemStub.Object, 2);

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
                var invItemStub = InventoryItemFake(5);
                var slotOne = new ItemSlot(invItemStub.Object, 4);
                var slotTwo = new ItemSlot(invItemStub.Object, 2);
                var expectedSlotOne = new ItemSlot(invItemStub.Object, 1);
                var expectedSlotTwo = new ItemSlot(invItemStub.Object, 5);

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
                var invItemStub = InventoryItemFake(5);
                var slotOne = new ItemSlot(invItemStub.Object, 5);
                var slotTwo = new ItemSlot(invItemStub.Object, 3);
                var expectedSlotOne = new ItemSlot(invItemStub.Object, 3);
                var expectedSlotTwo = new ItemSlot(invItemStub.Object, 5);

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
                var invItemStubOne = InventoryItemFake(5, "Stringo mock");
                var invItemStubTwo = InventoryItemFake(5, "Mujahadeenoo");
                var slotOne = new ItemSlot(invItemStubOne.Object, 1);
                var slotTwo = new ItemSlot(invItemStubTwo.Object, 2);

                itemContainer.AddItem(slotOne);
                itemContainer.AddItem(slotTwo);
                itemContainer.Swap(0, 1);

                var indexOneSlot = itemContainer.GetSlotByIndex(0);
                var indexTwoSlot = itemContainer.GetSlotByIndex(1);
                var itemOneEquivalent = indexOneSlot == slotTwo;
                var itemTwoEquivalent = indexTwoSlot == slotOne;

                Assert.True(itemOneEquivalent && itemTwoEquivalent);
            }
        }

        private static Mock<InventoryItem> InventoryItemFake(int maxStack = 1, string displayString = "Mock stringo")
        {
            var invItemStub = new Mock<InventoryItem>();

            invItemStub.Setup(x => x.GetInfoDisplayText()).Returns(displayString);
            invItemStub.SetupGet(x => x.MaxStack).Returns(() => maxStack);

            return invItemStub;
        }
    }
}