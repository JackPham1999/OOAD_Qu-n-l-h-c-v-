drop database HocVu
create database HocVu
use HocVu
create table MonHoc
(
	ID int identity(1,1) not null ,
	IDMH AS 'MH'+right('00'+cast(ID as varchar(2)), 2) PERSISTED,
	TenMonHoc nvarchar(100),
	constraint pk_mh primary key(ID),
)
create table GiaoVien
(
	ID int identity(1,1) not null ,
	IDGV AS 'GV'+right('00'+cast(ID as varchar(2)), 2) PERSISTED,
	Hoten nvarchar(100) NOT NULL,
	TenMonHoc int,
	SDT nvarchar(12),
	constraint pk_gv primary key (ID),
	constraint fk_gv_mh foreign key (TenMonHoc) references MonHoc(ID),
)
create table Lop
(
	ID int identity(1,1) not null ,
	IDL AS 'L'+right('000'+cast(ID as varchar(3)), 3) PERSISTED,
	GVCN int NOT NULL,
	SiSo int,
	constraint pk_l primary key(ID),
	constraint fk_l_gv foreign key (GVCN) references GiaoVien(ID),
)
create table HocSinh
(
	ID int identity(1,1) not null ,
	IDHS AS 'HS'+right('00000'+cast(ID as varchar(5)), 5) PERSISTED,
	MatKhau varchar(100),
	HoTen nvarchar(100) NOT NULL,
	TenLop int,
	GioiTinh nvarchar(10) check (GioiTinh in (N'Nam',N'Nữ',N'Khác')),
	NgaySinh datetime NOT NULL,
	DanToc nvarchar(10),
	CheDo nvarchar(20),
	DCTT nvarchar(50) NOT NULL,
	DCHT nvarchar(50) NOT NULL,
	SDT nvarchar(12),
	constraint pk_hs primary key (ID),
	constraint fk_hs_l foreign key (TenLop) references Lop(ID),
)
---------------y dinh sua hocky thanh tong ket hoc ky
create table HocKy
(
	IDHK varchar(50) NOT NULL,
	NamHoc varchar(50),
	TenHocKy varchar(20),
	constraint pk_hk primary key (IDHK),
)
create table KiemTra
(
	IDKT nvarchar(20) NOT NULL,
	TenMonHoc int,
	HinhThuc nvarchar(20),
	NgayKT datetime,
	Diem float,
	IDHK varchar(50),
	constraint pk_kt primary key (IDKT),
	constraint fk_kt_mh foreign key (TenMonHoc) references MonHoc(ID),
	constraint fk_kt_hk foreign key (IDHK) references HocKy(IDHK),
)
create table PhuHuynh
(
	ID int identity(1,1) not null ,
	IDPH AS 'PH'+right('000000'+cast(ID as varchar(6)), 6) PERSISTED,
	HoTen nvarchar(100) ,
	GioiTinh nvarchar(10),
	NgheNghiep nvarchar(50),
	SDT nvarchar(12),
	constraint pk_ph primary key (ID),
)
create table QuanHe
(
	TenQuanHe nvarchar(50),
	IDHS int,
	IDPH int,
	constraint fk_qh_hs foreign key (IDHS) references HocSinh(ID),
	constraint fk_qh_ph foreign key (IDPH) references PhuHuynh(ID),
	constraint pk_qh primary key (IDHS,IDPH),
)
create table BangDiem
(
	ID int identity(1,1) not null ,
	IDBD AS 'BD'+right('00000'+cast(ID as varchar(5)), 5) PERSISTED,
	IDHS int,
	IDHK varchar(50),
	IDMH int,
	TenBaiKT nvarchar(100),
	Diem float,
	DiemTB float,
	HanhKiem nvarchar(20),
	SoBuoiNghi int,
	XepLoai nvarchar(20),
	LenLop nvarchar(10) check (LenLop in ('True','False')),
	constraint pk_bd primary key (ID),
	constraint fk_bd_hk foreign key (IDHK) references HocKy(IDHK),
	constraint fk_bd_hs foreign key (IDHS) references HocSinh(ID),
	constraint fk_bd_mh foreign key (IDMH) references MonHoc(ID),
)
---------------------------Môn học
insert into MonHoc(TenMonHoc) values (N'Toán')
insert into MonHoc(TenMonHoc) values (N'Văn học')
insert into MonHoc(TenMonHoc) values (N'Anh văn')
insert into MonHoc(TenMonHoc) values (N'Sinh học')
insert into MonHoc(TenMonHoc) values (N'Vật lý')
insert into MonHoc(TenMonHoc) values (N'Hóa học')
insert into MonHoc(TenMonHoc) values (N'Lịch sử')
insert into MonHoc(TenMonHoc) values (N'Địa lý')
insert into MonHoc(TenMonHoc) values (N'Giáo dục công dân')
select*from MonHoc
-------------------Giáo viên
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn A',1,'4345345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn B',2,'4345346')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn C',3,'434534345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn D',4,'4322345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn E',5,'42245345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn F',6,'3555345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn G',7,'7745345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn H',8,'2245345')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Nguyễn Văn I',9,'4545345')
select*from GiaoVien
select GiaoVien.Hoten from GiaoVien where GiaoVien.TenMonHoc=1
select *from GiaoVien where GiaoVien.TenMonHoc=1

---delete from Lop
select*from Lop
insert into Lop(GVCN,SiSo)
	values(1,40)
insert into Lop(GVCN,SiSo)
	values(2,35)
insert into Lop(GVCN,SiSo)
	values(3,35)
insert into Lop(GVCN,SiSo)
	values(4,35)
insert into Lop(GVCN,SiSo)
	values(5,35)
insert into Lop(GVCN,SiSo)
	values(6,35)
insert into Lop(GVCN,SiSo)
	values(7,35)
insert into Lop(GVCN,SiSo)
	values(8,35)
insert into Lop(GVCN,SiSo)
	values(9,35)

select*from HocSinh
insert into HocSinh(MatKhau,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(N'123456789',N'Trần Thị A',1,N'Nam','3/2/2000',N'Kinh',N'tốt',N'Tp.HCM',
		N'Việt Nam','524234242')
insert into HocSinh(MatKhau,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(N'123456789',N'Trần Thị B',2,N'Nữ','3/2/2000',N'Kinh',N'tốt',N'Tp.HCM',
		N'Việt Nam','524234242')
insert into HocSinh(MatKhau,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(N'123456789',N'Trần Thị C',3,N'Nữ','3/2/2000',N'Kinh',N'tốt',N'Tp.HCM',
		N'Việt Nam','524234242')
insert into HocSinh(MatKhau,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(N'123456789',N'Trần Thị D',3,N'Nữ','3/2/2000',N'Kinh',N'tốt',N'Tp.HCM',
		N'Việt Nam','43434343')

select* from HocKy
insert into HocKy(IDHK,NamHoc,TenHocKy)
	values('HK12000','2000-2001','HK1')
insert into HocKy(IDHK,NamHoc,TenHocKy)
	values('HK22000','2000-2001','HK2')

insert into KiemTra(IDKT,IDHK,TenMonHoc,HinhThuc,NgayKT,Diem)
	values('KT001','HK12000',1,N'15 phút','1/1/2000',9.5)
insert into KiemTra(IDKT,IDHK,TenMonHoc,HinhThuc,NgayKT,Diem)
	values('KT002','HK12000',1,N'90 phút','1/1/2000',9.5)

---delete from PhuHuynh
---DBCC CHECKIDENT ('[PhuHuynh]', RESEED, 0);
insert into PhuHuynh(HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'Edward Newgate',N'Nam',N'Old Father','9353593')
insert into PhuHuynh(HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'Kaido',N'Nam',N'Great Saint','6745645')
insert into PhuHuynh(HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'Shank',N'Nam',N'Traveller','43345')
insert into PhuHuynh(HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'Big Mom',N'Nữ',N'Queen','4353453')

---delete from QuanHe
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Father',1,1)
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Father',2,2)
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Brother',3,3)
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Mother',4,4)
select*from QuanHe
select*from PhuHuynh
select*from HocSinh

insert into BangDiem(IDHS,IDHK,IDMH,TenBaiKT,Diem)
	values(1,'HK12000',1,'KT001',9)
insert into BangDiem(IDHS,IDHK,IDMH,TenBaiKT,Diem)
	values(2,'HK12000',1,'KT001',9)
insert into BangDiem(IDHS,IDHK,IDMH,TenBaiKT,Diem)
	values(3,'HK12000',1,'KT001',9)
insert into BangDiem(IDHS,IDHK,IDMH,TenBaiKT,Diem)
	values(4,'HK12000',1,'KT001',9)
select*from BangDiem