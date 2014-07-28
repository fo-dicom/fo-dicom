rmdir Distribute\ /S /Q
mkdir Distribute\
copy License.txt Distribute\ /Y
copy README.md Distribute\ /Y
copy ChangeLog.md Distribute\ /Y
mkdir Distribute\Bin\
copy "DICOM\bin\Release\Dicom.dll" Distribute\Bin\ /Y
copy "DICOM\bin\Release\Dicom.pdb" Distribute\Bin\ /Y
copy "DICOM\bin\Release\Dicom.xml" Distribute\Bin\ /Y
copy "DICOM [Native]\Release\Dicom.Native.dll" Distribute\Bin\ /Y
copy "DICOM [Native]\Release\Dicom.Native64.dll" Distribute\Bin\ /Y
copy "packages\NLog.3.0.0.0\lib\net40\NLog.dll" Distribute\Bin\ /Y
mkdir Distribute\Examples\
copy "Examples\C-Store SCP\bin\Release\Dicom.CStoreSCP.exe" Distribute\Examples\ /Y
copy "Examples\DICOM Media\bin\Release\Dicom.Media.exe" Distribute\Examples\ /Y
copy "DICOM\bin\Release\Dicom.dll" Distribute\Examples\ /Y
copy "DICOM [Native]\Release\Dicom.Native.dll" Distribute\Examples\ /Y
copy "DICOM [Native]\Release\Dicom.Native64.dll" Distribute\Examples\ /Y
copy "packages\NLog.3.0.0.0\lib\net40\NLog.dll" Distribute\Examples\ /Y
mkdir Distribute\Tools\
copy "Tools\DICOM Dump\bin\Release\Dicom.Dump.exe" Distribute\Tools\ /Y
copy "Tools\DICOM Compare\bin\Release\Dicom.Compare.exe" Distribute\Tools\ /Y
copy "DICOM\bin\Release\Dicom.dll" Distribute\Tools\ /Y
copy "DICOM [Native]\Release\Dicom.Native.dll" Distribute\Tools\ /Y
copy "DICOM [Native]\Release\Dicom.Native64.dll" Distribute\Tools\ /Y
copy "packages\NLog.3.0.0.0\lib\net40\NLog.dll" Distribute\Tools\ /Y