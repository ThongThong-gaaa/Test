using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Demo01_UploadFile
{
	
	public class Login_Grid : BaseTest_Grid
	{
		public Login_Grid () : base() { }

		[TestCase("duy01", "a123456")]
		[TestCase("duy01", "")]
		[TestCase("", "a123456")]
		[TestCase("admin' OR '1'='1", "abc1234")] // SQL Injection
		[TestCase("admin'; --", "abc1234")]
		public void LoginTest(string username, string password)
		{
			Console.WriteLine("Đang đăng nhập ...");
			_driver.Navigate().GoToUrl("http://localhost:22220/Account/Login");

			// Đợi page load
			IWebElement loginForm = _driver.FindElement(By.XPath("/html/body/section[2]/div/div/section/div/div/div/div"));
			wait.Until(lf => loginForm.Displayed);

			_driver.FindElement(By.Id("UserName")).SendKeys(username);
			_driver.FindElement(By.Id("Password")).SendKeys(password);
			_driver.FindElement(By.CssSelector("form > .btn:nth-child(4)")).Click();

			By logoutlink = By.XPath("//*[@id=\"header\"]/div[2]/div/div/div[2]/div/ul/li[5]/a");
			IWebElement logoutLink = _driver.FindElement(logoutlink);
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(logoutlink));
			Assert.IsTrue(logoutLink.Text.Contains("duy01, Logout"), "Đăng nhập thất bại");
			Console.WriteLine("Đăng nhập thành công. Logout link: " + logoutLink.Text);
		}

		


	}
}
