pushd "%~dp0"
if not exist lib.%1 md lib.%1
call :ensureLib %1 ntdllex
call :ensureLib %1 shell32ex
popd
goto:EOF

:ensureLib
if not exist lib.%1\%2.lib lib /def:def\%2.def /machine:%1 /out:lib.%1\%2.lib