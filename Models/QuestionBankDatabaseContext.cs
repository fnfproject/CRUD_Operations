using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=FNFIDVPRE20520; Database=QuestionBankDatabase; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PracticePaper>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__Practice__8A5B26BEB2E78733");

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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PracticePapers)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PracticeP__creat__571DF1D5");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC21549BBAF4A4F");

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
            entity.Property(e => e.Explaination)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("explaination");
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Questions__creat__412EB0B6");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Results__AFB3C316AD47D3EA");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.NoOfRightAnswers).HasColumnName("no_of_right_answers");
            entity.Property(e => e.NoOfWrongAnswers).HasColumnName("no_of_wrong_answers");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.Score)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("score");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__Results__test_id__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Results__user_id__534D60F1");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Tests__F3FF1C02BDEC09D6");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ExpiryTime)
                .HasColumnType("datetime")
                .HasColumnName("expiry_time");
            entity.Property(e => e.Hyperlinks)
                .HasMaxLength(300)
                .HasColumnName("hyperlinks");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.TestMaxMarks).HasColumnName("test_max_marks");
            entity.Property(e => e.TestName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("test_name");
            entity.Property(e => e.TestNoOfQuestions).HasColumnName("test_no_of_questions");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Tests__created_b__44FF419A");
        });

        modelBuilder.Entity<TestPaper>(entity =>
        {
            entity.HasKey(e => e.TestPaperId).HasName("PK__TestPape__7AD445B5687619EE");

            entity.ToTable("TestPaper");

            entity.Property(e => e.TestPaperId).HasColumnName("test_paper_id");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.TestPath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("test_path");

            entity.HasOne(d => d.Test).WithMany(p => p.TestPapers)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestPaper__test___47DBAE45");
        });

        modelBuilder.Entity<TestTrainee>(entity =>
        {
            entity.HasKey(e => e.TestTraineeId).HasName("PK__TestTrai__F26ECC95B997F248");

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
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Test).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestTrain__test___4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.TestTrainees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TestTrain__user___4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC075DAA89F6");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534938AC685").IsUnique();

            entity.Property(e => e.CreactedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Is2Faenabled)
                .HasDefaultValue(false)
                .HasColumnName("Is2FAEnabled");
            entity.Property(e => e.IsAdminApproved).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
