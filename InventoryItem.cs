using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo
{
    public class InventoryItem
    {
        private IWebElement itemContainer;

        public InventoryItem()
        {
        }

        public InventoryItem(IWebElement conteinerWebElement)
        {
            itemContainer = conteinerWebElement;
        }

        public IWebElement addToCartButton
        {
            get { return itemContainer.FindElement(By.ClassName("btn_inventory")); }
        }

        public string itemName
        {
            get { return itemContainer.FindElement(By.ClassName("inventory_item_name")).Text; }
        }

        public string itemPriceString
        {
            get { return itemContainer.FindElement(By.ClassName("inventory_item_price")).Text; }
        }

        public double itemPrice
        {
            get { return double.Parse(itemPriceString.Trim('$')); }
        }

        public string itemDescription
        {
            get { return itemContainer.FindElement(By.ClassName("inventory_item_desc")).Text; }
        }

    }
}
