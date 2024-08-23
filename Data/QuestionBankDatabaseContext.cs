using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Data;

public partial class QuestionBankDatabaseContext : DbContext
{
    public QuestionBankDatabaseContext()
    {
    }

    public QuestionBankDatabaseContext(DbContextOptions<QuestionBankDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PracticePaper> PracticePapers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestPaper> TestPapers { get; set; }

    public virtual DbSet<TestTrainee> TestTrainees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=FNFIDVPRE20520; Database=QuestionBankDatabase; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PracticePaper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Practice__8A5B26BE41A89C50");

            entity.Property(e => e.PaperId).HasColumnName("paper_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.PaperName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("paper_name");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PracticePapers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PracticeP__creat__5070F446");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549F9971A01");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.CorrectAnswer)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correct_answer");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("difficulty_level");
            entity.Property(e => e.OptionA)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option_a");
            entity.Property(e => e.OptionB)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option_b");
            entity.Property(e => e.OptionC)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option_c");
            entity.Property(e => e.OptionD)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("option_d");
            entity.Property(e => e.QuestionText)
                .HasColumnType("text")
                .HasColumnName("question_text");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.Topic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("topic");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Questions__creat__5165187F");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__AFB3C316CE95AF85");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.TraineeId).HasColumnName("trainee_id");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Results__test_id__52593CB8");

            entity.HasOne(d => d.Trainee).WithMany(p => p.Results)
                .HasForeignKey(d => d.TraineeId)
                .HasConstraintName("FK__Results__trainee__534D60F1");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Tests__F3FF1C02F6156EE5");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("expiry_time");
            entity.Property(e => e.TestMaxMarks).HasColumnName("test_max_marks");
            entity.Property(e => e.TestName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("test_name");
            entity.Property(e => e.TestNoOfQuestions).HasColumnName("test_no_of_questions");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Tests__created_b__5629CD9C");
        });

        modelBuilder.Entity<TestPaper>(entity =>
        {
            entity.HasKey(e => e.TestPaperId).HasName("PK__TestPape__7AD445B5CE66DC59");

            entity.ToTable("TestPaper");

            entity.Property(e => e.TestPaperId).HasColumnName("test_paper_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TestPapers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__TestPaper__quest__5535A963");

            entity.HasOne(d => d.Test).WithMany(p => p.TestPapers)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestPaper__test___5441852A");
        });

        modelBuilder.Entity<TestTrainee>(entity =>
        {
            entity.HasKey(e => e.TestTraineeId).HasName("PK__TestTrai__F26ECC9590C58D99");

            entity.Property(e => e.TestTraineeId).HasColumnName("test_trainee_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Registered")
                .HasColumnName("status");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.TraineeId).HasColumnName("trainee_id");

            entity.HasOne(d => d.Test).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestTrain__test___571DF1D5");

            entity.HasOne(d => d.Trainee).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.TraineeId)
                .HasConstraintName("FK__TestTrain__train__5812160E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F065B1266");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164092F4972").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AdminPermission)
                .HasDefaultValue(false)
                .HasColumnName("admin_permission");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.TwoFactorEnabled)
                .HasDefaultValue(false)
                .HasColumnName("two_factor_enabled");
            entity.Property(e => e.TwoFactorExpiryTime).HasColumnName("two_factor_expiry_time");
            entity.Property(e => e.TwoFactorSecretKey).HasColumnName("two_factor_secret_key");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
