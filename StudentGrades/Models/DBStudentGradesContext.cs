using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentGrades
{
    public partial class DBStudentGradesContext : DbContext
    {
        public DBStudentGradesContext()
        {
        }

        public DBStudentGradesContext(DbContextOptions<DBStudentGradesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public virtual DbSet<AssessmentType> AssessmentTypes { get; set; }
        public virtual DbSet<Caphedra> Caphedras { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseFinal> CourseFinals { get; set; }
        public virtual DbSet<CourseModule> CourseModules { get; set; }
        public virtual DbSet<CourseModuleResult> CourseModuleResults { get; set; }
        public virtual DbSet<CoursesTeacher> CoursesTeachers { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<ModuleType> ModuleTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentId> StudentIds { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherRole> TeacherRoles { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=LAPTOP-F1DL4E0O\\SQLEXPRESS; Database=DBStudentGrades; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AcademicDegree>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AssessmentType>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Caphedra>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Caphedra__A25C5AA70EB89257")
                    .IsUnique();

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.AssessmentType)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.AssessmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_AssessmentTypes");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_Disciplines");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courses_Terms");
            });

            modelBuilder.Entity<CourseFinal>(entity =>
            {
                entity.Property(e => e.ExamDate).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseFinals)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseFinals_Courses");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseFinals)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseFinals_Students");
            });

            modelBuilder.Entity<CourseModule>(entity =>
            {
                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseModules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseModules_Courses");

                entity.HasOne(d => d.ModuleType)
                    .WithMany(p => p.CourseModules)
                    .HasForeignKey(d => d.ModuleTypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseModules_ModuleTypes");
            });

            modelBuilder.Entity<CourseModuleResult>(entity =>
            {
                entity.HasOne(d => d.CourseModule)
                    .WithMany(p => p.CourseModuleResults)
                    .HasForeignKey(d => d.CourseModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseModuleResults_CourseModules");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseModuleResults)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseModuleResults_Students");
            });

            modelBuilder.Entity<CoursesTeacher>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursesTeachers)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursesTeachers_Courses");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.CoursesTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursesTeachers_Teachers");

                entity.HasOne(d => d.TeacherRole)
                    .WithMany(p => p.CoursesTeachers)
                    .HasForeignKey(d => d.TeacherRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursesTeachers_TeacherRoles");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Caphedra)
                    .WithMany(p => p.Disciplines)
                    .HasForeignKey(d => d.CaphedraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Disciplines_Caphedras");
            });

            modelBuilder.Entity<ModuleType>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.StudentId, "UQ__Students__32C52B980071F79C")
                    .IsUnique();

                entity.Property(e => e.Brirthday).HasColumnType("date");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_StudentGroups");

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_StudentIds");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Terms");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_UserInfos");
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<StudentId>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ__StudentI__A25C5AA785DC8937")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.HasOne(d => d.AcademicDegree)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.AcademicDegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teachers_AcademicDegrees");

                entity.HasOne(d => d.Caphedra)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.CaphedraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teachers_Caphedras");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.UserInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teachers_UserInfos");
            });

            modelBuilder.Entity<TeacherRole>(entity =>
            {
                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.HasIndex(e => e.Number, "UQ__Terms__78A1A19D489C9CC5")
                    .IsUnique();
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasIndex(e => e.Login, "UQ__UserInfo__5E55825BE6F97465")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__UserInfo__A9D105346682A785")
                    .IsUnique();

                entity.HasIndex(e => e.Telephone, "UQ__UserInfo__D9FEB74497C6365A")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
