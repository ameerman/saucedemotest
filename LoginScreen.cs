using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SauceDemo
{
    public class LoginScreen : SauceDemoScreen
    {
        const string txtUsernameID = "user-name";
        const string txtPasswordID = "password";
        const string btnLoginID = "login-button";
        const string txtErrorCSS = "h3";

        private IWebElement txtUsername => driver.FindElementById(txtUsernameID);
        private IWebElement txtPassword => driver.FindElementById(txtPasswordID);
        private IWebElement btnLogin => driver.FindElementById(btnLoginID);
        private IWebElement txtError => driver.FindElementByCssSelector(txtErrorCSS);

        public LoginScreen()
        {
            var chrdriverpath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\";
            this.driver = new ChromeDriver(chrdriverpath);
            this.waitcontroller = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            
        }

        public LoginScreen EnterUsername(string username)
        {
            txtUsername.Click();
            txtUsername.SendKeys(username);
            return this;
        }

        public LoginScreen EnterPassword(string password)
        {
            txtPassword.Click();
            txtPassword.SendKeys(password);
            return this;
        }

        public LoginScreen ClickLogin()
        {
            btnLogin.Click();
            return this;
        }

        public InventoryScreen ClickLogin_AndContinue()
        {
            btnLogin.Click();
            return new InventoryScreen(driver, waitcontroller);
        }

        public string GettErrorMessage()
        {
            return txtError.Text;
        }

        public InventoryScreen EnterValidCredentialsAndLogin()
        {
            return EnterUsername(LoginUsers.valid_username)
                    .EnterPassword(LoginUsers.valid_password)
                    .ClickLogin_AndContinue();
        }
    }
}
