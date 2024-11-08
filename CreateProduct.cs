using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Demo01_UploadFile
{
	[TestFixture("Safari", "17.0", "MacOS Sonoma", "Safari17")]
	[TestFixture("Firefox", "126.0", "Windows 10", "Firefox126")]
	[TestFixture("MicrosoftEdge", "125.0", "Windows 10", "Edge126")]
	[TestFixture("Chrome", "127.0", "Windows 11", "Chrome127")]
	public class CreateProduct : BaseTest_Lambda
	{
		// Test github

		// Do chạy song song nên sẽ gặp lỗi trùng tên sản phẩm, run 2 cái 1 lần là Pass

		// Kế thừa lớp cha cho code gọn hơn
		public CreateProduct(string browser, string version, string os, string name) : base(browser, version, os, name)
		{
			
		}

		// Lấy format đường dẫn tùy vào OS
		private string GetFilePathBasedOnOS(string os, string fileName)
		{
			string filePath;

			if (os.Contains("MacOS"))
			{
				// macOS
				filePath = $"/Users/ltuser/Downloads/{fileName}";
			}
			else if (os.Contains("Windows"))
			{
				// Windows
				filePath = $@"C:\Users\ltuser\Downloads\{fileName}";
			}
			else
			{
				throw new PlatformNotSupportedException("Unsupported OS: " + os);
			}

			return filePath;
		}


		[Test]
		public void CreateNewProduct()
		{
			_driver.Navigate().GoToUrl("http://localhost:22220/admin");

			// Đợi tới khi nút Add Product hiển thị
			IWebElement addProductBtn = _driver.FindElement(By.XPath("/html/body/section/div/div/a"));
			wait.Until(ap => addProductBtn.Displayed);

			addProductBtn.Click();
			_driver.FindElement(By.Id("Name")).Click();
			_driver.FindElement(By.Id("Name")).SendKeys("LambdaTest - ProductDemo09" + DateTime.Now);
			_driver.FindElement(By.Id("Price")).Click();
			_driver.FindElement(By.Id("Price")).SendKeys("40.99");
			_driver.FindElement(By.Id("Description")).Click();
			_driver.FindElement(By.Id("Description")).SendKeys("PM04. OS: " + _os + ", Browser: " + _browser);
			_driver.FindElement(By.Id("CategoryId")).Click();
			{
				var dropdown = _driver.FindElement(By.Id("CategoryId"));
				dropdown.FindElement(By.XPath("//option[. = 'Phone']")).Click();
			}
			_driver.FindElement(By.Id("BrandId")).Click();
			{
				var dropdown = _driver.FindElement(By.Id("BrandId"));
				dropdown.FindElement(By.XPath("//option[. = 'Acer']")).Click();
			}

			// Senkey với input type = file
			try
			{
				_driver.FindElement(By.Id("ImageUpload")).SendKeys(GetFilePathBasedOnOS(_os, "macbook-air-m1-2020-gray-600x600.jpg"));
			}
			catch (Exception e)
			{
				Console.WriteLine("Lỗi upload file: " + e.Message);
			}

			_driver.FindElement(By.XPath("/html/body/section/div/div/div/form/div[7]/button")).Click();
			
			try
			{
				IWebElement successMes = _driver.FindElement(By.XPath("/html/body/section/div/div/div"));
				wait.Until(s => successMes.Displayed);
				_driver.Manage().Timeouts().ImplicitWait = _driver.Manage().Timeouts().PageLoad;
				string mes = successMes.Text;
				
				Assert.That(mes.Contains("Thêm sản phẩm thành công"), Is.True);
				Console.WriteLine("Bắt được thông báo thành công");
			}
			catch (StaleElementReferenceException)
			{
				// Tìm lại phần tử nếu nó bị stale
				Console.WriteLine("Lỗi với Safari ở lần tìm đầu tiên: ");
				IWebElement successMes = _driver.FindElement(By.XPath("/html/body/section/div/div/div"));
				_driver.Manage().Timeouts().ImplicitWait = _driver.Manage().Timeouts().PageLoad;
				string mes = successMes.Text;
				
				Assert.That(mes.Contains("Thêm sản phẩm thành công"), Is.True);
				Console.WriteLine("Bắt được thông báo thành công");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Không thêm được sản phẩm. Lỗi: " + ex.Message);
			}
		}

		





	}
}
