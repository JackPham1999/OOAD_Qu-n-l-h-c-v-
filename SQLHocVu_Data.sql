insert into MonHoc(IDMH,TenMonHoc)
	values ('IT001','Mac-Lenin')
insert into MonHoc(IDMH,TenMonHoc)
	values (N'IT002',N'Tư tưởng HCM')
SELECT IDMH,LEN(IDMH) LEN,DATALENGTH(IDMH) LENGHT FROM MonHoc
insert into GiaoVien(IDGV,Hoten,TenMonHoc,SDT)
	values(111,'Hồ Chí Minh','IT001','9999999999')
insert into GiaoVien(IDGV,Hoten,TenMonHoc,SDT)
	values(222,'Lenin','IT002','7777777777')

insert into Lop(IDL,GVCN,SiSo)
	values('TH001',111,100)
insert into Lop(IDL,GVCN,SiSo)
	values('TH002',222,100)

insert into HocSinh(IDHS,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(17520333,'Sơn Tùng MTP','TH001','Nam','3/2/2000','Kinh','1','Việt Nam',
			'TP.HCM','28349234')
insert into HocSinh(IDHS,HoTen,TenLop,GioiTinh,NgaySinh,DanToc,CheDo,DCTT,DCHT,SDT)
	values(17520334,'Đen Vâu','TH002','Nam','3/3/2000','Kinh','1','Việt Nam',
			'TP.HCM','4534453453')
--------------------chưa add đc

insert into HocKy(IDHK,TenLop,DiemTB,IDHS,HanhKiem,SoBuoiNghi,XepLoai,LenLop)
	values('1','TH001',10,17520333,'tốt',0,'giỏi','True')
insert into HocKy(IDHK,TenLop,DiemTB,IDHS,HanhKiem,SoBuoiNghi,XepLoai,LenLop)
	values('2','TH001',10,17520334,'tốt',0,'giỏi','True')

insert into KiemTra(IDKT,TenMonHoc,HinhThuc,NgayKT,Diem,IDHK)
	values('KT1','TH001','1 tiết','1/1/2020',10,'HK1')

insert into PhuHuynh(IDPH,HoTen,GioiTinh,NgheNghiep,SDT)
	values(333,'Naruto','Nam','Hokage','23424234')
insert into PhuHuynh(IDPH,HoTen,GioiTinh,NgheNghiep,SDT)
	values(334,'Hinata','Nữ','Nội trợ','23424234')

insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values('Cha',17520333,333)
insert into QuanHe(TenQuanHe,IDHS,IDPH)
	values('Mẹ',17520333,334)

