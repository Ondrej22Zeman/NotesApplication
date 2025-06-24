﻿namespace NotesApplication.Models;

public class Tag
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public List<Note> Notes { get; set; } = new();
}