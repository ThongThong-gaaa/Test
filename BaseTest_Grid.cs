using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Demo01_UploadFile
{
	public abstract class BaseTest_Grid
	{
		protected RemoteWebDriver _driver;
		protected IJavaScriptExecutor js;
		protected WebDriverWait wait;

		public BaseTest_Grid() { 
			
		}

		[SetUp]
		public void SetUp()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			string localIP = GetLocalIPAddress();
			_driver = new RemoteWebDriver(new Uri($"http://{localIP}:4444"), new ChromeOptions().ToCapabilities());
			js = (IJavaScriptExecutor)_driver;
			_driver.Manage().Window.Maximize();
			wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
		}

		[TearDown]
		protected void TearDown()
		{
			_driver.Quit();
			_driver.Dispose();
		}

		// Hàm này để lấy IP hiện tại của máy
		// Do Grid dùng IP này để tạo hub
		// Đổi IP -> Grid đổi theo => Cần lấy IP chủ động trong code
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
	}
}
