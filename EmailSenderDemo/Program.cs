using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace NetConsoleApp {

	class Program {

		static void Main(string[] args) {

			MailAddress from = new MailAddress("bulat.sender@gmail.com", "Булат. Автоматичні повідомлення");
      //MailAddress to = new MailAddress("grigorashik@gmail.com");
		  MailAddress to = new MailAddress("strmisha.s@gmail.com");
      MailMessage m = new MailMessage(from, to) {
				Subject = "Перевірка зв\'язку",
				Body = "<h2>Тестовий лист</h2>",			
				IsBodyHtml = true
			};

			SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
			smtp.Credentials = new NetworkCredential("bulat.sender@gmail.com", "Bulat2234");
			smtp.EnableSsl = true;
			smtp.Send(m);
			Console.Read();
		}
	}
}