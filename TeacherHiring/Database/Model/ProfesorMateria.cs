using System;
using SQLite;

namespace TeacherHiring.Database.Model
{
	public class ProfesorMateria
	{
		[PrimaryKey]
		public int IdProfesorMateria
		{
			get;
			set;
		}
		public string NombreProfesor
		{
			get;
			set;
		}
		public string NombreMateria { get; set; }
		public string FechaDisponible { get; set; }
		public double Latitud { get; set; }
		public double Longitud { get; set; }
	}
}
