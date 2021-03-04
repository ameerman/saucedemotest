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
    public class CheckoutScreen : SauceDemoScreen
    {
        private IWebElement summarySubtotal => driver.FindElementByClassName("summary_subtotal_label");
        private IWebElement summaryTax => driver.FindElementByClassName("summary_tax_label");
        private IWebElement summaryTotal => driver.FindElementByClassName("summary_total_label");

        public CheckoutScreen(ChromeDriver drvr, WebDriverWait waitcntrl)
                               : base(drvr, waitcntrl)
        {
        }

        public string GetItemsSubtotal()
        {
            return summarySubtotal.Text;
        }

        public string GetItemsTax()
        {
            return summaryTax.Text;
        }

        public string GetItemsTotal()
        {
            return summaryTotal.Text;
        }

    }
}
