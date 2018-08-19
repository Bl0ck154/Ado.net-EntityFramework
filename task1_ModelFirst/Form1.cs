using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1_ModelFirst
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				using (LibraryModelContainer db = new LibraryModelContainer())
				{
					Author author = new Author() { FullName = "Lorenso Lamos" };
					db.Authors.Add(author);
					Author author1 = new Author() { FullName = "John Travolta" };
					db.Authors.Add(author1);
					Author author2 = new Author() { FullName = "Friedrich Nietzsche" };
					db.Authors.Add(author2);

					Book book = new Book() { Title = "Iskustvo Pofigizma", Pages = 45, Price = 240 };
					book.Author.Add(author);
					book.Author.Add(author1); 
					db.Books.Add(book);

					Book book1 = new Book() { Title = "Thus Spoke Zarathustra", Pages = 352, Price = 350 };
					book1.Author.Add(author2);
					db.Books.Add(book1);

					Author author3 = new Author() { FullName = "J.R.R.Tolkien" };
					db.Authors.Add(author3);
					Book book2 = new Book() { Title = "The Lord of the Rings", Pages = 1685, Price = 2650 };
					book2.Author.Add(author3);
					db.Books.Add(book2);

					Book book3 = new Book() { Title = "Bratva and the Ring", Pages = 684, Price = 1350 };
					book3.Author.Add(author3);
					db.Books.Add(book3);

					Customer[] customers = { new Customer() { Name = "Balde Keita" },
					 new Customer() { Name = "Timo Werner" },
					 new Customer() { Name = "Chupomoting" } };

					db.Customers.AddRange(customers);

					book3.Customer = customers[2];
					book2.Customer = customers[2];
					book.Customer = customers[1];

					db.SaveChanges();
				}

				(sender as Button).Text = "Done";
				(sender as Button).Enabled = false;
			}
			catch
			{ }
		}

		private void button2_Click(object sender, EventArgs e)
		{

			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = (from customer in db.Customers
											join book in db.Books on customer.Id equals book.CustomerId
											select new { Deptor = customer.Name }
											).Distinct().ToList();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Books.Where(b => b.Id == 3).
					FirstOrDefault().Author.Select(a => new { a.FullName }).ToList();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Books.Where(b => b.CustomerId == null)
					.Select(b => new { b.Title, b.Pages, b.Price }).ToList();
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Books.Where(b => b.CustomerId == 2)
					.Select(b => new { b.Title, b.Pages, b.Price }).ToList();
			}

		}

		private void button6_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				foreach (Book item in db.Books)
				{
					item.Customer = null;
					item.CustomerId = null;
				}
				db.SaveChanges();
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Books
					.Select(b => new { b.Id, b.Title, b.Price, b.Pages, b.CustomerId }).ToList();
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Authors
					.Select(a => new { a.Id, a.FullName }).ToList();
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			using (LibraryModelContainer db = new LibraryModelContainer())
			{
				dataGridView1.DataSource = db.Customers
					.Select(c => new { c.Id, c.Name }).ToList();
			}
		}
	}
}
