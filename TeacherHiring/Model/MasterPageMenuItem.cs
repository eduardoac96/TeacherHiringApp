﻿using System;
namespace TeacherHiring
{
	public class MasterPageMenuItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Type TargetType { get; set; }
		public string IconPath { get; set; }
	}
}
