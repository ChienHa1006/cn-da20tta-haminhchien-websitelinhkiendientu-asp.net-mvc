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

    public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPhams { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<LbRole> LbRoles { get; set; }

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
            entity.HasKey(e => e.MaChiTietDonHang).HasName("PK__ChiTietD__4B0B45DD3C69FB99");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaChiTietDonHang).ValueGeneratedNever();

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__ChiTietDo__MaDon__3E52440B");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietDo__MaSan__3F466844");
        });

        modelBuilder.Entity<ChiTietSanPham>(entity =>
        {
            entity.HasKey(e => e.MaChiTietSanPham).HasName("PK__ChiTietS__A6B023B04222D4EF");

            entity.ToTable("ChiTietSanPham");

            entity.Property(e => e.MaChiTietSanPham).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(255);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__ChiTietSa__MaSan__440B1D61");
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMucS__B375088707F6335A");

            entity.ToTable("DanhMucSanPham");

            entity.Property(e => e.MaDanhMuc).ValueGeneratedNever();
            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD37A5467C");

            entity.ToTable("DonHang");

            entity.Property(e => e.NgayLap).HasColumnType("datetime");

            entity.HasOne(d => d.MaUserNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaUser)
                .HasConstraintName("FK__DonHang__MaUser__398D8EEE");
        });

        modelBuilder.Entity<LbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__lbRole__8AFACE3A1DE57479");

            entity.ToTable("lbRole");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.TenVaiTro).HasMaxLength(20);
        });

        modelBuilder.Entity<LbUser>(entity =>
        {
            entity.HasKey(e => e.MaUser).HasName("PK__lbUser__55DAC4B721B6055D");

            entity.ToTable("lbUser");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DienThoai).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.LbUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__lbUser__RoleID__239E4DCF");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNhaCungCap).HasName("PK__NhaCungC__53DA92050425A276");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNhaCungCap).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DienThoai).HasMaxLength(20);
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D31EC6D26");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham).ValueGeneratedNever();
            entity.Property(e => e.Anh).HasMaxLength(250);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK__SanPham__MaDanhM__33D4B598");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNhaCungCap)
                .HasConstraintName("FK__SanPham__MaNhaCu__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
