using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo
{
    public class CartScreen : SauceDemoScreen
    {
        private const string checkoutBtnCLASS = "checkout_button"; 

        public CartScreen(ChromeDriver drvr, WebDriverWait waitcntrl)
                               : base(drvr, waitcntrl)
        {
        }

        public CheckoutInfoScreen ClickCheckout()
        {
            driver.FindElementByClassName(checkoutBtnCLASS).Click();
            return new CheckoutInfoScreen(driver, waitcontroller);
        }

    }
}
