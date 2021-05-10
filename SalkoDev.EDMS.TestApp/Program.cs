using System;

namespace SalkoDev.EDMS.TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("SalkoDev.EDMS Test app");

			try
			{
				_CreateManyUsers();

				Console.WriteLine("Done. Press Enter");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.WriteLine("Press Enter");
			}

			Console.ReadLine();
		}

		static void _CreateManyUsers()
		{
			//Генерация 10 тыс юзеров заняла несколько сек, и общий объем -- 2.8 МБ, индексов где-то на 200 КБ.
			//Такое кол-во пользователей уже будет крутым результатом, и по факту они мало займут места.
			//Для 50 тыс юзеров - 14 сек генерации, общий объем коллекции: 15 МБ, индексы 3 МБ.
			

			ManyUserCreate cr = new ManyUserCreate();
			cr.Create(50000);

		}


	}
}
