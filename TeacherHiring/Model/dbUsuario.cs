using System;
using SQLite;
namespace TeacherHiring
{
	[Table("dbUsuario")]
	public class dbUsuario
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
		public string ClaveAcceso
		{
			get;
			set;
		}
		public string Token
		{
			get;
			set;
		}
	}
}
