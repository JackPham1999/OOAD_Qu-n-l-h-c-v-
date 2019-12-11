create table BangDiem
(
	IDBD nvarchar(50),
	IDHS int,
	IDMH nvarchar(50),
	Diem float,
	constraint fk_bd_hs foreign key (IDHS) references HocSinh(IDHS),
	constraint fk_bd_mh foreign key (IDMH) references MonHoc(IDMH),
)
select * from BangDiem
insert into BangDiem(IDBD,IDHS,IDMH,Diem)
	values ('1',17520333,'IT001',10)
insert into BangDiem(IDBD,IDHS,IDMH,Diem)
	values ('2',17520334,'IT002',9.5)