delete from PhuHuynh
delete from QuanHe
delete from KiemTra
delete from HocKy
delete from BangDiem
delete from HocSinh
delete from Lop
delete from GiaoVien
delete from MonHoc
DBCC CHECKIDENT ('[GiaoVien]', RESEED, 0)
insert into MonHoc(IDMH,TenMonHoc)
	values (N'IT001',N'Mac-Lenin')
insert into MonHoc(IDMH,TenMonHoc)
	values (N'IT002',N'Tư tưởng HCM')

insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Hồ Chí Minh',N'IT001',N'9999999999')
insert into GiaoVien(Hoten,TenMonHoc,SDT)
	values(N'Lenin',N'IT002',N'7777777777')


select*from GiaoVien
insert into Lop(IDL,GVCN,SiSo)
	values(N'TH001',1,100)
insert into Lop(IDL,GVCN,SiSo)
	values(N'TH002',2,100)

insert into HocSinh(HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT,MatKhau)
	values(N'Sơn Tùng MTP',N'TH001',N'Nam','3/2/2000',N'Kinh',N'1',N'Việt Nam',
			N'TP.HCM',N'28349234',N'123456789')
insert into HocSinh(HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT,MatKhau)
	values(N'Đen Vâu',N'TH002',N'Nam','3/3/2000',N'Kinh',N'1',N'Việt Nam',
			N'TP.HCM',N'4534453453',N'123456789')

insert into HocKy(IDHK,TenLop,DiemTB,IDHS,HanhKiem,SoBuoiNghi,XepLoai,LenLop)
	values(N'1',N'TH001',10,1,N'tốt',0,N'giỏi',N'True')
insert into HocKy(IDHK,TenLop,DiemTB,IDHS,HanhKiem,SoBuoiNghi,XepLoai,LenLop)
	values(N'2',N'TH001',10,2,N'tốt',0,N'giỏi',N'True')

insert into KiemTra(IDKT,TenMonHoc,HinhThuc,NgayKT,Diem,IDHK)
	values(N'KT1',N'IT002',N'1 tiết','1/1/2020',10,N'1')
insert into KiemTra(IDKT,TenMonHoc,HinhThuc,NgayKT,Diem,IDHK)
	values(N'KT2',N'IT001',N'1 tiết','1/1/2020',10,N'2')
	---------------------
insert into PhuHuynh(IDPH,HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'PH001',N'Naruto',N'Nam',N'Hokage','23424234')
insert into PhuHuynh(IDPH,HoTen,GioiTinh,NgheNghiep,SDT)
	values(N'PH002',N'Hinata',N'Nữ',N'Nội trợ','23424234')

insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Cha',17520333,N'PH333')
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values(N'Mẹ',17520333,N'334')

insert into BangDiem(IDBD,IDHS,IDMH,Diem)
	values('1',17520333,'IT001',9.5)
insert into BangDiem(IDBD,IDHS,IDMH,Diem)
	values('2',17520334,'IT002',10)

