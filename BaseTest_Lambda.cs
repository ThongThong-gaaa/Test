using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace Demo01_UploadFile
{
	public abstract class BaseTest_Lambda
	{
		protected RemoteWebDriver _driver;
		protected IJavaScriptExecutor js;
		protected WebDriverWait wait;

		protected string _browser;
		protected string _version;
		protected string _os;
		protected string _name;
		protected string _ltUsername = "nguyenduy110803";
		protected string _ltAccessKey = "o9Qcp1LeYab62nB8yTh7DTcVJwf0QSHRIYNhJ1VPrbWjTpkSTv";

		public BaseTest_Lambda (string browser, string version, string os, string name)
		{
			_browser = browser;
			_version = version;
			_os = os;
			_name = name;
			Console.OutputEncoding = System.Text.Encoding.UTF8;
		}

		[SetUp]
		public void Setup()
		{
			var capability = GetBrowserOptions(_browser);

			#region Capabilities
			capability.BrowserVersion = _version;
			Dictionary<string, object> ltOptions = new Dictionary<string, object>();
			ltOptions.Add("username", _ltUsername);
			ltOptions.Add("accessKey", _ltAccessKey);
			ltOptions.Add("platformName", _os);
			ltOptions.Add("project", "BTL Parallel Test");
			ltOptions.Add("w3c", true);
			ltOptions.Add("plugin", "c#-nunit");

			// Tunnel để chạy localhost
			ltOptions.Add("tunnel", true);

			// Thêm file upload nếu cần
			String[] ltFile = new string[] { "iphone-12-white-600x600.jpg", "macbook-air-m1-2020-gray-600x600.jpg", "realmiphone.jfif" };
			ltOptions.Add("lambda:userFiles", ltFile);

			capability.AddAdditionalOption("LT:Options", ltOptions);
			#endregion

			var url = new Uri("https://" + _ltUsername + ":" + _ltAccessKey + "@hub.lambdatest.com/wd/hub");
			_driver = new RemoteWebDriver(url, capability);

			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
			js = (IJavaScriptExecutor)_driver;
			_driver.Manage().Window.Maximize();

			Console.WriteLine("My test is starting...");
		}

		[TearDown]
		protected void TearDown()
		{
			Console.WriteLine("My test ended");
			_driver.Quit();
			_driver.Dispose();
		}

		private dynamic GetBrowserOptions(string browserName)
		{
			if (browserName == "Chrome")
				return new ChromeOptions();

			if (browserName == "Firefox")
				return new FirefoxOptions();

			if (browserName == "MicrosoftEdge")
				return new EdgeOptions();

			if (browserName == "Safari")
				return new SafariOptions();

			return new ChromeOptions();
		}
	}
}
