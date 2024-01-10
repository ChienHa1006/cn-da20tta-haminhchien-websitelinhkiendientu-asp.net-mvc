using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebSiteLKDT.Models;

public partial class WslkdtContext : DbContext
{
    public WslkdtContext()
    {
    }

    public WslkdtContext(DbContextOptions<WslkdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<LbUser> LbUsers { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KARENNN;Initial Catalog=WSLKDT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDonHang).HasName("PK__ChiTietD__4B0B45DD08EA5793");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaChiTietDonHang).ValueGeneratedNever();

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__ChiTietDo__MaDon__1BFD2C07");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietDo__MaSan__1CF15040");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ChiTietGioHang");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany()
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK__ChiTietGi__MaGio__20C1E124");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany()
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietGi__MaSan__21B6055D");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMucS__B3750887145C0A3F");

            entity.ToTable("DanhMucSanPham");

            entity.Property(e => e.MaDanhMuc).HasMaxLength(20);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD0425A276");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDonHang).ValueGeneratedNever();
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaUserNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaUser)
                .HasConstraintName("FK__DonHang__MaUser__1B0907CE");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3182C9B23");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang).ValueGeneratedNever();

            entity.HasOne(d => d.MaUserNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaUser)
                .HasConstraintName("FK__GioHang__MaUser__1FCDBCEB");
        });

        modelBuilder.Entity<LbUser>(entity =>
        {
            entity.HasKey(e => e.MaUser).HasName("PK__lbUser__55DAC4B77F60ED59");

            entity.ToTable("lbUser");

            entity.Property(e => e.MaUser).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DienThoai).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.TrangThai).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNhaCungCap).HasName("PK__NhaCungC__53DA9205108B795B");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNhaCungCap).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DienThoai).HasMaxLength(20);
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D0CBAE877");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham).ValueGeneratedNever();
            entity.Property(e => e.Anh).HasMaxLength(250);
            entity.Property(e => e.MaDanhMuc).HasMaxLength(20);
            entity.Property(e => e.Mota).HasMaxLength(300);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__SanPham__MaDanhM__1DE57479");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNhaCungCap)
                .HasConstraintName("FK__SanPham__MaNhaCu__1ED998B2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
