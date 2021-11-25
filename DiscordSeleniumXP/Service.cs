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
        private Server _currentServer;

        public Service(WebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(_driver);
            _servers = new List<Server>();
            PrepareServers();
        }
        public async Task StartGrind(string serverName, string channelName)
        {
            _currentServer = _servers.Find(server => server.ServerName == serverName);
            if (_currentServer != null)
            {
                _currentServer.ChannelToGrind = channelName;
                _currentServer.PrepareToGrind();
                await _currentServer.Grind();
            }
        }

        public async void StopGrind()
        {
            await _currentServer.StopGrind();
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
            int id = 0;

            //Open all Server groups and get Servers and display server names
            foreach (var serverGroup in serverGroups)
            {

                _actions.MoveToElement(serverGroup.FindElement(By.XPath(".//div//div[2]"))).Perform();
                Thread.Sleep(250);
                serverGroup.FindElement(By.XPath(".//div//div[2]")).Click();
                _actions.Reset();

                try
                {
                    Thread.Sleep(250);
                    var temp = serverGroup.FindElement(By.XPath(".//div//div[@class='closedFolderIconWrapper-1I9YfS']"));

                    _actions.MoveToElement(serverGroup.FindElement(By.XPath(".//div//div[2]"))).Perform();
                    Thread.Sleep(250);
                    serverGroup.FindElement(By.XPath(".//div//div[2]")).Click();
                    _actions.Reset();
                }
                catch
                {
                    //empty catch to just keep going if closed folder is not there anymore
                }


                foreach (var serverElement in serverGroup.FindElements(By.XPath(".//ul//div[@class='listItem-2Ig28I']")))
                {
                    id++;
                    servers.Add(new Server(_driver, serverElement, id));
                }
            }

            return servers;
        }

        //choose server
        public Server ChooseServer()
        {
            foreach (var server in _servers)
            {
                Console.WriteLine(server.ServerName + " " + server.Id);
            }

            Console.WriteLine("Choose server with Id");
            var id = int.Parse(Console.ReadLine());
            return _servers.FirstOrDefault(server => server.Id == id);
        }
    }
}
