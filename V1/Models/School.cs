﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace V1.Models
{
 public class Student
{
public int StudentID{get;set;}
public string LastName{get;set;}
public string FirstMidName{get;set;}
public DateTime EnrollmentDate{get;set;}
public virtual ICollection<Enrollment>Enrollments{get;set;}
}
    public class Enrollment
{
public int EnrollmentID{get;set;}
public int CourseID{get;set;}
public int StudentID{get;set;}
public decimal? Grade{get;set;}
public virtual Course Course{get;set;}
public virtual Student Student{get;set;}
}
    public class Course
{
public int CourseID{get;set;}
public string Title{get;set;}
public int Credits{get;set;}
public virtual ICollection<Enrollment>Enrollments{get;set;}
}
    public class SchoolContext:DbContext
{
public DbSet<Student> Students{get;set;}
public DbSet<Enrollment> Enrollments{get;set;}
public DbSet<Course> Courses{get;set;}
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
}
}
}