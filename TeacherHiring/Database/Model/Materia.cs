using System;
using SQLite;
namespace TeacherHiring.Database.Model
{
	public class Materia
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
	}
}
