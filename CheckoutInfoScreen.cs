using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo
{
    public class CheckoutInfoScreen : SauceDemoScreen
    {
        private IWebElement firstNameTextbox => driver.FindElementById("first-name");
        private IWebElement lastNameTextbox => driver.FindElementById("last-name");
        private IWebElement postcodeTextbox => driver.FindElementById("postal-code");
        private IWebElement checkoutButton => driver.FindElementByClassName("btn_primary");

        public CheckoutInfoScreen(ChromeDriver drvr, WebDriverWait waitcntrl)
                               : base(drvr, waitcntrl)
        {
        }

        public CheckoutScreen FillDetailsAndClickCheckout(string firstname, string lastname, string postcode)
        {
            firstNameTextbox.SendKeys(firstname);
            lastNameTextbox.SendKeys(lastname);
            postcodeTextbox.SendKeys(postcode);
            checkoutButton.Click();

            return new CheckoutScreen(driver, waitcontroller);
        }

    }
}
