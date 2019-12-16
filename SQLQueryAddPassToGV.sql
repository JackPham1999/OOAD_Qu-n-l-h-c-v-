use HocVu
alter table GiaoVien 
add MatKhau varchar(100)
update GiaoVien
set MatKhau='123456'
create table LopDay
(
	IDGV int,
	IDL int,
	constraint fk_ld_gv foreign key (IDGV) references GiaoVien(ID),
	constraint fk_ld_mh foreign key (IDL) references Lop(ID),
	constraint pk_ld primary key (IDGV,IDL),
)
insert into LopDay values (1,1)
insert into LopDay values (1,2)
insert into LopDay values (1,3)
insert into LopDay values (1,4)
insert into LopDay values (1,5)
insert into LopDay values (1,6)
insert into LopDay values (1,7)
insert into LopDay values (1,8)
insert into LopDay values (1,9)
insert into LopDay values (2,1)
insert into LopDay values (2,2)
insert into LopDay values (2,3)
insert into LopDay values (2,4)
insert into LopDay values (2,5)
insert into LopDay values (2,6)
insert into LopDay values (2,4)
insert into LopDay values (2,5)
insert into LopDay values (2,6)
insert into LopDay values (2,7)
insert into LopDay values (2,8)
insert into LopDay values (2,9)