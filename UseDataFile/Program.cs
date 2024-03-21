using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Drawing;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System;

//Thiếu namespace ở đầu mỗi class, cũng ko ảnh hưởng gì lắm
//vd:

namespace DemoProjectTest 
{

[TestFixture]
public class LoginPageTest : IDisposable
{
    private IWebDriver driver;
    private string filePath = @"..\..\..\..\TestData\login.csv"; // Update with the path to your CSV file

    [SetUp]
    public void Setup()
    {
        var Chrome = new ChromeOptions();
        Chrome.BinaryLocation = @"D:\Duyen\VisualStudio\GoogleChromePortable64\App\Chrome-bin\chrome.exe";
        driver = new ChromeDriver(@"D:\Duyen\VisualStudio\chromedriver.exe", Chrome);
        driver.Manage().Window.Size = new Size(1024, 768);
    }

    [Test]

       public void ReadDataAndLogin()
   {
       using (var reader = new StreamReader(filePath))
       {
           while (!reader.EndOfStream)
           {
               var line = reader.ReadLine();
               var values = line.Split(',');

               string username = values[0];
               string password = values[1];
             //  string ConfirmXpath = values[2];
             //  string ConfirmMsg = values[3];

               // Now use these credentials to login
              VerifyLogin(username, password);
           }
       }
   }

public void VerifyLogin(string username, string password)
   {
       driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");

       Thread.Sleep(1000);
        driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys(username);
        driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys(password);
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();

        Thread.Sleep(2000);
      // driver.FindElement(By.LinkText("My Info")).Click();
       Thread.Sleep(2000);

       // Take a screenshot and save it to a file
       Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
       screenshot.SaveAsFile(@"..\..\..\..\TestResult\LoginOK.jpg");

       /*   

       driver.FindElement(By.XPath("ConfirmXpath")).getText();
       String BrMsg = text.getText();
       Assert.assertEquals(ConfirmMsg, BrMsg);
       /
        Thread.Sleep(1000);
       driver.FindElement(By.Name("username")).SendKeys("Admin");
       driver.FindElement(By.Name("password")).SendKeys("admin123");
       driver.FindElement(By.XPath("//button[@type='submit']")).Click();

       Thread.Sleep(2000);
       driver.FindElement(By.LinkText("My Info")).Click();
       Thread.Sleep(2000);

       // Take a screenshot and save it to a file
       Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
       screenshot.SaveAsFile(@"..\..\..\..\TestResult\LoginOK.jpg");

       */


}

[TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

}
