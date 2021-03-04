using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SauceDemo
{
    public class LoginUsers
    {
        public static string valid_username = "standard_user";
        public static string valid_password = "secret_sauce";
        public static string invalid_username = "invalidusername";
        public static string invalid_password = "invalidpassword";
    }
    
    public enum ItemSortOrder
    {
        az,
        za,
        lohi,
        hilo
    }

    public enum Products
    {
        Sauce_Labs_Backpack,
        Sauce_Labs_Bike_Light,
        Sauce_Labs_Bolt_T_Shirt,
        Sauce_Labs_Fleece_Jacket,
        Sauce_Labs_Onesie,
        TestallTheThings_TShirtRed,
    }

    public static class ProductsExtentions
    {
        public static string Name(this Products product)
        {
            switch (product){
                case Products.Sauce_Labs_Backpack: return "Sauce Labs Backpack";
                case Products.Sauce_Labs_Bike_Light: return "Sauce Labs Bike Light";
                case Products.Sauce_Labs_Bolt_T_Shirt: return "Sauce Labs Bolt T-Shirt";
                case Products.Sauce_Labs_Fleece_Jacket: return "Sauce Labs Fleece Jacket";
                case Products.Sauce_Labs_Onesie: return "Sauce Labs Onesie";
                case Products.TestallTheThings_TShirtRed: return "Test.allTheThings() T-Shirt (Red)";
                default: return "";
            }
        }

        public static string PriceString(this Products product)
        {
            switch (product)
            {
                case Products.Sauce_Labs_Backpack: return "$29.99";
                case Products.Sauce_Labs_Bike_Light: return "$9.99";
                case Products.Sauce_Labs_Bolt_T_Shirt: return "$15.99";
                case Products.Sauce_Labs_Fleece_Jacket: return "$49.99";
                case Products.Sauce_Labs_Onesie: return "$7.99";
                case Products.TestallTheThings_TShirtRed: return "$15.99";
                default: return "";
            }
        }

        public static string Description(this Products product)
        {
            switch (product)
            {
                case Products.Sauce_Labs_Backpack: return "carry.allTheThings() with the sleek, streamlined Sly Pack that melds uncompromising style with unequaled laptop and tablet protection.";
                case Products.Sauce_Labs_Bike_Light: return "A red light isn't the desired state in testing but it sure helps when riding your bike at night. Water-resistant with 3 lighting modes, 1 AAA battery included.";
                case Products.Sauce_Labs_Bolt_T_Shirt: return "Get your testing superhero on with the Sauce Labs bolt T-shirt. From American Apparel, 100% ringspun combed cotton, heather gray with red bolt.";
                case Products.Sauce_Labs_Fleece_Jacket: return "It's not every day that you come across a midweight quarter-zip fleece jacket capable of handling everything from a relaxing day outdoors to a busy day at the office.";
                case Products.Sauce_Labs_Onesie: return "Rib snap infant onesie for the junior automation engineer in development. Reinforced 3-snap bottom closure, two-needle hemmed sleeved and bottom won't unravel.";
                case Products.TestallTheThings_TShirtRed: return "This classic Sauce Labs t-shirt is perfect to wear when cozying up to your keyboard to automate a few tests. Super-soft and comfy ringspun combed cotton.";
                default: return "";
            }
        }

        public static double Price(this Products product)
        {
            return double.Parse(product.PriceString().TrimStart('$'));
        }
    }
    

    //
    // Login Screen Tests
    //

    [TestFixture]
    public class SauceDemo_LoginTests
    {
        [Test]
        public static void SauceDemo_Login_NoUsernameOrPassword()
        {
            var loginscreen = new LoginScreen()
                                        .EnterUsername("")
                                        .EnterPassword("")
                                        .ClickLogin();

            Assert.IsTrue(loginscreen.GettErrorMessage().Contains("Username is required"), "The login error message is not displayed");

            loginscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_Login_NoUsername()
        {
            var loginscreen = new LoginScreen()
                                        .EnterUsername("")
                                        .EnterPassword(LoginUsers.valid_password)
                                        .ClickLogin();

            Assert.IsTrue(loginscreen.GettErrorMessage().Contains("Username is required"), "The username required error message is not displayed");

            loginscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_Login_WrongUsername()
        {
            var loginscreen = new LoginScreen()
                                        .EnterUsername(LoginUsers.invalid_username)
                                        .EnterPassword(LoginUsers.valid_password)
                                        .ClickLogin();

            Assert.IsTrue(loginscreen.GettErrorMessage().Contains("Username and password do not match any user in this service"), "The login error message is not displayed");

            loginscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_Login_NoPassword()
        {
            var loginscreen = new LoginScreen()
                                        .EnterUsername(LoginUsers.valid_username)
                                        .EnterPassword("")
                                        .ClickLogin();

            Assert.IsTrue(loginscreen.GettErrorMessage().Contains("Password is required"), "The password error message is not displayed");

            loginscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_Login_WrongPassword()
        {
            var loginscreen = (LoginScreen)new LoginScreen()
                                        .EnterUsername(LoginUsers.valid_username)
                                        .EnterPassword(LoginUsers.invalid_password)
                                        .ClickLogin();

            Assert.IsTrue(loginscreen.GettErrorMessage().Contains("Username and password do not match any user in this service"), "The login error message is not displayed");

            loginscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_Login_ValidCredentials()
        {
            var inventoryscreen = new LoginScreen()
                                        .EnterUsername(LoginUsers.valid_username)
                                        .EnterPassword(LoginUsers.valid_password)
                                        .ClickLogin_AndContinue();

            Assert.IsTrue(inventoryscreen.IsDisplayed(), "The inventory screen screen is not displayed");

            inventoryscreen.CloseWebpage();
        }
    }
    

    //
    // Checkout Tests
    //

    [TestFixture]
    public class SauceDemo_CheckoutTests
    {
        [Test]
        //Check that all the items are visible on the page with the correct information
        public static void SauceDemo_CheckAllItemsAreVisible()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin();
            var itemslist = mainscreen.GetListOfItemsInOrder();

            //Loop through each expected product, and check the product appears on webpage with the correct name, price and description
            foreach (Products product in Enum.GetValues(typeof(Products)))
            {
                Assert.AreEqual(itemslist.FirstOrDefault(x => x.itemName == product.Name()).itemName, product.Name());
                Assert.AreEqual(itemslist.FirstOrDefault(x => x.itemName == product.Name()).itemPrice, product.Price());
                Assert.AreEqual(itemslist.FirstOrDefault(x => x.itemName == product.Name()).itemDescription, product.Description());
            }

            mainscreen.CloseWebpage();
        }

        [Test]
        //Test not ordering any items
        public static void SauceDemo_OrderNoItems()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin();

            //Check the icon displays a 0 or no number 
            Assert.AreEqual(0, mainscreen.GetNumberOfItemsInBasketFromIcon());

            //navigate to the checkout page
            var checkoutscreen = mainscreen.ClickCheckoutIcon()
                                            .ClickCheckout()
                                            .FillDetailsAndClickCheckout("test", "test", "test");

            //Check the total price values are correct
            Assert.AreEqual("Item total: $0", checkoutscreen.GetItemsSubtotal());
            Assert.AreEqual("Tax: $0.00", checkoutscreen.GetItemsTax());
            Assert.AreEqual("Total: $0.00", checkoutscreen.GetItemsTotal());
            checkoutscreen.CloseWebpage();
        }
        
        [Test]
        //Test ordering 1 of the the items
        public static void SauceDemo_Order1Item()
        {
            var inventoryScreen = new LoginScreen().EnterValidCredentialsAndLogin();

            InventoryItem BikeLight = inventoryScreen.GetListOfItemsInOrder().FirstOrDefault(x => x.itemName == "Sauce Labs Bike Light");

            //check the item button displays 'Add to cart' before clicking on it
            Assert.AreEqual("ADD TO CART", BikeLight.addToCartButton.Text);

            //click on the item
            BikeLight.addToCartButton.Click();

            //Check the icon displays a 1
            Assert.AreEqual(1, inventoryScreen.GetNumberOfItemsInBasketFromIcon());

            //check the item button displays 'Remove' after clicking on it
            Assert.AreEqual("REMOVE", BikeLight.addToCartButton.Text);

            //navigate to the checkout screen
            var checkoutScreen = inventoryScreen.ClickCheckoutIcon()
                                                .ClickCheckout()
                                                .FillDetailsAndClickCheckout("test", "test", "test");

            //check totals are correct
            Assert.AreEqual("Item total: $9.99", checkoutScreen.GetItemsSubtotal());
            Assert.AreEqual("Tax: $0.80", checkoutScreen.GetItemsTax());
            Assert.AreEqual("Total: $10.79", checkoutScreen.GetItemsTotal());
            checkoutScreen.CloseWebpage();
        }

        [Test]
        //Test ordering a few items
        public static void SauceDemo_Order3Items()
        {
            var inventoryScreen = new LoginScreen().EnterValidCredentialsAndLogin();

            InventoryItem Item1 = inventoryScreen.GetListOfItemsInOrder().FirstOrDefault(x => x.itemName == "Sauce Labs Bike Light");
            InventoryItem Item2 = inventoryScreen.GetListOfItemsInOrder().FirstOrDefault(x => x.itemName == "Sauce Labs Backpack");
            InventoryItem Item3 = inventoryScreen.GetListOfItemsInOrder().FirstOrDefault(x => x.itemName == "Sauce Labs Onesie");


            Assert.AreEqual("ADD TO CART", Item1.addToCartButton.Text);
            Assert.AreEqual("ADD TO CART", Item2.addToCartButton.Text);
            Assert.AreEqual("ADD TO CART", Item3.addToCartButton.Text);

            Item1.addToCartButton.Click();
            Item2.addToCartButton.Click();
            Item3.addToCartButton.Click();
            
            Assert.AreEqual("REMOVE", Item1.addToCartButton.Text);
            Assert.AreEqual("REMOVE", Item2.addToCartButton.Text);
            Assert.AreEqual("REMOVE", Item3.addToCartButton.Text);

            Assert.AreEqual(3, inventoryScreen.GetNumberOfItemsInBasketFromIcon());

            var checkoutScreen = inventoryScreen.ClickCheckoutIcon()
                                                .ClickCheckout()
                                                .FillDetailsAndClickCheckout("test", "test", "test");

            Assert.AreEqual("Item total: $47.97", checkoutScreen.GetItemsSubtotal());
            Assert.AreEqual("Tax: $3.84", checkoutScreen.GetItemsTax());
            Assert.AreEqual("Total: $51.81", checkoutScreen.GetItemsTotal());
            checkoutScreen.CloseWebpage();
        }
        
        [Test]
        //Test ordering all the items
        public static void SauceDemo_OrderAllItems()
        {
            var inventoryScreen = new LoginScreen().EnterValidCredentialsAndLogin();

            foreach (var checkoutBtn in inventoryScreen.GetListOfItemsInOrder().Select(x => x.addToCartButton))
            {
                checkoutBtn.Click();
            }

            Assert.AreEqual(6, inventoryScreen.GetNumberOfItemsInBasketFromIcon());

            var checkoutScreen = inventoryScreen.ClickCheckoutIcon()
                                       .ClickCheckout()
                                       .FillDetailsAndClickCheckout("test", "test", "test");

            Assert.AreEqual("Item total: $129.94", checkoutScreen.GetItemsSubtotal());
            Assert.AreEqual("Tax: $10.40", checkoutScreen.GetItemsTax());
            Assert.AreEqual("Total: $140.34", checkoutScreen.GetItemsTotal());
            checkoutScreen.CloseWebpage();
        }

        [Test]
        //Test that after adding an item, then deleting it, the totals go
        //back to 0 and the button displays 'add to cart'
        public static void SauceDemo_DeleteItem()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin();

            InventoryItem Item1 = mainscreen.GetListOfItemsInOrder()[0];

            Assert.AreEqual("ADD TO CART", Item1.addToCartButton.Text);
            Item1.addToCartButton.Click();
            Assert.AreEqual(1, mainscreen.GetNumberOfItemsInBasketFromIcon());
            Assert.AreEqual("REMOVE", Item1.addToCartButton.Text);

            mainscreen.GetListOfItemsInOrder()[0].addToCartButton.Click();
            Assert.AreEqual(0, mainscreen.GetNumberOfItemsInBasketFromIcon());
            Assert.AreEqual("ADD TO CART", Item1.addToCartButton.Text);

            var checkoutscreen = mainscreen.ClickCheckoutIcon()
                                            .ClickCheckout()
                                            .FillDetailsAndClickCheckout("test", "test", "test");

            Assert.AreEqual("Item total: $0", checkoutscreen.GetItemsSubtotal());
            Assert.AreEqual("Tax: $0.00", checkoutscreen.GetItemsTax());
            Assert.AreEqual("Total: $0.00", checkoutscreen.GetItemsTotal());
            checkoutscreen.CloseWebpage();
        }
    }

    
    //
    // Sort Order Tests
    //

    [TestFixture]
    public class SauceDemo_SortOrderTests
    {
        [Test]
        public static void SauceDemo_CheckOrder_AZ()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin()
                                              .SelectOrder(ItemSortOrder.az);

            CollectionAssert.AreEqual(CreateProductList().Select(o => o.Name()).OrderBy(x => x), mainscreen.GetListOfItemsInOrder().Select(x => x.itemName));
            mainscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_CheckOrder_ZA()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin()
                                              .SelectOrder(ItemSortOrder.za);

            CollectionAssert.AreEqual(CreateProductList().Select(o => o.Name()).OrderByDescending(x => x), mainscreen.GetListOfItemsInOrder().Select(x => x.itemName));
            mainscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_CheckOrder_HiLow()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin()
                                              .SelectOrder(ItemSortOrder.hilo);

            var expectedProductOrder = CreateProductList().Select(o => o.Price()).OrderByDescending(x => x);
            var actualProductOrder = mainscreen.GetListOfItemsInOrder().Select(x => x.itemPrice);

            CollectionAssert.AreEqual(expectedProductOrder, actualProductOrder);

            mainscreen.CloseWebpage();
        }

        [Test]
        public static void SauceDemo_CheckOrder_LowHi()
        {
            var mainscreen = new LoginScreen().EnterValidCredentialsAndLogin()
                                              .SelectOrder(ItemSortOrder.lohi);

            var expectedProductOrder = CreateProductList().Select(o => o.Price()).OrderBy(x => x);
            var actualProductOrder = mainscreen.GetListOfItemsInOrder().Select(x => x.itemPrice);

            CollectionAssert.AreEqual(expectedProductOrder, actualProductOrder);

            mainscreen.CloseWebpage();
        }

        //returns a list of products with expected names, prices and descriptions
        private static List<Products> CreateProductList()
        {
            var plist = Enum.GetNames(typeof(Products));
            return plist.Select(o => (Products)Enum.Parse(typeof(Products), o)).ToList();
        }
    }
}
