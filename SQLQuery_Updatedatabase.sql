alter table HocSinh add MatKhau nvarchar(100)
select * from HocSinh
update HocSinh set MatKhau='123456789'
where IDHS=17520333
update HocSinh set MatKhau='987654321'
where IDHS=17520334
select * from HocSinh
alter table HocSinh alter column NgaySinh date not null