using DiscordSeleniumXP;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

Console.OutputEncoding = System.Text.Encoding.Unicode;
WebDriver _driver = new ChromeDriver();
_driver.Navigate().GoToUrl("https://discord.com/login");

//login through mobile to avoid captcha

//Another Test stuff
Console.WriteLine("TEST TEST");
//stuff
Console.WriteLine("TEST TEST");
Console.WriteLine("TEST TEST");
Console.WriteLine("TEST TEST");
//COOL
Console.WriteLine("TEST TEST");
//extra stuff
Console.WriteLine("TEST TEST");




//RANDOM TEXT

//Some more stuff
//Alot of stuff



Console.WriteLine("TEST TEST");
// Some more stuff
Console.WriteLine("TEST TEST");
Console.WriteLine("TEST TEST");
//extra kool
Console.WriteLine("TEST TEST");
Console.WriteLine("TEST TEST");
<<<<<<< HEAD

//Few more stuff
=======
//this is a cool comment
>>>>>>> 333b26c (cool comment)
Console.WriteLine("TEST TEST");

Console.WriteLine("Login through mobile app scan QR to avoid captcha");
Console.ReadLine();

Service _service = new Service(_driver);
var choosenServer = _service.ChooseServer();
await _service.StartGrind(choosenServer.ServerName, "「💭」degeneral");

Console.ReadLine();

_service.StopGrind();

_driver.Close();