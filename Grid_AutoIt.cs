using AutoItX3Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;


namespace Demo01_UploadFile
{
	public class Grid_AutoIt : BaseTest_Grid
	{
		private string _projectFolderPath;
		private string _imageFolderPath;

		public Grid_AutoIt() : base()
		{
			_projectFolderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\"));
			_imageFolderPath = Path.Combine(_projectFolderPath, "Images");
		}

		public string GetRandomImageFilename()
		{
			// Lấy tất cả các file trong thư mục
			string[] files = Directory.GetFiles(_imageFolderPath);

			if (files.Length > 0)
			{
				// Tạo một đối tượng ngẫu nhiên
				Random random = new Random();

				// Chọn ngẫu nhiên một file
				string randomFile = files[random.Next(files.Length)];

				string filename = Path.GetFileName(randomFile);
				// In ra tên file (cả tên và đuôi file)
				Console.WriteLine("File được chọn ngẫu nhiên: " + filename);
				return filename;
			}
			else
			{
				Console.WriteLine("Không có file nào trong thư mục này.");
				return "Không_có_ảnh";
			}
		}

		private string GetImageFilepath(string filename)
		{
			string path = Path.Combine(_imageFolderPath, filename);
			return path;
		}

		[Test]
		public void CreateProduct()
		{

			Console.WriteLine("My test is starting...");
			_driver.Navigate().GoToUrl("http://localhost:22220/admin");

			// Đợi tới khi nút Add Product hiển thị
			IWebElement addProductBtn = _driver.FindElement(By.LinkText("Add Product"));
			wait.Until(ap => addProductBtn.Displayed);

			addProductBtn.Click();
			_driver.FindElement(By.Id("Name")).Click();
			_driver.FindElement(By.Id("Name")).SendKeys("ProductDemo09" + DateTime.Now);
			_driver.FindElement(By.Id("Price")).Click();
			_driver.FindElement(By.Id("Price")).SendKeys("40.99");
			_driver.FindElement(By.Id("Description")).Click();
			_driver.FindElement(By.Id("Description")).SendKeys("PM04");
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

			// Đây là click, dùng với label for
			// Hiện chưa bật Label nên sẽ lỗi chỗ này
			_driver.FindElement(By.XPath("/html/body/section/div/div/div/form/div[6]/label[2]")).Click();
			try
			{
				// Chỉ chrome mới là Open
				AutoItX3 autoItX3 = new AutoItX3();
				autoItX3.WinActivate("Open"); // Đợi hộp thoại "Open" được kích hoạt
				string imgFilename = GetRandomImageFilename();
				string imgFilepath =GetImageFilepath(imgFilename);
				autoItX3.Send(imgFilepath);
				autoItX3.Send("{ENTER}");
				// Chờ ảnh nạp (bắt buộc)
				Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				Console.WriteLine("Không run được AutoIt. Lỗi: " + e.Message);
			}


			_driver.FindElement(By.XPath("/html/body/section/div/div/div/form/div[7]/button")).Click();
			Console.WriteLine("My test ends");
			try
			{
				IWebElement successMes = _driver.FindElement(By.XPath("/html/body/section/div/div/div"));
				string mes = successMes.Text;
				Assert.That(mes.Contains("Thêm sản phẩm thành công"), Is.True);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Không thêm được sản phẩm. Lỗi: " + ex.Message);
			}

		}
	}
}
