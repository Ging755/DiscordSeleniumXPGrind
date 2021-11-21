using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSeleniumXP
{
    class Server
    {
        private WebDriver _driver;
        private Actions _actions;
        private IWebElement _serverElement;
        public Server(WebDriver driver, IWebElement serverElement)
        {
            _driver = driver;
            _actions = new Actions(_driver);
            _serverElement = serverElement;
        }

        public string? ServerName => _serverElement.FindElement(By.XPath(".//div[2]//div")).GetAttribute("data-dnd-name");
        public string? ChannelToGrind { get; set; }

        public void PrepareToGrind()
        {
            _actions.MoveToElement(_driver.FindElement(By.XPath($"//div[@data-dnd-name='{ServerName}']")));
            _actions.Perform();
            _actions.Reset();

            _driver.FindElement(By.XPath($"//div[@data-dnd-name='{ServerName}']")).Click();

            _actions.MoveToElement(_driver.FindElement(By.XPath($"//div[@data-dnd-name='{ChannelToGrind}']")));
            _actions.Perform();
            _actions.Reset();

            _driver.FindElement(By.XPath($"//div[@data-dnd-name='{ChannelToGrind}']")).Click();
        }
    
        public void Grind()
        {
            _driver.FindElement(By.XPath("//div[@role='textbox']")).SendKeys("Ovo je automatska poruka koja šalje bot");
            _driver.FindElement(By.XPath("//div[@role='textbox']")).SendKeys(Keys.Enter);
            Thread.Sleep(100);

            _driver.FindElement(By.XPath("//div[@role='textbox']")).Click();
            _driver.FindElement(By.XPath("//div[@role='textbox']")).SendKeys(Keys.ArrowUp);

            _driver.FindElement(By.XPath("//div[@data-slate-editor='true']")).Click();
            _driver.FindElement(By.XPath("//div[@data-slate-editor='true']")).SendKeys(Keys.Control + "a");

            _driver.FindElement(By.XPath("//div[@data-slate-editor='true']")).SendKeys(Keys.Delete);
            _driver.FindElement(By.XPath("//div[@data-slate-editor='true']")).SendKeys(Keys.Enter);
            _actions.SendKeys(Keys.Enter);
            _actions.Perform();
            _actions.Reset();
        }
    }
}
