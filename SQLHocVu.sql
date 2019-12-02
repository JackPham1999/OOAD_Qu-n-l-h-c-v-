create database HocVu
use HocVu
--drop database HocVu
create table HocSinh
(
	IDHS int NOT NULL,
	HoTen nvarchar(100) NOT NULL,
	TenLop nvarchar(20) NOT NULL,
	GioiTinh nvarchar(10) check (GioiTinh in ('Nam','Nữ','Khác')),
	NgaySinh datetime NOT NULL,
	DanToc nvarchar(10),
	CheDo nvarchar(20),
	DCTT nvarchar(50) NOT NULL,
	DCHT nvarchar(50) NOT NULL,
	SDT nvarchar(12),
	constraint pk_hs primary key (IDHS),
	constraint fk_hs_l foreign key (TenLop) references Lop(IDL),
)
create table Lop
(
	IDL nvarchar(20) NOT NULL,
	GVCN int NOT NULL,
	SiSo int,
	--HocVien int,
	constraint pk_l primary key(IDL),
	constraint fk_l_gv foreign key (GVCN) references GiaoVien(IDGV),
	--constraint fk_l_hs foreign key (HocVien) references HocSinh(IDHS),
)
create table GiaoVien
(
	IDGV int NOT NULL ,
	Hoten nvarchar(100) NOT NULL,
	TenMonHoc nvarchar(20),
	---LopChuNhiem nvarchar(100),
	SDT nvarchar(12),
	constraint pk_gv primary key (IDGV),
	constraint fk_gv_mh foreign key (TenMonHoc) references MonHoc(IDMH),
	--constraint fk_gv_l foreign key (LopChuNhiem) references Lop(IDL),
)
create table MonHoc
(
	IDMH nvarchar(20) NOT NULL ,
	TenMonHoc nvarchar(100),
	constraint pk_mh primary key(IDMH),
)
create table PhuHuynh
(
	IDPH int NOT NULL ,
	HoTen nvarchar(100) ,
	GioiTinh nvarchar(10),
	NgheNghiep nvarchar(50),
	SDT nvarchar(12),
	constraint pk_ph primary key (IDPH),
)
create table QuanHe
(
	TenQuanHe nvarchar(50),
	IDHS int,
	IDPH int,
	constraint fk_qh_hs foreign key (IDHS) references HocSinh(IDHS),
	constraint fk_qh_ph foreign key (IDPH) references PhuHuynh(IDPH),
	constraint pk_qh primary key (IDHS,IDPH),
)
create table HocKy
(
	IDHK nvarchar(20) NOT NULL,
	TenLop nvarchar(20),
	DiemTB float,
	IDHS int,
	HanhKiem nvarchar(15),
	SoBuoiNghi int,
	XepLoai nvarchar(15),
	LenLop nvarchar check (LenLop in ('True','False')),
	constraint pk_hk primary key (IDHK),
	constraint fk_hk_l foreign key (TenLop) references Lop(IDL),
	constraint fk_hk_hs foreign key (IDHS) references HocSinh(IDHS),
)
create table KiemTra
(
	IDKT nvarchar(20) NOT NULL,
	TenMonHoc nvarchar(20),
	HinhThuc nvarchar(10),
	NgayKT datetime,
	Diem float,
	IDHK nvarchar(20),
	constraint pk_kt primary key (IDKT),
	constraint fk_kt_mh foreign key (TenMonHoc) references MonHoc(IDMH),
	constraint fk_kt_hk foreign key (IDHK) references HocKy(IDHK),
)
