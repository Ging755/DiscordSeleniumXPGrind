using DiscordSeleniumXP;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

Console.OutputEncoding = System.Text.Encoding.Unicode;
WebDriver _driver = new ChromeDriver();
_driver.Navigate().GoToUrl("https://discord.com/login");

//login through mobile to avoid captcha

Console.WriteLine("Login through mobile app scan QR to avoid captcha");
Console.ReadLine();

Service _service = new Service(_driver);
var choosenServer = _service.ChooseServer();
await _service.StartGrind(choosenServer.ServerName, "「💭」degeneral");

Console.ReadLine();

_service.StopGrind();

_driver.Close();