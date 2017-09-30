using System;
namespace TeacherHiring
{
	public class AlumnoMateria
	{
		public int IdAlumnoMateria
		{
			get;
			set;
		}
		public string Materia
		{
			get;
			set;
		}
		public string Alumno
		{
			get;
			set;
		}
		public string Profesor
		{
			get;
			set;
		}
		public DateTime FechaRequerida { get; set; }
		public double Latitud { get; set; }
		public double Longitud { get; set; }
		public bool Aceptada { get; set; }
	}
}
