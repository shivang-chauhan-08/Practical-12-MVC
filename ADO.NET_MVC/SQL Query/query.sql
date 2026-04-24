use [Practical-12];

delete from Employee where FirstName = 'Shivang';

alter table Employee3 add constraint FK_key foreign key (DesignationId) references Designation(Id);