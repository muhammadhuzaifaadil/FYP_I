::echo off
::cd D:\Semester7\FYP_FINAL_WEBAPI\FYP_!\FYP_!\bin\Release\net6.0\publish

REM Start dotnet process in the background
::start "dotnet.exe" /B dotnet FYP_I.dll > D:\Semester7\logJob1.txt 2>&1

REM Wait for a few seconds (adjust the duration as needed)
::timeout /t 10 /nobreak > NUL

REM Run curl command without waiting for dotnet to finish
::curl -k -X GET https://localhost:5001/api/BahriaUni/BahriaCalendar > D:\Semester7\logJob1.txt 2>&1
::curl -k -X GET https://localhost:5000/api/BahriaUni/BahriaCalendar > D:\Semester7\logJob1.1.txt 2>&1
::curl -k -X GET https://localhost:7186/api/BahriaUni/BahriaCalendar > D:\Semester7\logJob1.2.txt 2>&1
::curl -k -X GET https://localhost:5001/api/BahriaUni/BahriaDepartments > D:\Semester7\logJob2.txt 2>&1
::curl -k -X GET https://localhost:5001/api/BahriaUni/BahriaFees > D:\Semester7\logJob3.txt 2>&1
::curl -k -X GET https://localhost:5001/api/ComsatsUni/ComsatsCalendar > D:\Semester7\logJob4.txt 2>&1
::curl -k -X GET https://localhost:5001/api/ComsatsUni/ComsatsDepartments > D:\Semester7\logJob5.txt 2>&1
::curl -k -X GET https://localhost:5001/api/ComsatsUni/ComsatsFees > D:\Semester7\logJob6.txt 2>&1
::curl -k -X GET https://localhost:5001/api/FastUni/FastCalendar > D:\Semester7\logJob7.txt 2>&1
::curl -k -X GET https://localhost:5001/api/FastUni/FastDepartments > D:\Semester7\logJob8.txt 2>&1
::curl -k -X GET https://localhost:5001/api/FastUni/FastFees > D:\Semester7\logJob9.txt 2>&1
::curl -k -X GET https://localhost:5001/api/KIETUni/KIETCalendar > D:\Semester7\logJob10.txt 2>&1
::curl -k -X GET https://localhost:5001/api/KIETUni/KIETDepartments > D:\Semester7\logJob11.txt 2>&1
::curl -k -X GET https://localhost:5001/api/KIETUni/KIETFees > D:\Semester7\logJob12.txt 2>&1
::curl -k -X GET https://localhost:5001/api/LUMSUni/LUMSCalendar > D:\Semester7\logJob13.txt 2>&1
::curl -k -X GET https://localhost:5001/api/LUMSUni/LUMSDepartments > D:\Semester7\logJob14.txt 2>&1
::curl -k -X GET https://localhost:5001/api/LUMSUni/LUMSFees > D:\Semester7\logJob15.txt 2>&1