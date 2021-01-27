using LOR.Pizzeria.Core.Models;
using LOR.Pizzeria.Logic;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using Xunit;

namespace LOR.Pizzeria.Tests
{
    public class PizzaTests
    {
        [Fact]
        public void CannotOrderPizzasNotAvailableAtLocation()
        {
            var logger = new Mock<ILogger>();
            var menuItems = new List<MenuItem> { new Pizza { Name = "Test Pizza", BasePrice = 199, } };
            var store = new Store { MenuItems = menuItems.ToArray() };

            var order = new Order(logger.Object, store);
            var fakeMenuItem = new Dessert { Name = "i dont exist on the store" };

            int lineItemCountBeforeBadMenuItem = order.LineItems.Count;
            order.AddItem(fakeMenuItem);
            int lineItemCountAfterBadMenuItem = order.LineItems.Count;

            Assert.Equal(lineItemCountBeforeBadMenuItem, lineItemCountAfterBadMenuItem);
        }

        [Fact]
        public void CanCustomizeBakingProcess()
        {
            var florenza = new FlorenzaPizza { };
            var margherita = new Pizza { };

            // dang, awkard coupling to System.Console methods make conveniently testing outputs difficult
            florenza.Cut();
            margherita.Cut();

            // assert florenza baked and cut differently than margherita 
            // assert 6 pieces with a special knife..
        }
    }
}
