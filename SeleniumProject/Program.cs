using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                try
                {
                    // Navigate to Amazon
                    driver.Navigate().GoToUrl("https://www.amazon.com/");
                    driver.Manage().Window.Maximize();
                    
                    // Wait for the page to load (using WebDriverWait is better for real cases)
                    System.Threading.Thread.Sleep(10000);

                    // Find the search box and input a search term
                    IWebElement searchBox = driver.FindElement(By.Id("twotabsearchtextbox"));
                    searchBox.SendKeys("tp-link n450 wifi router");
                    IWebElement submitButton = driver.FindElement(By.Id("nav-search-submit-button"));
                    submitButton.Click();
                    
                    // Wait for the search results to load and select the first item
                    IWebElement selectItem = driver.FindElement(By.XPath("(//span[@class='a-size-medium a-color-base a-text-normal'])[1]"));
                    selectItem.Click();

                    // Wait for the item page to load and add to cart
                    IWebElement addToCart = driver.FindElement(By.Id("add-to-cart-button"));
                    addToCart.Click();

                    // Wait for the cart page to update and navigate to the cart
                    IWebElement cartIcon = driver.FindElement(By.Id("nav-cart"));
                    cartIcon.Click();
                    
                    // Wait for the cart page to load
                    System.Threading.Thread.Sleep(10000);
                    
                    // Extract product name from cart
                    IWebElement nameElement = driver.FindElement(By.XPath("(//span[@class='a-truncate-cut'])[1]"));
                    string actualName = nameElement.Text;

                    // Extract price from cart
                    IWebElement priceElement = driver.FindElement(By.XPath("(//span[@class='a-size-medium a-color-base sc-price sc-white-space-nowrap'])[2]"));
                    string actualPrice = priceElement.Text;

                    // Define the expected values
                    string expectedName = "TP-Link Tri-Band BE9300 WiFi 7 Router Archer BE550 | 6-Stream 9.2Gbps | Full 2.5G Ports | 6 Internal Antennas | Covers Up to 2,000 Sq. Ft. | Add Easy-Mesh Device for Extended C…";
                    string expectedPrice = "$219.99";

                    // Verify the extracted product name and price
                    if (actualName.Equals(expectedName, StringComparison.OrdinalIgnoreCase) && actualPrice.Equals(expectedPrice, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("The product name and price in the cart are correct.");
                    }
                    else
                    {
                        Console.WriteLine($"The product name or price in the cart is incorrect. Expected Name: {expectedName}, Actual Name: {actualName}, Expected Price: {expectedPrice}, Actual Price: {actualPrice}");
                    }
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine($"Element not found: {e.Message}");
                }
                catch (WebDriverTimeoutException e)
                {
                    Console.WriteLine($"Timeout: {e.Message}");
                }
                finally
                {
                    // Close the browser
                    driver.Quit();
                }
            }
        }
    }
}
