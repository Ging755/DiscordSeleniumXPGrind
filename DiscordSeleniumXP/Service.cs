using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSeleniumXP
{
    class Service
    {
        private WebDriver _driver;
        private Actions _actions;
        private List<Server> _servers;

        public Service(WebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(_driver);
            _servers = new List<Server>();
            PrepareServers();
        }
        public void StartGrind(string serverName, string channelName)
        {
            var server = _servers.Find(server => server.ServerName == serverName);
            if(server != null)
            {
                server.ChannelToGrind = channelName;
                server.PrepareToGrind();
                server.Grind();
            }
        }

        private void PrepareServers()
        {
            //Get server groups
            var serverGroups = _driver.FindElements(By.ClassName("wrapper-shyHJt"));
            RemoveNotifications(serverGroups.ToList());
            _servers.AddRange(GetAllServers(serverGroups.ToList()));
        }

        private void RemoveNotifications(List<IWebElement> serverGroups)
        {
            //Remove Notifications first
            var serverGroupsWithNotification = serverGroups.Where(element =>
            {
                try
                {
                    element.FindElement(By.XPath(".//div[@class='lowerBadge-29hYVK']"));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            );

            foreach (var serverGroupWithNotification in serverGroupsWithNotification)
            {
                _actions.MoveToElement(serverGroupWithNotification).Perform();
                _actions.Reset();

                _actions.ContextClick(serverGroupWithNotification);
                _actions.Perform();
                _actions.Reset();

                _driver.FindElement(By.XPath("//*[contains(text(),'Mark Folder As Read')]")).Click();
            }
        }

        private List<Server> GetAllServers(List<IWebElement> serverGroups)
        {
            List<Server> servers = new List<Server>();

            //Open all Server groups and get Servers and display server names
            foreach (var serverGroup in serverGroups)
            {
                _actions.MoveToElement(serverGroup.FindElement(By.XPath(".//div//div[2]"))).Perform();
                serverGroup.FindElement(By.XPath(".//div//div[2]")).Click();
                _actions.Reset();

                foreach(var serverElement in serverGroup.FindElements(By.XPath(".//ul//div[@class='listItem-2Ig28I']")))
                {
                    servers.Add(new Server(_driver, serverElement));
                }
            }

            return servers;
        }
    }
}
