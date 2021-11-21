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
_service.StartGrind("𝒜𝓃𝒾𝓂𝑒 𝐵𝒶𝓁𝓀𝒶𝓃", "「💭」degeneral");

Console.ReadLine();

_driver.Close();