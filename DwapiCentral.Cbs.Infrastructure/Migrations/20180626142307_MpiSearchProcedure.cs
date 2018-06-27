using Microsoft.EntityFrameworkCore.Migrations;

namespace DwapiCentral.Cbs.Infrastructure.Migrations
{
    public partial class MpiSearchProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            /****** Object:  StoredProcedure [dbo].[Pr_SearchMasterPatientIndex]    Script Date: 6/26/2018 11:19:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC [dbo].[Pr_SearchMasterPatientIndex] 
@FirstName varchar (200)='', 
@MiddleName varchar (200)='',
@LastName varchar (200)='', 
@Gender varchar (10)='', 
@DateOfBirth Date, 
@Telephone varchar (20)='',
@NationalId Varchar(20)='',
@NHIFNumber varchar(20)='',
@Actual BIT = 0

AS 

DECLARE @dmLName varchar (50), @sxFname varchar(50), @dmMiddleNameLname varchar(50), 
@sxFnamedmLastNameDob varchar(100) , @Threshold FLOAT = 0.90



DECLARE @Results TABLE 
(

MflCode VARCHAR(20),
RegisteredFacility VARCHAR(50), 
FirstName VARCHAR(50), 
MiddleName VARCHAR (50), 
LastName VARCHAR (50),
Gender VARCHAR(50),
DateOfBirth DATE, 
PhoneNumber VARCHAR(50), 
HomeCounty VARCHAR (50), 
HomeSubCounty VARCHAR(50),
NationalId VARCHAR(50), 
NHIFNumber VARCHAR(50) , 
CCCNumber VARCHAR(50),
MatchingScore FLOAT

)


IF @Actual = 1 
      SET @Threshold = 0.98
ELSE 
      SET @Threshold = 0.86


---If the Telephone Number is supplied - High 
IF LEN (@Telephone) > 0 
 INSERT @Results 
 SELECT 

      SiteCode as MflCode,
      FacilityName as RegisteredFacility, 
      UPPER(FirstName) AS FirstName, 
      UPPER (MiddleName) AS MiddleName, 
      UPPER(LastName) AS LastName,
      Gender as Gender,
      CAST(Dob as DATE) as DateOfBirth, 
      PatientPhoneNumber AS PhoneNumber, 
      PatientCounty as HomeCounty, 
      PatientSubCounty AS HomeSubCounty,
      National_ID as NationalId, 
      NHIF_Number as NHIFNumber, 
      Serial as CCCNumber,
      1.0 AS MatchingScore

FROM [dbo].[MasterPatientIndices]
WHERE  PatientPhoneNumber = @Telephone


---If the National Id Number is supplied  - High 
IF LEN (@NationalId) > 0 
 INSERT @Results 
 SELECT 
      SiteCode as MflCode,
      FacilityName as RegisteredFacility, 
      UPPER(FirstName) AS FirstName, 
      UPPER (MiddleName) AS MiddleName, 
      UPPER(LastName) AS LastName,
      Gender as Gender,
      CAST(Dob as DATE) as DateOfBirth, 
      PatientPhoneNumber AS PhoneNumber, 
      PatientCounty as HomeCounty, 
      PatientSubCounty AS HomeSubCounty,
      National_ID as NationalId, 
      NHIF_Number as NHIFNumber, 
      Serial as CCCNumber,
      1.0 AS MatchingScore
FROM [dbo].[MasterPatientIndices]
WHERE  National_ID = @NationalId

--- If the NHIF Number is supplied - High
IF LEN (@NationalId) > 0 
 INSERT @Results 
 SELECT 
      SiteCode as MflCode,
      FacilityName as RegisteredFacility, 
      UPPER(FirstName) AS FirstName, 
      UPPER (MiddleName) AS MiddleName, 
      UPPER(LastName) AS LastName,
      Gender as Gender,
      CAST(Dob as DATE) as DateOfBirth, 
      PatientPhoneNumber AS PhoneNumber, 
      PatientCounty as HomeCounty, 
      PatientSubCounty AS HomeSubCounty,
      National_ID as NationalId, 
      NHIF_Number as NHIFNumber, 
      Serial as CCCNumber,
      1.0 AS MatchingScore
FROM [dbo].[MasterPatientIndices]
WHERE  NHIF_Number = @NHIFNumber


------PROBABULISTIC MATCHING
SET @sxFname= SOUNDEX(@FirstName)
SET @dmMiddleNameLname = [dbo].[fn_getPatientNameDoubleMetaphone]([dbo].[fnReplaceInvalidChars](@MiddleName)+[dbo].[fnReplaceInvalidChars](@LastName))
SET @dmLName = [dbo].[fn_getPatientNameDoubleMetaphone]([dbo].[fnReplaceInvalidChars](@LastName))
SET @sxFnamedmLastNameDob =CASE WHEN CHARINDEX(';',@dmLName)>0 THEN 
                                              LEFT(@Gender,1)+ @sxFname+ SUBSTRING(@dmLName,CHARINDEX(';',@dmLName)+1,LEN(@dmLName))+LTRIM(RTRIM(STR(YEAR(@DateOfBirth))))
                                            ELSE 
                                              LEFT(@Gender,1)+ @sxFname+@dmLName+LTRIM(RTRIM(STR(YEAR(@DateOfBirth))))
                                            END 


--LEFT(@Gender,1)+@sxFname+@dmLName+CONVERT(VARCHAR(20),@DateOfBirth, 112 )


INSERT @Results 
SELECT 
SiteCode as MflCode,
FacilityName as RegisteredFacility, 
      UPPER(FirstName) AS FirstName, 
      UPPER (MiddleName) AS MiddleName, 
      UPPER(LastName) AS LastName,
Gender as Gender,
CAST(Dob as DATE) as DateOfBirth, 
PatientPhoneNumber AS PhoneNumber, 
PatientCounty as HomeCounty, 
PatientSubCounty AS HomeSubCounty,
National_ID as NationalId, 
NHIF_Number as NHIFNumber, 
Serial as CCCNumber,
dbo.fn_calculateJaroWinkler (@sxFnamedmLastNameDob,sxdmPKValueDoB) AS MatchingScore

FROM [dbo].[MasterPatientIndices]
WHERE dbo.fn_calculateJaroWinkler (@sxFnamedmLastNameDob,sxdmPKValueDoB) >= @Threshold AND Gender = @Gender AND YEAR(Dob) = YEAR(@DateOfBirth)



SELECT * FROM @Results ORDER BY MatchingScore DESC

go 

            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC Pr_SearchMasterPatientIndex ");
        }
    }
}
