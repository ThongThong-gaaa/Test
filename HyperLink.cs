using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Demo01_UploadFile
{
	// Grid
	[TestFixture("Chrome")]
	[TestFixture("Firefox")]
	[TestFixture("MicrosoftEdge")]
	public class HyperLink
	{
		private RemoteWebDriver _driver;
		private WebDriverWait wait;
		private string _browser;

		public HyperLink(string browser)
		{
			_browser = browser;
			Console.OutputEncoding = System.Text.Encoding.UTF8;
		}

		// Test này để one time được vì chỉ truy cập link
		[SetUp]
		public void SetUp()
		{
			var capability = GetBrowserOptions(_browser);

			Console.OutputEncoding = System.Text.Encoding.UTF8;
			string localIP = GetLocalIPAddress();
			_driver = new RemoteWebDriver(new Uri($"http://{localIP}:4444"), capability.ToCapabilities());
			_driver.Manage().Window.Maximize();
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));

			Console.WriteLine("My test is starting...");
			_driver.Navigate().GoToUrl("http://localhost:22220");
			_driver.Manage().Timeouts().ImplicitWait = _driver.Manage().Timeouts().PageLoad;
		}

		[TearDown]
		protected void TearDown()
		{
			Console.WriteLine("My test is ending...");
			//Thread.Sleep(TimeSpan.FromSeconds(10));
			_driver.Quit();
			_driver.Dispose();
		}


		// Các browser mà grid hỗ trợ
		private dynamic GetBrowserOptions(string browserName)
		{
			if (browserName == "Chrome")
				return new ChromeOptions();

			if (browserName == "Firefox")
				return new FirefoxOptions();

			if (browserName == "MicrosoftEdge")
				return new EdgeOptions();

			return new ChromeOptions();
		}

		static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("Không tìm thấy địa chỉ IP cục bộ!");
		}

		[Test]
		public void FacebookLinkClick()
		{
			IWebElement fb = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div/div[2]/div/ul/li[1]/a/i"));
			fb.Click();
			Assert.IsTrue(_driver.Url.Contains("facebook.com"), "Không vào được Facebook");
			Console.WriteLine("Đã vào được Facebook");
		}

		[Test]
		public void XLinkClick()
		{
			IWebElement x = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div/div[2]/div/ul/li[2]/a/i"));
			x.Click();
			Assert.IsTrue(_driver.Url.Contains("x.com"), "Không vào được X");
			Console.WriteLine("Đã vào được X");
		}

		[Test]
		public void LinkedInLinkClick()
		{
			IWebElement li = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div/div[2]/div/ul/li[3]/a/i"));
			li.Click();
			Assert.IsTrue(_driver.Url.Contains("linkedin.com"), "Không vào được LinkedIn");
			Console.WriteLine("Đã vào được LinkedIn");
		}

		[Test]
		public void DribbbleLinkClick()
		{
			IWebElement dr = _driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div/div[2]/div/ul/li[4]/a/i"));
			dr.Click();
			Assert.IsTrue(_driver.Url.Contains("dribbble.com"), "Không vào được Dribbble");
			Console.WriteLine("Đã vào được Dribbble");
		}



		// [Test]
		public void Testabc()
		{
			Console.WriteLine(GetLocalIPAddress());
			Console.WriteLine(GetBrowserOptions(_browser));
		}
	}
}
