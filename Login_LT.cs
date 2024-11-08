using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_UploadFile
{
	[TestFixture("Safari", "17.0", "MacOS Sonoma", "Safari17")]
	[TestFixture("Chrome", "127.0", "Windows 11", "Chrome127")]
	public class Login_LT : BaseTest_Lambda
	{
		public Login_LT(string browser, string version, string os, string name)
			: base(browser, version, os, name) { }


		[TestCase("duy01", "a123456", "http://localhost:22220/")] // Trường hợp đăng nhập thành công
		[TestCase("wrongemail@gmail.com", "wrongpassword", "http://localhost:22220/account/login")] // Trường hợp đăng nhập không thành công
		[TestCase("", "", "http://localhost:22220/account/login")] // Trường hợp bỏ trống các field
		[TestCase("admin' OR '1'='1", "abc1234", "http://localhost:22220/account/login")] // SQL Injection
		[TestCase("admin'; --", "abc1234", "http://localhost:22220/account/login")]
		public void Dang_Nhap(string userName, string password, string expectedUrl)
		{
			_driver.Navigate().GoToUrl("http://localhost:22220/account/login");

			// Xác định vị trí trường tên người dùng và nhập tên người dùng
			var userNameField = _driver.FindElement(By.Name("UserName"));
			userNameField.Clear(); // Xóa giá trị trước đó (nếu có)
			userNameField.SendKeys(userName);

			// Xác định vị trí trường mật khẩu và nhập mật khẩu
			var passwordField = _driver.FindElement(By.Name("Password"));
			passwordField.Clear(); // Xóa giá trị trước đó (nếu có)
			passwordField.SendKeys(password);

			// Xác định vị trí nút đăng nhập và nhấp vào
			var loginButton = _driver.FindElement(By.XPath("//button[text()='Login']"));
			loginButton.Click();

			if (expectedUrl == "http://localhost:22220/")
			{
				wait.Until(driver => driver.Url == expectedUrl);
				Assert.That(_driver.Url, Is.EqualTo(expectedUrl), "URL không khớp với trang mong đợi sau khi đăng nhập.");
			}
			else
			{
				// Kiểm tra sự xuất hiện của thông báo lỗi cho từng trường
				if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
				{
					// Kiểm tra các thông báo lỗi riêng lẻ
					var userNameError = wait.Until(driver => driver.FindElement(By.CssSelector("input[name='UserName'] + .text-danger.field-validation-error")));
					var passwordError = wait.Until(driver => driver.FindElement(By.CssSelector("input[name='Password'] + .text-danger.field-validation-error")));

					Assert.IsTrue(userNameError.Displayed, "Không tìm thấy thông báo lỗi cho trường UserName.");
					Assert.IsTrue(passwordError.Displayed, "Không tìm thấy thông báo lỗi cho trường Password.");
				}
				else
				{
					// Kiểm tra sự xuất hiện của thông báo lỗi chung
					var errorElement = wait.Until(driver => driver.FindElement(By.CssSelector(".text-danger.validation-summary-errors")));
					Assert.IsTrue(errorElement.Displayed, "Không tìm thấy thông báo lỗi sau khi đăng nhập không thành công.");
				}
				
			}
		}
	}
}
