using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using TddStore.Core;
using Telerik.JustMock;

namespace TddStore.UnitTests
{
    [TestFixture]

    public class OrderServiceTests
    {
        [Test]

        public void WhenUserPlacesACorrectOrderThenAnOrderNumberShouldBeReturned()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.Items.Add(new ShoppingCartItem { ItemId = Guid.NewGuid(), Quantity = 1 });
            var customerId = Guid.NewGuid();
            var expectedOrderId = Guid.NewGuid();

            var orderDataService = Mock.Create<IOrderDataService>();
            Mock.Arrange(() => orderDataService.Save(Arg.IsAny<Order>()))
                .Returns(expectedOrderId)
                .OccursOnce();

            OrderService orderService = new OrderService(orderDataService);

            var result = orderService.PlaceOrder(customerId, shoppingCart);

            Assert.AreEqual(expectedOrderId, result);
            Mock.Assert(orderDataService);
        }

        public void WhenAUserAttemptsToOrderAnItemWithAQuantityOfZeroThrowInvalidOrderException()
        {

        }
    }
}
