# Hospital VRRAM
Hospital management system. This project was created when we were attending programming course at Armsoft IT company. This is our first experience of teamwork so please don't judge )).

## Information
Sofware can be used by both [doctors](https://github.com/ArmineT/HospitalVRRAM/new/master?readme=1#doctors) and [patients](https://github.com/ArmineT/HospitalVRRAM/new/master?readme=1#patients).
### Doctors
  Doctors accounts contain information about patients that are registered for consultation and corresponding time for consultation. There is also an archive which contains information about past consultations. When it's time for consultation doctor accepts the consultation from his/her account and when the consultation is ended the payment is done automatically. Doctor can also write diagnosis for patients, where he/she can add also medicine for treatment(the medicine should be present in hospital store). Doctors can be registered only by admin user.
### Patients
  Patients accounts contain information about consultations of patient both future and past. Patient can see all the doctors whose consultation he/she can attend (the price is not greater than patients balance). After that patient requests consultation time and system chooses a time as near as possible to specified when doctor is free and the registration is completed. As the registration is completed the consultation price is kept apart from patients balance and only if the consultation is unsuccessful (doctor or patient wasn't there) the kept money will be back to patients balance.
## Admin
  The hospital sys admin who has information about hospital budget and all users (doctors and patients) of hospital. Admin can hire and fire doctors. All the medicine information (price / quantity / image) is added by admin.
## Built With

* [C#/Windows Forms](https://docs.microsoft.com/en-us/dotnet/framework/winforms/windows-forms-overview) - Main language used
* [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview) - Technology used for data access
* [MS SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/sql-server-technical-documentation?view=sql-server-2017) - Databases

## Authors

* **Vahagn Altunyan** - [altunyanv](https://github.com/altunyanv)
* **Roza Adamyan** - [RozaAdamyan5](https://github.com/RozaAdamyan5)
* **Rafayel Mkrtchyan** - [Rafayel1998](https://github.com/Rafayel1998)
* **Armine Terteryan** - [ArmineT](https://github.com/ArmineT)
* **Maneh Harutyunyan** - [manehharutyunyan](https://github.com/manehharutyunyan)

As you can see the name of hospital comes from first letters of our first names :D and it's also personal name in our country.
