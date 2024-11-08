using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace Demo01_UploadFile
{
	internal class MyTestThings
	{
		// Test những thứ khác
		private string _projectFolderPath;
		private string _imageFolderPath;

		private IWebDriver _driver;

		[SetUp]
		public void SetUp()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			ChromeOptions capabilities = new ChromeOptions();
			_driver = new RemoteWebDriver(new Uri("http://192.168.1.6:4444"), capabilities.ToCapabilities());
			_driver.Manage().Window.Maximize();
		}

		[TearDown]
		protected void TearDown()
		{
			_driver.Quit();
			_driver.Dispose();
			
		}

		public MyTestThings()
		{
			_projectFolderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
			_imageFolderPath = Path.Combine(_projectFolderPath, "Images");
		}

		[Test]
		public void TestSomethingHere()
		{
			Console.WriteLine("Đường dẫn tới các thư mục có thể lấy ra từ Property: ");
			Console.WriteLine("- " + AppDomain.CurrentDomain.BaseDirectory);
			Console.WriteLine("- " + Directory.GetCurrentDirectory());
			string projectDirectory_v1 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
			Console.WriteLine("v1: " + projectDirectory_v1);
			//string projectDirectory_v2 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"));
			//Console.WriteLine("v2: " + projectDirectory_v2);
			//string projectDirectory_v3 = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\"));
			//Console.WriteLine("v3: " + projectDirectory_v3);
			Console.WriteLine("projectFolderPath: " + _projectFolderPath);
			Console.WriteLine("imagesFolderPath: " + _imageFolderPath);
		}

		[Test]
		public void GetRandomFilename()
		{
			// Lấy tất cả các file trong thư mục
			string[] files = Directory.GetFiles(_imageFolderPath);

			if (files.Length > 0)
			{
				// Tạo một đối tượng ngẫu nhiên
				Random random = new Random();

				// Chọn ngẫu nhiên một file
				string randomFile = files[random.Next(files.Length)];

				// In ra tên file (cả tên và đuôi file)
				Console.WriteLine("File được chọn ngẫu nhiên: " + Path.GetFileName(randomFile));
			}
			else
			{
				Console.WriteLine("Không có file nào trong thư mục này.");
			}
		}

		[TestCase(5)]
		[TestCase(6)]
		[TestCase(4)]
		public void CompareNumber(int number)
		{
			Assert.IsTrue(number > 5);
		}

		// [Test]
		public void MyTest2()
		{
			_driver.Navigate().GoToUrl("https://youtube.com");
			Console.WriteLine("Navigate to Youtube");
		}
	}
}
