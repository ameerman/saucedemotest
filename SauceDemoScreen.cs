using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo
{
    public abstract class SauceDemoScreen
    {
        public ChromeDriver driver;
        public WebDriverWait waitcontroller;

        public SauceDemoScreen()
        {
        }

        public SauceDemoScreen(ChromeDriver drvr, WebDriverWait waitcntrl)
        {
            driver = drvr;
            waitcntrl = waitcontroller;
        }

        public void CloseWebpage()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
