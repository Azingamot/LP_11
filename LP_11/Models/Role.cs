﻿using System.ComponentModel.DataAnnotations;

public class Role
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public int AccessLevel { get; set; }
}
