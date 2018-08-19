using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_DZ_EntityFramework
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			tbFind.KeyDown += tbFind_KeyDown;
		}

		private void tbFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(tbFind.Text))
				btFind.PerformClick();
		}

		private void tbFind_TextChanged(object sender, EventArgs e)
		{
			btFind.Enabled = string.IsNullOrWhiteSpace(tbFind.Text) ? false : true;
		}

		private void btFind_Click(object sender, EventArgs e)
		{
			using (Library2Entities db = new Library2Entities())
			{
				dataGridView1.DataSource = (from book in db.Book
										   join author in db.Author on book.IdAuthor equals author.Id
										   join publ in db.Publisher on book.IdPublisher equals publ.Id
										   where book.Author.LastName.Contains(tbFind.Text)
										   select new { book.Title,
											   Author = author.FirstName + " " + author.LastName,
											   book.Price, book.Pages, Publisher = publ.PublisherName }
										   ).ToList();

			}
		}
	}
}
