using System;
using SQLite;
namespace TeacherHiring.Database.Model
{
	public class Person
	{
		[PrimaryKey]
		public int Id
		{
			get;
			set;
		}
		public string Nombre
		{
			get;
			set;
		}
		public string ClaveUsuario
		{
			get;
			set;
		}
		public int IdTipoUsuario { get; set; }
		public string Token { get; set; }
	}
}
