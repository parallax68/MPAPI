call gmcs MPAPI\Properties\*.cs MPAPI\NodeRegistrationServer\*.cs MPAPI\*.cs -optimize+ -reference:deps\RemotingLite.dll -target:library -out:MPAPI.dll
call gmcs RegistrationServer\Properties\*.cs RegistrationServer\*.cs -optimize+ -reference:MPAPI.dll,System.Configuration.dll,deps\RemotingLite.dll -target:exe -out:RegistrationServer.exe
